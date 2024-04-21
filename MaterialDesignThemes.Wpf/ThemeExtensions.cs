using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf;

public static partial class ThemeExtensions
{
    internal static ColorPair ToPairedColor(this Hue hue)
        => new(hue.Color, hue.Foreground);

    internal static void SetPalette(this Theme theme, Palette palette)
    {
        List<Hue> allHues = palette.PrimarySwatch.PrimaryHues.ToList();

        Hue lightHue = allHues[palette.PrimaryLightHueIndex];
        Hue midHue = allHues[palette.PrimaryMidHueIndex];
        Hue darkHue = allHues[palette.PrimaryDarkHueIndex];

        theme.PrimaryLight = lightHue.ToPairedColor();
        theme.PrimaryMid = midHue.ToPairedColor();
        theme.PrimaryDark = darkHue.ToPairedColor();
    }

    public static BaseTheme GetBaseTheme(this Theme theme)
    {
        if (theme is null) throw new ArgumentNullException(nameof(theme));

        var foreground = theme.Background.Color?.ContrastingForegroundColor();
        return foreground == Colors.White ? BaseTheme.Dark : BaseTheme.Light;
    }

    public static void SetBaseTheme(this Theme theme, BaseTheme baseTheme)
    {
        switch (baseTheme)
        {
            case BaseTheme.Light:
                SetLightTheme(theme);
                break;
            case BaseTheme.Dark:
                SetDarkTheme(theme);
                break;
            default:
                switch (Theme.GetSystemTheme())
                {
                    case BaseTheme.Dark:
                        SetDarkTheme(theme);
                        break;
                    default:
                        SetLightTheme(theme);
                        break;
                }
                break;
        }
    }

    public static partial void SetLightTheme(this Theme theme);
    public static partial void SetDarkTheme(this Theme theme);

    public static void SetPrimaryColor(this Theme theme, Color primaryColor)
    {
        if (theme is null) throw new ArgumentNullException(nameof(theme));

        theme.PrimaryLight = primaryColor.Lighten();
        theme.PrimaryMid = primaryColor;
        theme.PrimaryDark = primaryColor.Darken();
    }

    public static void SetSecondaryColor(this Theme theme, Color secondaryColor)
    {
        if (theme is null) throw new ArgumentNullException(nameof(theme));
        theme.SecondaryLight = secondaryColor.Lighten();
        theme.SecondaryMid = secondaryColor;
        theme.SecondaryDark = secondaryColor.Darken();
    }
}
