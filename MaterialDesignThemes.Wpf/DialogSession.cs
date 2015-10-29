using System;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Allows an open dialog to be managed. Use is only permitted during a single display operation.
    /// </summary>
    public class DialogSession
    {
        private readonly DialogHost _owner;
        internal bool IsDisabled;

        internal DialogSession(DialogHost owner)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));

            _owner = owner;
        }

        /// <summary>
        /// Gets the <see cref="DialogHost.DialogContent"/> which is currently displayed, so this could be a view model or a UI element.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the dialog session has ended.</exception>
        public object Content
        {
            get
            {
                if (IsDisabled) throw new InvalidOperationException("Dialog session has ended.");

                return _owner.DialogContent;
            }
        }

        /// <summary>
        /// Update the currrent content in the dialog.
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="InvalidOperationException">Thrown if the dialog content is currently set via a binding, or if the dialog session has ended.</exception>
        public void UpdateContent(object content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (IsDisabled) throw new InvalidOperationException("Dialog session has ended.");

            _owner.AssertTargetableContent();
            _owner.DialogContent = content;
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the dialog session has ended, or a close operation is currently in progress.</exception>
        public void Close()
        {
            if (IsDisabled) throw new InvalidOperationException("Dialog session has ended.");

        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="parameter">Result parameter which will be returned in <see cref="DialogClosingEventArgs.Parameter"/> or from <see cref="DialogHost.Show(object)"/> method.</param>
        /// <exception cref="InvalidOperationException">Thrown if the dialog session has ended, or a close operation is currently in progress.</exception>
        public void Close(object parameter)
        {
            if (IsDisabled) throw new InvalidOperationException("Dialog session has ended.");

        }
    }
}