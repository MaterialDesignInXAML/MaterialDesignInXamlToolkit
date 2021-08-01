using System;
using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain
{
    public class PaletteSelectorViewModel : ViewModelBase
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;

            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

            if (theme is Theme internalTheme)
                IsColorAdjusted = internalTheme.ColorAdjustment is not null;

            if (paletteHelper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += (_, e) =>
                {
                    IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
                };
            }
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private bool _isColorAdjusted;
        public bool IsColorAdjusted
        {
            get => _isColorAdjusted;
            set
            {
                if (SetProperty(ref _isColorAdjusted, value))
                {
                    ModifyTheme(theme =>
                    {
                        if (theme is Theme internalTheme)
                            internalTheme.ColorAdjustment = value ? new ColorAdjustment() : null;
                    });
                }
            }
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        private static void ApplyPrimary(Swatch swatch) 
            => ModifyTheme(theme => theme.SetPrimaryColor(swatch.ExemplarHue.Color));

        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));

        private static void ApplyAccent(Swatch swatch)
        {
            if (swatch is { AccentExemplarHue: not null } )
            {
                ModifyTheme(theme => theme.SetSecondaryColor(swatch.AccentExemplarHue.Color));
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
    }
}
