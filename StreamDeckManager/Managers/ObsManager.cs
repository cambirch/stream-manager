using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Types;
using StreamDeckManager.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Managers
{
	/// <summary>
	/// Injected service which allows communication to OBS.
	/// </summary>
	public class ObsManager: IDisposable
	{
		private readonly IOptionsMonitor<OBSConnectionSettings> _obsConnectionSettings;
		public readonly OBSWebsocket _obs = new OBSWebsocket();

		public event EventHandler OnConnected;
		public event EventHandler OnDisconnected;
		public event EventHandler OnConnectionStateChanged;
		public event SceneChangeCallback OnSceneChanged;
		public event SceneChangeCallback OnPreviewSceneChanged;
		public event SceneItemUpdateCallback OnSceneItemVisibilityChanged;
		public event SourceMuteStateChangedCallback OnSourceMuteStateChanged;
		public event EventHandler OnTransitionBegin;

		/// <summary>
		/// Occurs when any other event happens.
		/// </summary>
		public event EventHandler OnEvent;

		public ObsManager(IOptionsMonitor<OBSConnectionSettings> obsConnectionSettings)
		{
			(_obsConnectionSettings) = (obsConnectionSettings);

			var obsSettings = _obsConnectionSettings.CurrentValue;
			if (obsSettings.AutoConnect) Connect();

			_obs.Connected += _obs_Connected;
			_obs.Disconnected += _obs_Disconnected;
			_obs.SceneChanged += _obs_SceneChanged;
			_obs.PreviewSceneChanged += _obs_PreviewSceneChanged;
			_obs.SceneItemVisibilityChanged += _obs_SceneItemVisibilityChanged;
			_obs.SourceMuteStateChanged += _obs_SourceMuteStateChanged;
			_obs.TransitionBegin += _obs_TransitionBegin;
		}

		private void _obs_TransitionBegin(object? sender, EventArgs e)
		{
			OnTransitionBegin?.Invoke(sender, e);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}

		private void _obs_SourceMuteStateChanged(OBSWebsocket sender, string sourceName, bool muted)
		{
			OnSourceMuteStateChanged?.Invoke(sender, sourceName, muted);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}

		private void _obs_SceneItemVisibilityChanged(OBSWebsocket sender, string sceneName, string itemName)
		{
			OnSceneItemVisibilityChanged?.Invoke(sender, sceneName, itemName);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}

		private void _obs_PreviewSceneChanged(OBSWebsocket sender, string newSceneName)
		{
			CurrentPreviewScene = newSceneName;
			OnPreviewSceneChanged?.Invoke(sender, newSceneName);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}
		private void _obs_SceneChanged(OBSWebsocket sender, string newSceneName)
		{
			CurrentActiveScene = newSceneName;
			OnSceneChanged?.Invoke(sender, newSceneName);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}

		private void _obs_Disconnected(object? sender, EventArgs e)
		{
			IsConnecting = false;
			OnDisconnected?.Invoke(sender, e);
			OnConnectionStateChanged?.Invoke(sender, e);
			OnEvent?.Invoke(sender, EventArgs.Empty);
		}

		private void _obs_Connected(object? sender, EventArgs e)
		{
			CurrentPreviewScene = _obs.GetPreviewScene().Name;

			IsConnecting = false;
			OnConnected?.Invoke(sender, e);
			OnConnectionStateChanged?.Invoke(sender, e);
			OnEvent?.Invoke(sender, EventArgs.Empty);

		}

		// Clean-up and close things
		public void Dispose()
		{
			if (_obs.IsConnected)
			{
				_obs.Disconnect();
			}
		}

		public Task Connect() => Task.Run(() =>
		{
			IsConnecting = true;
			_obs.Connect(_obsConnectionSettings.CurrentValue.Url, _obsConnectionSettings.CurrentValue.Password);
			IsConnecting = false;
			_obs_Connected(null, EventArgs.Empty);
		});

		public void Disconnect() => _obs.Disconnect();

		/// <summary>
		/// Triggers the event <see cref="OnEvent"/>.
		/// </summary>
		public void TriggerEvent() => OnEvent?.Invoke(this, EventArgs.Empty);

		public bool IsConnected => _obs.IsConnected;

		public bool IsConnecting { get; private set; }

		public JObject? SendRequest(string requestType, JObject? additionalFields = null)
		{
			if (!_obs.IsConnected) return null;
			return _obs.SendRequest(requestType, additionalFields);
		}

		public string CurrentPreviewScene { get; private set; }
		public string CurrentActiveScene { get; private set; }

		public List<OBSWebsocketDotNet.Types.SourceInfo> SourcesList => _obs.GetSourcesList();

		public OBSScene CurrentScene => _obs.GetCurrentScene();
		public OBSScene PreviewScene => _obs.GetPreviewScene();
	}
}
