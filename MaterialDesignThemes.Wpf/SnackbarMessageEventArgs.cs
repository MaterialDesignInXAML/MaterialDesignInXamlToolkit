using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class SnackbarMessageEventArgs : RoutedEventArgs
    {
        public SnackbarMessageEventArgs(SnackbarMessage message)
        {
            Message = message;
        }

        public SnackbarMessageEventArgs(RoutedEvent routedEvent, SnackbarMessage message) : base(routedEvent)
        {
            Message = message;
        }

        public SnackbarMessageEventArgs(RoutedEvent routedEvent, object source, SnackbarMessage message) : base(routedEvent, source)
        {
            Message = message;
        }

        public SnackbarMessage Message { get; }
    }
}