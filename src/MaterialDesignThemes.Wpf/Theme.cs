using System.Diagnostics;
using System.Windows.Media;
using MaterialDesignColors;
using Microsoft.Win32;

namespace MaterialDesignThemes.Wpf;

public partial class Theme
{
    /// <summary>
    /// Get the current Windows theme.
    /// Based on ControlzEx
    /// https://github.com/ControlzEx/ControlzEx/blob/48230bb023c588e1b7eb86ea83f7ddf7d25be735/src/ControlzEx/Theming/WindowsThemeHelper.cs#L19
    /// </summary>
    /// <returns></returns>
    public static BaseTheme? GetSystemTheme()
    {
        try
        {
            var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", null);

            if (registryValue is null)
            {
                return null;
            }

            return Convert.ToBoolean(registryValue) ? BaseTheme.Light : BaseTheme.Dark;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Get the current Windows accent color.
    /// Based on ControlzEx
    /// https://github.com/ControlzEx/ControlzEx/blob/48230bb023c588e1b7eb86ea83f7ddf7d25be735/src/ControlzEx/Theming/WindowsThemeHelper.cs#L53
    /// </summary>
    /// <returns></returns>
    public static Color? GetSystemAccentColor()
    {
        Color? accentColor = null;

        try
        {
            var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM", "ColorizationColor", null);

            if (registryValue is null)
            {
                return null;
            }

            // We get negative values out of the registry, so we have to cast to int from object first.
            // Casting from int to uint works afterwards and converts the number correctly.
            var pp = (uint)(int)registryValue;
            if (pp > 0)
            {
                var bytes = BitConverter.GetBytes(pp);
                accentColor = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
            }

            return accentColor;
        }
        catch (Exception exception)
        {
            Trace.TraceError(exception.ToString());
        }

        return accentColor;
    }

    public static Theme Create(BaseTheme baseTheme, Color primary, Color secondary)
    {
        Theme theme = new();

        theme.SetBaseTheme(baseTheme);
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(secondary);

        return theme;
    }

    public ColorAdjustment? ColorAdjustment { get; set; }

    public ColorPair SecondaryLight { get; set; }
    public ColorPair SecondaryMid { get; set; }
    public ColorPair SecondaryDark { get; set; }

    public ColorPair PrimaryLight { get; set; }
    public ColorPair PrimaryMid { get; set; }
    public ColorPair PrimaryDark { get; set; }

    internal ColorReference Resolve(ColorReference colorReference)
    {
        return colorReference.ThemeReference switch
        {
            ThemeColorReference.SecondaryLight => new ColorReference(colorReference.ThemeReference, SecondaryLight.Color),
            ThemeColorReference.SecondaryMid => new ColorReference(colorReference.ThemeReference, SecondaryMid.Color),
            ThemeColorReference.SecondaryDark => new ColorReference(colorReference.ThemeReference, SecondaryDark.Color),
            ThemeColorReference.PrimaryLight => new ColorReference(colorReference.ThemeReference, PrimaryLight.Color),
            ThemeColorReference.PrimaryMid => new ColorReference(colorReference.ThemeReference, PrimaryMid.Color),
            ThemeColorReference.PrimaryDark => new ColorReference(colorReference.ThemeReference, PrimaryDark.Color),
            _ => colorReference
        };
    }
}
