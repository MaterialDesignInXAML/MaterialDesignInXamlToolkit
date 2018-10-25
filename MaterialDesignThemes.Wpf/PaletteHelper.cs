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

namespace MaterialDesignThemes.Wpf
{
    public static class PaletteHelper
    {
        public static event EventHandler<ThemeSetEventArgs> ThemeChanged;
        public static event EventHandler<PaletteChangedEventArgs> PaletteChanged;
        public static event EventHandler<PrimarySwatchChangedEventArgs> PrimarySwatchChanged; 

        private static readonly SwatchesProvider _swatchesProvider = new SwatchesProvider();
        private static readonly RecommendedThemeProvider _themeProvider = new RecommendedThemeProvider();

        public static void SetLightDark(bool isDark)
        {
            Application.Current.Resources.WithTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            ThemeChanged?.Invoke(null, new ThemeSetEventArgs(isDark ? BaseTheme.Dark : BaseTheme.Light));
        }

        public static Palette ReplacePalette(string primaryName, string accentName)
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
        public static void ReplacePalette(Palette palette)
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

            PaletteChanged?.Invoke(null, new PaletteChangedEventArgs(palette));
        }

        /// <summary>
        /// Replaces the primary colour, selecting a balanced set of hues for the light, mid and dark hues.
        /// </summary>
        /// <param name="swatch"></param>
        public static void ReplacePrimaryColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            var list = swatch.PrimaryHues.ToList();
            var light = list[palette.PrimaryLightHueIndex];
            var mid = list[palette.PrimaryMidHueIndex];
            var dark = list[palette.PrimaryDarkHueIndex];

