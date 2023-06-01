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

    public static Theme Create(BaseTheme baseTheme, Color primary, Color accent)
    {
        Theme theme = new();

        theme.SetBaseTheme(baseTheme);
        theme.SetPrimaryColor(primary);
        theme.SetSecondaryColor(accent);

        return theme;
    }

    public ColorAdjustment? ColorAdjustment { get; set; }

    public ColorPair SecondaryLight { get; set; }
    public ColorPair SecondaryMid { get; set; }
    public ColorPair SecondaryDark { get; set; }

    public ColorPair PrimaryLight { get; set; }
    public ColorPair PrimaryMid { get; set; }
    public ColorPair PrimaryDark { get; set; }
}
