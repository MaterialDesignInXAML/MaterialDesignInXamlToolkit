using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public static RoutedCommand ChangeThemeCommand = new RoutedCommand();

        public static RoutedCommand ChangePaletteCommand = new RoutedCommand();

        public static RoutedCommand ChangeColorCommand = new RoutedCommand();

        public static void ChangeTheme(BaseTheme theme)
        {
            ChangeTheme(theme == Wpf.BaseTheme.Light ? MaterialDesignTheme.Light : MaterialDesignTheme.Dark);
        }

        public static void ChangeTheme(IBaseTheme theme)
        {
            ((ICommand)ChangeThemeCommand).Execute(theme); 
        }

        public static void ChangePrimaryColor(Color color)
        {
            ChangePalette(new ColorPalette(PaletteName.Primary, color));
        }

        public static void ChangeSecondaryColor(Color color)
        {
            ChangePalette(new ColorPalette(PaletteName.Secondary, color));
        }

        public static void ChangePalette(ColorPalette palette)
        {
            ((ICommand)ChangePaletteCommand).Execute(palette);
        }

        public static void ChangeColor(ColorName name, Color color)
        {
            ChangeColor(name.ToString(), color);
        }

        public static void ChangeColor(string name, Color color)
        {
            ((ICommand)ChangeColorCommand).Execute(new ColorChange(name, color));
        }

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

        public static IEnumerable<IBaseTheme> BaseThemes { get; } = new[] { Light, Dark };

        public static MaterialDesignTheme CreateFromColors(IBaseTheme theme, Color primaryColor, Color secondaryColor, CodePaletteHelper paletteHelper = null)
        {
            paletteHelper = paletteHelper ?? DefaultPaletteHelper;

            var primaryPalette = new ColorPalette(PaletteName.Primary, primaryColor);
            var secondaryPalette = new ColorPalette(PaletteName.Secondary, secondaryColor);
            return new MaterialDesignTheme(paletteHelper ?? DefaultPaletteHelper, theme, primaryPalette, secondaryPalette);
        }


    }
}