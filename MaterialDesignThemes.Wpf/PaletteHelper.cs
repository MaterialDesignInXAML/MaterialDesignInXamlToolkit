using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class PaletteHelper
    {
        public virtual ITheme GetTheme()
        {
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot get theme outside of a WPF application. Use ResourceDictionaryExtensions.GetTheme on the appropriate resource dictionary instead.");
            return Application.Current.Resources.GetTheme();
        }

        public virtual void SetTheme(ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot set theme outside of a WPF application. Use ResourceDictionaryExtensions.SetTheme on the appropriate resource dictionary instead.");
            Application.Current.Resources.SetTheme(theme);
        }

        public virtual IThemeManager? GetThemeManager()
        {
            if (Application.Current is null)
                throw new InvalidOperationException("Cannot get ThemeManager. Use ResourceDictionaryExtensions.GetThemeManager on the appropriate resource dictionary instead.");
            return Application.Current.Resources.GetThemeManager();
        }
    }
}
