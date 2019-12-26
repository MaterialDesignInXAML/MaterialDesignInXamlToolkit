using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo
{
    /// <summary>
    /// Interaction logic for Drawers.xaml
    /// </summary>
    public partial class Drawers : UserControl
    {
        public Drawers()
        {
            InitializeComponent();
        }

        private void StandardRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawerHost.OpenMode = DrawerHostOpenMode.Standard;
            OpenAllDrawerButton.IsEnabled = true;
        }

        private void StaysOpenRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawerHost.OpenMode = DrawerHostOpenMode.StaysOpen;
            OpenAllDrawerButton.IsEnabled = true;
        }

        private void PinnedRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            DrawerHost.OpenMode = DrawerHostOpenMode.Pinned;
            OpenAllDrawerButton.IsEnabled = false;
        }
    }
}
