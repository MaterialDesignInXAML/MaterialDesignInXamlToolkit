using MaterialDesignColors;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
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

        public void ReplacePrimaryColor(Swatch swatch, bool mahapps = false)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            var list = swatch.PrimaryHues.ToList();
            var light = list[2];
            var mid = swatch.ExemplarHue;
            var dark = list[7];

            foreach (var color in swatch.PrimaryHues)
            {
                ReplaceEntry(color.Name, color.Color, null, false);
                ReplaceEntry(color.Name + "Foreground", color.Foreground, null, false);
            }

            ReplaceEntry("PrimaryHueLightBrush", new SolidColorBrush(light.Color));
            ReplaceEntry("PrimaryHueLightForegroundBrush", new SolidColorBrush(light.Foreground));
            ReplaceEntry("PrimaryHueMidBrush", new SolidColorBrush(mid.Color));
            ReplaceEntry("PrimaryHueMidForegroundBrush", new SolidColorBrush(mid.Foreground));
            ReplaceEntry("PrimaryHueDarkBrush", new SolidColorBrush(dark.Color));
            ReplaceEntry("PrimaryHueDarkForegroundBrush", new SolidColorBrush(dark.Foreground));

            if (mahapps)
            {
                ReplaceEntry("HighlightBrush", new SolidColorBrush(dark.Color));
                ReplaceEntry("AccentColorBrush", new SolidColorBrush(list[5].Color));
                ReplaceEntry("AccentColorBrush2", new SolidColorBrush(list[4].Color));
                ReplaceEntry("AccentColorBrush3", new SolidColorBrush(list[3].Color));
                ReplaceEntry("AccentColorBrush4", new SolidColorBrush(list[2].Color));
                ReplaceEntry("WindowTitleColorBrush", new SolidColorBrush(dark.Color));
                ReplaceEntry("AccentSelectedColorBrush", new SolidColorBrush(list[5].Foreground));
                ReplaceEntry("ProgressBrush", new LinearGradientBrush(dark.Color, list[3].Color, 90.0));
                ReplaceEntry("CheckmarkFill", new SolidColorBrush(list[5].Color));
                ReplaceEntry("RightArrowFill", new SolidColorBrush(list[5].Color));
                ReplaceEntry("IdealForegroundColorBrush", new SolidColorBrush(list[5].Foreground));
                ReplaceEntry("IdealForegroundDisabledBrush", new SolidColorBrush(dark.Color) { Opacity = .4 });
            }
        }

        public void ReplacePrimaryColor(string name, bool mahapps = false)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            var swatch = new SwatchesProvider().Swatches.FirstOrDefault(
                s => string.Compare(s.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (swatch == null)
                throw new ArgumentException($"No such swatch '{name}'", nameof(name));

            ReplacePrimaryColor(swatch, mahapps);
        }

        public void ReplaceAccentColor(Swatch swatch)
        {
            if (swatch == null) throw new ArgumentNullException(nameof(swatch));

            foreach (var color in swatch.AccentHues)
            {
                ReplaceEntry(color.Name, color.Color,null, false);
                ReplaceEntry(color.Name + "Foreground", color.Foreground, null, false);
            }

            ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(swatch.AccentExemplarHue.Color));
            ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(swatch.AccentExemplarHue.Foreground));
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

        /// <summary>
        /// Replaces a certain entry anywhere in the parent dictionary and its merged dictionaries
        /// </summary>
        /// <param name="entryName">The entry to replace</param>
        /// <param name="newValue">The new entry value</param>
        /// <param name="parentDictionary">The root dictionary to start searching at. Null means using Application.Current.Resources</param>
        /// <returns>Weather the value was replaced (true) or not (false)</returns>
        private static bool ReplaceEntry(object entryName, object newValue, ResourceDictionary parentDictionary = null, bool animate = true)
        {
            const int DURATION_MS = 500; //Change the value if needed
            
            if (parentDictionary == null)
                parentDictionary = Application.Current.Resources;
            
            if (parentDictionary.Contains(entryName))
            {
                if (animate) //Fade animation is enabled
                {
                    try
                    {
                        ColorAnimation animation = new ColorAnimation()
                        {
                            From = (Color)parentDictionary[entryName],//The old color
                            To = (Color)newValue, //The new color
                            Duration = new Duration(new TimeSpan(0,0,0,0,DURATION_MS)) //Set the duration
                        };
                        parentDictionary[entryName] = parentDictionary[entryName] == null ? new SolidColorBrush() : parentDictionary[entryName]; //Set the resource as a new SolidColorBrush if null.
                        (parentDictionary[entryName] as SolidColorBrush).BeginAnimation(SolidColorBrush.ColorProperty, animation); //Begin the animation
                    }
                    catch
                    {
                        System.Diagnostics.Debug.WriteLine(@"The argument :" + nameof(newValue) + " is not a brush.");
                        goto setval; //Set the value normally if type is incorrect
                    }
                }
                else
            setval: parentDictionary[entryName] = newValue; //Set value normally
                return true;
            }
            foreach (var dictionary in parentDictionary.MergedDictionaries)
                if (ReplaceEntry(entryName, newValue, dictionary))
                    return true;
                    
            return false;
        }
    }
}
