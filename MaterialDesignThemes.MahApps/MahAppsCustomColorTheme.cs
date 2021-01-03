using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using System.Diagnostics.CodeAnalysis;
using Theme = ControlzEx.Theming.Theme;

namespace MaterialDesignThemes.MahApps
{
    public class MahAppsCustomColorTheme : CustomColorTheme
    {
        private static Guid GeneratedKey { get; } = Guid.NewGuid();

        protected override void ApplyTheme(ITheme theme)
        {
            base.ApplyTheme(theme);
            if (TryGetResourceDictionaries(theme, out ResourceDictionary? light, out ResourceDictionary? dark))
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

                if (this.GetThemeManager() is IThemeManager themeManager)
                {
                    themeManager.ThemeChanged += ThemeManagerOnThemeChanged;
                }
            }
        }

        private bool TryGetResourceDictionaries(ITheme theme, out ResourceDictionary? light, out ResourceDictionary? dark)
        {
            if (PrimaryColor is Color primaryColor &&
                SecondaryColor is Color secondaryColor &&
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

            static ResourceDictionary GetResourceDictionary(ITheme theme, Color primaryColor, Color secondaryColor, BaseTheme baseTheme)
            {
                string baseColorScheme = baseTheme.GetMahAppsBaseColorScheme();
                string colorScheme = $"MaterialDesign.{primaryColor}.{secondaryColor}";

                ResourceDictionary rv;
                if (ThemeManager.Current.Themes.FirstOrDefault(x => x.BaseColorScheme == baseColorScheme && x.ColorScheme == colorScheme) is ControlzEx.Theming.Theme mahAppsTheme)
                {
                    rv = mahAppsTheme.Resources;
                    rv.SetMahApps(theme, baseTheme);
                    return rv;
                }

                rv = new ResourceDictionary
                {
                    [GeneratedKey] = GeneratedKey
                };
                rv.SetMahApps(theme, baseTheme);

                rv[Theme.ThemeNameKey] = $"MaterialDesign.{primaryColor}.{secondaryColor}.{baseColorScheme}";
                rv[Theme.ThemeDisplayNameKey] = $"Material Design {primaryColor} with {secondaryColor}";
                rv[Theme.ThemeColorSchemeKey] = colorScheme;
                rv[Theme.ThemeBaseColorSchemeKey] = baseColorScheme;
                var themeInstance = new Theme(new LibraryTheme(rv, null));
                rv[Theme.ThemeInstanceKey] = themeInstance;
                ThemeManager.Current.AddTheme(themeInstance);

                return rv;
            }
        }

        private void ThemeManagerOnThemeChanged(object? sender, Wpf.ThemeChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = e.ResourceDictionary;

            ITheme newTheme = e.NewTheme;

            BaseTheme baseTheme = newTheme.Background.IsLightColor()
                ? Wpf.BaseTheme.Light
                : Wpf.BaseTheme.Dark;

            if (TryGetResourceDictionaries(newTheme, out ResourceDictionary? light, out ResourceDictionary? dark))
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