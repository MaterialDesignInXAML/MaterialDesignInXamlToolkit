using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.SnackBars;

public class SnackBarTests : TestBase
{
    public SnackBarTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    [Description("Issue 1223")]
    public async Task SnackBar_WithFontSizeAndWeight_AffectsDisplayedMessage()
    {
        await using var recorder = new TestRecorder(App);

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
        Assert.Equal(14, await textBlock.GetFontSize());
        Assert.Equal(FontWeights.Bold, await textBlock.GetFontWeight());

        recorder.Success();
    }
}
