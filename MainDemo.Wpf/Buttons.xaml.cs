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
using MaterialDesignColors.WpfExample.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Buttons.xaml
    /// </summary>
    public partial class Buttons : UserControl
    {
        private readonly ICommand _floatingActionDemoCommand;

        public Buttons()
        {
            InitializeComponent();

            _floatingActionDemoCommand = new AnotherCommandImplementation(Execute);
        }

        public ICommand FloatingActionDemoCommand
        {
            get { return _floatingActionDemoCommand; }
        }

        private void Execute(object o)
        {
            Console.WriteLine("Floating action button command. - " + (o ?? "NULL").ToString());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
			System.Diagnostics.Debug.WriteLine("Just checking we haven't suppressed the button.");
		}
    }
}
