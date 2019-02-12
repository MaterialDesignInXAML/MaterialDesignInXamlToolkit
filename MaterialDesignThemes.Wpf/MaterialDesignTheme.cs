using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public MaterialDesignTheme(IPaletteHelper paletteHelper, IBaseTheme theme, ColorPalette primaryPalette, ColorPalette secondaryPalette)
        {
            PaletteHelper = paletteHelper;
            BaseTheme = theme;
            PrimaryPalette = primaryPalette;
            SecondaryPalette = secondaryPalette;
        }

        public IPaletteHelper PaletteHelper { get; }
        public IBaseTheme BaseTheme { get; }
        public ColorPalette PrimaryPalette { get; }
        public ColorPalette SecondaryPalette { get; }

        public static IBaseTheme Light { get; } = new MaterialDesignDarkTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignLightTheme();

        public static MaterialDesignTheme CreateFromColors(IBaseTheme theme, Color primaryColor, Color secondaryColor, IPaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? MaterialDesignAssist.DefaultPaletteHelper;

            var primaryPalette = new ColorPalette(PaletteName.Primary, primaryColor);
            var secondaryPalette = new ColorPalette(PaletteName.Secondary, secondaryColor);
            return new MaterialDesignTheme(paletteHelper ?? MaterialDesignAssist.DefaultPaletteHelper, theme, primaryPalette, secondaryPalette);
        }
    }
}