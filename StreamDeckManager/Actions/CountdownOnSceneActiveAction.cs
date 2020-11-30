using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	class CountdownOnSceneActiveAction : IAction, IActionCustomText
	{
		private readonly ObsManager _obsManager;
		private System.Timers.Timer _countdownTimer;
		private DateTime _expectedEnd;

		public CountdownOnSceneActiveAction(ObsManager obsManager, string sceneName, TimeSpan timespan)
		{
			(_obsManager, SceneName, Timespan) = (obsManager, sceneName, timespan);

			_countdownTimer = new System.Timers.Timer(500);
			_countdownTimer.Elapsed += _countdownTimer_Elapsed;

			_obsManager.OnSceneChanged += _obsManager_OnSceneChanged;
		}

		private void _countdownTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			_obsManager.TriggerEvent();
		}

		private void _obsManager_OnSceneChanged(OBSWebsocketDotNet.OBSWebsocket sender, string newSceneName)
		{
			if (string.Equals(newSceneName, SceneName, StringComparison.InvariantCultureIgnoreCase))
			{
				_expectedEnd = DateTime.Now.Add(Timespan);
				_countdownTimer.Enabled = true;
			}
			else
			{
				// Disabling...
				if (_countdownTimer.Enabled) _obsManager.TriggerEvent();
				_countdownTimer.Enabled = false;
			}
		}

		public bool UseCustomText => string.Equals(_obsManager.CurrentActiveScene, SceneName, StringComparison.InvariantCultureIgnoreCase);

		public string Text => _expectedEnd.Subtract(DateTime.Now).ToString(@"h\:mm\:ss");

		public string SceneName { get; }
		public TimeSpan Timespan { get; }

		public Task Trigger() => Task.CompletedTask;
	}
}
