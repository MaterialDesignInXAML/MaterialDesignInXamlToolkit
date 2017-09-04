using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void StandardRadioButton_OnChecked(object sender, RoutedEventArgs e) {
            DrawerHost.OpenMode = DrawerHost.DrawerHostOpenMode.Standard;
            OpenAllDrawerButton.IsEnabled = true;
        }

        private void StaysOpenRadioButton_OnChecked(object sender, RoutedEventArgs e) {
            DrawerHost.OpenMode = DrawerHost.DrawerHostOpenMode.StaysOpen;
            OpenAllDrawerButton.IsEnabled = true;
        }

        private void PinnedRadioButton_OnChecked(object sender , RoutedEventArgs e) {
            DrawerHost.OpenMode = DrawerHost.DrawerHostOpenMode.Pinned;
            OpenAllDrawerButton.IsEnabled = false;
        }
    }
}
