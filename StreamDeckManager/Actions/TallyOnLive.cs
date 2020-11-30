using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	//class TallyOnLive : IAction
	//{
	//	private readonly ObsManager _obsManager;
	//	public string CamString { get; }
	//	private readonly string _tallyName;
	//	private readonly GetTallyInfo _getTallyInfo;
	//	private readonly TallyManager _tallyManager;

	//	public TallyOnLive(ObsManager obsManager, GetTallyInfo getTallyInfo, TallyManager tallyManager, string camString, string tallyName)
	//	{
	//		(_obsManager, _getTallyInfo, _tallyManager, CamString, _tallyName) = (obsManager, getTallyInfo, tallyManager, camString, tallyName);

	//		_obsManager.OnSceneChanged += _obsManager_OnSceneChanged;
	//		_obsManager.OnPreviewSceneChanged += _obsManager_OnPreviewSceneChanged;
	//		_obsManager.OnTransitionBegin += _obsManager_OnTransitionBegin;
	//	}

	//	private void _obsManager_OnTransitionBegin(object? sender, EventArgs e)
	//	{
	//		if (string.IsNullOrWhiteSpace(_getTallyInfo.TallyIp)) return;
	//		string newSceneName = _obsManager.CurrentPreviewScene;

	//		if (newSceneName.Contains(CamString, StringComparison.InvariantCultureIgnoreCase))
	//		{
	//			_tallyManager.SetTally($"{_tallyName}-program", true);
	//		}
	//		else
	//		{
	//			// Disabling...
	//			_tallyManager.SetTally($"{_tallyName}-program", false);
	//		}
	//	}

	//	private void _obsManager_OnPreviewSceneChanged(OBSWebsocketDotNet.OBSWebsocket sender, string newSceneName)
	//	{
	//		if (string.IsNullOrWhiteSpace(_getTallyInfo.TallyIp)) return;

	//		if (newSceneName.Contains(CamString, StringComparison.InvariantCultureIgnoreCase))
	//		{
	//			_tallyManager.SetTally($"{_tallyName}-preview", true);
	//		}
	//		else
	//		{
	//			// Disabling...
	//			_tallyManager.SetTally($"{_tallyName}-preview", false);
	//		}
	//	}

	//	private void _obsManager_OnSceneChanged(OBSWebsocketDotNet.OBSWebsocket sender, string newSceneName)
	//	{
	//		if (string.IsNullOrWhiteSpace(_getTallyInfo.TallyIp)) return;

	//		if (newSceneName.Contains(CamString, StringComparison.InvariantCultureIgnoreCase))
	//		{
	//			_tallyManager.SetTally($"{_tallyName}-program", true);
	//		}
	//		else
	//		{
	//			// Disabling...
	//			_tallyManager.SetTally($"{_tallyName}-program", false);
	//		}
	//	}


	//	public Task Trigger() => Task.CompletedTask;
	//}
}
