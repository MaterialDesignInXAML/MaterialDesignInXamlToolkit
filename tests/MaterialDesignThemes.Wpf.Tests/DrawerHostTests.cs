using System.ComponentModel;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class DrawerHostTests
{
    public static DrawerHost CreateDrawerHost()
    {
        var drawerHost = new DrawerHost();
        drawerHost.ApplyDefaultStyle();
        drawerHost.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
        return drawerHost;
    }

    [Test]
    [Description("Issue 2015")]
    public async Task WhenOpenedDrawerOpenedEventIsRaised()
    {
        DrawerHost drawerHost = CreateDrawerHost();
        Dock expectedPosition = Dock.Left;
        Dock openedPosition = Dock.Top;
        drawerHost.DrawerOpened += DrawerOpened;
        drawerHost.IsLeftDrawerOpen = true;

        DrawerHost.CloseDrawerCommand.Execute(Dock.Left, drawerHost);

        await Assert.That(openedPosition).IsEqualTo(expectedPosition);


        void DrawerOpened(object? sender, DrawerOpenedEventArgs eventArgs)
        {
            openedPosition = eventArgs.Dock;
        }
    }

    [Test]
    [Description("Issue 2015")]
    public async Task WhenClosingDrawerClosingEventIsRaised()
    {
        DrawerHost drawerHost = CreateDrawerHost();

        Dock expectedPosition = Dock.Left;
        Dock closedPosition = Dock.Top;
        drawerHost.DrawerClosing += DrawerClosing;
        drawerHost.IsLeftDrawerOpen = true;

        DrawerHost.CloseDrawerCommand.Execute(Dock.Left, drawerHost);

        await Assert.That(closedPosition).IsEqualTo(expectedPosition);

        void DrawerClosing(object? sender, DrawerClosingEventArgs eventArgs)
        {
            closedPosition = eventArgs.Dock;
        }
    }
}
