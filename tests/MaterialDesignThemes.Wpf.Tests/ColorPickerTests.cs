using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Tests;

//TODO: Many of these tests could be moved over to MaterialDesignThemes.UITests
[TestExecutor<STAThreadExecutor>]
public class ColorPickerTests
{
    public static ColorPicker CreateElement()
    {
        ColorPicker colorPicker = new();
        colorPicker.ApplyDefaultStyle();
        colorPicker.Arrange(new Rect(0, 0, 400, 100));
        return colorPicker;
    }

    public static Slider GetHueSlider(ColorPicker colorPicker)
        => colorPicker.FindVisualChild<Slider>(ColorPicker.HueSliderPartName);

    public static Canvas GetSaturationBrightnessPicker(ColorPicker colorPicker)
        => colorPicker.FindVisualChild<Canvas>(ColorPicker.SaturationBrightnessPickerPartName);

    public static Thumb GetSaturationBrightnessPickerThumb(ColorPicker colorPicker)
        => colorPicker.FindVisualChild<Thumb>(ColorPicker.SaturationBrightnessPickerThumbPartName);

    [Test]
    public async Task ColorPickerDefaultsToDefaultColor()
    {
        ColorPicker colorPicker = CreateElement();
        Slider slider = GetHueSlider(colorPicker);
        await Assert.That(colorPicker.Color).IsEqualTo(default(Color));
        await Assert.That(slider.Value).IsEqualTo(0);

        //Thumb should be in the bottom left corner
        Thumb saturationBrightnessPickerThumb = GetSaturationBrightnessPickerThumb(colorPicker);
        Canvas saturationBrightnessPicker = GetSaturationBrightnessPicker(colorPicker);
        await Assert.That(Canvas.GetLeft(saturationBrightnessPickerThumb)).IsEqualTo(0);
        await Assert.That(Canvas.GetTop(saturationBrightnessPickerThumb)).IsEqualTo(saturationBrightnessPicker.ActualHeight);
    }

    [Test]
    [Arguments(nameof(Colors.Green))]
    [Arguments(nameof(Colors.Red))]
    [Arguments(nameof(Colors.Blue))]
    [Arguments(nameof(Colors.Aqua))]
    [Arguments(nameof(Colors.Purple))]
    [Arguments(nameof(Colors.Pink))]
    [Arguments(nameof(Colors.Yellow))]
    [Arguments(nameof(Colors.Orange))]
    public async Task SettingTheColorUpdatesTheControls(string colorName)
    {
        ColorPicker colorPicker = CreateElement();
        Slider hueSlider = GetHueSlider(colorPicker);
        Canvas saturationBrightnessPicker = GetSaturationBrightnessPicker(colorPicker);
        Thumb saturationBrightnessPickerThumb = GetSaturationBrightnessPickerThumb(colorPicker);

        var converter = new ColorConverter();
        // ReSharper disable once PossibleNullReferenceException
        Color color = (Color)converter.ConvertFrom(colorName)!;
        var hsb = color.ToHsb();

        SetColor(colorPicker, color);

        await Assert.That(hueSlider.Value).IsEqualTo(hsb.Hue);
        var left = (saturationBrightnessPicker.ActualWidth) * hsb.Saturation;
        var top = ((1 - hsb.Brightness) * saturationBrightnessPicker.ActualHeight);
        await Assert.That(Canvas.GetLeft(saturationBrightnessPickerThumb)).IsEqualTo(left);
        await Assert.That(Canvas.GetTop(saturationBrightnessPickerThumb)).IsEqualTo(top);
    }

    [Test]
    public async Task SettingTheColorRaisesColorChangedEvent()
    {
        ColorPicker colorPicker = CreateElement();
        // capture variables
        Color oldValue = Colors.Transparent;
        Color newValue = Colors.Transparent;
        bool wasRaised = false;

        colorPicker.ColorChanged += (sender, args) =>
        {
            oldValue = args.OldValue;
            newValue = args.NewValue;
            wasRaised = true;
        };

        SetColor(colorPicker, Colors.Green);

        await Assert.That(oldValue).IsEqualTo(default(Color));
        await Assert.That(newValue).IsEqualTo(Colors.Green);
        await Assert.That(wasRaised).IsTrue();

        // reset capture variables
        oldValue = Colors.Transparent;
        newValue = Colors.Transparent;
        wasRaised = false;

        SetColor(colorPicker, Colors.Red);

        await Assert.That(oldValue).IsEqualTo(Colors.Green);
        await Assert.That(newValue).IsEqualTo(Colors.Red);
        await Assert.That(wasRaised).IsTrue();
    }

    [Test]
    public async Task DraggingTheHueSliderChangesHue()
    {
        ColorPicker colorPicker = CreateElement();
        Slider hueSlider = GetHueSlider(colorPicker);

        //This ensures we have some saturation and brightness
        SetColor(colorPicker, Colors.Red);

        Color lastColor = colorPicker.Color;
        while (hueSlider.Value < hueSlider.Maximum)
        {
            hueSlider.Value += hueSlider.LargeChange;
            await Assert.That(colorPicker.Color).IsNotEqualTo(lastColor);
            lastColor = colorPicker.Color;
        }
    }

    [Test]
    [Skip("This test never entered the while loop before the MTP conversion")]
    public async Task DraggingTheThumbChangesSaturation()
    {
        ColorPicker colorPicker = CreateElement();
        Canvas saturationBrightnessPicker = GetSaturationBrightnessPicker(colorPicker);
        Thumb saturationBrightnessPickerThumb = GetSaturationBrightnessPickerThumb(colorPicker);

        SetColor(colorPicker, Colors.Red);
        DragThumb(saturationBrightnessPickerThumb, horizontalOffset: -Canvas.GetLeft(saturationBrightnessPickerThumb));

        double lastSaturation = colorPicker.Color.ToHsb().Saturation;

        while (Canvas.GetLeft(saturationBrightnessPickerThumb) < saturationBrightnessPicker.ActualWidth)
        {
            DragThumb(saturationBrightnessPickerThumb, horizontalOffset: 10);
            double currentSaturation = colorPicker.Color.ToHsb().Saturation;
            await Assert.That(currentSaturation).IsGreaterThan(lastSaturation).Because($"At left {Canvas.GetLeft(saturationBrightnessPickerThumb)}, saturation {currentSaturation} is not grater than {lastSaturation}");
            lastSaturation = currentSaturation;
        }
    }

    private static void SetColor(ColorPicker colorPicker, Color color)
    {
        //The internal HsbProperty is set as affects arrange so this simulates that.
        colorPicker.Color = color;
        colorPicker.Arrange(new Rect(0, 0, colorPicker.ActualWidth, colorPicker.ActualHeight));
    }

    private static void DragThumb(Thumb thumb, double horizontalOffset = 0, double verticalOffset = 0)
    {
        thumb.RaiseEvent(new DragDeltaEventArgs(horizontalOffset, verticalOffset));
        Canvas.SetTop(thumb, Canvas.GetTop(thumb) + verticalOffset);
        Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + horizontalOffset);
    }
}
