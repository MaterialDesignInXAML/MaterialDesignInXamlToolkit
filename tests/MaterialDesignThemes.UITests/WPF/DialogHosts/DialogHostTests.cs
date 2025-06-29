using System.ComponentModel;
using System.Windows.Media;
using MaterialDesignThemes.UITests.Samples.DialogHost;

using static MaterialDesignThemes.UITests.MaterialDesignSpec;


namespace MaterialDesignThemes.UITests.WPF.DialogHosts;

public class DialogHostTests : TestBase
{
    public DialogHostTests()
    {
        AttachedDebuggerToRemoteProcess = false;
    }

    [Test]
    public async Task OnOpenDialog_OverlayCoversContent()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement dialogHost = await LoadUserControl<WithCounter>();
        var overlay = await dialogHost.GetElement<Grid>("PART_ContentCoverGrid");

        var resultTextBlock = await dialogHost.GetElement<TextBlock>("ResultTextBlock");
        await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 0");

        var testOverlayButton = await dialogHost.GetElement<Button>("TestOverlayButton");
        await testOverlayButton.LeftClick();
        await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");

        var showDialogButton = await dialogHost.GetElement<Button>("ShowDialogButton");
        await showDialogButton.LeftClick();

        var closeDialogButton = await dialogHost.GetElement<Button>("CloseDialogButton");
        await Wait.For(async () => await closeDialogButton.GetIsVisible() == true);

        await testOverlayButton.LeftClick();
        await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");
        await Task.Delay(200, TestContext.Current!.CancellationToken);
        await closeDialogButton.LeftClick();

