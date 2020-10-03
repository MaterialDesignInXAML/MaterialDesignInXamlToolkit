using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignColors.WpfExample
{
    public partial class Chips : UserControl
    {
        public Chips() => InitializeComponent();

        private void ButtonsDemoChip_OnClick(object sender, RoutedEventArgs e)
            => MainWindow.Snackbar.MessageQueue.Enqueue("Chip clicked!");

        private void ButtonsDemoChip_OnDeleteClick(object sender, RoutedEventArgs e)
            => MainWindow.Snackbar.MessageQueue.Enqueue("Chip delete clicked!");

    }
}
