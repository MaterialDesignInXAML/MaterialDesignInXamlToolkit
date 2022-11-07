using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.DialogHost;

[ObservableObject]
public partial class ClosingEventViewModel
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
