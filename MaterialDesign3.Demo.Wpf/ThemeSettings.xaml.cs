using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class ThemeSettings
    {
        public ThemeSettings()
        {
            DataContext = new ThemeSettingsViewModel();
            InitializeComponent();
        }
    }
}
