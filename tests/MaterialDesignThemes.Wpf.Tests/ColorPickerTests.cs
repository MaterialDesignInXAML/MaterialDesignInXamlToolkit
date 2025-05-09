using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Tests;

//TODO: Many of these tests could be moved over to MaterialDesignThemes.UITests
public class ColorPickerTests
{
    private readonly ColorPicker _colorPicker;
    private readonly Slider _hueSlider;
    private readonly Canvas _saturationBrightnessPicker;
    private readonly Thumb _saturationBrightnessPickerThumb;

    public ColorPickerTests()
    {
        _colorPicker = new ColorPicker();
        _colorPicker.ApplyDefaultStyle();
        _colorPicker.Arrange(new Rect(0, 0, 400, 100));

        _hueSlider = _colorPicker.FindVisualChild<Slider>(ColorPicker.HueSliderPartName);
        _saturationBrightnessPicker = _colorPicker.FindVisualChild<Canvas>(ColorPicker.SaturationBrightnessPickerPartName);
        _saturationBrightnessPickerThumb = _colorPicker.FindVisualChild<Thumb>(ColorPicker.SaturationBrightnessPickerThumbPartName);
    }

    [Test, STAThreadExecutor]
    public async Task ColorPickerDefaultsToDefaultColor()
    {
        await Assert.That(_colorPicker.Color).IsEqualTo(default);
        await Assert.That(_hueSlider.Value).IsEqualTo(0);

        //Thumb should be in the bottom left corner
        await Assert.That(Canvas.GetLeft(_saturationBrightnessPickerThumb)).IsEqualTo(0);
        await Assert.That(Canvas.GetTop(_saturationBrightnessPickerThumb)).IsEqualTo(_saturationBrightnessPicker.ActualHeight);
    }

    [Test, STAThreadExecutor]
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
        var converter = new ColorConverter();
        // ReSharper disable once PossibleNullReferenceException
        Color color = (Color)converter.ConvertFrom(colorName)!;
        var hsb = color.ToHsb();

        SetColor(color);

        await Assert.That(_hueSlider.Value).IsEqualTo(hsb.Hue);
        var left = (_saturationBrightnessPicker.ActualWidth) / (1 / hsb.Saturation);
        var top = ((1 - hsb.Brightness) * _saturationBrightnessPicker.ActualHeight);
        await Assert.That(Canvas.GetLeft(_saturationBrightnessPickerThumb)).IsEqualTo(left);
        await Assert.That(Canvas.GetTop(_saturationBrightnessPickerThumb)).IsEqualTo(top);
    }

    [Test, STAThreadExecutor]
    public async Task SettingTheColorRaisesColorChangedEvent()
    {
        // capture variables
        Color oldValue = Colors.Transparent;
        Color newValue = Colors.Transparent;
        bool wasRaised = false;

        _colorPicker.ColorChanged += (sender, args) =>
        {
            oldValue = args.OldValue;
            newValue = args.NewValue;
            wasRaised = true;
        };

        SetColor(Colors.Green);

        await Assert.That(oldValue).IsEqualTo(default(Color));
        await Assert.That(newValue).IsEqualTo(Colors.Green);
        await Assert.That(wasRaised).IsTrue();

        // reset capture variables
        oldValue = Colors.Transparent;
        newValue = Colors.Transparent;
        wasRaised = true;

        SetColor(Colors.Red);

        await Assert.That(oldValue).IsEqualTo(Colors.Green);
        await Assert.That(newValue).IsEqualTo(Colors.Red);
        await Assert.That(wasRaised).IsTrue();
    }

    [Test, STAThreadExecutor]
    public async Task DraggingTheHueSliderChangesHue()
    {
        //This ensures we have some saturation and brightness
        SetColor(Colors.Red);

        Color lastColor = _colorPicker.Color;
        while (_hueSlider.Value < _hueSlider.Maximum)
        {
            _hueSlider.Value += _hueSlider.LargeChange;
            await Assert.That(_colorPicker.Color).IsNotEqualTo(lastColor);
            lastColor = _colorPicker.Color;
        }
    }

    [Test, STAThreadExecutor]
    public async Task DraggingTheThumbChangesSaturation()
    {
        SetColor(Colors.Red);
        DragThumb(horizontalOffset: -Canvas.GetLeft(_saturationBrightnessPickerThumb));

        double lastSaturation = _colorPicker.Color.ToHsb().Saturation;

        while (Canvas.GetLeft(_saturationBrightnessPicker) < _saturationBrightnessPicker.ActualWidth)
        {
            DragThumb(horizontalOffset: 10);
            double currentSaturation = _colorPicker.Color.ToHsb().Saturation;
            await Assert.That(currentSaturation).IsGreaterThan(lastSaturation).Because($"At left {Canvas.GetLeft(_saturationBrightnessPicker)}, saturation {currentSaturation} is not grater than {lastSaturation}");
        }
    }

    private void SetColor(Color color)
    {
        //The internal HsbProperty is set as affects arrange so this simulates that.
        _colorPicker.Color = color;
        _colorPicker.Arrange(new Rect(0, 0, _colorPicker.ActualWidth, _colorPicker.ActualHeight));
    }

    private void DragThumb(double horizontalOffset = 0, double verticalOffset = 0)
    {
        _saturationBrightnessPickerThumb.RaiseEvent(new DragDeltaEventArgs(horizontalOffset, verticalOffset));

        Canvas.SetTop(_saturationBrightnessPickerThumb, Canvas.GetTop(_saturationBrightnessPickerThumb) + verticalOffset);
        Canvas.SetLeft(_saturationBrightnessPickerThumb, Canvas.GetLeft(_saturationBrightnessPickerThumb) + horizontalOffset);
    }
}
