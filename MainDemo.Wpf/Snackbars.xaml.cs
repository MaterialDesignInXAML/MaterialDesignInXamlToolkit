using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignDemo
{
    /// <summary>
    /// Interaction logic for Snackbars.xaml
    /// </summary>
    public partial class Snackbars : UserControl
    {
        public Snackbars()
        {
            InitializeComponent();
        }

        private void SnackBar3_OnClick(object sender, RoutedEventArgs e)
        {
            //use the message queue to send a message.
            var messageQueue = SnackbarThree.MessageQueue;
            var message = MessageTextBox.Text;

            //the message queue can be called from any thread
            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
        }

        private void SnackBar4_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var s in ExampleFourTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                SnackbarFour.MessageQueue.Enqueue(
                s,
                "TRACE",
                param => Trace.WriteLine("Actioned: " + param),
                s);
            }
        }
    }
}
