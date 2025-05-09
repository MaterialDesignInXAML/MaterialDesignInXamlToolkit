using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MaterialDesignThemes.UITests.Samples.AutoSuggestBoxes;

/// <summary>
/// Interaction logic for AutoSuggestTextBoxWithCollectionView.xaml
/// </summary>
public partial class AutoSuggestTextBoxWithCollectionView
{
    public AutoSuggestTextBoxWithCollectionView()
    {
        DataContext = new AutoSuggestTextBoxWithCollectionViewViewModel();
        InitializeComponent();
    }
}

public partial class AutoSuggestTextBoxWithCollectionViewViewModel : ObservableObject
{
    private ObservableCollection<string> BaseSuggestions { get; }

    public ICollectionView Suggestions { get; }

    [ObservableProperty]
    private string? _autoSuggestText;

    [ObservableProperty]
    private string? _selectedItem;

    partial void OnAutoSuggestTextChanged(string? oldValue, string? newValue)
    {
        if (!string.IsNullOrWhiteSpace(newValue))
        {
            Suggestions.Filter = x => IsMatch((string)x, newValue);
        }
        else
        {
            Suggestions.Filter = null;
        }
        OnPropertyChanged(nameof(Suggestions));
    }

    public AutoSuggestTextBoxWithCollectionViewViewModel()
    {
        BaseSuggestions =
        [
            "Apples",
            "Bananas",
            "Beans",
            "Mtn Dew",
            "Orange",
        ];
        Suggestions = CollectionViewSource.GetDefaultView(BaseSuggestions);
    }

    private static bool IsMatch(string item, string currentText)
    {
#if NET6_0_OR_GREATER
        return item.Contains(currentText, StringComparison.OrdinalIgnoreCase);
#else
        return item.IndexOf(currentText, StringComparison.OrdinalIgnoreCase) >= 0;
#endif
    }
}
