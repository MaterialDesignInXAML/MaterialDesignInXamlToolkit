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

        private string _selectedColor;

        public string SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public ICommand ChangeHueCommand { get; }
        public ICommand ChangeToPrimaryCommand { get; }
        public ICommand ChangeToSecondaryCommand { get; }
        public ICommand ChangeToPrimaryForegroundCommand { get; }
        public ICommand ChangeToSecondaryForegroundCommand { get; }

        public ColorToolViewModel()
        {
            ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);
            ChangeToPrimaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Primary));
            ChangeToSecondaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Secondary));
            ChangeToPrimaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.PrimaryForeground));
            ChangeToSecondaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.SecondaryForeground));

            var palette = new PaletteHelper().QueryPalette();

            var primary = palette.PrimarySwatch.PrimaryHues.ToArray()[palette.PrimaryMidHueIndex];
            var secondary = palette.AccentSwatch.AccentHues.ToArray()[palette.AccentHueIndex];

            var primaryInterval = Regex.Match(primary.Name, @"[a-zA-Z]+(\d+)").Groups[1].Value;
            var secondaryInterval = "A" + Regex.Match(secondary.Name, @"[a-zA-Z]+(\d+)").Groups[1].Value;

            foreach (var swatch in SwatchHelper.Swatches)
            {
                var stripped = swatch.Name.Replace(" ", "");
                if (string.Equals(palette.PrimarySwatch.Name, stripped, System.StringComparison.OrdinalIgnoreCase))
                {
                    var hue = swatch.Hues.First(o => o.Interval == primaryInterval);
                    _primaryColor = hue;
                }
                else if (string.Equals(palette.AccentSwatch.Name, stripped, System.StringComparison.OrdinalIgnoreCase))
                {
                    _secondaryColor = swatch.Hues.First(o => o.Interval == secondaryInterval);
                }
            }

            SelectedColor = _primaryColor.FullName;
        }

        private void ChangeScheme(ColorScheme scheme)
        {
            ActiveScheme = scheme;
            if (ActiveScheme == ColorScheme.Primary)
            {
                SelectedColor = _primaryColor.FullName;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                SelectedColor = _secondaryColor.FullName;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SelectedColor = _primaryForegroundColor?.FullName;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SelectedColor = _secondaryForegroundColor?.FullName;
            }
        }

        private CodeHue _primaryColor;

        private CodeHue _secondaryColor;

        private CodeHue _primaryForegroundColor;

        private CodeHue _secondaryForegroundColor;

        private void ChangeHue(object obj)
        {
            var hue = (CodeHue)obj;

            SelectedColor = hue.FullName;
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
