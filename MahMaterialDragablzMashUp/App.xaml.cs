using System.Windows.Media;
using MaterialDesignThemes.Wpf.Theming;
using ShowMeTheXAML;
using ThemeChangedEventArgs = MaterialDesignThemes.Wpf.Theming.ThemeChangedEventArgs;

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
            ThemeManager themeManager = new ThemeManager(Resources);

            themeManager.ThemeChanged += ThemeManager_ThemeChanged;
        }

        private void ThemeManager_ThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            Resources["SecondaryAccentBrush"] = new SolidColorBrush(e.NewTheme.SecondaryMid.Color);
        }
    }
}
