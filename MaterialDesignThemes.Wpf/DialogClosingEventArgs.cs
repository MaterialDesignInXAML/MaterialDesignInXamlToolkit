using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class DialogClosingEventArgs : RoutedEventArgs
    {
        public DialogClosingEventArgs(DialogSession session, RoutedEvent routedEvent)
            : base(routedEvent)
            => Session = session ?? throw new ArgumentNullException(nameof(session));

        /// <summary>
        /// Cancel the close.
        /// </summary>
        public void Cancel() => IsCancelled = true;

        /// <summary>
        /// Indicates if the close has already been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the parameter originally provided to <see cref="DialogHost.CloseDialogCommand"/>/
        /// </summary>
        public object? Parameter => Session.CloseParameter;

        /// <summary>
        /// Allows interaction with the current dialog session.
        /// </summary>
        public DialogSession Session { get; }
    }
}