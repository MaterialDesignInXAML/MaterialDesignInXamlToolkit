using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.SnackBars;

public class SnackBarTests : TestBase
{
    [Test]
    [Description("Issue 1223")]
    public async Task SnackBar_WithFontSizeAndWeight_AffectsDisplayedMessage()
    {
        //Arrange
        IVisualElement<Snackbar> snackBar = await LoadXaml<Snackbar>("""
            <materialDesign:Snackbar
                IsActive="True"
                Message="Message"
                FontSize="14"
                FontWeight="Bold"/>
            """);

        IVisualElement<TextBlock> textBlock = await snackBar.GetElement<TextBlock>();

        // Assert
        await Assert.That(await textBlock.GetFontSize()).IsEqualTo(14);
        await Assert.That(await textBlock.GetFontWeight()).IsEqualTo(FontWeights.Bold);
    }
}
