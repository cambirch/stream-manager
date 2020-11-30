using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	class ToggleSourceMuteAction : IAction, IActionHasActive
	{
		private readonly ObsManager _obsManager;
		public ToggleSourceMuteAction(ObsManager obsManager, string sourceName)
		{
			(_obsManager, SourceName) = (obsManager, sourceName);

			_obsManager.OnSourceMuteStateChanged += _obsManager_OnSourceMuteStateChanged;
			_obsManager.OnConnected += _obsManager_OnConnected;

			UpdateCurrentState();
		}

		private void _obsManager_OnConnected(object? sender, EventArgs e) => UpdateCurrentState();

		private void UpdateCurrentState()
		{
			try
			{
				var result = _obsManager.SendRequest("GetMute", new JObject { { "source", SourceName } });
				IsMuted = result?.Value<bool>("muted") ?? false;
			}
			catch { }
		}

		private void _obsManager_OnSourceMuteStateChanged(OBSWebsocketDotNet.OBSWebsocket sender, string sourceName, bool muted)
		{
			if (string.Equals(sourceName, SourceName, StringComparison.InvariantCultureIgnoreCase))
			{
				IsMuted = muted;
			}
		}

		public bool IsActive => !IsMuted;

		public string SourceName { get; }

		protected bool IsMuted { get; set; }

		public Task Trigger()
		{
			var props = new JObject
			{
				{ "source", SourceName },
				{ "mute", !IsMuted },
			};
			_obsManager.SendRequest("SetMute", props);

			return Task.CompletedTask;
		}
	}
}
