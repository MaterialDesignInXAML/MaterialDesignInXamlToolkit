using MaterialDesignDemo.Domain;
using MaterialDesignDemo.Domain;
using System.Windows.Navigation;

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