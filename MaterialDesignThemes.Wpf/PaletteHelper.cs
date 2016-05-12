using MaterialDesignColors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Runtime.InteropServices;
namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
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
                allHues[palette.PrimaryDarkHueIndex],
                allHues);

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
            var light = list[palette.PrimaryLightHueIndex];
            var mid = list[palette.PrimaryMidHueIndex];
            var dark = list[palette.PrimaryDarkHueIndex];

            ReplacePrimaryColor(swatch, light, mid, dark, list);
        }      

        public virtual void ReplacePrimaryColor(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
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

            var hue = swatch.AccentHues.ElementAt(palette.AccentHueIndex);

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

            //TODO could cache this statically
            var swatchesProvider = new SwatchesProvider();
            var swatchByPrimaryHueIndex = swatchesProvider
                .Swatches
                .SelectMany(s => s.PrimaryHues.Select(h => new {s, h}))
                .ToDictionary(a => a.h.Color, a => a.s);
            var swatchByAccentHueIndex = swatchesProvider
                .Swatches
                .Where(s => s.IsAccented)
                .SelectMany(s => s.AccentHues.Select(h => new { s, h }))
                .ToDictionary(a => a.h.Color, a => a.s);

            var primaryMidBrush = GetBrush("PrimaryHueMidBrush");
            var accentBrush = GetBrush("SecondaryAccentBrush");

            Swatch primarySwatch;
            if (!swatchByPrimaryHueIndex.TryGetValue(primaryMidBrush.Color, out primarySwatch))
                throw new InvalidOperationException("PrimaryHueMidBrush is not from standard swatches");
            Swatch accentSwatch;
            if (!swatchByAccentHueIndex.TryGetValue(accentBrush.Color, out accentSwatch))
                throw new InvalidOperationException("SecondaryAccentBrush is not from standard swatches");

            var primaryLightBrush = GetBrush("PrimaryHueLightBrush");
            var primaryDarkBrush = GetBrush("PrimaryHueDarkBrush");

            var primaryLightHueIndex = GetHueIndex(primarySwatch, primaryLightBrush.Color, false);
            var primaryMidHueIndex = GetHueIndex(primarySwatch, primaryMidBrush.Color, false);
            var primaryDarkHueIndex = GetHueIndex(primarySwatch, primaryDarkBrush.Color, false);
            var accentHueIndex = GetHueIndex(accentSwatch, accentBrush.Color, true);

            return new Palette(primarySwatch, accentSwatch, primaryLightHueIndex, primaryMidHueIndex, primaryDarkHueIndex, accentHueIndex);
        }

        private static void ReplacePrimaryColor(Swatch swatch, Hue light, Hue mid, Hue dark, IList<Hue> allHues)
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
            ReplaceEntry("AccentColorBrush", new SolidColorBrush(allHues[5].Color));
            ReplaceEntry("AccentColorBrush2", new SolidColorBrush(allHues[4].Color));
            ReplaceEntry("AccentColorBrush3", new SolidColorBrush(allHues[3].Color));
            ReplaceEntry("AccentColorBrush4", new SolidColorBrush(allHues[2].Color));
            ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(allHues[5].Foreground));
            ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark.Color, allHues[3].Color, 90.0));
            ReplaceEntry("CheckmarkFill", new SolidColorBrush(allHues[5].Color));
            ReplaceEntry("RightArrowFill", new SolidColorBrush(allHues[5].Color));
            ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(allHues[5].Foreground));
            ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });
        }

        private static int GetHueIndex(Swatch swatch, Color color, bool isAccent)
        {
            var x = (isAccent ? swatch.AccentHues : swatch.PrimaryHues).Select((h, i) => new {h, i})
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
            var solidColorBrush = group.First().e.Value as SolidColorBrush;
            if (solidColorBrush == null)
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
        private static void ReplaceEntry(object entryName, object newValue, ResourceDictionary parentDictionary = null)
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
    }    
}
