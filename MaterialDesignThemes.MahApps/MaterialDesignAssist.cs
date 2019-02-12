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
            // TODO
            theme.PaletteHelper.ThemeChanged += PaletteHelperOnThemeChanged;
            theme.PaletteHelper.PaletteChanged += PaletteHelperOnPaletteChanged;

            CreateIfNotExists(GetControlsUri());
            CreateIfNotExists(GetFontsUri());
            CreateIfNotExists(GetColorsUri());
            CreateIfNotExists(GetThemeUri(theme.BaseTheme));
            CreateIfNotExists(GetCompatibilityUri());

            foreach (var kvp in GetPrimaryBrushes(theme.PrimaryPalette))
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

        private static Uri GetThemeUri(IBaseTheme theme)
            => new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml");

        private static void PaletteHelperOnPaletteChanged(object sender, PaletteChangedEventArgs e)
        {
            if (e.Palette.Name == PaletteName.Primary)
            {
                foreach (var kvp in GetPrimaryBrushes(e.Palette))
                {
                    Application.Current.Resources.ReplaceEntry(kvp.Key, kvp.Value);
                }
            }
        }

        private static void PaletteHelperOnThemeChanged(object sender, ThemeSetEventArgs e)
        {
            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            var source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(e.Theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
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

        private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, Uri source)
        {
            return dictionaries.FirstOrDefault(x => x.Source == source);
        }
    }
}