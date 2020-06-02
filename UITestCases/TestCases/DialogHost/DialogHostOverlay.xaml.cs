using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UITestCases.TestCases.DialogHost
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    [TestCaseName("DialogHost/ShowsDialog")]
    public partial class DialogHostOverlay : UserControl, ITestCase
    {
        public DialogHostOverlay()
        {
            InitializeComponent();
            SetClickText();
        }

        public Uri Link => new Uri("https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/issues/1825");

        private int NumClicks { get; set; }

        public Task Execute() => Task.CompletedTask;

        private void TestOverlayClick(object sender, System.Windows.RoutedEventArgs e)
        {
            NumClicks++;
            SetClickText();
        }

        private void SetClickText() => ResultTextBlock.Text = $"Clicks: {NumClicks}";
    }
}
