using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	public class ToggleItemVisibilityAction : IAction, IActionHasActive
	{
		private readonly ObsManager _obsManager;

		public ToggleItemVisibilityAction(ObsManager obsManager, string sceneName, string sceneItem)
		{
			(_obsManager, SceneName, SceneItem) = (obsManager, sceneName, sceneItem);

			_obsManager.OnConnected += _obsManager_OnConnected;
			_obsManager.OnSceneItemVisibilityChanged += _obsManager_OnSceneItemVisibilityChanged;
			UpdateCurrentState();
		}

		private void _obsManager_OnSceneItemVisibilityChanged(OBSWebsocketDotNet.OBSWebsocket sender, string sceneName, string itemName)
		{
			if (
				string.Equals(sceneName, SceneName, StringComparison.InvariantCultureIgnoreCase)
				&& string.Equals(itemName, SceneItem, StringComparison.InvariantCultureIgnoreCase)
				)
			{
				IsActive = !IsActive;
			}
		}

		private void _obsManager_OnConnected(object? sender, EventArgs e) => UpdateCurrentState();

		private void UpdateCurrentState()
		{
			try
			{
				var sceneItemProperties = _obsManager.SendRequest("GetSceneItemProperties", new JObject
				{
					{ "scene-name", SceneName },
					{ "item", SceneItem }
				})!;
				IsActive = sceneItemProperties?.Value<bool>("visible") ?? false;
			}
			catch { }
		}

		public string SceneName { get; }
		public string SceneItem { get; }

		public bool IsActive { get; private set; }

		public Task Trigger()
		{
			var props = new JObject
			{
				{ "scene-name", SceneName },
				{ "item", SceneItem },
				{ "visible", !IsActive }
			};
			_obsManager.SendRequest("SetSceneItemProperties", props);

			return Task.CompletedTask;
		}
	}
}
