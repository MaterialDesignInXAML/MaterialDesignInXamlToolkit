using System.Windows;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;

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
            var helper = new PaletteHelper();
            var theme = helper.GetTheme();
            if (((ToggleButton)sender).IsChecked == true)
            {
                theme.SetBaseTheme(Wpf.Theme.Dark);
            }
            else
            {
                theme.SetBaseTheme(Wpf.Theme.Light);
            }
            helper.SetTheme(theme);
        }
    }
}
