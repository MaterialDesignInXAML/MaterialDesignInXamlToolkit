using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public static CodePaletteHelper DefaultPaletteHelper { get; } = new CodePaletteHelper();

        public MaterialDesignTheme(CodePaletteHelper paletteHelper, IBaseTheme theme, ColorPalette primaryPalette, ColorPalette secondaryPalette)
        {
            PaletteHelper = paletteHelper;
            BaseTheme = theme;
            PrimaryPalette = primaryPalette;
            SecondaryPalette = secondaryPalette;
        }

        public CodePaletteHelper PaletteHelper { get; }
        public IBaseTheme BaseTheme { get; }
        public ColorPalette PrimaryPalette { get; }
        public ColorPalette SecondaryPalette { get; }

        public static IBaseTheme Light { get; } = new MaterialDesignLightTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignDarkTheme();


        public static MaterialDesignTheme CreateFromColors(IBaseTheme theme, Color primaryColor, Color secondaryColor, CodePaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? DefaultPaletteHelper;

            var primaryPalette = new ColorPalette(PaletteName.Primary, primaryColor);
            var secondaryPalette = new ColorPalette(PaletteName.Secondary, secondaryColor);
            return new MaterialDesignTheme(paletteHelper ?? DefaultPaletteHelper, theme, primaryPalette, secondaryPalette);
        }
    }
}