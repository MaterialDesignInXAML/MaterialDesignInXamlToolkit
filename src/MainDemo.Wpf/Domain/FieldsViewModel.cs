using System.Collections.ObjectModel;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignDemo.Shared.Domain;

namespace MaterialDesignDemo.Domain;
public partial class FieldsViewModel : ObservableObject
{
    private string? _password1Validated = "pre-filled";
    private string? _password2Validated = "pre-filled";
    private readonly List<string>? _originalAutoSuggestBox1Suggestions;
    private readonly List<KeyValuePair<string, Color>>? _originalAutoSuggestBox2Suggestions;
    private readonly List<string> _originalAutoSuggestBox3Suggestions;

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string? _name2;

    [ObservableProperty]
    private string? _text1;

    [ObservableProperty]
    private string? _text2;

    [ObservableProperty]
    private string? _password1 = string.Empty;

    [ObservableProperty]
    private string? _password2 = "pre-filled";

    public string? Password1Validated
    {
        get => _password1Validated;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Password cannot be empty");
            SetProperty(ref _password1Validated, value);
        }
    }

    public string? Password2Validated
    {
        get => _password2Validated;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Password cannot be empty");
            SetProperty(ref _password2Validated, value);
        }
    }

    public FieldsTestObject TestObject => new() { Name = "Mr. Test" };

    [ObservableProperty]
    private ObservableCollection<string>? _autoSuggestBox1Suggestions;

    [ObservableProperty]
    private ObservableCollection<KeyValuePair<string, Color>>? _autoSuggestBox2Suggestions;

    [ObservableProperty]
    private List<string>? _autoSuggestBox3Suggestions;
    

    [ObservableProperty]
    private string? _autoSuggestBox1Text;

    partial void OnAutoSuggestBox1TextChanged(string? value)
    {
        if (_originalAutoSuggestBox1Suggestions != null && value != null)
        {
            var searchResult = _originalAutoSuggestBox1Suggestions.Where(x => IsMatch(x, value));
            AutoSuggestBox1Suggestions = new(searchResult);
        }
    }

    [ObservableProperty]
    private string? _autoSuggestBox2Text;

    partial void OnAutoSuggestBox2TextChanged(string? value)
    {
        if (_originalAutoSuggestBox2Suggestions != null && value != null)
        {
            var searchResult = _originalAutoSuggestBox2Suggestions.Where(x => IsMatch(x.Key, value));
            AutoSuggestBox2Suggestions = new(searchResult);
        }
    }

    [ObservableProperty]
    private string? _autoSuggestBox3Text;

    partial void OnAutoSuggestBox3TextChanged(string? value)
    {
        if (value is not null)
        {
            var searchResult = _originalAutoSuggestBox3Suggestions.Where(x => IsMatch(x, value));
            AutoSuggestBox3Suggestions = new(searchResult);
        }
    }

    [RelayCommand]
    private void RemoveAutoSuggestBox3Suggestion(string suggestion)
    {
        _originalAutoSuggestBox3Suggestions.Remove(suggestion);
        if (string.IsNullOrEmpty(AutoSuggestBox3Text))
        {
            AutoSuggestBox3Suggestions = new(_originalAutoSuggestBox3Suggestions);
        }
        else
        {
            var searchResult = _originalAutoSuggestBox3Suggestions.Where(x => IsMatch(x, AutoSuggestBox3Text!));
            AutoSuggestBox3Suggestions = new(searchResult);
        }
    }

    public ICommand SetPassword1FromViewModelCommand { get; }
    public ICommand SetPassword2FromViewModelCommand { get; }

    public FieldsViewModel()
    {
        SetPassword1FromViewModelCommand = new AnotherCommandImplementation(_ => Password1 = "Set from ViewModel!");
        SetPassword2FromViewModelCommand = new AnotherCommandImplementation(_ => Password2 = "Set from ViewModel!");

        _originalAutoSuggestBox1Suggestions =
            [
                "Burger", "Fries", "Shake", "Lettuce"
            ];

        _originalAutoSuggestBox2Suggestions = new(GetColors());
        _originalAutoSuggestBox3Suggestions =
            [
                "jsmith", "jdoe", "mscott", "pparker", "bwilliams", "ljohnson", "abrown", "dlee", "cmiller", "tmoore"
            ];

        AutoSuggestBox1Suggestions = new ObservableCollection<string>(_originalAutoSuggestBox1Suggestions);
    }

    private static bool IsMatch(string item, string currentText)
    {
#if NET6_0_OR_GREATER
        return item.Contains(currentText, StringComparison.OrdinalIgnoreCase);
#else
        return item.IndexOf(currentText, StringComparison.OrdinalIgnoreCase) >= 0;
#endif
    }

    private static IEnumerable<KeyValuePair<string, Color>> GetColors()
    {
        return typeof(Colors)
            .GetProperties()
            .Where(prop =>
                typeof(Color).IsAssignableFrom(prop.PropertyType))
            .Select(prop =>
                new KeyValuePair<string, Color>(prop.Name, (Color)prop.GetValue(null)!));
    }
}

public partial class FieldsTestObject : ObservableObject
{
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string? _content;
}
