using System.Windows;
using System.Windows.Input;

namespace MaterialDesignColors.WpfExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        private void PopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("PopupButton Clicked");
        }
    } 
}
