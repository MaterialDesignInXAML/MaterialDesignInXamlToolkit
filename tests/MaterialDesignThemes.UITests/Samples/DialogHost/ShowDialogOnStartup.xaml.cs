namespace MaterialDesignThemes.UITests.Samples.DialogHost;

/// <summary>
/// Interaction logic for ShowDialogOnStartup.xaml
/// </summary>
public partial class ShowDialogOnStartup : UserControl
{
    public ShowDialogOnStartup()
    {
        InitializeComponent();

        // Issue 3804: show the dialog from the constructor, before the
        // window is launched or loaded, exactly as an app's MainWindow
        // would during startup. The DialogHost is initialized at this
        // point (via InitializeComponent) but has not raised Loaded yet,
        // so Show must find it via its OnInitialized registration.
        _ = global::MaterialDesignThemes.Wpf.DialogHost.Show("Content");
    }
}
