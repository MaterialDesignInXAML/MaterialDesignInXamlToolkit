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
        public Snackbar()
        {
            InitializeComponent();
        }

        private async void ShowSimpleSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            await SnackbarHost.ShowAsync("RootSnackbarHost", "This is a simple Snackbar.");
        }

        private async void ShowSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            await SnackbarHost.ShowAsync("RootSnackbarHost", "Hello from the Snackbar!", new SnackbarAction("HELLO", async (object s, RoutedEventArgs a) => {
                await Task.Delay(2000);

                await SnackbarHost.ShowAsync("RootSnackbarHost", "A second hello from the Snackbar!", new SnackbarAction("BYE"));
            }));
        }

        private async void ShowMultilineSnackbarButtonClickHandler(object sender, RoutedEventArgs args)
        {
            await SnackbarHost.ShowAsync(
                    "RootSnackbarHost",
                    "The specs says that the maximum with should be 568dp. However there sould be at most only two lines of text.",
                    new SnackbarAction("GOT IT"));
        }
    }
}
