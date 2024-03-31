namespace MaterialDesignThemes.UITests.Samples.PasswordBox;

public partial class BoundPasswordBoxWindow
{
    public BoundPasswordBoxWindow() => InitializeComponent();

    private void BoundPasswordBoxWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        Activate();
        Topmost = true;
        Topmost = false;
        Focus();
    }
}
