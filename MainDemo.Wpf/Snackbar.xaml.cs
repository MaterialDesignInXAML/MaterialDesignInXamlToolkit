using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MaterialDesignThemes.Wpf;

namespace MaterialDesignColors.WpfExample
{
    public partial class Snackbar : UserControl
    {
        private enum SnackbarDemoMode
        {
            Simple,
            ShowSecondAfterDelay,
            Multiline
        }

        private SnackbarDemoMode? _mode;

        public Snackbar()
        {
            _mode = null;

            InitializeComponent();

            //manualSnackbar.DataContext = manualToggle;
        }

        private void ShowSimpleSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            _mode = SnackbarDemoMode.Simple;

            //snackbar.ActionLabel = null;
            //snackbar.VisibilityTimeout = 3000;

            // .NET caches and reuses string constants
            //     without explicitly create a new string object, calling the setter will not raise a property changed after the second call
            //     (reference to same string in memory)
            //snackbar.Content = new string("This is a simple Snackbar.".ToCharArray());
        }

        private void ShowSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            _mode = SnackbarDemoMode.ShowSecondAfterDelay;

            //snackbar.ActionLabel = "HELLO";
            //snackbar.VisibilityTimeout = 3000;
            //snackbar.Content = new string("Hello from the Snackbar!".ToCharArray());
        }

        private void ShowMultilineSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            _mode = SnackbarDemoMode.Multiline;

            //snackbar.ActionLabel = "GOT IT";
            //snackbar.VisibilityTimeout = 6000;
            //snackbar.Content = new string("The specs says that the maximum width should be 568dp. However there sould be at most only two lines of text.".ToCharArray());
        }

        private async void SnackbarActionClickHandler(object sender, RoutedEventArgs args)
        {
            if (_mode == SnackbarDemoMode.Simple || _mode == SnackbarDemoMode.Multiline)
            {
                await DialogHost.Show(Resources["dialogContent"], "RootDialog");
            }
            else if (_mode == SnackbarDemoMode.ShowSecondAfterDelay)
            {
                await Task.Delay(2000);

                _mode = null;

                //snackbar.ActionLabel = "BYE";
                //snackbar.Content = new string("A second hello from the Snackbar!".ToCharArray());
            }
        }
    }
}
