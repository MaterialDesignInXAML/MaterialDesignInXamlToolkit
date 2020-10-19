using MaterialDesignColors.Recommended;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using System.Windows;
using MaterialDesignColors;
using ShowMeTheXAML;

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
        }
    }
}
