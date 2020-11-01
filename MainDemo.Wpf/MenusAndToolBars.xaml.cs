using System.Windows;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo
{
    public partial class MenusAndToolBars
    {
        public MenusAndToolBars() => InitializeComponent();

        private void TwitterButton_OnClick(object sender, RoutedEventArgs e)
            => Link.OpenInBrowser("https://twitter.com/James_Willock");
    }
}
