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
using System.Windows.Shapes;
using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for ListsWindow.xaml
    /// </summary>
    public partial class ListsWindow : Window
    {
        public ListsWindow()
        {
            InitializeComponent();

            DataContext = new ListsWindowViewModel();
        }
    }
}
