namespace MaterialDesignThemes.UITests.Samples.Theme
{
    /// <summary>
    /// Interaction logic for ColorAdjustWindow.xaml
    /// </summary>
    public partial class ColorAdjustWindow : Window
    {
        public ColorAdjustWindow() => InitializeComponent();

        private void ChangeThemeClick(object sender, RoutedEventArgs e)
        {
            Wpf.Theming.Theme theme = ThemeManager.GetApplicationTheme();
            if (((ToggleButton)sender).IsChecked == true)
            {
                theme.SetBaseTheme(Wpf.Theming.Theme.Dark);
            }
            else
            {
                theme.SetBaseTheme(Wpf.Theming.Theme.Light);
            }
            ThemeManager.SetApplicationTheme(theme);
        }
    }
}
