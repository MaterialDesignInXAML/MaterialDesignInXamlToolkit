using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class DialogClosingEventArgs : RoutedEventArgs
    {
        public DialogClosingEventArgs(object parameter, object content)
        {
            Parameter = parameter;
            Content = content;
        }

        public DialogClosingEventArgs(object parameter, object content, RoutedEvent routedEvent) : base(routedEvent)
        {
            Parameter = parameter;
            Content = content;
        }

        public DialogClosingEventArgs(object parameter, object content, RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
            Parameter = parameter;
            Content = content;
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

        /// <summary>
        /// Gets the <see cref="DialogHost.DialogContent"/> which is currently displayed, so this could be a view model or a UI element.
        /// </summary>
        public object Content { get; }
    }
}