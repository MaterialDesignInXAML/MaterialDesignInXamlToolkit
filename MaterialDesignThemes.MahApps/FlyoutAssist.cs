using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps;

public static class FlyoutAssist
{
    public static readonly DependencyProperty HeaderColorModeProperty = DependencyProperty.RegisterAttached(
        "HeaderColorMode", typeof(ColorZoneMode), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(ColorZoneMode), FrameworkPropertyMetadataOptions.Inherits));

    public static void SetHeaderColorMode(DependencyObject element, ColorZoneMode value)
        => element.SetValue(HeaderColorModeProperty, value);

    public static ColorZoneMode GetHeaderColorMode(DependencyObject element)
        => (ColorZoneMode)element.GetValue(HeaderColorModeProperty);
    
    public static readonly DependencyProperty HeaderElevationProperty = DependencyProperty.RegisterAttached(
        "HeaderElevation", typeof(Elevation), typeof(FlyoutAssist), new FrameworkPropertyMetadata(default(Elevation), FrameworkPropertyMetadataOptions.Inherits));

    public static void SetHeaderElevation(DependencyObject element, Elevation value)
        => element.SetValue(HeaderElevationProperty, value);

    public static Elevation GetHeaderElevation(DependencyObject element)
        => (Elevation)element.GetValue(HeaderElevationProperty);
}
