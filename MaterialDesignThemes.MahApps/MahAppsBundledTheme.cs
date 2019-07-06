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

            BaseTheme baseTheme = foreground == Colors.Black ? BaseTheme.Light : BaseTheme.Dark;
            resourceDictionary.SetMahApps(newTheme, baseTheme);
        }
    }
}