        var retry = new Retry(5, TimeSpan.FromSeconds(5));
        try
        {
            await Wait.For(async () => await overlay.GetVisibility() != Visibility.Visible, retry);
        }
        catch (TimeoutException)
        {
            await closeDialogButton.LeftClick();
            await Wait.For(async () => await overlay.GetVisibility() != Visibility.Visible, retry);
        }
        await testOverlayButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That((await resultTextBlock.GetText())!).IsEqualTo("Clicks: 2");
        }, retry);

        recorder.Success();
    }

    [Test]
    [Description("Issue 2282")]
    public async Task ClosingDialogWithIsOpenProperty_ShouldRaiseDialogClosingEvent()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement dialogHost = await LoadUserControl<ClosingEventCounter>();
        var showButton = await dialogHost.GetElement<Button>("ShowDialogButton");
        var closeButton = await dialogHost.GetElement<Button>("CloseButton");
        var resultTextBlock = await dialogHost.GetElement<TextBlock>("ResultTextBlock");

        await showButton.LeftClick();
        await Wait.For(async () => await closeButton.GetIsVisible());
        await Task.Delay(300, TestContext.Current!.CancellationToken);
        await closeButton.LeftClick();

        await Wait.For(async () =>
        {
            await Assert.That(await resultTextBlock.GetText()).IsEqualTo("1");
        });
        recorder.Success();
    }

    [Test]
    [Description("Issue 2398")]
    public async Task FontSettingsShouldInheritIntoDialog()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement grid = await LoadXaml<Grid>(@"
<Grid TextElement.FontSize=""42""
      TextElement.FontFamily=""Times New Roman""
      TextElement.FontWeight=""ExtraBold"">
  <Grid.ColumnDefinitions>
    <ColumnDefinition />
    <ColumnDefinition />
  </Grid.ColumnDefinitions>
  <materialDesign:DialogHost>
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock1"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton1"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
  <materialDesign:DialogHost Style=""{StaticResource MaterialDesignEmbeddedDialogHost}"" Grid.Column=""1"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock2"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton2"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
</Grid>");
        var showButton1 = await grid.GetElement<Button>("ShowButton1");
        var showButton2 = await grid.GetElement<Button>("ShowButton2");

        await showButton1.LeftClick();
        await showButton2.LeftClick();
        await Task.Delay(300, TestContext.Current!.CancellationToken);

        var text1 = await grid.GetElement<TextBlock>("TextBlock1");
        var text2 = await grid.GetElement<TextBlock>("TextBlock2");

        await Assert.That(await text1.GetFontSize()).IsEqualTo(42);
        await Assert.That((await text1.GetFontFamily())?.FamilyNames.Values.Contains("Times New Roman")).IsTrue();
        await Assert.That(await text1.GetFontWeight()).IsEqualTo(FontWeights.ExtraBold);

        await Assert.That(await text2.GetFontSize()).IsEqualTo(42);
        await Assert.That((await text2.GetFontFamily())?.FamilyNames.Values.Contains("Times New Roman")).IsTrue();
        await Assert.That(await text2.GetFontWeight()).IsEqualTo(FontWeights.ExtraBold);

        recorder.Success();
    }

    [Test]
    [Description("PR 2236")]
    public async Task ContentBackground_SetsDialogBackground()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement grid = await LoadXaml<Grid>(@"
<Grid>
  <Grid.ColumnDefinitions>
    <ColumnDefinition />
    <ColumnDefinition />
  </Grid.ColumnDefinitions>
  <materialDesign:DialogHost DialogBackground=""Red"" x:Name=""DialogHost1"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock1"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton1"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
  <materialDesign:DialogHost Style=""{StaticResource MaterialDesignEmbeddedDialogHost}"" DialogBackground=""Red"" x:Name=""DialogHost2"" Grid.Column=""1"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock2"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton2"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
</Grid>");
        var showButton1 = await grid.GetElement<Button>("ShowButton1");
        var showButton2 = await grid.GetElement<Button>("ShowButton2");

        await showButton1.LeftClick();
        await showButton2.LeftClick();

        var dialogHost1 = await grid.GetElement<DialogHost>("DialogHost1");
        var dialogHost2 = await grid.GetElement<DialogHost>("DialogHost2");

        await Wait.For(async () =>
        {
            var card1 = await dialogHost1.GetElement<Card>("PART_PopupContentElement");
            var card2 = await dialogHost2.GetElement<Card>("PART_PopupContentElement");

            await Assert.That(await card1.GetBackgroundColor()).IsEqualTo(Colors.Red);
            await Assert.That(await card2.GetBackgroundColor()).IsEqualTo(Colors.Red);
        });

        recorder.Success();
    }

    [Test]
    [Arguments(BaseTheme.Inherit)]
    [Arguments(BaseTheme.Dark)]
    [Arguments(BaseTheme.Light)]
    public async Task DialogBackgroundShouldInheritThemeBackground(BaseTheme dialogTheme)
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement grid = await LoadXaml<Grid>($@"
<Grid>
  <Grid.ColumnDefinitions>
    <ColumnDefinition />
    <ColumnDefinition />
  </Grid.ColumnDefinitions>

  <materialDesign:DialogHost DialogTheme=""{dialogTheme}"" x:Name=""DialogHost1"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock1"" Margin=""50"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton1"" Command=""{{x:Static materialDesign:DialogHost.OpenDialogCommand}}"" />
  </materialDesign:DialogHost>

  <materialDesign:DialogHost Style=""{{StaticResource MaterialDesignEmbeddedDialogHost}}"" DialogTheme=""{dialogTheme}"" x:Name=""DialogHost2"" Grid.Column=""1"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" x:Name=""TextBlock2"" Margin=""50"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton2"" Command=""{{x:Static materialDesign:DialogHost.OpenDialogCommand}}"" />
  </materialDesign:DialogHost>

</Grid>");
        var showButton1 = await grid.GetElement<Button>("ShowButton1");
        var showButton2 = await grid.GetElement<Button>("ShowButton2");

        await showButton1.LeftClick();
        await showButton2.LeftClick();

        var dialogHost1 = await grid.GetElement<DialogHost>("DialogHost1");
        var dialogHost2 = await grid.GetElement<DialogHost>("DialogHost2");

        var card1 = await Wait.For(async () => await dialogHost1.GetElement<Card>("PART_PopupContentElement"));
        var card2 = await Wait.For(async () => await dialogHost2.GetElement<Card>("PART_PopupContentElement"));

        IResource paperResource1 = await card1.GetResource("MaterialDesign.Brush.Background");
        var paperBrush1 = paperResource1.GetAs<SolidColorBrush>();
        await Assert.That(paperBrush1).IsNotNull();
        paperBrush1!.Freeze();
        IResource paperResource2 = await card1.GetResource("MaterialDesign.Brush.Background");
        var paperBrush2 = paperResource2.GetAs<SolidColorBrush>();
        await Assert.That(paperBrush2).IsNotNull();
        paperBrush2!.Freeze();

        await Assert.That(await card1.GetBackgroundColor()).IsEqualTo(paperBrush1.Color);
        await Assert.That(await card2.GetBackgroundColor()).IsEqualTo(paperBrush2.Color);

        var textBlock1 = await dialogHost1.GetElement<TextBlock>("TextBlock1");
        var textBlock2 = await dialogHost2.GetElement<TextBlock>("TextBlock2");

        await Wait.For(async () =>
        {
            Color? foreground1 = await textBlock1.GetForegroundColor();
            await Assert.That(foreground1).IsNotNull();
            await AssertContrastRatio(
                foreground1.Value,
                await textBlock1.GetEffectiveBackground(),
                MinimumContrastSmallText);
        });

        await Wait.For(async () =>
        {
            Color? foreground2 = await textBlock2.GetForegroundColor();
            await Assert.That(foreground2).IsNotNull();
            await AssertContrastRatio(
                foreground2.Value,
                await textBlock2.GetEffectiveBackground(),
                MinimumContrastSmallText);
        });

        recorder.Success();
    }

    [Test]
    [Description("Issue 2772")]
    public async Task CornerRadius_AppliedToContentCoverBorder_WhenSetOnDialogHost()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement grid = await LoadXaml<Grid>(@"
<Grid>
  <materialDesign:DialogHost x:Name=""DialogHost"" CornerRadius=""1,2,3,4"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
</Grid>");

        var showButton = await grid.GetElement<Button>("ShowButton");
        var dialogHost = await grid.GetElement<DialogHost>("DialogHost");

        await showButton.LeftClick();

        await Wait.For(async () =>
        {
            var contentCoverBorder = await dialogHost.GetElement<Border>("ContentCoverBorder");

            await Assert.That((await contentCoverBorder.GetCornerRadius()).TopLeft).IsEqualTo(1);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).TopRight).IsEqualTo(2);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).BottomRight).IsEqualTo(3);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).BottomLeft).IsEqualTo(4);
        });

        recorder.Success();
    }

    [Test]
    [Description("Issue 2772")]
    public async Task CornerRadius_AppliedToContentCoverBorder_WhenSetOnEmbeddedDialogHost()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement grid = await LoadXaml<Grid>(@"
<Grid>
  <materialDesign:DialogHost x:Name=""DialogHost"" Style=""{StaticResource MaterialDesignEmbeddedDialogHost}"" CornerRadius=""1,2,3,4"">
    <materialDesign:DialogHost.DialogContent>
      <TextBlock Text=""Some Text"" />
    </materialDesign:DialogHost.DialogContent>
    <Button Content=""Show Dialog"" x:Name=""ShowButton"" Command=""{x:Static materialDesign:DialogHost.OpenDialogCommand}"" />
  </materialDesign:DialogHost>
</Grid>");

        var showButton = await grid.GetElement<Button>("ShowButton");
        var dialogHost = await grid.GetElement<DialogHost>("DialogHost");

        await showButton.LeftClick();

        await Wait.For(async () =>
        {
            var contentCoverBorder = await dialogHost.GetElement<Border>("ContentCoverBorder");
                
            await Assert.That((await contentCoverBorder.GetCornerRadius()).TopLeft).IsEqualTo(1);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).TopRight).IsEqualTo(2);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).BottomRight).IsEqualTo(3);
            await Assert.That((await contentCoverBorder.GetCornerRadius()).BottomLeft).IsEqualTo(4);
        });

        recorder.Success();
    }

    [Test]
    [Description("Issue 3069")]
    public async Task DialogHost_WithOpenDialog_ShowsPopupWhenLoaded()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> rootGrid = (await LoadUserControl<LoadAndUnloadControl>()).As<Grid>();

        IVisualElement<Button> loadButton = await rootGrid.GetElement<Button>("LoadDialogHost");
        IVisualElement<Button> unloadButton = await rootGrid.GetElement<Button>("UnloadDialogHost");
        IVisualElement<Button> toggleButton = await rootGrid.GetElement<Button>("ToggleIsOpen");

        IVisualElement<DialogHost> dialogHost = await rootGrid.GetElement<DialogHost>("DialogHost");
        IVisualElement<Button> closeButton = await dialogHost.GetElement<Button>("CloseButton");

        await toggleButton.LeftClick();

        await Wait.For(async () => await Assert.That(await dialogHost.GetIsOpen()).IsTrue());
        await Wait.For(async () => await Assert.That(await closeButton.GetIsVisible()).IsTrue());

        await unloadButton.LeftClick();

        await Wait.For(async () => await Assert.That(await closeButton.GetIsVisible()).IsFalse() == false);

        await loadButton.LeftClick();

        await Wait.For(async () => await Assert.That(await closeButton.GetIsVisible()).IsTrue());

        await Wait.For(async () =>
        {
            await closeButton.LeftClick();
            await Assert.That(await dialogHost.GetIsOpen()).IsFalse();
        });

        recorder.Success();
    }

    [Test]
    [Description("Issue 3094")]
    public async Task DialogHost_ChangesSelectedTabItem_DoesNotPerformTabChangeWhenRestoringFocus()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> rootGrid = (await LoadUserControl<RestoreFocus>()).As<Grid>();
        IVisualElement<TabItem> tabItem1 = await rootGrid.GetElement<TabItem>("TabItem1");
        IVisualElement<TabItem> tabItem2 = await rootGrid.GetElement<TabItem>("TabItem2");
        IVisualElement<Button> navigateHomeButton = await rootGrid.GetElement<Button>("NavigateHomeButton");

        // Select TabItem2
        await tabItem2.LeftClick();

        // Open menu
        IVisualElement<MenuItem> menuItem1 = await rootGrid.GetElement<MenuItem>("MenuItem1");
        await menuItem1.LeftClick();
        await Task.Delay(1000, TestContext.Current!.CancellationToken); // Wait for menu to open
        IVisualElement<MenuItem> menuItem2 = await rootGrid.GetElement<MenuItem>("MenuItem2");
        await menuItem2.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to show

        // Click navigate button
        await navigateHomeButton.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to close

        await Assert.That(await tabItem1.GetIsSelected()).IsTrue();
        await Assert.That(await tabItem2.GetIsSelected()).IsFalse();

        recorder.Success();
    }

    [Test]
    [Description("Issue 3094")]
    public async Task DialogHost_ChangesSelectedRailItem_DoesNotPerformRailChangeWhenRestoringFocus()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> rootGrid = (await LoadUserControl<RestoreFocus>()).As<Grid>();
        IVisualElement<TabItem> railItem1 = await rootGrid.GetElement<TabItem>("RailItem1");
        IVisualElement<TabItem> railItem2 = await rootGrid.GetElement<TabItem>("RailItem2");
        IVisualElement<Button> navigateHomeButton = await rootGrid.GetElement<Button>("NavigateHomeButton");

        // Select TabItem2
        await railItem2.LeftClick();

        // Open menu
        IVisualElement<MenuItem> menuItem1 = await rootGrid.GetElement<MenuItem>("MenuItem1");
        await menuItem1.LeftClick();
        await Task.Delay(1000, TestContext.Current!.CancellationToken); // Wait for menu to open
        IVisualElement<MenuItem> menuItem2 = await rootGrid.GetElement<MenuItem>("MenuItem2");
        await menuItem2.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to show

        // Click navigate button
        await navigateHomeButton.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to close

        await Assert.That(await railItem1.GetIsSelected()).IsTrue();
        await Assert.That(await railItem2.GetIsSelected()).IsFalse();

        recorder.Success();
    }

    [Test]
    [Description("Issue 3094")]
    public async Task DialogHost_ChangesSelectedTabItem_DoesNotPerformTabChangeWhenRestoreFocusIsDisabled()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> rootGrid = (await LoadUserControl<RestoreFocusDisabled>()).As<Grid>();
        IVisualElement<TabItem> tabItem1 = await rootGrid.GetElement<TabItem>("TabItem1");
        IVisualElement<TabItem> tabItem2 = await rootGrid.GetElement<TabItem>("TabItem2");
        IVisualElement<Button> navigateHomeButton = await rootGrid.GetElement<Button>("NavigateHomeButton");

        // Select TabItem2
        await tabItem2.LeftClick();

        // Open menu
        IVisualElement<MenuItem> menuItem1 = await rootGrid.GetElement<MenuItem>("MenuItem1");
        await menuItem1.LeftClick();
        await Task.Delay(1000, TestContext.Current!.CancellationToken); // Wait for menu to open
        IVisualElement<MenuItem> menuItem2 = await rootGrid.GetElement<MenuItem>("MenuItem2");
        await menuItem2.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to show

        // Click navigate button
        await navigateHomeButton.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to close

        await Assert.That(await tabItem1.GetIsSelected()).IsTrue();
        await Assert.That(await tabItem2.GetIsSelected()).IsFalse();

        recorder.Success();
    }

    [Test]
    [Description("Issue 3094")]
    public async Task DialogHost_ChangesSelectedRailItem_DoesNotPerformRailChangeWhenRestoreFocusIsDisabled()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<Grid> rootGrid = (await LoadUserControl<RestoreFocusDisabled>()).As<Grid>();
        IVisualElement<TabItem> railItem1 = await rootGrid.GetElement<TabItem>("RailItem1");
        IVisualElement<TabItem> railItem2 = await rootGrid.GetElement<TabItem>("RailItem2");
        IVisualElement<Button> navigateHomeButton = await rootGrid.GetElement<Button>("NavigateHomeButton");

        // Select TabItem2
        await railItem2.LeftClick();

        // Open menu
        IVisualElement<MenuItem> menuItem1 = await rootGrid.GetElement<MenuItem>("MenuItem1");
        await menuItem1.LeftClick();
        await Task.Delay(1000, TestContext.Current!.CancellationToken); // Wait for menu to open
        IVisualElement<MenuItem> menuItem2 = await rootGrid.GetElement<MenuItem>("MenuItem2");
        await menuItem2.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to show

        // Click navigate button
        await navigateHomeButton.LeftClick();
        await Task.Delay(1000, TestContext.Current.CancellationToken); // Wait for dialog content to close

        await Assert.That(await railItem1.GetIsSelected()).IsTrue();
        await Assert.That(await railItem2.GetIsSelected()).IsFalse();

        recorder.Success();
    }

    [Test]
    [Description("Issue 3450")]
    public async Task DialogHost_WithComboBox_CanSelectItem()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement dialogHost = await LoadUserControl<WithComboBox>();

        var comboBox = await dialogHost.GetElement<ComboBox>("TargetedPlatformComboBox");
        await Task.Delay(500, TestContext.Current!.CancellationToken);
        await comboBox.LeftClick();
        
        var item = await Wait.For(() => comboBox.GetElement<ComboBoxItem>("TargetItem"));
        await Task.Delay(TimeSpan.FromSeconds(1));
        await item.LeftClick();

        await Wait.For(async () =>
        {
            var index = await comboBox.GetSelectedIndex();
            await Assert.That(index).IsEqualTo(1);
        });


        recorder.Success();
    }
}
