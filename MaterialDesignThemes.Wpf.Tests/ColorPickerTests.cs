using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{

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

        [StaFact]
        public void ColorPickerDefaultsToDefaultColor()
        {
            Assert.Equal(default, _colorPicker.Color);
            Assert.Equal(0, _hueSlider.Value);

            //Thumb should be in the bottom left corner
            Assert.Equal(0, Canvas.GetLeft(_saturationBrightnessPickerThumb));
            Assert.Equal(_saturationBrightnessPicker.ActualHeight, Canvas.GetTop(_saturationBrightnessPickerThumb));
        }

        [StaTheory]
        [InlineData(nameof(Colors.Green))]
        [InlineData(nameof(Colors.Red))]
        [InlineData(nameof(Colors.Blue))]
        [InlineData(nameof(Colors.Aqua))]
        [InlineData(nameof(Colors.Purple))]
        [InlineData(nameof(Colors.Pink))]
        [InlineData(nameof(Colors.Yellow))]
        [InlineData(nameof(Colors.Orange))]
        public void SettingTheColorUpdatesTheControls(string colorName)
        {
            var converter = new ColorConverter();
            // ReSharper disable once PossibleNullReferenceException
            Color color = (Color)converter.ConvertFrom(colorName)!;
            var hsb = color.ToHsb();

            SetColor(color);

            Assert.Equal(hsb.Hue, _hueSlider.Value);
            var left = (_saturationBrightnessPicker.ActualWidth) / (1 / hsb.Saturation);
            var top = ((1 - hsb.Brightness) * _saturationBrightnessPicker.ActualHeight);
            Assert.Equal(left, Canvas.GetLeft(_saturationBrightnessPickerThumb));
            Assert.Equal(top, Canvas.GetTop(_saturationBrightnessPickerThumb));
        }

        [StaFact]
        public void SettingTheColorRaisesColorChangedEvent()
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

            Assert.Equal(default(Color), oldValue);
            Assert.Equal(Colors.Green, newValue);
            Assert.True(wasRaised);

            // reset capture variables
            oldValue = Colors.Transparent;
            newValue = Colors.Transparent;
            wasRaised = true;

            SetColor(Colors.Red);

            Assert.Equal(Colors.Green, oldValue);
            Assert.Equal(Colors.Red, newValue);
            Assert.True(wasRaised);
        }

        [StaFact]
        public void DraggingTheHueSliderChangesHue()
        {
            //This ensures we have some saturation and brightness
            SetColor(Colors.Red);

            Color lastColor = _colorPicker.Color;
            while (_hueSlider.Value < _hueSlider.Maximum)
            {
                _hueSlider.Value += _hueSlider.LargeChange;
                Assert.NotEqual(lastColor, _colorPicker.Color);
                lastColor = _colorPicker.Color;
            }
        }

        [StaFact]
        public void DraggingTheThumbChangesBrightness()
        {
            SetColor(Colors.Red);
            DragThumb(verticalOffset: -Canvas.GetTop(_saturationBrightnessPickerThumb));

            double lastBrightness = _colorPicker.Color.ToHsb().Brightness;

            while (Canvas.GetTop(_saturationBrightnessPickerThumb) < _saturationBrightnessPicker.ActualHeight)
            {
                DragThumb(verticalOffset: 10);
                double currentBrightness = _colorPicker.Color.ToHsb().Brightness;
                Assert.True(currentBrightness < lastBrightness, $"At top {Canvas.GetTop(_saturationBrightnessPicker)}, brightness {currentBrightness} is not less than {lastBrightness}");
            }
        }

        [StaFact]
        public void DraggingTheThumbChangesSaturation()
        {
            SetColor(Colors.Red);
            DragThumb(horizontalOffset: -Canvas.GetLeft(_saturationBrightnessPickerThumb));

            double lastSaturation = _colorPicker.Color.ToHsb().Saturation;

            while (Canvas.GetLeft(_saturationBrightnessPicker) < _saturationBrightnessPicker.ActualWidth)
            {
                DragThumb(horizontalOffset: 10);
                double currentSaturation = _colorPicker.Color.ToHsb().Saturation;
                Assert.True(currentSaturation > lastSaturation, $"At left {Canvas.GetLeft(_saturationBrightnessPicker)}, saturation {currentSaturation} is not grater than {lastSaturation}");
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
}