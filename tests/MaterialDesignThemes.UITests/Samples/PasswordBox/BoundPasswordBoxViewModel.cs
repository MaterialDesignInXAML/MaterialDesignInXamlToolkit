using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignThemes.UITests.Samples.PasswordBox;

internal partial class BoundPasswordBoxViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _password;
}
