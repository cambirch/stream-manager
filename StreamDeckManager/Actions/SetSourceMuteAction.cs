using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions
{
	class SetSourceMuteAction : IAction
	{
		private readonly ObsManager _obsManager;
		public SetSourceMuteAction(ObsManager obsManager, string sourceName, bool setMuted)
		{
			(_obsManager, SourceName, SetMuted) = (obsManager, sourceName, setMuted);
		}

		public string SourceName { get; }

		public bool SetMuted { get; set; }

		public Task Trigger()
		{
			var props = new JObject
			{
				{ "source", SourceName },
				{ "mute", SetMuted },
			};
			_obsManager.SendRequest("SetMute", props);

			return Task.CompletedTask;
		}
	}
}
