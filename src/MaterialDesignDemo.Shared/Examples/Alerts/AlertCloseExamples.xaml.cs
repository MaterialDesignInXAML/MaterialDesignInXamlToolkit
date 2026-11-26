namespace MaterialDesignDemo.Shared.Examples.Alerts;

public partial class AlertCloseExamples : UserControl
{
    public AlertCloseExamples() => InitializeComponent();

    private void Alert_OnCloseClick(object sender, RoutedEventArgs e)
        => ((UIElement)sender).Visibility = Visibility.Hidden;

    private void Reset_OnClick(object sender, RoutedEventArgs e)
    {
        InfoAlert.Visibility = Visibility.Visible;
        WarningAlert.Visibility = Visibility.Visible;
    }
}
