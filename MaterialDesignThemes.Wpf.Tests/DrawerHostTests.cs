using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class DrawerHostTests : IDisposable
    {
        private readonly DrawerHost _drawerHost;

        public DrawerHostTests()
        {
            _drawerHost = new DrawerHost();
            _drawerHost.ApplyDefaultStyle();
            _drawerHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        }

        public void Dispose()
        {
            _drawerHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
        }

        [StaFact]
        [Description("Issue 2015")]
        public void WhenOpenedDrawerOpenedEventIsRaised()
        {
            Dock expectedPosition = Dock.Left;
            Dock openedPosition = Dock.Top;
            _drawerHost.DrawerOpened += DrawerOpened;
            _drawerHost.IsLeftDrawerOpen = true;

            DrawerHost.CloseDrawerCommand.Execute(Dock.Left, _drawerHost);

            Assert.Equal(expectedPosition, openedPosition);

            void DrawerOpened(object? sender, DrawerOpenedEventArgs eventArgs)
            {
                openedPosition = eventArgs.Dock;
            }
        }

        [StaFact]
        [Description("Issue 2015")]
        public void WhenClosingDrawerClosingEventIsRaised()
        {
            Dock expectedPosition = Dock.Left;
            Dock closedPosition = Dock.Top;
            _drawerHost.DrawerClosing += DrawerClosing;
            _drawerHost.IsLeftDrawerOpen = true;

            DrawerHost.CloseDrawerCommand.Execute(Dock.Left, _drawerHost);

            Assert.Equal(expectedPosition, closedPosition);

            void DrawerClosing(object? sender, DrawerClosingEventArgs eventArgs)
            {
                closedPosition = eventArgs.Dock;
            }
        }
    }
}