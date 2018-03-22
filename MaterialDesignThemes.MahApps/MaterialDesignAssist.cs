using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public static class MaterialDesignAssist
    {
        public static MaterialDesignTheme WithMahApps(this MaterialDesignTheme theme)
        {
            var resources = Application.Current.Resources;
            PaletteHelper.ThemeChanged -= PaletteHelperOnThemeChanged;
            PaletteHelper.ThemeChanged += PaletteHelperOnThemeChanged;
            PaletteHelper.PaletteChanged -= PaletteHelperOnPaletteChanged;
            PaletteHelper.PaletteChanged += PaletteHelperOnPaletteChanged;
            PaletteHelper.PrimarySwatchChanged -= PaletteHelperOnPrimarySwatchChanged;
            PaletteHelper.PrimarySwatchChanged += PaletteHelperOnPrimarySwatchChanged;

            CreateIfNotExists(GetControlsUri());
            CreateIfNotExists(GetFontsUri());
            CreateIfNotExists(GetColorsUri());
            CreateIfNotExists(GetThemeUri(theme.BaseTheme));
            CreateIfNotExists(GetCompatibilityUri());

            foreach (var kvp in GetBrushesFromPalette(theme.Palette))
            {
                resources[kvp.Key] = kvp.Value;
            }
            
            return theme;

            void CreateIfNotExists(Uri source)
            {
                if (FindDictionary(resources.MergedDictionaries, source) == null)
                {
                    resources.MergedDictionaries.Add(new ResourceDictionary { Source = source });
                }
            }
        }

        private static Uri GetCompatibilityUri() =>
            new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Defaults.xaml");

        private static Uri GetControlsUri() =>
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml");

        private static Uri GetFontsUri() => 
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml");

        private static Uri GetColorsUri() =>
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml");

        private static Uri GetThemeUri(BaseTheme theme)
            => new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == BaseTheme.Dark ? "BaseDark" : "BaseLight")}.xaml");

        private static void PaletteHelperOnPaletteChanged(object sender, PaletteChangedEventArgs e)
        {
            foreach (var kvp in GetBrushesFromPalette(e.Palette))
            {
                Application.Current.Resources.ReplaceEntry(kvp.Key, kvp.Value);
            }
        }

        private static void PaletteHelperOnThemeChanged(object sender, ThemeSetEventArgs e)
        {
            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            var source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(e.Theme == BaseTheme.Dark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

        private static void PaletteHelperOnPrimarySwatchChanged(object sender, PrimarySwatchChangedEventArgs e)
        {
            foreach (var kvp in GetPrimaryBrushes(e.Dark, e.Mid, e.Light))
            {
                Application.Current.Resources.ReplaceEntry(kvp.Key, kvp.Value);
            }
        }

        private static IEnumerable<KeyValuePair<string, Brush>> GetBrushesFromPalette(Palette palette)
        {
            Hue dark = palette.PrimarySwatch.PrimaryHues[palette.PrimaryDarkHueIndex];
            Hue mid = palette.PrimarySwatch.PrimaryHues[palette.PrimaryMidHueIndex];
            Hue light = palette.PrimarySwatch.PrimaryHues[palette.PrimaryLightHueIndex];

            return GetPrimaryBrushes(dark, mid, light);
        }

        private static IEnumerable<KeyValuePair<string, Brush>> GetPrimaryBrushes(Hue dark, Hue mid, Hue light)
        {
            yield return new KeyValuePair<string, Brush>("HighlightBrush", new SolidColorBrush(dark.Color));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush", new SolidColorBrush(dark.Color));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush2", new SolidColorBrush(mid.Color));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush3", new SolidColorBrush(light.Color));
            yield return new KeyValuePair<string, Brush>("AccentColorBrush4", new SolidColorBrush(light.Color) { Opacity = .82 });
            yield return new KeyValuePair<string, Brush>("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
            yield return new KeyValuePair<string, Brush>("AccentSelectedColorBrush", new SolidColorBrush(dark.Foreground));
            yield return new KeyValuePair<string, Brush>("ProgressBrush", new LinearGradientBrush(dark.Color, mid.Color, 90.0));
            yield return new KeyValuePair<string, Brush>("CheckmarkFill", new SolidColorBrush(dark.Color));
            yield return new KeyValuePair<string, Brush>("RightArrowFill", new SolidColorBrush(dark.Color));
            yield return new KeyValuePair<string, Brush>("IdealForegroundColorBrush", new SolidColorBrush(dark.Foreground));
            yield return new KeyValuePair<string, Brush>("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });
        }

        private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, Uri source)
        {
            return dictionaries.FirstOrDefault(x => x.Source == source);
        }
    }
}