using System;
using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;
using Theme = MaterialDesignThemes.Wpf.Theme;

namespace MaterialDesignThemes.MahApps
{
    internal static class BaseThemeExtensions
    {
        public static string GetMahAppsBaseColorScheme(this BaseTheme baseTheme)
        {
            return baseTheme switch
            {
                BaseTheme.Light => ThemeManager.BaseColorLightConst,
                BaseTheme.Dark => ThemeManager.BaseColorDarkConst,
                BaseTheme.Inherit => Theme.GetSystemTheme() switch
                    {
                        BaseTheme.Dark => ThemeManager.BaseColorDarkConst,
                        _ => ThemeManager.BaseColorLightConst
                    },
                _ => throw new InvalidOperationException()
            };
        }
    }
}