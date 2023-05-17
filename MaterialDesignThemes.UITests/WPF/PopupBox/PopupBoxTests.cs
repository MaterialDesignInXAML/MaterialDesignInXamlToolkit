using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.PopupBox;

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
        IVisualElement<Wpf.PopupBox> popupBox = await LoadXaml<Wpf.PopupBox>($@"
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
}
