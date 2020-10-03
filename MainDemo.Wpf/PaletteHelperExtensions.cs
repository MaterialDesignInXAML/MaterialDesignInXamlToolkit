using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    public static class PaletteHelperExtensions
    {
        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten());
            theme.SecondaryMid = new ColorPair(color);
            theme.SecondaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}