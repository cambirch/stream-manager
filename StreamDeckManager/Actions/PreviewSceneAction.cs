using Newtonsoft.Json.Linq;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamDeckManager.Actions
{
    public class PreviewSceneAction : IAction, IActionHasActive
    {
        private readonly ObsManager _obsManager;

        public PreviewSceneAction(ObsManager obsManager, string sceneName)
        {
            (_obsManager, SceneName) = (obsManager, sceneName);
        }

        public string SceneName { get; }

        public bool IsActive => string.Equals(_obsManager.CurrentPreviewScene, SceneName, StringComparison.InvariantCultureIgnoreCase);

        public Task Trigger()
        {
            var props = new JObject
            {
                { "scene-name", SceneName }
            };
            try
            {
                _obsManager.SendRequest("SetPreviewScene", props);
            }
            catch { }

            return Task.CompletedTask;
        }
    }
}
