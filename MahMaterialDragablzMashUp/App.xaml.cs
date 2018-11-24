using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace MahMaterialDragablzMashUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.WithMaterialDesign(BaseTheme.Light, PrimaryColor.DeepPurple, AccentColor.Lime)
                .WithMahApps();
            base.OnStartup(e);
        }
    }
}
