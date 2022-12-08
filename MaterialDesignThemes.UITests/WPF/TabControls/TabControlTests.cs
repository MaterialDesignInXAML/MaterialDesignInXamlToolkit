using System.ComponentModel;
using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.TabControls;

public class TabControlTests : TestBase
{
    public TabControlTests(ITestOutputHelper output)
        : base(output)
    { }

    [Fact]
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
        Assert.NotNull(foreground);
        Assert.NotNull(background);

        MaterialDesignSpec.AssertContrastRatio(foreground.Value, background.Value, MaterialDesignSpec.MinimumContrastSmallText);

        Assert.Equal(foreground, selectedTabUnderline);

        recorder.Success();
    }

    [Theory]
    [InlineData("Center")]
    [InlineData("Left")]
    [InlineData("Right")]
    [InlineData("Stretch")]
    [InlineData("")]
    public async Task TabItem_ShouldKeepDataContext_WhenContextMenuOpens(string horizontalContentAlignment)
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
          materialDesign:ColorZoneAssist.Mode=""PrimaryMid""
          Style=""{{StaticResource MaterialDesignFilledTabControl}}"">
    <system:String>aaaa</system:String>
    <system:String>bbbb</system:String>
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
        Assert.Equal("aaaa", dataContext);

        // Act
        await button.MoveCursorTo();
        await button.RightClick();
        await tabControl.MoveCursorTo();
        await tabControl.LeftClick(Position.TopLeft);
        await Task.Delay(50); // allow a little time for the disconnect to occur
        
        // Assert data context still present
        tabItem = await tabControl.GetElement<TabItem>();
        dataContext = await tabItem.GetDataContext();
        Assert.Equal("aaaa", dataContext);

        recorder.Success();
    }
}
