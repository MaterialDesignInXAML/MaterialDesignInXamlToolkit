using MaterialDesignColors;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignDemo
{
    class ColorToolViewModel
    {
        public enum Scheme
        {
            Primary,
            Secondary
        }

        private Scheme _activeScheme;

        public IEnumerable<ISwatch> Swatches { get; } =  SwatchHelper.Swatches;

        public ICommand ChangeHueCommand { get; }
        public ICommand ChangeToPrimaryCommand { get; }
        public ICommand ChangeToSecondaryCommand { get; }

        public ColorToolViewModel()
        {
            ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);
            ChangeToPrimaryCommand = new AnotherCommandImplementation(o => _activeScheme = Scheme.Primary);
            ChangeToSecondaryCommand = new AnotherCommandImplementation(o => _activeScheme = Scheme.Secondary);
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

            var scheme = _activeScheme.ToString();
            PaletteHelper.ReplaceEntry($"{scheme}HueLightBrush", new SolidColorBrush(light));
            PaletteHelper.ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(lightForeground));
            PaletteHelper.ReplaceEntry($"{scheme}HueMidBrush", new SolidColorBrush(mid));
            PaletteHelper.ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(midForeground));
            PaletteHelper.ReplaceEntry($"{scheme}HueDarkBrush", new SolidColorBrush(dark));
            PaletteHelper.ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(darkForeground));
        }
    }
}
