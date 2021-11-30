using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.UITests.Samples.DrawHost;
using MaterialDesignThemes.Wpf;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.DrawerHosts;

public class DialogHostTests : TestBase
{
    public DialogHostTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task DrawerHost_OpenAndClose_RaisesEvents()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<DrawerHost> drawerHost = await LoadXaml<DrawerHost>(@"
<materialDesign:DrawerHost>
  <materialDesign:DrawerHost.LeftDrawerContent>
    <StackPanel Width=""150"" x:Name=""DrawerContents"" />
  </materialDesign:DrawerHost.LeftDrawerContent>

  <StackPanel HorizontalAlignment=""Center"" VerticalAlignment=""Top"">
    <ToggleButton Content=""L"" IsChecked=""{Binding IsLeftDrawerOpen, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type materialDesign:DrawerHost}}}""
                  Style=""{StaticResource MaterialDesignActionLightToggleButton}"" ToolTip=""Open Left Drawer"" />
    <Button Content=""Open Left Drawer"" Margin=""10"" Command=""{x:Static materialDesign:DrawerHost.OpenDrawerCommand}"" CommandParameter=""{x:Static Dock.Left}"" />
  </StackPanel>
</materialDesign:DrawerHost>");

        var contentCover = await drawerHost.GetElement<FrameworkElement>("PART_ContentCover");
        var toggleButton = await drawerHost.GetElement<ToggleButton>("/ToggleButton");
        var showButton = await drawerHost.GetElement<Button>("/Button");
        var contents = await drawerHost.GetElement<StackPanel>("DrawerContents");

        var openedEvent = await drawerHost.RegisterForEvent(nameof(DrawerHost.DrawerOpened));
        var closingEvent = await drawerHost.RegisterForEvent(nameof(DrawerHost.DrawerClosing));

        await toggleButton.LeftClick();
        //Allow for animations to start
        await Task.Delay(10);

        await Wait.For(async () =>
        {
            var invocations = await openedEvent.GetInvocations();
            Assert.Equal(1, invocations.Count);
        });

        await drawerHost.LeftClick();

        await Wait.For(async () => await contentCover.GetOpacity() <= 0.0);
        await Wait.For(async () =>
        {
            var invocations = await closingEvent.GetInvocations();
            Assert.Equal(1, invocations.Count);
        });

        await showButton.LeftClick();
        //Allow for animations to start
        await Task.Delay(10);

        await Wait.For(async () =>
        {
            var invocations = await openedEvent.GetInvocations();
            Assert.Equal(2, invocations.Count);
        });
        await Wait.For(async () => await contentCover.GetOpacity() > 0.0);

        await drawerHost.LeftClick();

        await Wait.For(async () => await contentCover.GetOpacity() <= 0.0);
        await Wait.For(async () =>
        {
            var invocations = await closingEvent.GetInvocations();
            Assert.Equal(2, invocations.Count);
        });

        recorder.Success();
    }

    [Fact]
    public async Task DrawerHost_CancelingClosingEvent_DrawerStaysOpen()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<DrawerHost> drawerHost = (await LoadUserControl<CancellingDrawerHost>()).As<DrawerHost>();
        var showButton = await drawerHost.GetElement<Button>("ShowButton");
        var closeButton = await drawerHost.GetElement<Button>("CloseButton");
        var closeButtonDp = await drawerHost.GetElement<Button>("CloseButtonDp");

        var openedEvent = await drawerHost.RegisterForEvent(nameof(DrawerHost.DrawerOpened));

        await showButton.LeftClick();
        //Allow open animation to finish
        await Task.Delay(300);
        await Wait.For(async () => (await openedEvent.GetInvocations()).Count == 1);

        var closingEvent = await drawerHost.RegisterForEvent(nameof(DrawerHost.DrawerClosing));

        //Attempt closing with routed command
        await closeButton.LeftClick();
        await Task.Delay(100);
        await Wait.For(async () => (await closingEvent.GetInvocations()).Count == 1);
        Assert.True(await drawerHost.GetIsLeftDrawerOpen());

        //Attempt closing with click away
        await drawerHost.LeftClick();
        await Task.Delay(100);
        await Wait.For(async () => (await closingEvent.GetInvocations()).Count == 2);
        Assert.True(await drawerHost.GetIsLeftDrawerOpen());

        //Attempt closing with DP property toggle
        await closeButtonDp.LeftClick();
        await Task.Delay(100);
        await Wait.For(async () => (await closingEvent.GetInvocations()).Count == 3);
        Assert.True(await drawerHost.GetIsLeftDrawerOpen());


        recorder.Success();
    }
}

