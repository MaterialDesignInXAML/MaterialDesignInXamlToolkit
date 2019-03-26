using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public static class ResourceDictionaryExtensions
    {
        public static ResourceDictionary WithTheme(this ResourceDictionary resourceDictionary, BaseTheme theme)
        {
            var baseTheme = theme == BaseTheme.Light ? MaterialDesignTheme.Light : MaterialDesignTheme.Dark;
            return resourceDictionary.WithTheme(baseTheme);
        }

        public static ResourceDictionary WithTheme(this ResourceDictionary resourceDictionary, IBaseTheme theme)
        {
            // TODO this is just here because BaseTheme.Inherit doesn't have a sensible alternative than null at the moment
            if (theme == null) return resourceDictionary;

            foreach (var p in theme.GetColorProperties())
            {
                resourceDictionary.ReplaceEntry(p.Name, new SolidColorBrush((Color)p.GetValue(theme)));
            }

            return resourceDictionary;
        }

        private static IEnumerable<PropertyInfo> GetColorProperties(this object obj)
        {
            return obj.GetType().GetProperties().Where(o => o.PropertyType == typeof(Color));
        }

        public static ResourceDictionary WithPrimaryColor(this ResourceDictionary resourceDictionary, MaterialDesignColor primaryColor)
        {
            var color = SwatchHelper.Lookup(primaryColor);
            return resourceDictionary.WithPrimaryColor(color);
        }

        public static ResourceDictionary WithPrimaryColor(this ResourceDictionary resourceDictionary, Color color)
        {
            return resourceDictionary.WithPalette(PaletteName.Primary.ToString(), color);
        }

        public static ResourceDictionary WithSecondaryColor(this ResourceDictionary resourceDictionary, MaterialDesignColor secondaryColor)
        {
            var color = SwatchHelper.Lookup(secondaryColor);
            return resourceDictionary.WithSecondaryColor(color);
        }

        public static ResourceDictionary WithSecondaryColor(this ResourceDictionary resourceDictionary, Color color)
        {
            return resourceDictionary.WithPalette(PaletteName.Secondary.ToString(), color);
        }

        public static ResourceDictionary WithPalette(this ResourceDictionary resourceDictionary, string name, Color color)
        {
            return resourceDictionary.WithPalette(new ColorPalette(name, color));
        }

        public static ResourceDictionary WithPalette(this ResourceDictionary resourceDictionary, ColorPalette palette)
        {
            var name = palette.Name;
            resourceDictionary.ReplaceEntry($"{name}HueLightBrush", new SolidColorBrush(palette.Light));
            resourceDictionary.ReplaceEntry($"{name}HueLightForegroundBrush", new SolidColorBrush(palette.LightForeground));
            resourceDictionary.ReplaceEntry($"{name}HueMidBrush", new SolidColorBrush(palette.Mid));
            resourceDictionary.ReplaceEntry($"{name}HueMidForegroundBrush", new SolidColorBrush(palette.MidForeground));
            resourceDictionary.ReplaceEntry($"{name}HueDarkBrush", new SolidColorBrush(palette.Dark));
            resourceDictionary.ReplaceEntry($"{name}HueDarkForegroundBrush", new SolidColorBrush(palette.DarkForeground));

            // moved this yet again, as long as it covers the migrtion path...
            if(name == PaletteName.Secondary.ToString())
            {
                // backwards compatability for now
                resourceDictionary.ReplaceEntry("SecondaryAccentBrush", new SolidColorBrush(palette.Mid));
                resourceDictionary.ReplaceEntry("SecondaryAccentForegroundBrush", new SolidColorBrush(palette.MidForeground));
            }

            return resourceDictionary;
        }

        public static ResourceDictionary WithColor(this ResourceDictionary resourceDictionary, string name, Color color)
        {
            resourceDictionary.ReplaceEntry(name, new SolidColorBrush(color));
            return resourceDictionary;
        }

        /// <summary>
        /// Replaces a certain entry anywhere in the source dictionary and its merged dictionaries
        /// </summary>
        /// <param name="sourceDictionary">The source dictionary to start with</param>
        /// <param name="name">The entry to replace</param>
        /// <param name="value">The new entry value</param>
        public static void ReplaceEntry(this ResourceDictionary sourceDictionary, object name, object value)
        {
            if (sourceDictionary.Contains(name))
            {
                if (sourceDictionary[name] is SolidColorBrush brush &&
                    !brush.IsFrozen && value is SolidColorBrush newBrush)
                {
                    var animation = new ColorAnimation {
                        From = brush.Color,
                        To = newBrush.Color,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                }
                else
                {
                    sourceDictionary[name] = value; //Set value normally
                }
            }

            foreach (var dictionary in sourceDictionary.MergedDictionaries)
            {
                ReplaceEntry(dictionary, name, value);
            }

        }
    }
}