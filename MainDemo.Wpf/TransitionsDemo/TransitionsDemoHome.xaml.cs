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

namespace MaterialDesignDemo.TransitionsDemo
{
    /// <summary>
    /// Interaction logic for TransitionsDemoHome.xaml
    /// </summary>
    public partial class TransitionsDemoHome : UserControl
    {
        public TransitionsDemoHome()
        {
            InitializeComponent();
			XamlDisplayerPanel.Initialize(new SourceRouter("TransitionsDemo/"+this.GetType().Name).GetSource());
        }
    }
}
