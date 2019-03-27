using System;
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
    [TemplatePart(Name = HsbSLPickerPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = HsbSLPickerThumbPartName, Type = typeof(Thumb))]
    public class HsbColorPicker : Control
    {
        public const string HsbSelectedValuePartName = "PART_HsbSelectedValue";
        public const string HsbHueSliderPartName = "PART_HsbHueSlider";
        public const string HsbSLPickerPartName = "PART_HsbSBPicker";
        public const string HsbSLPickerThumbPartName = "PART_HsbSBPickerThumb";

        static HsbColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HsbColorPicker), new FrameworkPropertyMetadata(typeof(HsbColorPicker)));
        }

        private Thumb _slThumb;
        private Canvas _slPicker;

        private enum ChangeSource
        {
            None,
            Color,
            Hsb
        }

        private ChangeSource _changeSource;

        public static readonly DependencyProperty HueProperty = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(HueChangedCallback)));

        public double Hue
        {
            get { return (double)GetValue(HueProperty); } 
            set { SetValue(HueProperty, value); }
        }

        private static void HueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var colorPicker = (HsbColorPicker)d;
            colorPicker.SetHsbAndColor();
        }

        public static readonly DependencyProperty SaturationProperty = DependencyProperty.Register(nameof(Saturation), typeof(double), typeof(HsbColorPicker), 
            new FrameworkPropertyMetadata(new PropertyChangedCallback(SaturationPropertyChangedCallback)));

        public double Saturation
        {
            get { return (double)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        private static void SaturationPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var colorPicker = (HsbColorPicker)d;
            colorPicker.SetThumbLeft();
            colorPicker.SetHsbAndColor();
        }

        public static readonly DependencyProperty BrightnessProperty = DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(BrightnessPropertyChangedCallback)));

        public double Brightness
        {
            get { return (double)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }

        private static void BrightnessPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var colorPicker = (HsbColorPicker)d;
            colorPicker.SetThumbTop();
            colorPicker.SetHsbAndColor();
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(Color), typeof(Color), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(ColorPropertyChangedCallback)));

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void ColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var colorPicker = (HsbColorPicker)d;

            if (colorPicker._changeSource == ChangeSource.Hsb) return;
            colorPicker._changeSource = ChangeSource.Color;

            colorPicker.Hsb = colorPicker.Color.ToHsb();
            colorPicker.Hue = colorPicker.Hsb.H;
            colorPicker.Saturation = colorPicker.Hsb.S;
            colorPicker.Brightness = colorPicker.Hsb.B;

            colorPicker._changeSource = ChangeSource.None;
        }

        public static readonly DependencyProperty HsbProperty = DependencyProperty.Register(nameof(Hsb), typeof(Hsb), typeof(HsbColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(HsbPropertyChangedCallback)));

        public Hsb Hsb
        {
            get { return (Hsb)GetValue(HsbProperty); }
            set { SetValue(HsbProperty, value); }
        }

        private static void HsbPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue) return;
            var colorPicker = (HsbColorPicker)d;

            if (colorPicker._changeSource == ChangeSource.Color) return;
            colorPicker._changeSource = ChangeSource.Hsb;

            colorPicker.Color = colorPicker.Hsb.ToColor();
            colorPicker.Hue = colorPicker.Hsb.H;
            colorPicker.Saturation = colorPicker.Hsb.S;
            colorPicker.Brightness = colorPicker.Hsb.B;

            colorPicker._changeSource = ChangeSource.None;
        }

        private void SetHsbAndColor()
        {
            if (_changeSource != ChangeSource.None) return;
            var hsb = new Hsb(Hue, Saturation, Brightness);
            Hsb = hsb;
            Color = hsb.ToColor();
        }

        public override void OnApplyTemplate()
        {
            if (_slPicker != null) _slPicker.MouseDown -= _slPicker_MouseDown;
            _slPicker = GetTemplateChild(HsbSLPickerPartName) as Canvas;
            if (_slPicker != null)
            {
                _slPicker.Focusable = true;
                _slPicker.Background = Brushes.Transparent;
                _slPicker.MouseDown += _slPicker_MouseDown;
                _slPicker.MouseMove += _slPicker_MouseMove;
                _slPicker.MouseUp += _slPicker_MouseUp;
            }

            if (_slThumb != null) _slThumb.DragDelta -= _slThumb_DragDelta;
            _slThumb = GetTemplateChild(HsbSLPickerThumbPartName) as Thumb;
            if (_slThumb != null) _slThumb.DragDelta += _slThumb_DragDelta;

            base.OnApplyTemplate();
        }

        private void _slPicker_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _slThumb.CaptureMouse();
        }

        private void _slPicker_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var element = (UIElement)sender;
                var position = e.GetPosition(_slPicker);
                ApplyThumbPostion(position.X, position.Y);
            }
        }

        private void _slPicker_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _slThumb.ReleaseMouseCapture();
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);
            SetThumbLeft();
            SetThumbTop();
            return result;
        }

        private void _slThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = (UIElement)e.Source;

            var left = Canvas.GetLeft(thumb) + e.HorizontalChange;
            var top = Canvas.GetTop(thumb) + e.VerticalChange;
            ApplyThumbPostion(left, top);
        }

        private void ApplyThumbPostion(double left, double top)
        {
            if (left < 0) left = 0;
            if (top < 0) top = 0;

            if (left > _slPicker.ActualWidth) left = _slPicker.ActualWidth;
            if (top > _slPicker.ActualHeight) top = _slPicker.ActualHeight;

            Saturation = 1 / (_slPicker.ActualWidth / left);
            Brightness = 1 - (top / _slPicker.ActualHeight);
        }

        private void SetThumbLeft()
        {
            if (_slPicker == null) return;
            var left = (_slPicker.ActualWidth) / (1 / Saturation);
            Canvas.SetLeft(_slThumb, left);
        }

        private void SetThumbTop()
        {
            if (_slPicker == null) return;
            var top = ((1 - Brightness) * _slPicker.ActualHeight);
            Canvas.SetTop(_slThumb, top);
        }
    }
}
