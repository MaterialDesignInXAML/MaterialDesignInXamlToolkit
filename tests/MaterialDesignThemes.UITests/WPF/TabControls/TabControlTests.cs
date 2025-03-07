using System.ComponentModel;
using System.Windows.Media;


namespace MaterialDesignThemes.UITests.WPF.TabControls;

public class TabControlTests : TestBase
{
    public TabControlTests(ITestOutputHelper output)
        : base(output)
    { }

    [Test]
    [Description("Issue 2602")]
    public async Task OnLoad_ThemeBrushesSet()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<TabControl> tabControl = await LoadXaml<TabControl>(@"
<TabControl 
        materialDesign:ColorZoneAssist.Mode=""PrimaryMid""
        Style=""{StaticResource MaterialDesignFilledTabControl}"">
    <TabItem Header=""TAB 1"">
        <TextBlock Margin=""8"" Text=""PrimaryMid Tab 1"" />
    </TabItem>
    <TabItem Header=""TAB 2"">
        <TextBlock Margin=""8"" Text=""PrimaryMid Tab 2"" />
    </TabItem>
</TabControl>");

        IVisualElement<TextBlock> textBlock = await tabControl.GetElement<TextBlock>(@"/TabItem[0]/TextBlock[0]");
        IVisualElement<Border> selectedTabBorder = await tabControl.GetElement<Border>(@"/TabItem[0]~SelectionHighlightBorder");

        //Act
        Color? foreground = await textBlock.GetForegroundColor();
        Color? background = await textBlock.GetEffectiveBackground();
        Color? selectedTabUnderline = await selectedTabBorder.GetBorderBrushColor();

        //Assert
        await Assert.NotNull(foreground);
        await Assert.NotNull(background);

        MaterialDesignSpec.AssertContrastRatio(foreground.Value, background.Value, MaterialDesignSpec.MinimumContrastSmallText);

        await Assert.Equal(foreground, selectedTabUnderline);

        recorder.Success();
    }

    [Description("Issue 2983")]
    [Test]
    [Arguments("Center", true)]
    [Arguments("Center", false)]
    [Arguments("Left", true)]
    [Arguments("Left", false)]
    [Arguments("Right", true)]
    [Arguments("Right", false)]
    [Arguments("Stretch", true)]
    [Arguments("Stretch", false)]
    [Arguments("", true)]
    [Arguments("", false)]
    public async Task TabItem_ShouldKeepDataContext_WhenContextMenuOpens(string horizontalContentAlignment, bool hasUniformTabWidth)
    {
        await using var recorder = new TestRecorder(App);

        string alignment = string.Empty;
        if (!string.IsNullOrEmpty(horizontalContentAlignment))
        {
            alignment = $"HorizontalContentAlignment=\"{horizontalContentAlignment}\"";
        }

        //Arrange
        IVisualElement<StackPanel> stackPanel = await LoadXaml<StackPanel>(@$"
<StackPanel Orientation=""Vertical"">
  <TabControl
          {alignment}
          materialDesign:TabAssist.HasUniformTabWidth=""{hasUniformTabWidth}""
          materialDesign:ColorZoneAssist.Mode=""PrimaryMid""
          Style=""{{StaticResource MaterialDesignFilledTabControl}}"">
    <system:String>aaaa</system:String>
    <system:String>bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb</system:String>
    <TabControl.ItemTemplate>
      <DataTemplate DataType=""system:String"">
        <TextBlock Text=""{{Binding}}"" />
      </DataTemplate>
    </TabControl.ItemTemplate>
    <TabControl.ContentTemplate>
      <DataTemplate DataType=""system:String"">
        <TextBlock Text=""{{Binding}}"" />
      </DataTemplate>
    </TabControl.ContentTemplate>
  </TabControl>
  <Button Margin=""50"" Width=""200"" Content=""Button with context menu"">
    <Button.ContextMenu>
      <ContextMenu>
        <MenuItem Header=""Menu item"" />
      </ContextMenu>
    </Button.ContextMenu>
  </Button>
</StackPanel>", ("system", typeof(string)));

        IVisualElement<TabControl> tabControl = await stackPanel.GetElement<TabControl>();
        IVisualElement<Button> button = await stackPanel.GetElement<Button>();

        // Assert initial data context
        IVisualElement<TabItem> tabItem = await tabControl.GetElement<TabItem>();
        object? dataContext = await tabItem.GetDataContext();
        await Assert.Equal("aaaa", dataContext);

        // Act
        await button.MoveCursorTo();
        await button.RightClick();
        await tabControl.MoveCursorTo();
        await tabControl.LeftClick(Position.TopLeft);
        await Task.Delay(50); // allow a little time for the disconnect to occur
        
        // Assert data context still present
        tabItem = await tabControl.GetElement<TabItem>();
        dataContext = await tabItem.GetDataContext();
        await Assert.Equal("aaaa", dataContext);

        recorder.Success();
    }

    [Test]
    [Description("Issue 3271")]
    public async Task TabControl_ShouldRespectSelectedContentTemplate_WhenSetDirectlyOnTabItem()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<TabControl> tabControl = await LoadXaml<TabControl>("""
            <TabControl materialDesign:ColorZoneAssist.Mode="PrimaryMid"
                        Style="{StaticResource MaterialDesignFilledTabControl}">
              <TabControl.Resources>
                <DataTemplate x:Key="CustomContentTemplate">
                  <Border Background="Fuchsia" Padding="10" Margin="10" CornerRadius="10">
                    <TextBlock Text="{Binding .}" />
                  </Border>
                </DataTemplate>
              </TabControl.Resources>
              <TabItem Content="Tab content string" ContentTemplate="{StaticResource CustomContentTemplate}" />
            </TabControl>
            """);

        IVisualElement<Border> selectedContentBorder = await tabControl.GetElement<Border>("PART_BorderSelectedContent");

        //Act
        var customContentBorder = await selectedContentBorder.GetElement<Border>("/Border");
        IVisualElement<TextBlock> customContent = await customContentBorder.GetElement<TextBlock>(@"/TextBlock");

        //Assert
        await Assert.Equal(Colors.Fuchsia, await customContentBorder.GetBackgroundColor());
        await Assert.Equal("Tab content string", await customContent.GetText());

        recorder.Success();
    }
}
