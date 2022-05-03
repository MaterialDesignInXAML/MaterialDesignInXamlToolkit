using System.Diagnostics;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MaterialDesign3.Demo.WinUI;

/// <summary>
/// An empty window that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private int _counter;
    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        myButton.Content = $"Clicked {_counter++}";
    }

    private void myButton_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        Debug.WriteLine("Button Pressed");
    }

    private void myButton_PointerReleased(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
    {
        Debug.WriteLine("Button Released");
    }
}
