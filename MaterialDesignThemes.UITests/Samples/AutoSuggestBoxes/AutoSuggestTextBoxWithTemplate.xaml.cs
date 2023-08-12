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

public class AutoSuggestTextBoxWithTemplateViewModel
{
    public List<SuggestionThing> Suggestions { get; }

    public AutoSuggestTextBoxWithTemplateViewModel()
    {
        Suggestions = new()
        {
            new("Apples"),
            new("Bananas"),
            new("Beans"),
            new("Mtn Dew"),
            new("Orange"),
        };
    }
}

public class SuggestionThing
{
    public string Name { get; }

    public SuggestionThing(string name) => Name = name;
}
