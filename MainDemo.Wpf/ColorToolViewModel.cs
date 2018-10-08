using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private string _selectedPrimaryColor;

        public string SelectedPrimaryColor
        {
            get => _selectedPrimaryColor;
            set
            {
                _selectedPrimaryColor = value;
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
            ChangeToPrimaryCommand = new AnotherCommandImplementation(o => ActiveScheme = ColorScheme.Primary);
            ChangeToSecondaryCommand = new AnotherCommandImplementation(o => ActiveScheme = ColorScheme.Secondary);
        }

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

            if (ActiveScheme == ColorScheme.Primary)
            {
                SelectedPrimaryColor = hue.FullName;
            }
            if (ActiveScheme == ColorScheme.Secondary)
            {
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
