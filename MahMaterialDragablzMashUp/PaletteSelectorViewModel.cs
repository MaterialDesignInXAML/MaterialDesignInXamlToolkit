using System;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace MahMaterialDragablzMashUp
{
    public class PaletteSelectorViewModel
    {
        static PaletteHelper palette = new PaletteHelper();

        public ICommand ToggleBaseCommand { get; } = new DelegateCommand<bool>(palette.SetLightDark);

        public IEnumerable<Swatch> Swatches { get; } = new SwatchesProvider(true).Swatches;

        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; } = new DelegateCommand<Swatch>(ApplyPrimary, new Predicate<Swatch>(s => s != palette.Primary));

        private static void ApplyPrimary(Swatch swatch)
        {
            palette.ReplacePrimaryColor(swatch);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand<Swatch> ApplyAccentCommand { get; } = new DelegateCommand<Swatch>(ApplyAccent, new Predicate<Swatch>(s => s != palette.Accent));

        private static void ApplyAccent(Swatch swatch)
        {
            palette.ReplaceAccentColor(swatch);
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }
    }
}
