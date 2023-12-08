using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.SplitButton;

/// <summary>
/// Interaction logic for SplitButtonWithCommandBinding.xaml
/// </summary>
public partial class SplitButtonWithCommandBinding
{
    public bool CommandInvoked => ((SplitButtonWithCommandBindingViewModel) DataContext).CommandInvoked;

    public SplitButtonWithCommandBinding()
    {
        DataContext = new SplitButtonWithCommandBindingViewModel();
        InitializeComponent();
    }
}

public partial class SplitButtonWithCommandBindingViewModel : ObservableObject
{
    public bool CommandInvoked { get; private set; }

    [RelayCommand]
    private void LeftSideButtonClicked() => CommandInvoked = true;
}
