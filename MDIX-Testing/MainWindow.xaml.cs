using System.Diagnostics;
using System.Windows;

namespace MDIX_Testing;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigationTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine($">>>>> SelectionChanged :: SelectedIndex = {NavigationTabControl.SelectedIndex}");
    }

    private void NavigationTabControl_OnGotFocus(object sender, RoutedEventArgs e)
    {
        //Debug.WriteLine($">>>>> GotFocus :: SelectedIndex = {NavigationTabControl.SelectedIndex}");
    }

    private void NavigationTabControl_OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        //Debug.WriteLine($">>>>> PreviewGotKeyboardFocus :: SelectedIndex = {NavigationTabControl.SelectedIndex}");
    }
}
