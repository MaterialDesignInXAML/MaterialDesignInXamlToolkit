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

        public ColorToolViewModel()
        {
            ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);
            ChangeToPrimaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Primary));
            ChangeToSecondaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Secondary));
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
                    _primaryColor = swatch.Hues.First(o => o.Interval == primaryInterval);
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
        }

        private CodeHue _primaryColor;

        private CodeHue _secondaryColor;

        private void ChangeHue(object obj)
        {
            var hue = (CodeHue)obj;
            var light = hue.Lighten();
            var mid = hue.Color;
            var dark = hue.Darken();

            var lightForeground = CodeHue.ContrastingForeGroundColor(light);
            var midForeground = CodeHue.ContrastingForeGroundColor(mid);
            var darkForeground = CodeHue.ContrastingForeGroundColor(dark);

            var scheme = ActiveScheme.ToString();
            PaletteHelper.ReplaceEntry($"{scheme}HueLightBrush", new SolidColorBrush(light));
            PaletteHelper.ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(lightForeground));
            PaletteHelper.ReplaceEntry($"{scheme}HueMidBrush", new SolidColorBrush(mid));
            PaletteHelper.ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(midForeground));
            PaletteHelper.ReplaceEntry($"{scheme}HueDarkBrush", new SolidColorBrush(dark));
            PaletteHelper.ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(darkForeground));

            SelectedColor = hue.FullName;
            if (ActiveScheme == ColorScheme.Primary)
            {
                _primaryColor = hue;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _secondaryColor = hue;
                PaletteHelper.ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(mid));
                PaletteHelper.ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(midForeground));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
