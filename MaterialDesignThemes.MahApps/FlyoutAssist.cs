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

        [Obsolete("Use FlyoutAssist.HeaderElevationProperty instead")]
        public static readonly DependencyProperty HeaderShadowDepthProperty = DependencyProperty.RegisterAttached(
            "HeaderShadowDepth", typeof(ShadowDepth), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.Inherits, OnHeaderShadowDepthPropertyChanged));

        [Obsolete]
        private static void OnHeaderShadowDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            static Elevation GetElevation(ShadowDepth depth) => depth switch
            {
                ShadowDepth.Depth0 => Elevation.Dp0,
                ShadowDepth.Depth1 => Elevation.Dp2,
                ShadowDepth.Depth2 => Elevation.Dp4,
                ShadowDepth.Depth3 => Elevation.Dp8,
                ShadowDepth.Depth4 => Elevation.Dp12,
                ShadowDepth.Depth5 => Elevation.Dp24,
                _ => throw new ArgumentOutOfRangeException(nameof(depth), depth, null)
            };

            if (e.NewValue is ShadowDepth depth)
            {
                d.SetValue(HeaderElevationProperty, GetElevation(depth));
            }
        }

        [Obsolete("Use FlyoutAssist.SetHeaderElevation instead")]
        public static void SetHeaderShadowDepth(DependencyObject element, ShadowDepth value)
            => element.SetValue(HeaderShadowDepthProperty, value);

        [Obsolete("Use FlyoutAssist.GetHeaderElevation instead")]
        public static ShadowDepth GetHeaderShadowDepth(DependencyObject element)
            => (ShadowDepth)element.GetValue(HeaderShadowDepthProperty);

        public static readonly DependencyProperty HeaderElevationProperty = DependencyProperty.RegisterAttached(
            "HeaderElevation", typeof(Elevation), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(Elevation), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHeaderElevation(DependencyObject element, Elevation value)
            => element.SetValue(HeaderElevationProperty, value);

        public static Elevation GetHeaderElevation(DependencyObject element)
            => (Elevation)element.GetValue(HeaderElevationProperty);
    }
}
