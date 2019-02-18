using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.Wpf;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public class MahAppsResourceDictionary : ResourceDictionary
    {
        public MahAppsResourceDictionary()
        {
            Application.Current.Resources = this;

            CreateIfNotExists(GetControlsUri());
            CreateIfNotExists(GetFontsUri());
            CreateIfNotExists(GetColorsUri());
            CreateIfNotExists(GetThemeUri(MaterialDesignTheme.Light));
            MergedDictionaries.Add(MaterialDesignResourceDictionary.CreateDefaultThemeDictionary());
            MergedDictionaries.Add(MaterialDesignResourceDictionary.CreateEmptyThemeDictionary());
            MergedDictionaries.Add(MaterialDesignResourceDictionary.CreateEmptyPaletteDictionary());
            CreateIfNotExists(GetCompatibilityUri());

            void CreateIfNotExists(Uri source)
            {
                if (FindDictionary(MergedDictionaries, source) == null)
                {
                    MergedDictionaries.Add(new ResourceDictionary { Source = source });
                }
            }

            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeThemeCommand, ChangeThemeCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangePaletteCommand, ChangePaletteCommandExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaterialDesignTheme.ChangeColorCommand, ChangeColorCommandExecuted));
        }

        public virtual void ChangeThemeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var theme = (IBaseTheme)e.Parameter;
            this.WithTheme(theme);
            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            var source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

        public virtual void ChangePaletteCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var palette = (ColorPalette)e.Parameter;
            this.WithPalette(palette);
            if (palette.Name == PaletteName.Primary.ToString())
            {
                foreach (var kvp in GetPrimaryBrushes(palette))
                {
                    Application.Current.Resources.ReplaceEntry(kvp.Key, kvp.Value);
                }
            }
        }

        public virtual void ChangeColorCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var colorChange = (ColorChange)e.Parameter;
            this.WithColor(colorChange.Name, colorChange.Color);
        }

        private BaseTheme _theme = BaseTheme.Light;

        public BaseTheme Theme
        {
            get => _theme;
            set => this.WithTheme(_theme = value);
        }

        private MaterialDesignColor _primaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor PrimaryColor
        {
            get => _primaryColor;
            set => this.WithPrimaryColor(_primaryColor = value);
        }

        private MaterialDesignColor _secondaryColor = MaterialDesignColor.DeepPurple;
        public MaterialDesignColor SecondaryColor
        {
            get => _secondaryColor;
            set => this.WithSecondaryColor(_secondaryColor = value);
        }

        private static Uri GetControlsUri() =>
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml");

        private static Uri GetFontsUri() =>
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml");

        private static Uri GetColorsUri() =>
            new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml");

        private static Uri GetThemeUri(IBaseTheme theme)
            => new Uri($"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(theme == MaterialDesignTheme.Dark ? "BaseDark" : "BaseLight")}.xaml");

        private static Uri GetCompatibilityUri() =>
            new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Defaults.xaml");

        private static ResourceDictionary FindDictionary(IEnumerable<ResourceDictionary> dictionaries, Uri source)
        {
            return dictionaries.FirstOrDefault(x => x.Source == source);
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
    }
}