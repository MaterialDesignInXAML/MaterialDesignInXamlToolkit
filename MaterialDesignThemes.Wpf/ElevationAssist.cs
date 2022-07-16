using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf;

public enum Elevation
{
    Dp0,
    Dp1,
    Dp2,
    Dp3,
    Dp4,
    Dp5,
    Dp6,
    Dp7,
    Dp8,
    Dp12,
    Dp16,
    Dp24
}

public static class ElevationAssist
{
    private static readonly IDictionary<Elevation, DropShadowEffect?> ShadowsDictionary;

    public static readonly DependencyProperty ElevationProperty = DependencyProperty.RegisterAttached(
        "Elevation",
        typeof(Elevation),
        typeof(ElevationAssist),
        new FrameworkPropertyMetadata(default(Elevation), FrameworkPropertyMetadataOptions.AffectsRender));

    static ElevationAssist()
    {
        const string shadowsUri = "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Shadows.xaml";
        var resourceDictionary = new ResourceDictionary { Source = new Uri(shadowsUri, UriKind.Absolute) };

        ShadowsDictionary = new Dictionary<Elevation, DropShadowEffect?>
        {
            { Elevation.Dp0, null },
            { Elevation.Dp1, resourceDictionary["MaterialDesignElevationShadow1"] as DropShadowEffect },
            { Elevation.Dp2, resourceDictionary["MaterialDesignElevationShadow2"] as DropShadowEffect },
            { Elevation.Dp3, resourceDictionary["MaterialDesignElevationShadow3"] as DropShadowEffect },
            { Elevation.Dp4, resourceDictionary["MaterialDesignElevationShadow4"] as DropShadowEffect },
            { Elevation.Dp5, resourceDictionary["MaterialDesignElevationShadow5"] as DropShadowEffect },
            { Elevation.Dp6, resourceDictionary["MaterialDesignElevationShadow6"] as DropShadowEffect },
            { Elevation.Dp7, resourceDictionary["MaterialDesignElevationShadow7"] as DropShadowEffect },
            { Elevation.Dp8, resourceDictionary["MaterialDesignElevationShadow8"] as DropShadowEffect },
            { Elevation.Dp12, resourceDictionary["MaterialDesignElevationShadow12"] as DropShadowEffect },
            { Elevation.Dp16, resourceDictionary["MaterialDesignElevationShadow16"] as DropShadowEffect },
            { Elevation.Dp24, resourceDictionary["MaterialDesignElevationShadow24"] as DropShadowEffect }
        };
    }

    public static void SetElevation(DependencyObject element, Elevation value) => element.SetValue(ElevationProperty, value);
    public static Elevation GetElevation(DependencyObject element) => (Elevation)element.GetValue(ElevationProperty);

    public static DropShadowEffect? GetDropShadow(Elevation elevation) => ShadowsDictionary[elevation];
}
