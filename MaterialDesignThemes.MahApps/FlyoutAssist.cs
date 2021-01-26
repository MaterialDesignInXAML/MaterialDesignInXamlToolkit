using System.Windows;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public static class FlyoutAssist
    {
        public static readonly DependencyProperty HeaderColorModeProperty = DependencyProperty.RegisterAttached(
            "HeaderColorMode", typeof(ColorZoneMode), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(ColorZoneMode), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHeaderColorMode(DependencyObject element, ColorZoneMode value)
            => element.SetValue(HeaderColorModeProperty, value);

        public static ColorZoneMode GetHeaderColorMode(DependencyObject element)
            => (ColorZoneMode)element.GetValue(HeaderColorModeProperty);

        public static readonly DependencyProperty HeaderShadowDepthProperty = DependencyProperty.RegisterAttached(
            "HeaderShadowDepth", typeof(ShadowDepth), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHeaderShadowDepth(DependencyObject element, ShadowDepth value)
            => element.SetValue(HeaderShadowDepthProperty, value);

        public static ShadowDepth GetHeaderShadowDepth(DependencyObject element)
            => (ShadowDepth)element.GetValue(HeaderShadowDepthProperty);
    }
}
