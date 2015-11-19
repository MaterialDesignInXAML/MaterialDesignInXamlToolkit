using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Microsoft.Expression.Interactivity.Core;
using Prism.Commands;

namespace MaterialDesignColors.WpfExample
{
    public class PaletteSelectorViewModel
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;        
		    ApplyPrimary(PaletteHelper.AutoPrimary);
            ApplyAccent(PaletteHelper.AutoAccent);
            ApplyAccentCommand.RaiseCanExecuteChanged();
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
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
