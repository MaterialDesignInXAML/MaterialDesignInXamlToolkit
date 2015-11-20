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
    public class PaletteSelectorViewModel : PaletteHelper
    {
        public PaletteSelectorViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
            ApplyPrimaryCommand = new DelegateCommand<Swatch>(ApplyPrimary, new Predicate<Swatch>(s => s != Primary));
            ApplyAccentCommand = new DelegateCommand<Swatch>(ApplyAccent, new Predicate<Swatch>(s => s != Accent));
            ToggleBaseCommand = new DelegateCommand<bool?>(SetLightDark);
            ApplyPrimary(this.AutoPrimary);
            ApplyAccent(this.AutoAccent);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }

        public ICommand ToggleBaseCommand { get; }
        public IEnumerable<Swatch> Swatches { get; }

        public DelegateCommand<Swatch> ApplyPrimaryCommand { get; }

        private void ApplyPrimary(Swatch swatch)
        {
            ReplacePrimaryColor(swatch);
            ApplyPrimaryCommand.RaiseCanExecuteChanged();
        }

        public DelegateCommand<Swatch> ApplyAccentCommand { get; }

        private void ApplyAccent(Swatch swatch)
        {
            ReplaceAccentColor(swatch);
            ApplyAccentCommand.RaiseCanExecuteChanged();
        }
    }
}
