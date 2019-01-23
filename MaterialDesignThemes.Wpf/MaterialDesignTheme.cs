using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public MaterialDesignTheme(IPaletteHelper paletteHelper, IBaseTheme theme, Color primaryColor, Color secondaryColor)
        {
            PaletteHelper = paletteHelper;
            BaseTheme = theme;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
        }

        public IPaletteHelper PaletteHelper { get; }
        public IBaseTheme BaseTheme { get; }
        public Color PrimaryColor { get; }
        public Color SecondaryColor { get; }

        public static IBaseTheme Light { get; } = new MaterialDesignDarkTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignLightTheme();
    }
}