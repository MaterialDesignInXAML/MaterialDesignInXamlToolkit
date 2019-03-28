using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = HsbSelectedValuePartName, Type = typeof(Grid))]
    [TemplatePart(Name = HsbHueSliderPartName, Type = typeof(Slider))]
    [TemplatePart(Name = HsbSBPickerPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = HsbSBPickerThumbPartName, Type = typeof(Thumb))]
    public class HsbColorPicker : Control
    {
        //TODO: This appears un-used
        public const string HsbSelectedValuePartName = "PART_HsbSelectedValue";

        public const string HsbHueSliderPartName = "PART_HsbHueSlider";
        public const string HsbSBPickerPartName = "PART_HsbSBPicker";
        public const string HsbSBPickerThumbPartName = "PART_HsbSBPickerThumb";

        static HsbColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HsbColorPicker), new FrameworkPropertyMetadata(typeof(HsbColorPicker)));
        }

        private Thumb _sbThumb;
        private Canvas _sbPicker;
        private Slider _hueSlider;

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChangedCallback));

        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = (HsbColorPicker)d;
            if (colorPicker._inCallback)
            {
                return;
            }
            
            colorPicker._inCallback = true;
            colorPicker.SetCurrentValue(HsbProperty, ((Color)e.NewValue).ToHsb());
            colorPicker._inCallback = false;
        }

        public static readonly DependencyProperty HsbProperty = DependencyProperty.Register(nameof(Hsb), typeof(Hsb), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HsbPropertyChangedCallback));

        public Hsb Hsb
        {
            get => (Hsb)GetValue(HsbProperty);
            set => SetValue(HsbProperty, value);
        }

        private bool _inCallback = false;
        private static void HsbPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var colorPicker = (HsbColorPicker)d;
            if (colorPicker._inCallback)
            {
                return;
            }

            colorPicker._inCallback = true;
            
            colorPicker.SetCurrentValue(ColorProperty, ((e.NewValue as Hsb)?.ToColor() ?? default(Color)));

            colorPicker._inCallback = false;
        }

        public override void OnApplyTemplate()
        {
            if (_sbPicker != null)
            {
                _sbPicker.MouseDown -= SbPickerMouseDown;
                _sbPicker.MouseMove -= SbPickerMouseMove;
                _sbPicker.MouseUp -= SbPickerMouseUp;
            }
            _sbPicker = GetTemplateChild(HsbSBPickerPartName) as Canvas;
            if (_sbPicker != null)
            {
                _sbPicker.Focusable = true;
                _sbPicker.Background = Brushes.Transparent;
                _sbPicker.MouseDown += SbPickerMouseDown;
                _sbPicker.MouseMove += SbPickerMouseMove;
                _sbPicker.MouseUp += SbPickerMouseUp;
            }

            if (_sbThumb != null) _sbThumb.DragDelta -= SbThumbDragDelta;
            _sbThumb = GetTemplateChild(HsbSBPickerThumbPartName) as Thumb;
            if (_sbThumb != null) _sbThumb.DragDelta += SbThumbDragDelta;

            if (_hueSlider != null)
            {
                _hueSlider.ValueChanged -= HueSliderOnValueChanged;
            }
            _hueSlider = GetTemplateChild(HsbHueSliderPartName) as Slider;
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

        private void SbPickerMouseDown(object sender, MouseButtonEventArgs e)
        {
            _sbThumb.CaptureMouse();
        }

        private void SbPickerMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var position = e.GetPosition(_sbPicker);
                ApplyThumbPosition(position.X, position.Y);
            }
        }

        private void SbPickerMouseUp(object sender, MouseButtonEventArgs e)
        {
            _sbThumb.ReleaseMouseCapture();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);
            SetThumbLeft();
            SetThumbTop();
            return result;
        }

        private void SbThumbDragDelta(object sender, DragDeltaEventArgs e)
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

            if (left > _sbPicker.ActualWidth) left = _sbPicker.ActualWidth;
            if (top > _sbPicker.ActualHeight) top = _sbPicker.ActualHeight;

            var saturation = 1 / (_sbPicker.ActualWidth / left);
            var brightness = 1 - (top / _sbPicker.ActualHeight);

            SetCurrentValue(HsbProperty, new Hsb(Hsb.H, saturation, brightness));
        }

        private void SetThumbLeft()
        {
            if (_sbPicker == null || Hsb == null) return;
            var left = (_sbPicker.ActualWidth) / (1 / Hsb.S);
            Canvas.SetLeft(_sbThumb, left);
        }

        private void SetThumbTop()
        {
            if (_sbPicker == null || Hsb == null) return;
            var top = ((1 - Hsb.B) * _sbPicker.ActualHeight);
            Canvas.SetTop(_sbThumb, top);
        }
    }
}
