using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OBSWebsocketDotNet.Types;
using StreamDeckManager.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace StreamDeckManager.Managers
{
	class TallyManager : IDisposable
	{
		public event EventHandler<IReadOnlyDictionary<string, int>> OnTallyChange;
		public event EventHandler<TallyDisplayEventArgs> OnRemoteConnectionChanged;

		private readonly GetTallyInfo _getTallyInfo;
		private readonly ObsManager _obsManager;
		private readonly IOptionsMonitor<RemoteTallySettings> _remoteTallySettings;
		//private WebsocketClient? _client = null;
		private List<TallyClient> _clients = new List<TallyClient>();

		private List<OBSScene> _allScenes = new List<OBSScene>();

		private readonly Dictionary<string, int> tallyValues = new Dictionary<string, int>();
		private readonly Timer timer;

		public TallyManager(
			GetTallyInfo getTallyInfo,
			ObsManager obsManager,
			IOptionsMonitor<RemoteTallySettings> remoteTallySettings
		)
		{
			(_getTallyInfo, _obsManager, _remoteTallySettings) = (getTallyInfo, obsManager, remoteTallySettings);
			CreateIfRequired();

			// Perform Ping/Pong
			timer = TallyManager.SetIntervalThread(() => {
				CreateIfRequired();
				if (_clients.Count == 0) return;

				for (int i = 0; i < _clients.Count; i++)
				{
					if (!_clients[i].IsConnected) continue;
					_clients[i].Client.Send("P");
				}
			}, 5000);

			_obsManager._obs.SceneItemAdded += _obs_SceneItemAdded;
			_obsManager._obs.SceneItemRemoved += _obs_SceneItemRemoved;
			//_obsManager._obs.SceneItemSelected += _obs_SceneItemSelected;
			//_obsManager._obs.SceneItemTransformChanged += _obs_SceneItemTransformChanged;
			_obsManager._obs.SceneItemVisibilityChanged += _obs_SceneItemVisibilityChanged;

			_obsManager.OnPreviewSceneChanged += _obsManager_OnPreviewSceneChanged;
			_obsManager.OnSceneChanged += _obsManager_OnSceneChanged;
			_obsManager.OnConnected += _obsManager_OnConnected;
			if (_obsManager.IsConnected) UpdateCurrentTally(true);
			//client.Send("{\"camera1-preview\":2}");
		}

		private void _obs_SceneItemRemoved(OBSWebsocketDotNet.OBSWebsocket sender, string sceneName, string itemName)
			=> Task.Run(() => UpdateCurrentTally());
		private void _obs_SceneItemSelected(OBSWebsocketDotNet.OBSWebsocket sender, string sceneName, string itemName, string itemId)
			=> _allScenes = _obsManager._obs.ListScenes();
		private void _obs_SceneItemVisibilityChanged(OBSWebsocketDotNet.OBSWebsocket sender, string sceneName, string itemName)
			=> Task.Run(() => UpdateCurrentTally());
		private void _obs_SceneItemTransformChanged(OBSWebsocketDotNet.OBSWebsocket sender, SceneItemTransformInfo transform)
			=> _allScenes = _obsManager._obs.ListScenes();
		private void _obs_SceneItemAdded(OBSWebsocketDotNet.OBSWebsocket sender, string sceneName, string itemName)
			=> Task.Run(() => UpdateCurrentTally());

		private void _obsManager_OnConnected(object? sender, EventArgs e)
			=> Task.Run(() => UpdateCurrentTally(true));

		private void UpdateCurrentTally(bool updateProgram = false)
		{
			_allScenes = _obsManager._obs.ListScenes();

			var programScene = _allScenes.Find(f => f.Name == _obsManager.CurrentActiveScene);
			var previewScene = _allScenes.Find(f => f.Name == _obsManager.CurrentPreviewScene);

			if (updateProgram && programScene is object)
			{
				ClearForTallyType("program");
				MarkIfActive(programScene, "program");
			}
			if (previewScene is object)
			{
				ClearForTallyType("preview");
				MarkIfActive(previewScene, "preview");
			}

			SendToTally();
			OnTallyChange?.Invoke(this, tallyValues);
		}

		private void _obsManager_OnSceneChanged(OBSWebsocketDotNet.OBSWebsocket sender, string newSceneName)
		{
			//var sourceTallies = _remoteTallySettings.CurrentValue.SourceTallies;
			//var tallies = _remoteTallySettings.CurrentValue.Tallies;
			ClearForTallyType("program");

			var programScene = _allScenes.Find(f => f.Name == newSceneName);
			if (programScene is object) MarkIfActive(programScene, "program");

			SendToTally();
			OnTallyChange?.Invoke(this, tallyValues);
		}

		private void ClearForTallyType(string tallyType) {
			foreach (var tallyItem in tallyValues.ToArray())
			{
				if (tallyItem.Key.EndsWith(tallyType))
				{
					tallyValues[tallyItem.Key] = 0;
				}
			}
		}

		private void _obsManager_OnPreviewSceneChanged(OBSWebsocketDotNet.OBSWebsocket sender, string newSceneName)
		{
			//var sourceTallies = _remoteTallySettings.CurrentValue.SourceTallies;
			//var tallies = _remoteTallySettings.CurrentValue.Tallies;
			ClearForTallyType("preview");

			var previewScene = _allScenes.Find(f => f.Name == newSceneName);
			if (previewScene is object) MarkIfActive(previewScene, "preview");

			//var sceneItems = _obsManager.PreviewScene;
			//foreach (var item in sceneItems.Items)
			//{
			//	var matchingSourceTally = sourceTallies.Find(f => f.Name == item.SourceName);
			//	if (matchingSourceTally is object)
			//	{
			//		foreach (var tallyID in matchingSourceTally.TallyIDs)
			//		{
			//			var tallyDefinition = tallies.Find(f => f.ID == tallyID);
			//			if (tallyDefinition is null) continue;
			//			if (!tallyDefinition.HasPreview) continue;

			//			tallyValues[$"{matchingSourceTally.TallyIDs}-preview"] = 1;
			//		}
			//	}
			//}

			SendToTally();
			OnTallyChange?.Invoke(this, tallyValues);
		}
		private void MarkIfActive(OBSScene currentScene, string activeType)
		{
			var sourceTallies = _remoteTallySettings.CurrentValue.SourceTallies;
			var tallies = _remoteTallySettings.CurrentValue.Tallies;

			foreach (var item in currentScene.Items)
			{
				// Skip non-visible items
				if (!item.Render) continue;

				var matchingSourceTally = sourceTallies.Find(f => f.Name == item.SourceName);
				if (matchingSourceTally is object)
				{
					foreach (var tallyID in matchingSourceTally.TallyIDs)
					{
						var tallyDefinition = tallies.Find(f => f.ID == tallyID);
						if (tallyDefinition is null) continue;
						if (!tallyDefinition.HasPreview) continue;

						tallyValues[$"{tallyID}-{activeType}"] = 1;
					}
				}

				// Dive into sub scenes if present
				if (item.InternalType == "scene")
				{
					OBSScene? otherScene = _allScenes.Find(f => f.Name == item.SourceName);
					if (otherScene is object)
					{
						MarkIfActive(otherScene, activeType);
					}
				}
			}
		}

		public void Dispose()
		{
			foreach (var client in _clients)
			{
				client.Client?.Dispose();
			}
			timer.Dispose();
		}

		/// <summary>
		/// Usage: var timer = SetIntervalThread(DoThis, 1000);
		/// UI Usage: BeginInvoke((Action)(() =>{ SetIntervalThread(DoThis, 1000); }));
		/// </summary>
		/// <returns>Returns a timer object which can be disposed.</returns>
		public static System.Threading.Timer SetIntervalThread(Action Act, int Interval)
		{
			//TimerStateManager state = new TimerStateManager();
			System.Threading.Timer tmr = new System.Threading.Timer(new TimerCallback(_ => Act()), null, Interval, Interval);
			//state.TimerObject = tmr;
			return tmr;
		}

		private void RaiseTallyConnectionChanged(TallyDisplayEventArgs args) => OnRemoteConnectionChanged?.Invoke(this, args);

		private void CreateIfRequired()
		{
			foreach (var remoteDisplay in _remoteTallySettings.CurrentValue.Displays)
			{
				TallyClient? theClient = _clients.Find(f => f.IPAddress == remoteDisplay.IPAddress);
				if (theClient is null)
				{
					theClient = new TallyClient(remoteDisplay, RaiseTallyConnectionChanged, tallyValues);
					_clients.Add(theClient);
				}
			}
		}
		private void SendToTally()
		{
			CreateIfRequired();
			if (_clients.Count == 0) return;

			for (int i = 0; i < _clients.Count; i++)
			{
				if (!_clients[i].IsConnected) continue;

				string jsonString = JsonConvert.SerializeObject(tallyValues);
				_clients[i].Client.Send(jsonString);
			}
		}

		private class TallyClient
		{
			private readonly Action<TallyDisplayEventArgs> OnRemoteConnectionChanged;

			public TallyClient(
				RemoteDisplay remoteDisplay,
				Action<TallyDisplayEventArgs> onRemoteConnectionChanged,
				Dictionary<string, int> tallyValues
			)
			{
				(IPAddress, OnRemoteConnectionChanged) = (remoteDisplay.IPAddress, onRemoteConnectionChanged);

				OnRemoteConnectionChanged(new TallyDisplayEventArgs
				{
					Display = remoteDisplay,
					IsConnected = false,
				});

				Client = new WebsocketClient(new Uri($"ws://{remoteDisplay.IPAddress}:81"));
				Client.ReconnectTimeout = TimeSpan.FromSeconds(30);
				Client.DisconnectionHappened.Subscribe((_) =>
				{
					IsConnected = false;

					OnRemoteConnectionChanged(new TallyDisplayEventArgs
					{
						Display = remoteDisplay,
						IsConnected = false,
					});
				});
				Client.ReconnectionHappened.Subscribe((_) =>
				{
					IsConnected = true;
					Client.Send(JsonConvert.SerializeObject(tallyValues));

					OnRemoteConnectionChanged(new TallyDisplayEventArgs
					{
						Display = remoteDisplay,
						IsConnected = true,
					});
				});

				Task.Run(async () =>
				{
					try
					{
						await Client.Start();

						IsConnected = true;
						Client.Send(JsonConvert.SerializeObject(tallyValues));

						OnRemoteConnectionChanged(new TallyDisplayEventArgs
						{
							Display = remoteDisplay,
							IsConnected = true,
						});
					}
					catch
					{
						//TODO:
					}
				});
			}

			public WebsocketClient Client { get; }
			public string IPAddress { get; }

			public bool IsConnected { get; private set; }
		}
	}

	public class TallyDisplayEventArgs : EventArgs
	{
		public RemoteDisplay Display { get; set; }
		public bool IsConnected { get; set; }
	}
}
