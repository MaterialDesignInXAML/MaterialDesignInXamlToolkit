using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using MaterialDesignColors.WpfExample.Domain;

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

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }
    }
}
