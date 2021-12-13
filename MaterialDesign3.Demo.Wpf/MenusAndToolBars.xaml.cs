using System.Windows;
using MaterialDesign3Demo.Domain;

namespace MaterialDesign3Demo
{
    public partial class MenusAndToolBars
    {
        public MenusAndToolBars() => InitializeComponent();

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("https://twitter.com/James_Willock");
    }
}
