namespace MaterialDesignThemes.UITests.Samples.DialogHost;

/// <summary>
/// Interaction logic for LoadAndUnloadControl.xaml
/// </summary>
public partial class LoadAndUnloadControl
{
    public LoadAndUnloadControl()
    {
        InitializeComponent();
    }

    private void LoadDialogHost_Click(object sender, RoutedEventArgs e)
    {
        RootGrid.Children.Add(DialogHost);
    }

    private void UnloadDialogHost_Click(object sender, RoutedEventArgs e)
    {
        RootGrid.Children.Remove(DialogHost);
    }

    private void ToggleIsOpen_Click(object sender, RoutedEventArgs e)
    {
        DialogHost.IsOpen = !DialogHost.IsOpen;
    }
}
