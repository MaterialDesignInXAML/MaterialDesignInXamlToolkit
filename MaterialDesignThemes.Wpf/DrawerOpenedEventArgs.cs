using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public class DrawerOpenedEventArgs : RoutedEventArgs
    {
        public DrawerOpenedEventArgs(Dock dock, RoutedEvent routedEvent) : base(routedEvent)
        {
            Dock = dock;
        }

        /// <summary>
        /// Allows interation with the current dialog session.
        /// </summary>
        public Dock Dock { get; }
    }
}