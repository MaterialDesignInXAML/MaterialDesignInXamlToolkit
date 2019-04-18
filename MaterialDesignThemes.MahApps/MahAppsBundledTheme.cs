using System.Windows;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public class MahAppsBundledTheme : BundledTheme
    {
        protected override void SetTheme(ITheme theme, ResourceDictionary resourceDictionary)
        {
            base.SetTheme(theme, resourceDictionary);
            resourceDictionary.SetMahApps(theme, BaseTheme);
            IThemeManager themeManager = resourceDictionary.GetThemeManager();
            if (themeManager != null)
            {
                themeManager.ThemeChanged += ThemeManagerOnThemeChanged;
            }
        }

        private void ThemeManagerOnThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = e.ResourceDictionary;

            ITheme newTheme = e.NewTheme;

            var foreground = newTheme.Background.ContrastingForegroundColor();
            if (foreground == Colors.Black)
            {
                //Theme has a light color background
                resourceDictionary.SetMahAppsBaseTheme(BaseTheme.Light);
            }
            else if (foreground == Colors.White)
            {
                //Theme has a dark color background
                resourceDictionary.SetMahAppsBaseTheme(BaseTheme.Dark);
            }
        }
    }
}