using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace MaterialDesignColors.WpfExample
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }

        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        private static void ApplyBase(bool isDark)
        {
            //TODO
            //MaterialDesignAssist.DefaultPaletteHelper.SetLightDark(isDark);
            new XamlPaletteHelper().SetLightDark(isDark);
        }

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        private static void ApplyPrimary(Swatch swatch)
        {
            //MaterialDesignAssist.DefaultPaletteHelper.ReplacePrimaryColor(swatch);
            new XamlPaletteHelper().ReplacePrimaryColor(swatch);
        }

        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));

        private static void ApplyAccent(Swatch swatch)
        {
            //MaterialDesignAssist.DefaultPaletteHelper.ReplaceAccentColor(swatch);
            new XamlPaletteHelper().ReplaceAccentColor(swatch);
        }
    }
}
