using System;
using MahApps.Metro;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    internal static class BaseThemeExtensions
    {
        public static string GetMahAppsBaseColorScheme(this BaseTheme baseTheme)
        {
            return baseTheme switch
            {
                Wpf.BaseTheme.Light => ThemeManager.BaseColorLight,
                Wpf.BaseTheme.Dark => ThemeManager.BaseColorDark,
                _ => throw new InvalidOperationException()
            };
        }
    }
}