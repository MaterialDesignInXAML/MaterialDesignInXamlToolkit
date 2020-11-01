using System.Windows;

namespace MaterialDesignDemo
{
    public partial class Chips
    {
        public Chips() => InitializeComponent();

        private void ButtonsDemoChip_OnClick(object sender, RoutedEventArgs e)
            => MainWindow.Snackbar.MessageQueue.Enqueue("Chip clicked!");

        private void ButtonsDemoChip_OnDeleteClick(object sender, RoutedEventArgs e)
            => MainWindow.Snackbar.MessageQueue.Enqueue("Chip delete clicked!");

    }
}
