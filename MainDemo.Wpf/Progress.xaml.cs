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
using MaterialDesignDemo.Helper;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Progress.xaml
    /// </summary>
    public partial class Progress : UserControl
    {
        public Progress()
        {
            InitializeComponent();
			XamlDisplayerPanel.Initialize(new SourceRouter(this.GetType().Name).GetSource());
        }
    }
}
