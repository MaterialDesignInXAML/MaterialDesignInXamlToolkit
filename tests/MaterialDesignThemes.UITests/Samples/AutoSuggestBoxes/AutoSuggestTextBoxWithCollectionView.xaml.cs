using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

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
        base.OnPropertyChanged(nameof(Suggestions));
    }

    public AutoSuggestTextBoxWithCollectionViewViewModel()
    {
        BaseSuggestions = new()
        {
            "Apples",
            "Bananas",
            "Beans",
            "Mtn Dew",
            "Orange",
        };
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
