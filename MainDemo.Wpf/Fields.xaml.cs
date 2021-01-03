using System.Windows.Navigation;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
{
    public partial class Fields
    {
        public Fields()
        {
            InitializeComponent();
            DataContext = new FieldsViewModel();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
            => Link.OpenInBrowser(e.Uri.AbsoluteUri);
    }
}