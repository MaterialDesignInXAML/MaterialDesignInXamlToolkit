using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.SplitButton;

/// <summary>
/// Interaction logic for SplitButtonWithCommandBinding.xaml
/// </summary>
public partial class SplitButtonWithCommandBinding
{
    private SplitButtonWithCommandBindingViewModel ViewModel { get; }

    public bool CommandInvoked => ViewModel.CommandInvoked;

    public bool CommandCanExecute
    {
        get => ViewModel.CommandCanExecute;
        set => ViewModel.CommandCanExecute = value;
    }

    public SplitButtonWithCommandBinding()
    {
        DataContext = ViewModel = new SplitButtonWithCommandBindingViewModel();
        InitializeComponent();
    }
}

public partial class SplitButtonWithCommandBindingViewModel : ObservableObject
{
    public bool CommandInvoked { get; private set; }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LeftSideButtonClickedCommand))]
    private bool _commandCanExecute = true;

    [RelayCommand(CanExecute = nameof(CommandCanExecute))]
    private void LeftSideButtonClicked() => CommandInvoked = true;
}
