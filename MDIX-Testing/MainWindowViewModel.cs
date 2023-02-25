using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace MDIX_Testing;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private int _selectedIndex;

    [RelayCommand]
    private async Task OpenDialogAsync()
    {
        var view = new Dialog()
        {
            DataContext = this
        };

        await DialogHost.Show(view);
    }

    [RelayCommand]
    private async Task NavigateHomeAsync()
    {
        DialogHost.CloseDialogCommand.Execute(null, null);

        /// If the dialog has been opened through the menu (System.Windows.Controls),
        /// adding a delay  of at least 200-300 makes the tabitem switch via code possible
        //await Task.Delay(500);

        SelectedIndex = 0;
        await Task.CompletedTask;
    }
}
