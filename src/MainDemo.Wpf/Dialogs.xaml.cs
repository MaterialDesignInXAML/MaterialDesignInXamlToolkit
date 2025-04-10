using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo;

public partial class Dialogs
{
    public Dialogs()
    {
        DataContext = new DialogsViewModel();
        InitializeComponent();
        BlurRadiusSlider.Value = DialogHost.DefaultBlurRadius;
        tbBlurRadius.Text = DialogOptions.Default.BlurRadius.ToString();
    }

    private void Sample1_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
    {
        Debug.WriteLine($"SAMPLE 1: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        //you can cancel the dialog close:
        //eventArgs.Cancel();

        if (!Equals(eventArgs.Parameter, true))
            return;

        if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
            FruitListBox.Items.Add(FruitTextBox.Text.Trim());
    }

    private void Sample1_DialogHost_OnDialogClosed(object sender, DialogClosedEventArgs eventArgs)
    {
        Debug.WriteLine($"SAMPLE 1: Closed dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        //you can cancel the dialog close:
        //eventArgs.Cancel();

        if (!Equals(eventArgs.Parameter, true))
            return;

        if (!string.IsNullOrWhiteSpace(FruitTextBox.Text))
            FruitListBox.Items.Add(FruitTextBox.Text.Trim());
    }

    // Used for DialogHost.DialogClosingAttached
    private void Sample2_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        => Debug.WriteLine($"SAMPLE 2: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

    private void Sample2_DialogHost_OnDialogClosed(object sender, DialogClosedEventArgs eventArgs)
        => Debug.WriteLine($"SAMPLE 2: Closed dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

    private void Sample5_DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
    {
        Debug.WriteLine($"SAMPLE 5: Closing dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        //you can cancel the dialog close:
        //eventArgs.Cancel();

        if (!Equals(eventArgs.Parameter, true))
            return;

        if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
            AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
    }

    private void Sample5_DialogHost_OnDialogClosed(object sender, DialogClosedEventArgs eventArgs)
    {
        Debug.WriteLine($"SAMPLE 5: Closed dialog with parameter: {eventArgs.Parameter ?? string.Empty}");

        if (!Equals(eventArgs.Parameter, true))
            return;

        if (!string.IsNullOrWhiteSpace(AnimalTextBox.Text))
            AnimalListBox.Items.Add(AnimalTextBox.Text.Trim());
    }

    private async void Sample6_OpenDialog(object sender, RoutedEventArgs e)
    {
        var sampleMessageDialog = new SampleMessageDialog
        {
            Message = { Text = "Some dialog content" }
        };

        await DialogHost.Show(sampleMessageDialog, "sampleDialog6");
    }

    private void Sample6_ResetBlur(object sender, RoutedEventArgs e)
    {
        BlurRadiusSlider.Value = DialogHost.DefaultBlurRadius;
    }

    private async void Sample7_OpenWithDialogOptions(object sender, RoutedEventArgs e)
    {
        var options = new DialogOptions()
        {
            OpenedEventHandler = cbOpenedEventHandler.IsChecked == true ? (_, _) => MessageBox.Show("OpenedEventHandler raised") : null,
            ClosingEventHandler = cbClosingEventHandler.IsChecked == true ? (_, _) => MessageBox.Show("ClosingEventHandler raised") : null,
            ClosedEventHandler = cbClosedEventHandler.IsChecked == true ? (_, _) => MessageBox.Show("ClosedEventHandler raised") : null,
            IsFullscreen = cbIsFullscreen.IsChecked!.Value,
            ShowCloseButton = cbShowCloseButton.IsChecked!.Value,
            CloseOnClickAway = cbCloseOnClickAway.IsChecked!.Value,
            ApplyBlurEffect = cbApplyBlurEffect.IsChecked!.Value,
            BlurRadius = double.TryParse(tbBlurRadius.Text, out double parsedRadius) ? parsedRadius : 0
        };

        var dialogContent = new TextBlock()
        {
            Text = "Some dialog content",
            Margin = new Thickness(32)
        };
        await DialogHost.Show(dialogContent, "RootDialog", options);
    }
}
