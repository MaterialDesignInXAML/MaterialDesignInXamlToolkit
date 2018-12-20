using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public MaterialDesignTheme(PaletteHelper paletteHelper, BaseTheme theme, Color primaryColor, Color secondaryColor)
        {
            PaletteHelper = paletteHelper;
            BaseTheme = theme;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
        }

        public PaletteHelper PaletteHelper { get; }
        public BaseTheme BaseTheme { get; }
        public Color PrimaryColor { get; }
        public Color SecondaryColor { get; }
    }
}