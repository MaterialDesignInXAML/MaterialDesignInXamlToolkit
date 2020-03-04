using System.Windows.Controls;
using System.Windows.Navigation;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignDemo.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for TextFields.xaml
    /// </summary>
    public partial class Fields : UserControl
    {
        public Fields()
        {
            InitializeComponent();
            DataContext = new FieldsViewModel();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Link.OpenInBrowser(e.Uri.AbsoluteUri);
        }
    }
}
