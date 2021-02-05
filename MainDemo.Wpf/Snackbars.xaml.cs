using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignDemo
{
    public partial class Snackbars
    {
        public Snackbars() => InitializeComponent();

        private void SnackBar3_OnClick(object sender, RoutedEventArgs e)
        {
            if (SnackbarThree.MessageQueue is { } messageQueue)
            {
                //use the message queue to send a message.
                var message = MessageTextBox.Text;
                //the message queue can be called from any thread
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        private void SnackBar4_OnClick(object sender, RoutedEventArgs e)
        {
            if (SnackbarFour.MessageQueue is { } messageQueue)
            {
                SnackbarFour.MessageQueue.DiscardDuplicates = DiscardDuplicateCheckBox.IsChecked ?? false;
                foreach (var s in ExampleFourTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    messageQueue.Enqueue(
                    s,
                    "TRACE",
                    param => Trace.WriteLine("Actioned: " + param),
                    s);
                }
            }

        }

        private void SnackBar4_OnClearClick(object sender, RoutedEventArgs e)
            => SnackbarFour.MessageQueue?.Clear();

        private void SnackBar7_OnClick(object sender, RoutedEventArgs e)
        {
            var duration = MessageDurationOverrideSlider.Value;
            SnackbarSeven.MessageQueue?.Enqueue(
                $"Hello world! Showing message for {duration:F1} seconds.",
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(duration));
        }
    }
}
