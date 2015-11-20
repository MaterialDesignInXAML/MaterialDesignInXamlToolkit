using System;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace MahMaterialDragablzMashUp
{
    public class PaletteSelectorViewModel
    {
        PaletteHelper palette = new PaletteHelper();
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider(true).Swatches;
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary, new Predicate<Swatch>(s => s != palette.Primary));
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent, new Predicate<Swatch>(s => s != palette.Accent));
            ToggleBaseCommand = new DelegateCommand<bool?>(palette.SetLightDark);
        }

        public ICommand ToggleBaseCommand { get; }
        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; }

        private void ApplyPrimary(Swatch swatch)
        {
            palette.ReplacePrimaryColor(swatch);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand<Swatch> ApplyAccentCommand { get; }

        private void ApplyAccent(Swatch swatch)
        {
            palette.ReplaceAccentColor(swatch);
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }
    }
}
