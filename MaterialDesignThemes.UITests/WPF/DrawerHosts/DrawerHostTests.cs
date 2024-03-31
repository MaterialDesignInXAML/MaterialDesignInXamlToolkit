using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.DrawHost;
using Xunit.Sdk;

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
            Assert.Single(invocations);
        });

        await Wait.For(async () =>
        {
            Assert.True(await contentCover.GetIsHitTestVisible());
            Assert.True(await contentCover.GetOpacity() > 0.0);
        });

        //Wait before clicking so the animations have time to finish
        await Task.Delay(100);

        await drawerHost.LeftClick();

        await Wait.For(async () => await contentCover.GetOpacity() <= 0.0);
        await Wait.For(async () =>
        {
            var invocations = await closingEvent.GetInvocations();
            Assert.Single(invocations);
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

    [Fact]
    [Description("Issue 3224")]
    public async Task DrawerHost_ShouldInvokeCustomContentTemplateSelector_WhenSetExplicitly()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        IVisualElement<Grid> grid = await LoadXaml<Grid>("""
            <Grid>
              <Grid.Resources>
                <local:CustomContentTemplateSelector x:Key="LeftDrawerContentTemplateSelector" ContentText="LeftDrawerContent" />
                <local:CustomContentTemplateSelector x:Key="TopDrawerContentTemplateSelector" ContentText="TopDrawerContent" />
                <local:CustomContentTemplateSelector x:Key="RightDrawerContentTemplateSelector" ContentText="RightDrawerContent" />
                <local:CustomContentTemplateSelector x:Key="BottomDrawerContentTemplateSelector" ContentText="BottomDrawerContent" />
              </Grid.Resources>
              <materialDesign:DrawerHost
                LeftDrawerContentTemplateSelector="{StaticResource LeftDrawerContentTemplateSelector}"
                TopDrawerContentTemplateSelector="{StaticResource TopDrawerContentTemplateSelector}"
                RightDrawerContentTemplateSelector="{StaticResource RightDrawerContentTemplateSelector}"
                BottomDrawerContentTemplateSelector="{StaticResource BottomDrawerContentTemplateSelector}">
              </materialDesign:DrawerHost>
            </Grid>
            """, ("local", typeof(CustomContentTemplateSelector)));
        IVisualElement<DrawerHost> drawerHost = await grid.GetElement<DrawerHost>();

        // Act
        string? leftDrawerContent = await GetDrawerContent("PART_LeftDrawer");
        string? topDrawerContent = await GetDrawerContent("PART_TopDrawer");
        string? rightDrawerContent = await GetDrawerContent("PART_RightDrawer");
        string? bottomDrawerContent = await GetDrawerContent("PART_BottomDrawer");

        async Task<string?> GetDrawerContent(string drawerElementKey)
        {
            var drawer = await drawerHost.GetElement<Grid>(drawerElementKey);
            try
            {
                var contentElement = await drawer.GetElement<TextBlock>();
                return await contentElement.GetText();
            }
            catch
            {
                throw FailException.ForFailure($"Failed to find 'TextBlock' content in '{drawerElementKey}'. ContentTemplateSelector not properly applied.");
            }
        }

        // Assert
        Assert.Equal("LeftDrawerContent", leftDrawerContent);
        Assert.Equal("TopDrawerContent", topDrawerContent);
        Assert.Equal("RightDrawerContent", rightDrawerContent);
        Assert.Equal("BottomDrawerContent", bottomDrawerContent);

        recorder.Success();
    }
}

public class CustomContentTemplateSelector : DataTemplateSelector
{
    public string? ContentText { get; set; }

    public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
    {
        var template = new DataTemplate();
        FrameworkElementFactory content = new FrameworkElementFactory(typeof(TextBlock));
        content.SetValue(TextBlock.TextProperty, ContentText);
        template.VisualTree = content;
        return template;
    }
}
