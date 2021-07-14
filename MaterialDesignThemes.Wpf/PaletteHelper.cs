using System;
using System.Linq;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        public virtual ITheme GetTheme()
        {
            if (Application.Current is null)
                throw new InvalidOperationException($"Cannot get theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetTheme)} on the appropriate resource dictionary instead.");
            return GetResourceDictionary().GetTheme();
        }

        public virtual void SetTheme(ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (Application.Current is null)
                throw new InvalidOperationException($"Cannot set theme outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.SetTheme)} on the appropriate resource dictionary instead.");

            GetResourceDictionary().SetTheme(theme);
        }

        public virtual IThemeManager? GetThemeManager()
        {
            if (Application.Current is null)
                throw new InvalidOperationException($"Cannot get ThemeManager outside of a WPF application. Use {nameof(ResourceDictionaryExtensions)}.{nameof(ResourceDictionaryExtensions.GetThemeManager)} on the appropriate resource dictionary instead.");
            return GetResourceDictionary().GetThemeManager();
        }

        private static ResourceDictionary GetResourceDictionary()
            => Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x is IMaterialDesignThemeDictionary) ??
                Application.Current.Resources;
    }
}
