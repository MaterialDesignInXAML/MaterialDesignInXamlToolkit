namespace MaterialDesignThemes.UITests.Samples.DialogHost;

public partial class RestoreFocusNoOverride : UserControl
{
    public RestoreFocusNoOverride()
    {
        InitializeComponent();
    }

    private void NavigateHomeButton_OnClick(object sender, RoutedEventArgs e)
    {
        Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        TabControl.SelectedItem = TabItem1;
    }
}
