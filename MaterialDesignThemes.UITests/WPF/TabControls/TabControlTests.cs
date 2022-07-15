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
}
