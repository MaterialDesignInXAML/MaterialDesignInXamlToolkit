using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// An optional parameter for the <see cref="SnackbarHost.ShowAsync(object, string, SnackbarAction)"/> method to provide an action for the <see cref="Snackbar"/>.
    /// A <see cref="ActionLabel"/> is mandatory to label the action button. 
    /// The <see cref="ActionHandler"/> is an optional event handler, which will be called by clicking the action button.
    /// </summary>
    public class SnackbarAction
    {
        private string _actionLabel;
        private SnackbarActionEventHandler _actionHandler;

        /// <summary>
        /// Optional event handler, which will be called by clicking the action button.
        /// </summary>
        public SnackbarActionEventHandler ActionHandler
        {
            get
            {
                return _actionHandler;
            }
        }

        /// <summary>
        /// Mandatory label for the action button.
        /// </summary>
        public string ActionLabel
        {
            get
            {
                return _actionLabel;
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="actionLabel">Mandatory label for the action button.</param>
        public SnackbarAction(string actionLabel) : this(actionLabel, null) { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="actionLabel">Mandatory label for the action button.</param>
        /// <param name="actionHandler">Optional event handler, which will be called by clicking the action button.</param>
        public SnackbarAction(string actionLabel, SnackbarActionEventHandler actionHandler)
        {
            _actionLabel = actionLabel;
            _actionHandler = actionHandler;
        }
    }
}
