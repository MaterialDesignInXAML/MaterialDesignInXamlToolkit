using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
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
