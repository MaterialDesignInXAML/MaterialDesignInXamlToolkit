using System.Windows;
using MaterialDesignColors;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;

namespace MahMaterialDragablzMashUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.WithMaterialDesign(BaseTheme.Light, MaterialDesignColor.DeepPurple, MaterialDesignColor.LimeAccent).WithMahApps();
            base.OnStartup(e);
        }
    }
}
