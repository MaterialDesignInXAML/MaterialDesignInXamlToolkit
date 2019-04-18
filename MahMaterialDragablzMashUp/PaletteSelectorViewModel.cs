using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace MahMaterialDragablzMashUp
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }

        public ICommand ToggleStyleCommand { get; } = new AnotherCommandImplementation(o => ApplyStyle((bool)o));

        public ICommand ToggleBaseCommand { get; } = new AnotherCommandImplementation(o => ApplyBase((bool)o));

        public IEnumerable<Swatch> Swatches { get; }

        public ICommand ApplyPrimaryCommand { get; } = new AnotherCommandImplementation(o => ApplyPrimary((Swatch)o));

        public ICommand ApplyAccentCommand { get; } = new AnotherCommandImplementation(o => ApplyAccent((Swatch)o));

        private static void ApplyStyle(bool alternate)
        {
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri(@"pack://application:,,,/Dragablz;component/Themes/materialdesign.xaml")
            };

            var styleKey = alternate ? "MaterialDesignAlternateTabablzControlStyle" : "MaterialDesignTabablzControlStyle";
            var style = (Style) resourceDictionary[styleKey];

            foreach (var tabablzControl in Dragablz.TabablzControl.GetLoadedInstances())
            {
                tabablzControl.Style = style;
            }
        }

        private static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }

    }
}
