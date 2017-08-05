using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MaterialDesignDemo.Helper;

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
			XamlDisplayerPanel.Initialize(new SourceRouter(this.GetType().Name).GetSource());
        }

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/James_Willock");
        }
    }
}
