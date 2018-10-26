using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    class ColorToolViewModel : INotifyPropertyChanged
    {
        private ColorScheme _activeScheme;

        public ColorScheme ActiveScheme
        {
            get
            {
                return _activeScheme;
            }
            set
            {
                _activeScheme = value;
                OnPropertyChanged();
            }
        }

        private Color? _selectedColor;

        public Color? SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public ICommand ChangeCustomHueCommand { get; }

        public ICommand ChangeHueCommand { get; }
        public ICommand ChangeToPrimaryCommand { get; }
        public ICommand ChangeToSecondaryCommand { get; }
        public ICommand ChangeToPrimaryForegroundCommand { get; }
        public ICommand ChangeToSecondaryForegroundCommand { get; }

        public ColorToolViewModel()
        {
            ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);
            ChangeCustomHueCommand = new AnotherCommandImplementation(ChangeCustomColor);
            ChangeToPrimaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Primary));
            ChangeToSecondaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Secondary));
            ChangeToPrimaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.PrimaryForeground));
            ChangeToSecondaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.SecondaryForeground));

            var palette = new PaletteHelper().QueryPalette();

            var primary = palette.PrimarySwatch.PrimaryHues.ToArray()[palette.PrimaryMidHueIndex];
            var secondary = palette.AccentSwatch.AccentHues.ToArray()[palette.AccentHueIndex];

            foreach (var swatch in SwatchHelper.Swatches)
            {
                var stripped = swatch.Name.Replace(" ", "");
                if (string.Equals(palette.PrimarySwatch.Name, stripped, System.StringComparison.OrdinalIgnoreCase))
                {
                    var hue = swatch.Hues.First(o => o == primary.Color);
                    _primaryColor = hue;
                }
                else if (string.Equals(palette.AccentSwatch.Name, stripped, System.StringComparison.OrdinalIgnoreCase))
                {
                    _secondaryColor = swatch.Hues.First(o => o == secondary.Color);
                }
            }

            SelectedColor = _primaryColor;
        }

        private void ChangeCustomColor(object obj)
        {
            var color = (Color)obj;

            SelectedColor = null;
            if (ActiveScheme == ColorScheme.Primary)
            {
                PaletteHelper.SetPrimaryPalette(color);
                _primaryColor = null;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                PaletteHelper.SetSecondaryPalette(color);
                _secondaryColor = null;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                PaletteHelper.SetPrimaryForeground(color);
                _primaryForegroundColor = null;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                PaletteHelper.SetSecondaryForeground(color);
                _secondaryForegroundColor = null;
            }
        }

        private void ChangeScheme(ColorScheme scheme)
        {
            ActiveScheme = scheme;
            if (ActiveScheme == ColorScheme.Primary)
            {
                SelectedColor = _primaryColor;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                SelectedColor = _secondaryColor;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SelectedColor = _primaryForegroundColor;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SelectedColor = _secondaryForegroundColor;
            }
        }

        private Color? _primaryColor;

        private Color? _secondaryColor;

        private Color? _primaryForegroundColor;

        private Color? _secondaryForegroundColor;

        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;

            SelectedColor = hue;
            if (ActiveScheme == ColorScheme.Primary)
            {
                PaletteHelper.SetPrimaryPalette(hue);
                _primaryColor = hue;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                PaletteHelper.SetSecondaryPalette(hue);
                _secondaryColor = hue;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                PaletteHelper.SetPrimaryForeground(hue);
                _primaryForegroundColor = hue;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                PaletteHelper.SetSecondaryForeground(hue);
                _secondaryForegroundColor = hue;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
