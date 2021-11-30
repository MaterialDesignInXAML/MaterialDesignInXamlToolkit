using System.Windows.Controls;

namespace MaterialDesignThemes.UITests.Samples.DrawHost
{
    /// <summary>
    /// Interaction logic for CancellingDrawerHost.xaml
    /// </summary>
    public partial class CancellingDrawerHost : UserControl
    {
        public CancellingDrawerHost()
            => InitializeComponent();

        private void DrawerHost_DrawerClosing(object sender, Wpf.DrawerClosingEventArgs e)
        {
            //Always cancel
            e.Cancel();
        }

        private void CloseButtonAlt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DrawerHost.IsLeftDrawerOpen = false;
        }
    }
}
