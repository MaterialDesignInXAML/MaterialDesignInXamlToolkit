using System.Windows;

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
    }
}
