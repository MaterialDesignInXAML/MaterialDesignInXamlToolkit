namespace MaterialDesignThemes.UITests.Samples.DialogHost;

/// <summary>
/// Interaction logic for OpenDialogInConstructorWindow.xaml
/// </summary>
public partial class OpenDialogInConstructorWindow : Window
{
    public OpenDialogInConstructorWindow()
    {
        InitializeComponent();
        MaterialDesignThemes.Wpf.DialogHost.Show("Dialog opened in constructor");
    }
}
