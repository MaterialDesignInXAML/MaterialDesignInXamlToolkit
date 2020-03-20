using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public class MahAppsBundledTheme : BundledTheme
    {
        private static Guid GeneratedKey { get; } = Guid.NewGuid();

        protected override void ApplyTheme(ITheme theme)
        {
            base.ApplyTheme(theme);
            if (TryGetResourceDictionaries(theme, out ResourceDictionary light, out ResourceDictionary dark))
            {
                switch (BaseTheme)
                {
                    case Wpf.BaseTheme.Light:
                        MergedDictionaries.Add(light);
                        break;
                    case Wpf.BaseTheme.Dark:
                        MergedDictionaries.Add(dark);
                        break;
                }

                IThemeManager themeManager = this.GetThemeManager();
                if (themeManager != null)
                {
                    themeManager.ThemeChanged += ThemeManagerOnThemeChanged;
                }
            }
        }

        private bool TryGetResourceDictionaries(ITheme theme, out ResourceDictionary light, out ResourceDictionary dark)
        {
            if (PrimaryColor is PrimaryColor primaryColor &&
                SecondaryColor is SecondaryColor secondaryColor &&
                BaseTheme is BaseTheme baseTheme)
            {
                light = GetResourceDictionary(theme, primaryColor, secondaryColor, Wpf.BaseTheme.Light);
                dark = GetResourceDictionary(theme, primaryColor, secondaryColor, Wpf.BaseTheme.Dark);
                return true;
            }
            else
            {
                light = null;
                dark = null;
                return false;
            }

            static ResourceDictionary GetResourceDictionary(ITheme theme, PrimaryColor primaryColor, SecondaryColor secondaryColor, BaseTheme baseTheme)
            {
                string baseColorScheme = baseTheme.GetMahAppsBaseColorScheme();
                string colorScheme = $"MaterialDesign.{primaryColor}.{secondaryColor}";

                ResourceDictionary rv;
                if (ThemeManager.Themes.FirstOrDefault(x => x.BaseColorScheme == baseColorScheme && x.ColorScheme == primaryColor.ToString()) is global::MahApps.Metro.Theme mahAppsTheme)
                {
                    rv = mahAppsTheme.Resources;
                    rv.SetMahApps(theme, baseTheme);
                    return rv;
                }

                rv = new ResourceDictionary();
                rv[GeneratedKey] = GeneratedKey;
                rv.SetMahApps(theme, baseTheme);

                rv["Theme.Name"] = $"MaterialDesign.{baseColorScheme}.{primaryColor}.{secondaryColor}";
                rv["Theme.DisplayName"] = $"Material Design {primaryColor} with {secondaryColor}";
                rv["Theme.ColorScheme"] = colorScheme;
                ThemeManager.AddTheme(rv);

                return rv;
            }
        }

        private void ThemeManagerOnThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = e.ResourceDictionary;

            ITheme newTheme = e.NewTheme;

            var foreground = newTheme.Background.ContrastingForegroundColor();

            BaseTheme baseTheme = foreground == Colors.Black ? Wpf.BaseTheme.Light : Wpf.BaseTheme.Dark;

            if (TryGetResourceDictionaries(newTheme, out ResourceDictionary light, out ResourceDictionary dark))
            {
                for (int i = resourceDictionary.MergedDictionaries.Count - 1; i >= 0; i--)
                {
                    var dictionary = resourceDictionary.MergedDictionaries[i];
                    if (dictionary.Keys.Cast<object>().OfType<Guid>().Any(x => x == GeneratedKey))
                    {
                        resourceDictionary.MergedDictionaries.RemoveAt(i);
                    }
                }
                switch (baseTheme)
                {
                    case Wpf.BaseTheme.Light:
                        resourceDictionary.MergedDictionaries.Add(light);
                        break;
                    case Wpf.BaseTheme.Dark:
                        resourceDictionary.MergedDictionaries.Add(dark);
                        break;
                }
            }
        }
    }
}