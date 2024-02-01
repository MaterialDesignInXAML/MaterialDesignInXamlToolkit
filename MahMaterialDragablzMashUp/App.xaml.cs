using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using ShowMeTheXAML;

namespace MahMaterialDragablzMashUp;

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
        //Dragablz is still using the old theme brush names so we forward those here
        Resources["SecondaryAccentBrush"] = new SolidColorBrush(e.NewTheme.SecondaryMid.Color);
        Resources[SystemColors.ControlTextBrushKey] = new SolidColorBrush(e.NewTheme.Foreground);
    }
}
