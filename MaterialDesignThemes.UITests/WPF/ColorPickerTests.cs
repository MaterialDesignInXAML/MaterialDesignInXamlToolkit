using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.UITests.WPF;

public class ColorPickerTests : TestBase
{
    public ColorPickerTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    public async Task OnLostFocusIfSelectedTimeIsNull_DatePartWillBeToday()
    {
        await using var recorder = new TestRecorder(App);

        IVisualElement<ColorPicker> colorPicker = await LoadXaml<ColorPicker>(@"
    <materialDesign:ColorPicker Width=""400"" Height=""100"" Color=""Red""/>");

        var thumb = await colorPicker.GetElement<Thumb>(ColorPicker.SaturationBrightnessPickerThumbPartName);

        Color color = await colorPicker.GetColor();
        double lastBrightness = color.ToHsb().Brightness;

        Rect thumbLocation = await thumb.GetCoordinates();

        await thumb.SendInput(MouseInput.MoveToElement(Position.BottomLeft));
        await thumb.SendInput(MouseInput.MoveRelative(xOffset: -5, yOffset:-10));
        await thumb.SendInput(MouseInput.LeftDown());
        await thumb.SendInput(MouseInput.MoveRelative(yOffset:25));
        await Task.Delay(100);
        await thumb.SendInput(MouseInput.LeftUp());


        double currentBrightness = (await colorPicker.GetColor()).ToHsb().Brightness;
        Assert.True(currentBrightness < lastBrightness, $"Brightness {currentBrightness} is not less than {lastBrightness}");
        
        recorder.Success();
    }
}
