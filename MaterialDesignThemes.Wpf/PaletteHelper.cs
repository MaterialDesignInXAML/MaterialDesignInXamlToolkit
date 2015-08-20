using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        public void SetLightDark(bool isDark)
        {
            var existingResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.AbsolutePath, @"(\/MaterialDesignThemes.Wpf;component\/Themes\/MaterialDesignTheme\.)((Light)|(Dark))").Success);
            if (existingResourceDictionary == null)
                throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");

            var source =
                $"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{(isDark ? "Dark" : "Light")}.xaml";

            var newResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);
        }

        public void ReplacePrimaryColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            ResourceDictionary oldColorResourceDictionary;
            if (!TryFindSwatchDictionary(Application.Current.Resources, "PrimaryHueMidBrush", out oldColorResourceDictionary))
                throw new ApplicationException("Unable to find primary color definition in Application resources.");

            var list = swatch.PrimaryHues.ToList();
            var light = list[2];
            var mid = swatch.ExemplarHue;
            var dark = list[7];

            var newColorResourceDictionary = new ResourceDictionary
            {
                {"PrimaryHueLightBrush", new SolidColorBrush(light.Color)},
                {"PrimaryHueLightForegroundBrush", new SolidColorBrush(light.Foreground)},
                {"PrimaryHueMidBrush", new SolidColorBrush(mid.Color)},
                {"PrimaryHueMidForegroundBrush", new SolidColorBrush(mid.Foreground)},
                {"PrimaryHueDarkBrush", new SolidColorBrush(dark.Color)},
                {"PrimaryHueDarkForegroundBrush", new SolidColorBrush(dark.Foreground)}
            };

            Application.Current.Resources.MergedDictionaries.Remove(oldColorResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newColorResourceDictionary);
        }

        public void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            ResourceDictionary oldColorResourceDictionary;
            if (!TryFindSwatchDictionary(Application.Current.Resources, "SecondaryAccentBrush", out oldColorResourceDictionary))
                throw new ApplicationException("Unable to find accent color definition in Application resources.");

            var newColorResourceDictionary = new ResourceDictionary
            {
                {"SecondaryAccentBrush", new SolidColorBrush(swatch.AccentExemplarHue.Color)},
                {"SecondaryAccentForegroundBrush", new SolidColorBrush(swatch.AccentExemplarHue.Foreground)},
            };

            Application.Current.Resources.MergedDictionaries.Remove(oldColorResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newColorResourceDictionary);
        }

        private static bool TryFindSwatchDictionary(ResourceDictionary parentDictionary, string expectedBrushName, out ResourceDictionary dictionary)
        {
            dictionary = parentDictionary.MergedDictionaries.SingleOrDefault(rd => rd[expectedBrushName] != null);
            return dictionary != null;
        }
    }
}
