using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public static class MaterialDesignAssist
    {
        public static MaterialDesignTheme WithMahApps(this MaterialDesignTheme theme)
        {
            theme.ThemeManager.ThemeChangeHandlers.Add(OnChangeTheme);
            theme.ThemeManager.PaletteChangeHandlers.Add(OnChangePalette);

            CreateIfNotExists(GetThemeUri(theme.BaseTheme));

            foreach (var kvp in GetPrimaryBrushes(theme.PrimaryPalette))
            {
                theme.ThemeManager.Resources[kvp.Key] = kvp.Value;
            }

            return theme;

            void CreateIfNotExists(Uri source)
            {
                if (FindDictionary(theme.ThemeManager.Resources.MergedDictionaries, source) == null)
                {
                    theme.ThemeManager.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = source });
                }
            }
        }

        private static IEnumerable<KeyValuePair<string, Brush>> GetPrimaryBrushes(ColorPalette palette)
        {
            yield return new KeyValuePair<string, Brush>("HighlightBrush", new SolidColorBrush(palette.Dark));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush", new SolidColorBrush(palette.Dark));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush2", new SolidColorBrush(palette.Mid));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush3", new SolidColorBrush(palette.Light));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush4", new SolidColorBrush(palette.Light) { Opacity = .82 });
            yield return new KeyValuePair<string, Brush>("WindowTitleColorBrush", new SolidColorBrush(palette.Dark));
            yield return new KeyValuePair<string, Brush>("AccentSelectedColorBrush", new SolidColorBrush(palette.DarkForeground));
            yield return new KeyValuePair<string, Brush>("ProgressBrush", new LinearGradientBrush(palette.Dark, palette.Mid, 90.0));
            yield return new KeyValuePair<string, Brush>("CheckmarkFill", new SolidColorBrush(palette.Dark));
            yield return new KeyValuePair<string, Brush>("RightArrowFill", new SolidColorBrush(palette.Dark));
            yield return new KeyValuePair<string, Brush>("IdealForegroundColorBrush", new SolidColorBrush(palette.DarkForeground));
            yield return new KeyValuePair<string, Brush>("IdealForegroundDisabledBrush", new SolidColorBrush(palette.Dark) { Opacity = .4 });
        }

        private static ResourceDictionary CreateEmptyPaletteDictionary()
        {
            return new ResourceDictionary {
                ["HighlightBrush"] = new SolidColorBrush(),
                ["AccentColorBrush"] = new SolidColorBrush(),
                ["AccentColorBrush2"] = new SolidColorBrush(),
                ["AccentColorBrush3"] = new SolidColorBrush(),
                ["AccentColorBrush4"] = new SolidColorBrush(),
                ["WindowTitleColorBrush"] = new SolidColorBrush(),
                ["AccentSelectedColorBrush"] = new SolidColorBrush(),
                ["ProgressBrush"] = new SolidColorBrush(),
                ["CheckmarkFill"] = new SolidColorBrush(),
                ["RightArrowFill"] = new SolidColorBrush(),
                ["IdealForegroundColorBrush"] = new SolidColorBrush(),
                ["IdealForegroundDisabledBrush"] = new SolidColorBrush(),
            };
        }

        public static void OnChangeTheme(ResourceDictionary resources, IBaseTheme theme)
        {
            var existingMahAppsResourceDictionary = resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary != null)
            {
                resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            }

            var source = $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

        public static void OnChangePalette(ResourceDictionary resources, ColorPalette palette)
        {
            if (palette.Name == PaletteName.Primary.ToString())
            {
                foreach (var kvp in GetPrimaryBrushes(palette))
                {
                    resources.ReplaceEntry(kvp.Key, kvp.Value);
                }
            }
        }

        public static void OnChangeColor(ResourceDictionary resources, ColorChange color)
        {
            //TODO
        }

        private static Uri GetThemeUri(IBaseTheme theme)
            => new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml");

        private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, Uri source)
        {
            return dictionaries.FirstOrDefault(x => x.Source == source);
        }
    }
}
