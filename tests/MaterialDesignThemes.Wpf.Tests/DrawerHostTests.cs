using System.ComponentModel;

using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

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

    [Test, STAThreadExecutor]
    [Description("Issue 2015")]
    public async Task WhenOpenedDrawerOpenedEventIsRaised()
    {
        Dock expectedPosition = Dock.Left;
        Dock openedPosition = Dock.Top;
        _drawerHost.DrawerOpened += DrawerOpened;
        _drawerHost.IsLeftDrawerOpen = true;

        DrawerHost.CloseDrawerCommand.Execute(Dock.Left, _drawerHost);

        await Assert.That(openedPosition).IsEqualTo(expectedPosition);

        void DrawerOpened(object? sender, DrawerOpenedEventArgs eventArgs)
        {
            openedPosition = eventArgs.Dock;
        }
    }

    [Test, STAThreadExecutor]
    [Description("Issue 2015")]
    public async Task WhenClosingDrawerClosingEventIsRaised()
    {
        Dock expectedPosition = Dock.Left;
        Dock closedPosition = Dock.Top;
        _drawerHost.DrawerClosing += DrawerClosing;
        _drawerHost.IsLeftDrawerOpen = true;

        DrawerHost.CloseDrawerCommand.Execute(Dock.Left, _drawerHost);

        await Assert.That(closedPosition).IsEqualTo(expectedPosition);

        void DrawerClosing(object? sender, DrawerClosingEventArgs eventArgs)
        {
            closedPosition = eventArgs.Dock;
        }
    }
}
