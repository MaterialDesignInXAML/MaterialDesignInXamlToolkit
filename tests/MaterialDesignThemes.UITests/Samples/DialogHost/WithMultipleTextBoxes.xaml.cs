namespace MaterialDesignThemes.UITests.Samples.DialogHost;

/// <summary>
/// Interaction logic for WithMultipleTextBoxes.xaml
/// </summary>
public partial class WithMultipleTextBoxes : UserControl
{
    public WithMultipleTextBoxes()
    {
        InitializeComponent();
    }
    private void DialogHost_Loaded(object sender, RoutedEventArgs e)
    {
        SampleDialogHost.IsOpen = true;
    }
}
