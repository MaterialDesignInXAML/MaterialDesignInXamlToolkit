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

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Typography.xaml
    /// </summary>
    public partial class Typography : UserControl
    {
        public Typography()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var t = sender as Button;
            var toBeCopied = t.Content.ToString().Split('-')[1];
            toBeCopied = "Style=\"{StaticResource "+ toBeCopied +"}\"";
            Clipboard.SetDataObject(toBeCopied);  
            MainWindow.Snackbar.MessageQueue.Enqueue($"{toBeCopied}\nis copied to clipboard!");
        }
    }
}
