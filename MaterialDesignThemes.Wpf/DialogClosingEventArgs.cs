using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class DialogClosingEventArgs : RoutedEventArgs
    {
        public DialogClosingEventArgs(object parameter)
        {
            Parameter = parameter;
        }

        public DialogClosingEventArgs(object parameter, RoutedEvent routedEvent) : base(routedEvent)
        {
            Parameter = parameter;
        }

        public DialogClosingEventArgs(object parameter, RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            Parameter = parameter;
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the paramter originally provided to <see cref="DialogHost.CloseDialogCommand"/>/
        /// </summary>
        public object Parameter { get; }
    }
}