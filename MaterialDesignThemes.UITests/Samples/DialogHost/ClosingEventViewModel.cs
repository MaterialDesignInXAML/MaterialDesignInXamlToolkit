using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.DialogHost;

public partial class ClosingEventViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _dialogIsOpen;

    [RelayCommand]
    private void OpenDialog()
        => DialogIsOpen = true;

    [RelayCommand]
    private void CloseDialog()
        => DialogIsOpen = false;
}
