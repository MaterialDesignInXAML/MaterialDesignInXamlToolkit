using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignTheme
    {
        public static IBaseTheme Light { get; } = new MaterialDesignLightTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignDarkTheme();
        public static IDictionary<BaseTheme, IBaseTheme> BaseThemes { get; } = new Dictionary<BaseTheme, IBaseTheme>
        {
            { Wpf.BaseTheme.Inherit, null },
            { Wpf.BaseTheme.Light, Light },
            { Wpf.BaseTheme.Dark, Dark },
        };

        public ThemeManager ThemeManager { get; }
        public IBaseTheme BaseTheme { get; }
        public ColorPalette PrimaryPalette { get; }
        public ColorPalette SecondaryPalette { get; }

        public static RoutedCommand ChangeThemeCommand = new RoutedCommand();
        public static RoutedCommand ChangePaletteCommand = new RoutedCommand();
        public static RoutedCommand ChangeColorCommand = new RoutedCommand();

        public static void ChangeTheme(BaseTheme theme)
        {
            ChangeTheme(theme == Wpf.BaseTheme.Light ? Light : Dark);
        }

        public static void ChangeTheme(IBaseTheme theme)
        {
            ((ICommand)ChangeThemeCommand).Execute(theme); 
        }

        public static void ChangePrimaryColor(Color color)
        {
            ChangePalette(new ColorPalette(PaletteName.Primary, color));
        }

        public static void ChangePrimaryColor(MaterialDesignColor color)
        {
            ChangePalette(new ColorPalette(PaletteName.Primary, SwatchHelper.Lookup[color]));
        }

        public static void ChangeSecondaryColor(Color color)
        {
            ChangePalette(new ColorPalette(PaletteName.Secondary, color));
        }

        public static void ChangeSecondaryColor(MaterialDesignColor color)
        {
            ChangePalette(new ColorPalette(PaletteName.Secondary, SwatchHelper.Lookup[color]));
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

        public MaterialDesignTheme(ThemeManager themeManager, IBaseTheme theme, ColorPalette primaryPalette, ColorPalette secondaryPalette)
        {
            ThemeManager = themeManager;
            BaseTheme = theme;
            PrimaryPalette = primaryPalette;
            SecondaryPalette = secondaryPalette;
        }

        public static MaterialDesignTheme Create(ThemeManager themeManager, IBaseTheme theme, Color primaryColor, Color secondaryColor)
        {
            var primaryPalette = new ColorPalette(PaletteName.Primary, primaryColor);
            var secondaryPalette = new ColorPalette(PaletteName.Secondary, secondaryColor);
            return new MaterialDesignTheme(themeManager, theme, primaryPalette, secondaryPalette);
        }
    }
}