using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MaterialDesignThemes.UITests.Samples.AutoSuggestBoxes;

/// <summary>
/// Interaction logic for AutoSuggestTextBoxWithInteractiveTemplate.xaml
/// </summary>
public partial class AutoSuggestTextBoxWithInteractiveTemplate : UserControl
{
    public AutoSuggestTextBoxWithInteractiveTemplate()
    {
        DataContext = new AutoSuggestTextBoxWithInteractiveTemplateViewModel();
        InitializeComponent();
    }
}

public partial class AutoSuggestTextBoxWithInteractiveTemplateViewModel : ObservableObject
{
    private readonly List<SuggestionThing2> _baseSuggestions;

    [ObservableProperty]
    private List<SuggestionThing2> _suggestions = [];

    [ObservableProperty]
    private string? _autoSuggestText;

    partial void OnAutoSuggestTextChanged(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var searchResult = _baseSuggestions.Where(x => IsMatch(x.Name, value));
            Suggestions = new(searchResult);
        }
        else
        {
            Suggestions = new(_baseSuggestions);
        }
    }

    public AutoSuggestTextBoxWithInteractiveTemplateViewModel()
    {
        _baseSuggestions =
        [
            new("Apples"),
            new("Bananas"),
            new("Beans"),
            new("Mtn Dew"),
            new("Orange")
        ];
        Suggestions = [.. _baseSuggestions];
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

public partial class SuggestionThing2(string name) : ObservableObject
{
    public string Name { get; } = name;

    [ObservableProperty]
    private int _count = 0;

    [RelayCommand]
    private void IncrementCount() => Count++;
}
