using System.ComponentModel;
using System.Windows.Controls;

namespace MaterialDesignThemes.UITests.Samples.DialogHost
{
    /// <summary>
    /// Interaction logic for ClosingEventCounter.xaml
    /// </summary>
    public partial class ClosingEventCounter : UserControl
    {
        public ClosingEventCounter()
            => InitializeComponent();

        private int _ClosingCount;
        private void DialogHost_DialogClosing(object sender, Wpf.DialogClosingEventArgs eventArgs)
            => ResultTextBlock.Text = (++_ClosingCount).ToString();
    }
}
