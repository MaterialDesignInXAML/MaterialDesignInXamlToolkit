using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Google.Protobuf.WellKnownTypes;

namespace MaterialDesignThemes.UITests.Samples.AutoSuggestTextBoxes;

/// <summary>
/// Interaction logic for AutoSuggestTextBoxWithTemplate.xaml
/// </summary>
public partial class AutoSuggestTextBoxWithTemplate
{
    public AutoSuggestTextBoxWithTemplate()
    {
        DataContext = new AutoSuggestTextBoxWithTemplateViewModel();
        InitializeComponent();
    }
}

public partial class AutoSuggestTextBoxWithTemplateViewModel : ObservableObject
{
    private List<SuggestionThing> BaseSuggestions { get; }

    [ObservableProperty]
    private ObservableCollection<SuggestionThing> _suggestions = new();

    [ObservableProperty]
    private string? _autoSuggestText;

    partial void OnAutoSuggestTextChanged(string? oldValue, string? newValue)
    {
        if (!string.IsNullOrWhiteSpace(newValue))
        {
            var searchResult = BaseSuggestions.Where(x => IsMatch(x.Name, newValue));
            Suggestions = new(searchResult);
        }
        else
        {
            Suggestions = new(BaseSuggestions);
        }
    }

    public AutoSuggestTextBoxWithTemplateViewModel()
    {
        BaseSuggestions = new()
        {
            new("Apples"),
            new("Bananas"),
            new("Beans"),
            new("Mtn Dew"),
            new("Orange"),
        };
        Suggestions = new ObservableCollection<SuggestionThing>(BaseSuggestions);
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

public class SuggestionThing
{
    public string Name { get; }

    public SuggestionThing(string name) => Name = name;
}
