namespace MaterialDesignThemes.UITests.Samples.DialogHost;

public partial class RestoreFocusDisabled : UserControl
{
    public RestoreFocusDisabled()
    {
        InitializeComponent();
    }

    private void NavigateHomeButton_OnClick(object sender, RoutedEventArgs e)
    {
        Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        NavigationRail.SelectedItem = RailItem1;
        TabControl.SelectedItem = TabItem1;
    }
}
