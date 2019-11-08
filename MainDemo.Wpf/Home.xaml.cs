using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignDemo.Domain;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser(ConfigurationManager.AppSettings["GitHub"]);
        }

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("https://twitter.com/James_Willock");
        }

        private void ChatButton_OnClick(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("https://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit");
        }

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("mailto://james@dragablz.net");
        }

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
        {
            Link.OpenInBrowser("https://opencollective.com/materialdesigninxaml");
        }
    }
}
