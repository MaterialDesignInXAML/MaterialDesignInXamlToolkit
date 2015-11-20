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

namespace MaterialDesignColors.WpfExample
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
