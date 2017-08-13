using System.Windows;
using System.Windows.Controls;
using System.Xml;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignDemo.Helper;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for TextFields.xaml
    /// </summary>
    public partial class TextFields : UserControl
    {
        public TextFields()
        {
            InitializeComponent();	        
			DataContext = new TextFieldsViewModel();
			
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {            
        }

    }
}
