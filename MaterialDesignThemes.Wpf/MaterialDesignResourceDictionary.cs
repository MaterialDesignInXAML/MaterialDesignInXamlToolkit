using System.Windows;
using MaterialDesignColors.Wpf;

namespace MaterialDesignThemes.Wpf
{
    public class MaterialDesignResourceDictionary : ResourceDictionary
    {
        protected MaterialDesignTheme MaterialDesignTheme { get; }

        public MaterialDesignResourceDictionary()
        {
            MaterialDesignTheme = this.WithMaterialDesign(Theme, PrimaryColor, SecondaryColor, new ThemeManager(this).AttachThemeEventsToWindow());
        }

        private BaseTheme _theme;

        public BaseTheme Theme
        {
            get => _theme;
            set => MaterialDesignTheme.ThemeManager.ChangeTheme(MaterialDesignTheme.BaseThemes[_theme = value]);
        }

        private MaterialDesignColor _primaryColor;

        public MaterialDesignColor PrimaryColor
        {
            get => _primaryColor;
            set => MaterialDesignTheme.ThemeManager.ChangePalette(ColorPalette.CreatePrimaryPalette(_primaryColor = value));
        }

        private MaterialDesignColor _secondaryColor;

        public MaterialDesignColor SecondaryColor
        {
            get => _secondaryColor;
            set => MaterialDesignTheme.ThemeManager.ChangePalette(ColorPalette.CreateSecondaryPalette(_secondaryColor = value));
        }
    }
}