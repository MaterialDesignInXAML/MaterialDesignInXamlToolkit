namespace MaterialDesignThemes.UITests.Samples.DialogHost;

public partial class RestoreFocus : UserControl
{
    public RestoreFocus()
    {
        InitializeComponent();
    }

    private void NavigateHomeButton_OnClick(object sender, RoutedEventArgs e)
    {
        Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        TabControl.SelectedItem = TabItem1;
    }
}
