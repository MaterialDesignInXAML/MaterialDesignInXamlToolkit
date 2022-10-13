﻿namespace MaterialDesignThemes.Wpf
{
    public class DrawerOpenedEventArgs : RoutedEventArgs
    {
        public DrawerOpenedEventArgs(Dock dock, RoutedEvent routedEvent) : base(routedEvent)
        {
            Dock = dock;
        }

        /// <summary>
        /// Allows interaction with the current dialog session.
        /// </summary>
        public Dock Dock { get; }
    }
}
