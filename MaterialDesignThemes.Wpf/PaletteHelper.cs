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

            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.AbsolutePath, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(isDark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
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

            //TODO reuse some of the dupes, freeze.

            var newColorResourceDictionary = new ResourceDictionary
            {
                {"PrimaryHueLightBrush", new SolidColorBrush(light.Color)},
                {"PrimaryHueLightForegroundBrush", new SolidColorBrush(light.Foreground)},
                {"PrimaryHueMidBrush", new SolidColorBrush(mid.Color)},
                {"PrimaryHueMidForegroundBrush", new SolidColorBrush(mid.Foreground)},
                {"PrimaryHueDarkBrush", new SolidColorBrush(dark.Color)},
                {"PrimaryHueDarkForegroundBrush", new SolidColorBrush(dark.Foreground)}
            };

            if (oldColorResourceDictionary.Keys.OfType<string>().Contains("HighlightBrush"))
            {
                newColorResourceDictionary.Add("HighlightBrush", new SolidColorBrush(dark.Color));
                newColorResourceDictionary.Add("AccentColorBrush", new SolidColorBrush(list[5].Color));
                newColorResourceDictionary.Add("AccentColorBrush2", new SolidColorBrush(list[4].Color));
                newColorResourceDictionary.Add("AccentColorBrush3", new SolidColorBrush(list[3].Color));
                newColorResourceDictionary.Add("AccentColorBrush4", new SolidColorBrush(list[2].Color));
                newColorResourceDictionary.Add("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
                newColorResourceDictionary.Add("AccentSelectedColorBrush", new SolidColorBrush(list[5].Foreground));
                newColorResourceDictionary.Add("ProgressBrush", new LinearGradientBrush(dark.Color, list[3].Color, 90.0));
                newColorResourceDictionary.Add("CheckmarkFill", new SolidColorBrush(list[5].Color));
                newColorResourceDictionary.Add("RightArrowFill", new SolidColorBrush(list[5].Color));
                newColorResourceDictionary.Add("IdealForegroundColorBrush", new SolidColorBrush(list[5].Foreground));
                newColorResourceDictionary.Add("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });
            }

            Application.Current.Resources.MergedDictionaries.Remove(oldColorResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newColorResourceDictionary);
        }

        public void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch);
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

        public void ReplaceAccentColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0 && s.IsAccented);

            if (swatch == null)
                throw new ArgumentException($"No such accented swatch '{name}'", nameof(name));

            ReplaceAccentColor(swatch);
        }

        private static bool TryFindSwatchDictionary(ResourceDictionary parentDictionary, string expectedBrushName, out ResourceDictionary dictionary)
        {
            dictionary = parentDictionary.MergedDictionaries.SingleOrDefault(rd => rd[expectedBrushName] != null);
            return dictionary != null;
        }
    }
}
