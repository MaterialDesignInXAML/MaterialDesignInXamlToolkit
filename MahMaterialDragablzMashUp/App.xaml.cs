using MaterialDesignColors.Recommended;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using System.Windows;
using MaterialDesignColors;

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
