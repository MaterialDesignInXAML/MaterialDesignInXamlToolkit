using System.Configuration;
using System.Windows;
using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class Home
    {
        public Home() => InitializeComponent();

        private void GitHubButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser(ConfigurationManager.AppSettings["GitHub"]);

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("https://twitter.com/James_Willock");

        private void ChatButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("https://gitter.im/ButchersBoy/MaterialDesignInXamlToolkit");

        private void EmailButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("mailto://james@dragablz.net");

        private void DonateButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("https://opencollective.com/materialdesigninxaml");
    }
}
