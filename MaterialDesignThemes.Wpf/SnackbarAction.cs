using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf
{
    public class SnackbarAction
    {
        private string _actionLabel;
        private SnackbarActionEventHandler _actionHandler;

        public SnackbarActionEventHandler ActionHandler
        {
            get
            {
                return _actionHandler;
            }
        }

        public string ActionLabel
        {
            get
            {
                return _actionLabel;
            }
        }

        public SnackbarAction(string actionLabel) : this(actionLabel, null) { }

        public SnackbarAction(string actionLabel, SnackbarActionEventHandler actionHandler)
        {
            _actionLabel = actionLabel;
            _actionHandler = actionHandler;
        }
    }
}
