using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MahMaterialDragablzMashUp;

public class DialogsViewModel
{
    public ICommand ShowInputDialogCommand { get; }

    public ICommand ShowProgressDialogCommand { get; }

    public ICommand ShowLeftFlyoutCommand { get; }

    private readonly ResourceDictionary DialogDictionary = new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.MahApps;component/Themes/MaterialDesignTheme.MahApps.Dialogs.xaml") };

    public DialogsViewModel()
    {
        ShowInputDialogCommand = new AnotherCommandImplementation(async _ => await InputDialog());
        ShowProgressDialogCommand = new AnotherCommandImplementation(async _ => await ProgressDialog());
        ShowLeftFlyoutCommand = new AnotherCommandImplementation(_ => ShowLeftFlyout());
    }

    public Flyout? LeftFlyout { get; set; }

    private async Task InputDialog()
    {
        var metroDialogSettings = new MetroDialogSettings
        {
            CustomResourceDictionary = DialogDictionary,
            NegativeButtonText = "CANCEL"
        };

        await DialogCoordinator.Instance.ShowInputAsync(this, "MahApps Dialog", "Using Material Design Themes", metroDialogSettings);
    }

    private async Task ProgressDialog()
    {
        var metroDialogSettings = new MetroDialogSettings
        {
            CustomResourceDictionary = DialogDictionary,
            NegativeButtonText = "CANCEL"
        };

        var controller = await DialogCoordinator.Instance.ShowProgressAsync(this, "MahApps Dialog", "Using Material Design Themes (WORK IN PROGRESS)", true, metroDialogSettings);
        controller.SetIndeterminate();
        await Task.Delay(3000);
        await controller.CloseAsync();
    }

    private void ShowLeftFlyout()
    {
        // Avoid direct cast to the project's MainWindow type (XAML-generated partials can confuse the analyzer).
        // Find the named flyout from the main window and toggle its IsOpen state.
        var leftFlyout = Application.Current?.MainWindow?.FindName("LeftFlyout") as Flyout;
        if (leftFlyout != null)
        {
            leftFlyout.IsOpen = !leftFlyout.IsOpen;
        }
    }
}
