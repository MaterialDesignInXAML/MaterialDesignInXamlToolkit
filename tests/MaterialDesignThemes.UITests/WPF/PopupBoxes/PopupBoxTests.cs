using System.ComponentModel;
using System.Windows.Media;
using MaterialDesignThemes.UITests.Samples.PopupBox;

namespace MaterialDesignThemes.UITests.WPF.PopupBoxes;

public class PopupBoxTests : TestBase
{
    public PopupBoxTests(ITestOutputHelper output)
        : base(output)
    { }

    [Theory]
    [InlineData(Elevation.Dp0)]
    [InlineData(Elevation.Dp16)]
    [InlineData(Elevation.Dp24)]
    [Description("Issue 3129")]
    public async Task PopupBox_WithElevation_AppliesElevationToNestedCard(Elevation elevation)
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement<PopupBox> popupBox = await LoadXaml<PopupBox>($@"
<materialDesign:PopupBox VerticalAlignment=""Top""
                         PopupElevation=""{elevation}"">
  <StackPanel>
    <Button Content=""More"" />
    <Button Content=""Options"" />
  </StackPanel>
</materialDesign:PopupBox>");

        IVisualElement<Card> card = await popupBox.GetElement<Card>("/Card");

        // Assert
        Assert.Equal(elevation, await card.GetProperty<Elevation?>(ElevationAssist.ElevationProperty));

        recorder.Success();
    }

    [Fact]
    public async Task PopupBox_WithContentTemplateSelector_ChangesContent()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        IVisualElement grid = (await LoadUserControl<PopupBoxWithTemplateSelector>());

        IVisualElement<Button> button = await grid.GetElement<Button>();
        IVisualElement<PopupBox> popupBox = await grid.GetElement<PopupBox>();


        // Assert
        var border = await popupBox.GetElement<Border>();
        Assert.Equal(Colors.Blue, await border.GetBackgroundColor());

        await button.LeftClick();

        await Wait.For(async () =>
        {
            border = await popupBox.GetElement<Border>();
            Assert.Equal(Colors.Red, await border.GetBackgroundColor());
        });


        recorder.Success();
    }
}
