using MaterialDesignThemes.Wpf;

namespace Issue4017
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OpenDialog_Click(object sender, RoutedEventArgs e)
        {
            await DialogHostEx.ShowDialog(this, null!);
            Task.Delay(5000).Wait();    // Block UI thread (i.e. spinner stops spinning)
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.Close(null);
        }
    }
}
