using System;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace MahMaterialDragablzMashUp
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;        
		    ApplyPrimary(PaletteHelper.AutoPrimary);
            ApplyAccent(PaletteHelper.AutoAccent);   
        }

        public ICommand ToggleBaseCommand { get; } = new DelegateCommand<bool?>(new PaletteHelper().SetLightDark);
        public IEnumerable<Swatch> Swatches { get; }

        public static DelegateCommand<Swatch> ApplyPrimaryCommand { get; } = new DelegateCommand<Swatch>(ApplyPrimary, new Func<Swatch, bool>(s => s != PaletteHelper.Primary));

        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
        }

        public static DelegateCommand<Swatch> ApplyAccentCommand { get; } = new DelegateCommand<Swatch>(ApplyAccent, new Func<Swatch, bool>(s => s != PaletteHelper.Accent));

        private static void ApplyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }
    }
}
