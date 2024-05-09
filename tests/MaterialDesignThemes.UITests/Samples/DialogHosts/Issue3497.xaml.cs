using System.Threading;

namespace MaterialDesignThemes.UITests.Samples.DialogHosts;

/// <summary>
/// Interaction logic for Issue3497.xaml
/// </summary>
public partial class Issue3497
{
    public Issue3497()
    {
        InitializeComponent();
    }

    private async void ShowButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await DoWithPreloader(() =>
            {
                Thread.Sleep(60_000);
            });
        }
        catch (Exception ex)
        {
            
        }
    }

    public static async Task DoWithPreloader(Action action, string host = "DialogId")
    {
        await DialogHost.Show(null, host, new DialogOpenedEventHandler((_, args) => {
            action();
            try
            {
                if (!args.Session.IsEnded)
                {
                    args.Session.Close(false);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }));
    }
}
