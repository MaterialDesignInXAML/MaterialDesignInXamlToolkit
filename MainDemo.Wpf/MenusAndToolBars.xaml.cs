using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for MenusAndToolBars.xaml
    /// </summary>
    public partial class MenusAndToolBars : UserControl
    {
        public MenusAndToolBars()
        {
            InitializeComponent();
        }

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/James_Willock");
        }
    }
}
