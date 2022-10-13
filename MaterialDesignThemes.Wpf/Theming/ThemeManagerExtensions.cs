using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Theming
{
    public static class ThemeManagerExtensions
    {
        public static void ChangePrimaryColor(this ThemeManager themeManager, Color color)
        {
            Theme theme = themeManager.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            themeManager.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this ThemeManager themeManager, Color color)
        {
            Theme theme = themeManager.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten());
            theme.SecondaryMid = new ColorPair(color);
            theme.SecondaryDark = new ColorPair(color.Darken());

            themeManager.SetTheme(theme);
        }
    }
}
