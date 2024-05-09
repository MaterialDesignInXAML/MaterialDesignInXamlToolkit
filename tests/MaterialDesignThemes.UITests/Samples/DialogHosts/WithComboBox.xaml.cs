namespace MaterialDesignThemes.UITests.Samples.DialogHosts;

/// <summary>
/// Interaction logic for WithComboBox.xaml
/// </summary>
public partial class WithComboBox : UserControl
{
    public WithComboBox()
    {
        InitializeComponent();
    }

    private void DialogHost_Loaded(object sender, RoutedEventArgs e)
    {
        SampleDialogHost.IsOpen = true;
    }
}
