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
using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            new CardsWindow().Show();
            new ListsWindow().Show();
            new PaletteSelectorWindow()
            {
                DataContext = new PaletteSelectorViewModel()
            }.Show();
        }

	    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
	    {
		    FieldsRadioButton.IsChecked = ((ToggleButton) sender).IsChecked;
			ButtonsRadioButton.IsChecked = !((ToggleButton)sender).IsChecked;
		}

        private void ProgressButton_OnClick(object sender, RoutedEventArgs e)
        {
            new ProgressWindow().Show();
        }
    }
}
