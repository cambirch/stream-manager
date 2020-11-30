using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions.Interfaces
{
    public interface IActionCustomText : IAction
    {
        /// <summary>
        /// Allows custom text actions to be occasional instead of always
        /// </summary>
        public bool UseCustomText { get; }

        /// <summary>
        /// The custom text to apply when <see cref="UseCustomText"/> is true.
        /// </summary>
        public string Text { get; }
    }
}
