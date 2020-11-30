using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	class SceneTransitionAction : IAction
	{
		private readonly ObsManager _obsManager;
		public SceneTransitionAction(ObsManager obsManager, string transitionName, int duration)
		{
			(_obsManager, TransitionName, Duration) = (obsManager, transitionName, duration);
		}

		public string TransitionName { get; }
		public int Duration { get; }

		public Task Trigger()
		{
			var props = new JObject
			{
				{ "with-transition", new JObject {
					{ "duration", Duration },
					{ "name", TransitionName },
				}}
			};
			_obsManager.SendRequest("TransitionToProgram", props);

			return Task.CompletedTask;
		}
	}
}
