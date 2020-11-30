using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Actions.Interfaces
{
    /// <summary>
    /// Marks an action as able to display an active state.
    /// </summary>
    /// <seealso cref="StreamDeckManager.Actions.IAction" />
    public interface IActionHasActive : IAction
    {
        /// <summary>
        /// The active state of the action.
        /// </summary>
        bool IsActive { get; }
    }
}
