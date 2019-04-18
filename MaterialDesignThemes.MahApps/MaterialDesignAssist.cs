using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public static class MaterialDesignAssist
    {
        public static void SetMahApps(this ResourceDictionary resourceDictionary, ITheme theme, BaseTheme baseTheme)
        {
            resourceDictionary.SetMahAppsBaseTheme(baseTheme);

            resourceDictionary.SetBrush("HighlightBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentColorBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentColorBrush2", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("AccentColorBrush3", theme.PrimaryLight.Color);
            resourceDictionary.SetBrush("AccentColorBrush4", theme.PrimaryLight.Color, 0.82);
            resourceDictionary.SetBrush("WindowTitleColorBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentSelectedColorBrush", theme.PrimaryDark.GetForegroundColor());
            resourceDictionary.SetBrush("ProgressBrush", new LinearGradientBrush(theme.PrimaryDark.Color, theme.PrimaryMid.Color, 90.0));
            resourceDictionary.SetBrush("CheckmarkFill", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("RightArrowFill", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("IdealForegroundColorBrush", theme.PrimaryDark.GetForegroundColor());
            resourceDictionary.SetBrush("IdealForegroundDisabledBrush", theme.PrimaryDark.GetForegroundColor(), 0.4);
        }

        private static void SetBrush(this ResourceDictionary sourceDictionary, string name, Color value, double opacity = 1.0)
        {
            if (sourceDictionary == null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (sourceDictionary[name] is SolidColorBrush brush)
            {
                if (brush.Color == value) return;

                if (!brush.IsFrozen)
                {
                    var animation = new ColorAnimation {
                        From = brush.Color,
                        To = value,
                        Duration = new Duration(TimeSpan.FromMilliseconds(300))
                    };
                    brush.BeginAnimation(SolidColorBrush.ColorProperty, animation);
                    return;
                }
            }
            sourceDictionary[name] = new SolidColorBrush(value) { Opacity = opacity }; //Set value directly
        }

        private static void SetBrush(this ResourceDictionary sourceDictionary, string name, Brush value)
        {
            if (sourceDictionary == null) throw new ArgumentNullException(nameof(sourceDictionary));
            if (name == null) throw new ArgumentNullException(nameof(name));

            sourceDictionary[name] = value; //Set value directly
        }

        internal static void SetMahAppsBaseTheme(this ResourceDictionary resourceDictionary, BaseTheme baseTheme)
        {
            if (resourceDictionary == null) throw new ArgumentNullException(nameof(resourceDictionary));

            var existingMahAppsResourceDictionary = resourceDictionary.MergedDictionaries
                .Where(rd => rd.Source != null)
                .SingleOrDefault(rd => Regex.Match(rd.Source.OriginalString, @"(\/MahApps.Metro;component\/Styles\/Accents\/)((BaseLight)|(BaseDark))").Success);
            if (existingMahAppsResourceDictionary != null)
            {
                resourceDictionary.MergedDictionaries.Remove(existingMahAppsResourceDictionary);
            }

            var source = $"pack://application:,,,/MahApps.Metro;component/Styles/Accents/{(baseTheme == BaseTheme.Dark ? "BaseDark" : "BaseLight")}.xaml";
            var newMahAppsResourceDictionary = new ResourceDictionary { Source = new Uri(source) };

            resourceDictionary.MergedDictionaries.Add(newMahAppsResourceDictionary);
        }

    }
}
