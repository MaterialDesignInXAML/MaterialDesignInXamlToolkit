using MaterialDesignColors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
using MaterialDesignColors.ColorManipulation;
using System.Text.RegularExpressions;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        // public event EventHandler<PrimarySwatchChangedEventArgs> PrimarySwatchChanged; TODO??

        private readonly SwatchesProvider _swatchesProvider = new SwatchesProvider();
        private readonly RecommendedThemeProvider _themeProvider = new RecommendedThemeProvider();

        public virtual void SetLightDark(bool isDark)
        {
            var existingResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MaterialDesignThemes.Wpf;component\/Themes\/MaterialDesignTheme\.)((Light)|(Dark))").Success);
            if (existingResourceDictionary == null)
                throw new ApplicationException("Unable to find Light/Dark base theme in Application resources.");

            var source =
                $"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{(isDark ? "Dark" : "Light")}.xaml";
            var newResourceDictionary = new ResourceDictionary() { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newResourceDictionary);

            var existingMahAppsResourceDictionary = Application.Current.Resources.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary == null) return;

            source =
                $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(isDark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            Application.Current.Resources.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            Application.Current.Resources.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

        public virtual Palette ReplacePalette(string primaryName, string accentName)
        {
            if (primaryName == null) throw new ArgumentNullException(nameof(primaryName));
            if (accentName == null) throw new ArgumentNullException(nameof(accentName));

            PrimaryTheme primaryTheme = _themeProvider.RecommendedThemes.OfType<PrimaryTheme>().FirstOrDefault(x =>
                string.Compare(x.Swatch.Name, primaryName, StringComparison.InvariantCultureIgnoreCase) == 0)
                ?? throw new ArgumentException($"Could not find primary theme for '{primaryName}'", nameof(primaryName));

            AccentTheme accentTheme = _themeProvider.RecommendedThemes.OfType<AccentTheme>().FirstOrDefault(x =>
                string.Compare(x.Swatch.Name, accentName, StringComparison.InvariantCultureIgnoreCase) == 0)
                ?? throw new ArgumentException($"Could not find accent theme for '{accentName}'", nameof(accentName));

            var palette = new Palette(
                primaryTheme.Swatch, 
                accentTheme.Swatch, 
                primaryTheme.LightHueIndex,
                primaryTheme.MidHueIndex, 
                primaryTheme.DarkHueIndex, 
                accentTheme.HueIndex);
            ReplacePalette(palette);
            return palette;
        }

        /// <summary>
        /// Replaces the entire palette
        /// </summary>
        public virtual void ReplacePalette(Palette palette)
        {
            if (palette == null) throw new ArgumentNullException(nameof(palette));

            var allHues = palette.PrimarySwatch.PrimaryHues.ToList();
            ReplacePrimaryColor(
                palette.PrimarySwatch,
                allHues[palette.PrimaryLightHueIndex],
                allHues[palette.PrimaryMidHueIndex],
                allHues[palette.PrimaryDarkHueIndex]);

            var accentHue = palette.AccentSwatch.AccentHues.ElementAt(palette.AccentHueIndex);
            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(accentHue.Color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(accentHue.Foreground));
        }

        /// <summary>
        /// Replaces the primary colour, selecting a balanced set of hues for the light, mid and dark hues.
        /// </summary>
        /// <param name="swatch"></param>
        public virtual void ReplacePrimaryColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            var list = swatch.PrimaryHues.ToList();
            var light = list[palette?.PrimaryLightHueIndex ?? 2];
            var mid = list[palette?.PrimaryMidHueIndex ?? 5];
            var dark = list[palette?.PrimaryDarkHueIndex ?? 7];

            ReplacePrimaryColor(swatch, light, mid, dark);
        }

        public virtual void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = _swatchesProvider.Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch);
        }

        public virtual void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            foreach (var color in swatch.AccentHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            var hue = swatch.AccentHues.ElementAt(palette?.AccentHueIndex ?? swatch.AccentExemplarHueIndex);

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(hue.Color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(hue.Foreground));
        }

        public virtual void ReplaceAccentColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0 && s.IsAccented);

            if (swatch == null)
                throw new ArgumentException($"No such accented swatch '{name}'", nameof(name));

            ReplaceAccentColor(swatch);
        }

        /// <summary>
        /// Attempts to query the current palette configured in the application's resources.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if there is any ambiguouty regarding the palette. Provided
        /// standard guidleines have been followed for palette configureation, this should not happen.</exception>
        public Palette QueryPalette()
        {
            //it's not safe to to query for the included swatches, so we find the mid (or accent) colour, 
            //& cross match it with the entirety of all available hues to find the owning swatch.
            
            var swatchesProvider = _swatchesProvider;
            var swatchByPrimaryHueIndex = swatchesProvider
                .Swatches
                .SelectMany(s => s.PrimaryHues.Select(h => new { s, h }))
                .ToDictionary(a => a.h.Color, a => a.s);
            var swatchByAccentHueIndex = swatchesProvider
                .Swatches
                .Where(s => s.IsAccented)
                .SelectMany(s => s.AccentHues.Select(h => new { s, h })).Distinct()
                .ToDictionary(a => a.h.Color, a => a.s);

            var primaryMidBrush = GetBrush("PrimaryHueMidBrush");
            var accentBrush = GetBrush("SecondaryAccentBrush");

            if (!swatchByPrimaryHueIndex.TryGetValue(primaryMidBrush.Color, out var primarySwatch)) return null;
                //throw new InvalidOperationException("PrimaryHueMidBrush is not from standard swatches");
            if (!swatchByAccentHueIndex.TryGetValue(accentBrush.Color, out var accentSwatch)) return null;
                //throw new InvalidOperationException("SecondaryAccentBrush is not from standard swatches");

            var primaryLightBrush = GetBrush("PrimaryHueLightBrush");
            var primaryDarkBrush = GetBrush("PrimaryHueDarkBrush");

            var primaryLightHueIndex = TryGetHueIndex(primarySwatch, primaryLightBrush.Color, false);
            var primaryMidHueIndex = TryGetHueIndex(primarySwatch, primaryMidBrush.Color, false);
            var primaryDarkHueIndex = TryGetHueIndex(primarySwatch, primaryDarkBrush.Color, false);
            var accentHueIndex = TryGetHueIndex(accentSwatch, accentBrush.Color, true);

            return new Palette(primarySwatch, accentSwatch, primaryLightHueIndex ?? 2, primaryMidHueIndex ?? 5, primaryDarkHueIndex ?? 7, accentHueIndex ?? 7);
        }

        private static void ReplacePrimaryColor(Swatch swatch, Hue light, Hue mid, Hue dark)
        {
            foreach (var color in swatch.PrimaryHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            ReplaceEntry("PrimaryHueLightBrush", new SolidColorBrush(light.Color));
            ReplaceEntry("PrimaryHueLightForegroundBrush", new SolidColorBrush(light.Foreground));
            ReplaceEntry("PrimaryHueMidBrush", new SolidColorBrush(mid.Color));
            ReplaceEntry("PrimaryHueMidForegroundBrush", new SolidColorBrush(mid.Foreground));
            ReplaceEntry("PrimaryHueDarkBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("PrimaryHueDarkForegroundBrush", new SolidColorBrush(dark.Foreground));
            
            //mahapps brushes            
            ReplaceEntry("HighlightBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentColorBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentColorBrush2", new SolidColorBrush(mid.Color));
            ReplaceEntry("AccentColorBrush3", new SolidColorBrush(light.Color));
            ReplaceEntry("AccentColorBrush4", new SolidColorBrush(light.Color) { Opacity = .82 });
            ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(dark.Foreground));
            ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark.Color, mid.Color, 90.0));
            ReplaceEntry("CheckmarkFill", new SolidColorBrush(dark.Color));
            ReplaceEntry("RightArrowFill", new SolidColorBrush(dark.Color));
            ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(dark.Foreground));
            ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });
        }

        private static int? TryGetHueIndex(Swatch swatch, Color color, bool isAccent)
        {
            var x = (isAccent ? swatch.AccentHues : swatch.PrimaryHues).Select((h, i) => new { h, i })
                .FirstOrDefault(a => a.h.Color == color);
            return x == null ? (int?)null : x.i;
                //throw new InvalidOperationException($"Color {color} not found in swatch {swatch.Name}.");
        }

        private static SolidColorBrush GetBrush(string name)
        {
            var group = GetTree(Application.Current.Resources)
                .SelectMany(d => GetEntries(d).Select(e => new { d, e }))
                .Where(a => a.e.Value is SolidColorBrush)
                .GroupBy(a => (SolidColorBrush)a.e.Value)
                .SingleOrDefault(g => g.First().e.Key.Equals(name));

            if (group == null)
                throw new InvalidOperationException($"Unable to safely determine a single resource definition for {name}.");
            
            if (!(group.First().e.Value is SolidColorBrush solidColorBrush))
                throw new InvalidOperationException($"Expected {name} to be a SolidColorBrush");

            return solidColorBrush;
        }

        private static IEnumerable<DictionaryEntry> GetEntries(IDictionary dictionary)
        {
            var dictionaryEnumerator = dictionary.GetEnumerator();
            while (dictionaryEnumerator.MoveNext())
            {
                yield return dictionaryEnumerator.Entry;
            }
        }

        private static IEnumerable<ResourceDictionary> GetTree(ResourceDictionary node)
        {
            yield return node;

            foreach (var descendant in node.MergedDictionaries.SelectMany(GetTree))
            {
                yield return descendant;
            }
        }

        /// <summary>
        /// Replaces a certain entry anywhere in the parent dictionary and its merged dictionaries
        /// </summary>
        /// <param name="name">The entry to replace</param>
        /// <param name="value">The new entry value</param>
        internal static void ReplaceEntry(object name, object value)
        {
            Application.Current.Resources.ReplaceEntry(name, value);
        }
    }    
}
