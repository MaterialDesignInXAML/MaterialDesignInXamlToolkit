using System.Windows.Media;

namespace MaterialDesignThemes.UITests.WPF.Buttons;

public class RaisedButtonTests : TestBase
{
    [Test]
    public async Task OnLoad_ThemeBrushesSet()
    {
        //Arrange
        IVisualElement<Button> button = await LoadXaml<Button>(@"<Button Content=""Button"" />");
        Color midColor = await GetThemeColor("MaterialDesign.Brush.Primary");

        //Act
        Color? color = await button.GetBackgroundColor();

        //Assert
        await Assert.That(color).IsEqualTo(midColor);
    }
}
