using System;
using ControlzEx.Theming;
using MaterialDesignThemes.Wpf;

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
                _ => throw new InvalidOperationException()
            };
        }
    }
}