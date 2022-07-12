using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class DialogOpenedEventArgs : RoutedEventArgs
    {
        public DialogOpenedEventArgs(DialogSession session, RoutedEvent routedEvent)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));

            Session = session;
            RoutedEvent = routedEvent;
        }

        /// <summary>
        /// Allows interaction with the current dialog session.
        /// </summary>
        public DialogSession Session { get; }
    }
}