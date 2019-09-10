using System.Windows;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignThemes.MahApps
{
    public class MahAppsCustomColorTheme : CustomColorTheme
    {
        protected override void ApplyTheme(ITheme theme)
        {
            base.ApplyTheme(theme);
            if (BaseTheme is BaseTheme baseTheme)
            {
                this.SetMahApps(theme, baseTheme);
                IThemeManager themeManager = this.GetThemeManager();
                if (themeManager != null)
                {
                    themeManager.ThemeChanged += ThemeManagerOnThemeChanged;
                }
            }
        }

        private static void ThemeManagerOnThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            ResourceDictionary resourceDictionary = e.ResourceDictionary;

            ITheme newTheme = e.NewTheme;

            var foreground = newTheme.Background.ContrastingForegroundColor();

            BaseTheme baseTheme = foreground == Colors.Black ? Wpf.BaseTheme.Light : Wpf.BaseTheme.Dark;
            resourceDictionary.SetMahApps(newTheme, baseTheme);
        }
    }
}