using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public class DrawerClosingEventArgs : RoutedEventArgs
    {
        public DrawerClosingEventArgs(Dock dock, RoutedEvent routedEvent) : base(routedEvent)
        {
            Dock = dock;
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
        /// Allows interation with the current dialog session.
        /// </summary>
        public Dock Dock { get; }
    }
}