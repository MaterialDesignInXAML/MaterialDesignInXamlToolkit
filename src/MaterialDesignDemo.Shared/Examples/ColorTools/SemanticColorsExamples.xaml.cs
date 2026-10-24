using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignDemo.Shared.Examples.ColorTools;

public partial class SemanticColorsExamples : UserControl
{
    public SemanticColorsExamples()
    {
        DataContext = new SemanticColorsViewModel();
        InitializeComponent();
    }
}

public partial class SemanticColorsViewModel : ObservableObject
{
    [RelayCommand]
    private void CopyResource(string resourceKey)
        => System.Windows.Clipboard.SetText(resourceKey);
}
