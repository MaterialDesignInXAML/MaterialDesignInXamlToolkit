using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = HueSliderPartName, Type = typeof(Slider))]
    [TemplatePart(Name = SaturationBrightnessPickerPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = SaturationBrightnessPickerThumbPartName, Type = typeof(Thumb))]
    public class ColorPicker : Control
    {
        public const string HueSliderPartName = "PART_HsbHueSlider";
        public const string SaturationBrightnessPickerPartName = "PART_HsbSBPicker";
        public const string SaturationBrightnessPickerThumbPartName = "PART_HsbSBPickerThumb";

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }

        private Thumb _saturationBrightnessThumb;
        private Canvas _saturationBrightnessCanvas;
        private Slider _hueSlider;

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker),
            new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChangedCallback));

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)d;
            if (colorPicker._inCallback)
            {
                return;
            }
            
            colorPicker._inCallback = true;
            colorPicker.SetCurrentValue(HsbProperty, ((Color)e.NewValue).ToHsb());
            colorPicker._inCallback = false;
        }

        internal static readonly DependencyProperty HsbProperty = DependencyProperty.Register(nameof(Hsb), typeof(Hsb), typeof(ColorPicker),
            new FrameworkPropertyMetadata(default(Hsb), FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HsbPropertyChangedCallback));

        internal Hsb Hsb
        {
            get => (Hsb)GetValue(HsbProperty);
            set => SetValue(HsbProperty, value);
        }

        private bool _inCallback;
        private static void HsbPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (ColorPicker)d;
            if (colorPicker._inCallback)
            {
                return;
            }

            colorPicker._inCallback = true;

            var color = default(Color);
            if (e.NewValue is Hsb hsb)
            {
                color = hsb.ToColor();
            }
            colorPicker.SetCurrentValue(ColorProperty, color);

            colorPicker._inCallback = false;
        }

        public override void OnApplyTemplate()
        {
            if (_saturationBrightnessCanvas != null)
            {
                _saturationBrightnessCanvas.MouseDown -= SaturationBrightnessCanvasMouseDown;
                _saturationBrightnessCanvas.MouseMove -= SaturationBrightnessCanvasMouseMove;
                _saturationBrightnessCanvas.MouseUp -= SaturationBrightnessCanvasMouseUp;
            }
            _saturationBrightnessCanvas = GetTemplateChild(SaturationBrightnessPickerPartName) as Canvas;
            if (_saturationBrightnessCanvas != null)
            {
                _saturationBrightnessCanvas.Focusable = true;
                _saturationBrightnessCanvas.Background = Brushes.Transparent;
                _saturationBrightnessCanvas.MouseDown += SaturationBrightnessCanvasMouseDown;
                _saturationBrightnessCanvas.MouseMove += SaturationBrightnessCanvasMouseMove;
                _saturationBrightnessCanvas.MouseUp += SaturationBrightnessCanvasMouseUp;
            }

            if (_saturationBrightnessThumb != null) _saturationBrightnessThumb.DragDelta -= SaturationBrightnessThumbDragDelta;
            _saturationBrightnessThumb = GetTemplateChild(SaturationBrightnessPickerThumbPartName) as Thumb;
            if (_saturationBrightnessThumb != null) _saturationBrightnessThumb.DragDelta += SaturationBrightnessThumbDragDelta;

            if (_hueSlider != null)
            {
                _hueSlider.ValueChanged -= HueSliderOnValueChanged;
            }
            _hueSlider = GetTemplateChild(HueSliderPartName) as Slider;
            if (_hueSlider != null)
            {
                _hueSlider.ValueChanged += HueSliderOnValueChanged;
            }

            base.OnApplyTemplate();
        }

        private void HueSliderOnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Hsb is Hsb hsb)
            {
                Hsb = new Hsb(e.NewValue, hsb.S, hsb.B);
            }
        }

        private void SaturationBrightnessCanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            _saturationBrightnessThumb.CaptureMouse();
        }

        private void SaturationBrightnessCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var position = e.GetPosition(_saturationBrightnessCanvas);
                ApplyThumbPosition(position.X, position.Y);
            }
        }

        private void SaturationBrightnessCanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            _saturationBrightnessThumb.ReleaseMouseCapture();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);
            SetThumbLeft();
            SetThumbTop();
            return result;
        }

        private void SaturationBrightnessThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = (UIElement)e.Source;

            var left = Canvas.GetLeft(thumb) + e.HorizontalChange;
            var top = Canvas.GetTop(thumb) + e.VerticalChange;
            ApplyThumbPosition(left, top);
        }

        private void ApplyThumbPosition(double left, double top)
        {
            if (left < 0) left = 0;
            if (top < 0) top = 0;

            if (left > _saturationBrightnessCanvas.ActualWidth) left = _saturationBrightnessCanvas.ActualWidth;
            if (top > _saturationBrightnessCanvas.ActualHeight) top = _saturationBrightnessCanvas.ActualHeight;

            var saturation = 1 / (_saturationBrightnessCanvas.ActualWidth / left);
            var brightness = 1 - (top / _saturationBrightnessCanvas.ActualHeight);

            SetCurrentValue(HsbProperty, new Hsb(Hsb.H, saturation, brightness));
        }

        private void SetThumbLeft()
        {
            if (_saturationBrightnessCanvas == null) return;
            var left = (_saturationBrightnessCanvas.ActualWidth) / (1 / Hsb.S);
            Canvas.SetLeft(_saturationBrightnessThumb, left);
        }

        private void SetThumbTop()
        {
            if (_saturationBrightnessCanvas == null) return;
            var top = ((1 - Hsb.B) * _saturationBrightnessCanvas.ActualHeight);
            Canvas.SetTop(_saturationBrightnessThumb, top);
        }
    }
}