            ReplacePrimaryColor(swatch, light, mid, dark);
            PrimarySwatchChanged?.Invoke(null, new PrimarySwatchChangedEventArgs(swatch, light, mid, dark));
        }

        public static void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = _swatchesProvider.Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch);
        }

        public static void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var palette = QueryPalette();

            foreach (var color in swatch.AccentHues)
            {
                ReplaceEntry(color.Name, color.Color);
                ReplaceEntry(color.Name + "Foreground", color.Foreground);
            }

            var hue = swatch.AccentHues.ElementAt(palette.AccentHueIndex);

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(hue.Color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(hue.Foreground));
        }

        public static void ReplaceAccentColor(string name)
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
        public static Palette QueryPalette()
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
                .SelectMany(s => s.AccentHues.Select(h => new { s, h }))
                .ToDictionary(a => a.h.Color, a => a.s);

            var primaryMidBrush = GetBrush("PrimaryHueMidBrush");
            var accentBrush = GetBrush("SecondaryAccentBrush");

            if (!swatchByPrimaryHueIndex.TryGetValue(primaryMidBrush.Color, out var primarySwatch))
                throw new InvalidOperationException("PrimaryHueMidBrush is not from standard swatches");
            if (!swatchByAccentHueIndex.TryGetValue(accentBrush.Color, out var accentSwatch))
                throw new InvalidOperationException("SecondaryAccentBrush is not from standard swatches");

            var primaryLightBrush = GetBrush("PrimaryHueLightBrush");
            var primaryDarkBrush = GetBrush("PrimaryHueDarkBrush");

            var primaryLightHueIndex = GetHueIndex(primarySwatch, primaryLightBrush.Color, false);
            var primaryMidHueIndex = GetHueIndex(primarySwatch, primaryMidBrush.Color, false);
            var primaryDarkHueIndex = GetHueIndex(primarySwatch, primaryDarkBrush.Color, false);
            var accentHueIndex = GetHueIndex(accentSwatch, accentBrush.Color, true);

            return new Palette(primarySwatch, accentSwatch, primaryLightHueIndex, primaryMidHueIndex, primaryDarkHueIndex, accentHueIndex);
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
        }

        private static int GetHueIndex(Swatch swatch, Color color, bool isAccent)
        {
            var x = (isAccent ? swatch.AccentHues : swatch.PrimaryHues).Select((h, i) => new { h, i })
                .FirstOrDefault(a => a.h.Color == color);
            if (x == null)
                throw new InvalidOperationException($"Color {color} not found in swatch {swatch.Name}.");
            return x.i;
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
        /// <param name="entryName">The entry to replace</param>
        /// <param name="newValue">The new entry value</param>
        /// <param name="parentDictionary">The root dictionary to start searching at. Null means using Application.Current.Resources</param>
        public static void ReplaceEntry(object entryName, object newValue, ResourceDictionary parentDictionary = null)
        {            
            if (parentDictionary == null)
                parentDictionary = Application.Current.Resources;
            
            if (parentDictionary.Contains(entryName))
            {
                var brush = parentDictionary[entryName] as SolidColorBrush;
                if (brush != null && !brush.IsFrozen)
                {                 
                    var animation = new ColorAnimation
                    {
                        From = ((SolidColorBrush)parentDictionary[entryName]).Color,
                        To = ((SolidColorBrush)newValue).Color,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                }
                else
                    parentDictionary[entryName] = newValue; //Set value normally
            }

            foreach (var dictionary in parentDictionary.MergedDictionaries)
                ReplaceEntry(entryName, newValue, dictionary);
        }

        public static void SetPalettes(Color primary, Color secondary)
        {
            SetPrimaryPalette(primary);
            SetSecondaryPalette(secondary);
        }

        public static void SetPrimaryForeground(Color color)
        {
            SetForegroundBrushes(color, "Primary");
        }

        public static void SetSecondaryForeground(Color color)
        {
            SetForegroundBrushes(color, "Secondary");
        }

        private static void SetForegroundBrushes(Color color, string scheme)
        {
            ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(color));
            ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(color));
        }

        public static void SetPrimaryPalette(Color color)
        {
            var light = color.Lighten();
            var mid = color;
            var dark = color.Darken();

            var darkForeground = ColorHelper.ContrastingForeGroundColor(dark);

            SetPalette(color, "Primary");

            ReplaceEntry("HighlightBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentColorBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentColorBrush2", new SolidColorBrush(mid));
            ReplaceEntry("AccentColorBrush3", new SolidColorBrush(light));
            ReplaceEntry("AccentColorBrush4", new SolidColorBrush(light) { Opacity = .82 });
            ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark));
            ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(darkForeground));
            ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark, mid, 90.0));
            ReplaceEntry("CheckmarkFill", new SolidColorBrush(dark));
            ReplaceEntry("RightArrowFill", new SolidColorBrush(dark));
            ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(darkForeground));
            ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark) { Opacity = .4 });
        }

        public static void SetSecondaryPalette(Color color)
        {
            SetPalette(color, "Secondary");

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(ColorHelper.ContrastingForeGroundColor(color)));
        }

        private static void SetPalette(Color color, string scheme)
        {
            var light = color.Lighten();
            var mid = color;
            var dark = color.Darken();

            var lightForeground = ColorHelper.ContrastingForeGroundColor(light);
            var midForeground = ColorHelper.ContrastingForeGroundColor(mid);
            var darkForeground = ColorHelper.ContrastingForeGroundColor(dark);

            ReplaceEntry($"{scheme}HueLightBrush", new SolidColorBrush(light));
            ReplaceEntry($"{scheme}HueLightForegroundBrush", new SolidColorBrush(lightForeground));
            ReplaceEntry($"{scheme}HueMidBrush", new SolidColorBrush(mid));
            ReplaceEntry($"{scheme}HueMidForegroundBrush", new SolidColorBrush(midForeground));
            ReplaceEntry($"{scheme}HueDarkBrush", new SolidColorBrush(dark));
            ReplaceEntry($"{scheme}HueDarkForegroundBrush", new SolidColorBrush(darkForeground));
        }
    }    
}
