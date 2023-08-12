using System.Collections.ObjectModel;

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

public partial class AutoSuggestTextBoxWithTemplateViewModel : BaseViewModel
{
    private List<SuggestionThing> _baseSuggestions { get; }

    private ObservableCollection<SuggestionThing> _suggestions = new();
    public ObservableCollection<SuggestionThing> Suggestions
    {
        get => _suggestions;
        set => SetProperty(ref _suggestions, value);
    }

    private string? _autoSuggestText;

    public string? AutoSuggestText
    {
        get => _autoSuggestText;
        set
        {
            if (SetProperty(ref _autoSuggestText, value) && !string.IsNullOrEmpty(value))
            {
                var searchResult = _baseSuggestions.Where(x => IsMatch(x.Name, value));
                //MessageBox.Show(searchResult.Count().ToString(), "Test");
                Suggestions = new(searchResult);
            }
        }
    }


    public AutoSuggestTextBoxWithTemplateViewModel()
    {
        _baseSuggestions = new()
        {
            new("Apples"),
            new("Bananas"),
            new("Beans"),
            new("Mtn Dew"),
            new("Orange"),
        };
        Suggestions = new ObservableCollection<SuggestionThing>(_baseSuggestions);
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
