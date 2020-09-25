using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.UITests.Samples.DialogHost
{
    /// <summary>
    /// Interaction logic for WithCounter.xaml
    /// </summary>
    public partial class WithCounter : UserControl
    {
        public WithCounter()
        {
            InitializeComponent();
            SetClickText();
        }

        private int NumClicks { get; set; }

        private void TestOverlayClick(object sender, RoutedEventArgs e)
        {
            NumClicks++;
            SetClickText();
        }

        private void SetClickText() => ResultTextBlock.Text = $"Clicks: {NumClicks}";
    }
}
