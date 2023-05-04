using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.Validation;

/// <summary>
/// Interaction logic for ValidationUpdates.xaml
/// </summary>
public partial class ValidationUpdates 
{
    public ValidationUpdates()
    {
        DataContext = new ValidationUpdatesViewModel();
        InitializeComponent();
    }
}

public partial class ValidationUpdatesViewModel : ObservableObject, INotifyDataErrorInfo
{
    [ObservableProperty]
    private string? _text;

    private string? Error { get; set; }

    public bool HasErrors => Error != null;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName == nameof(Text))
        {
            if (Error != null)
            {
                return new[] { Error };
            }
        }
        return Enumerable.Empty<string>();
    }

    [RelayCommand]
    private async Task CauseErrors()
    {
        Error = "Some error";
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Text)));
        await Task.Delay(100);
        Error += " + more";
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Text)));
    }
}
