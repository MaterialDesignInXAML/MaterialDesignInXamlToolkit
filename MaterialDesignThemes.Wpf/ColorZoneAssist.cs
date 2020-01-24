using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ColorZoneAssist
    {
        public static readonly DependencyProperty ModeProperty = DependencyProperty.RegisterAttached(
            "Mode", typeof(ColorZoneMode), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(ColorZoneMode), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMode(DependencyObject element, ColorZoneMode value)
        {
            element.SetValue(ModeProperty, value);
        }

        public static ColorZoneMode GetMode(DependencyObject element)
        {
            return (ColorZoneMode)element.GetValue(ModeProperty);
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.RegisterAttached(
            "Background", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetBackground(DependencyObject element, Brush value)
        {
            element.SetValue(BackgroundProperty, value);
        }

        public static Brush GetBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(BackgroundProperty);
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.RegisterAttached(
            "Foreground", typeof(Brush), typeof(ColorZoneAssist), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetForeground(DependencyObject element, Brush value)
        {
            element.SetValue(ForegroundProperty, value);
        }

        public static Brush GetForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(ForegroundProperty);
        }
    }
}
