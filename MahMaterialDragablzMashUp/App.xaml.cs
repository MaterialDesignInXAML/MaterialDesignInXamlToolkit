using MaterialDesignColors.Recommended;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using System.Windows;
using MaterialDesignColors;
using ShowMeTheXAML;
using System.Windows.Media;

namespace MahMaterialDragablzMashUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            XamlDisplay.Init();
            base.OnStartup(e);

            //Add/Update brush used by Dragablz when the theme changes
            //Solution for https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues/2349
            PaletteHelper helper = new PaletteHelper();
            if (helper.GetThemeManager() is { } themeManager)
            {
                themeManager.ThemeChanged += ThemeManager_ThemeChanged;
            }
        }

        private void ThemeManager_ThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            Resources["SecondaryAccentBrush"] = new SolidColorBrush(e.NewTheme.SecondaryMid.Color);
        }
    }
}
