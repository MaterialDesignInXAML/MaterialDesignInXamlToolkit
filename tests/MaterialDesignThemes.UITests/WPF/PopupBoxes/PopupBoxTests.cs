using System.ComponentModel;
using System.Windows.Media;
using MaterialDesignThemes.UITests.Samples.PopupBox;

namespace MaterialDesignThemes.UITests.WPF.PopupBoxes;

public class PopupBoxTests : TestBase
{
    [Test]
    [Arguments(Elevation.Dp0)]
    [Arguments(Elevation.Dp16)]
    [Arguments(Elevation.Dp24)]
    [Description("Issue 3129")]
    public async Task PopupBox_WithElevation_AppliesElevationToNestedCard(Elevation elevation)
    {
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
        await Assert.That(await card.GetProperty<Elevation?>(ElevationAssist.ElevationProperty)).IsEqualTo(elevation);
    }

    [Test]
    public async Task PopupBox_WithContentTemplateSelector_ChangesContent()
    {
        //Arrange
        IVisualElement grid = (await LoadUserControl<PopupBoxWithTemplateSelector>());

        IVisualElement<Button> button = await grid.GetElement<Button>();
        IVisualElement<PopupBox> popupBox = await grid.GetElement<PopupBox>();


        // Assert
        var border = await popupBox.GetElement<Border>();
        await Assert.That(await border.GetBackgroundColor()).IsEqualTo(Colors.Blue);

        await button.LeftClick();

        await Wait.For(async () =>
        {
            border = await popupBox.GetElement<Border>();
            await Assert.That(await border.GetBackgroundColor()).IsEqualTo(Colors.Red);
        });
    }
}
