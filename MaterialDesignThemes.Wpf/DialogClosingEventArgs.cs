using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class DialogClosingEventArgs : RoutedEventArgs
    {
        public DialogClosingEventArgs(DialogSession session, object parameter)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            Session = session;

            Parameter = parameter;         
        }

        public DialogClosingEventArgs(DialogSession session, object parameter, RoutedEvent routedEvent) : base(routedEvent)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            Session = session;

            Parameter = parameter;            
        }

        public DialogClosingEventArgs(DialogSession session, object parameter, RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            Session = session;

            Parameter = parameter;            
        }

        /// <summary>
        /// Cancel the close.
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// Indicates if the close has already been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the paramter originally provided to <see cref="DialogHost.CloseDialogCommand"/>/
        /// </summary>
        public object Parameter { get; }

        /// <summary>
        /// Allows interation with the current dialog session.
        /// </summary>
        public DialogSession Session { get; }

        /// <summary>
        /// Gets the <see cref="DialogHost.DialogContent"/> which is currently displayed, so this could be a view model or a UI element.
        /// </summary>
        [Obsolete("Prefer Session.Content")]
        public object Content => Session.Content;
    }
}