using System;
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
            resourceDictionary.SetBrush("AccentBaseColorBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentColorBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentColorBrush2", theme.PrimaryMid.Color, 0.8);
            resourceDictionary.SetBrush("AccentColorBrush3", theme.PrimaryLight.Color);
            resourceDictionary.SetBrush("AccentColorBrush4", theme.PrimaryLight.Color, 0.8);
            resourceDictionary.SetBrush("WindowTitleColorBrush", theme.PrimaryDark.Color);
            resourceDictionary.SetBrush("AccentSelectedColorBrush", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("ProgressBrush", new LinearGradientBrush(theme.PrimaryDark.Color, theme.PrimaryMid.Color, 90.0));
            resourceDictionary.SetBrush("CheckmarkFill", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("RightArrowFill", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("IdealForegroundColorBrush", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("IdealForegroundDisabledBrush", theme.PrimaryDark.GetForegroundColor(), 0.4);

            resourceDictionary.SetBrush("MetroDataGrid.HighlightBrush", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("MetroDataGrid.HighlightTextBrush", theme.PrimaryMid.GetForegroundColor());
            resourceDictionary.SetBrush("MetroDataGrid.MouseOverHighlightBrush", theme.PrimaryLight.Color);
            resourceDictionary.SetBrush("MetroDataGrid.FocusBorderBrush", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("MetroDataGrid.InactiveSelectionHighlightBrush", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("MetroDataGrid.InactiveSelectionHighlightTextBrush", theme.PrimaryMid.GetForegroundColor());

            resourceDictionary.SetBrush("MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchBrush.Win10", theme.PrimaryMid.Color);
            resourceDictionary.SetBrush("MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchMouseOverBrush.Win10", theme.PrimaryMid.Color, 0.8);
            resourceDictionary.SetBrush("MahApps.Metro.Brushes.ToggleSwitchButton.ThumbIndicatorCheckedBrush.Win10", theme.PrimaryMid.GetForegroundColor(), 0.8);
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
