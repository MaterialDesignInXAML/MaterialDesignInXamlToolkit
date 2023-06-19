using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Xml;


namespace MaterialDesignDemo.Domain
{
    private string? _name;
    private string? _name2;
    private string? _password1 = string.Empty;
    private string? _password2 = "pre-filled";
    private string? _password1Validated = "pre-filled";
    private string? _password2Validated = "pre-filled";
    private string? _text1;
    private string? _text2;

    public string? Name
    {
        private string? _name;
        private string? _name2;
        private string? _password1 = string.Empty;
        private string? _password2 = "pre-filled";
        private string? _password1Validated = "pre-filled";
        private string? _password2Validated = "pre-filled";
        private string? _text1;
        private string? _text2;
        private ObservableCollection<string>? _autoSuggestBox1Suggestions;
        private string? _autoSuggestBox1Text;
        private List<string>? _originalAutoSuggestBox1Suggestions;
        private ObservableCollection<KeyValuePair<string, Brush>>? _autoSuggestBox2Suggestions;
        private string? _autoSuggestBox2Text;
        private List<KeyValuePair<string, Brush>>? _originalAutoSuggestBox2Suggestions;

    public string? Name2
    {
        get => _name2;
        set => SetProperty(ref _name2, value);
    }

    public string? Text1
    {
        get => _text1;
        set => SetProperty(ref _text1, value);
    }

    public string? Text2
    {
        get => _text2;
        set => SetProperty(ref _text2, value);
    }

    public string? Password1
    {
        get => _password1;
        set => SetProperty(ref _password1, value);
    }

    public string? Password2
    {
        get => _password2;
        set => SetProperty(ref _password2, value);
    }

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

        public FieldsTestObject TestObject => new() { Name = "Mr. Test" };

        public ObservableCollection<string>? AutoSuggestBox1Suggestions
        {
            get { return _autoSuggestBox1Suggestions; }
            set { SetProperty(ref _autoSuggestBox1Suggestions, value); }
        }

        public ObservableCollection<KeyValuePair<string, Brush>>? AutoSuggestBox2Suggestions
        {
            get { return _autoSuggestBox2Suggestions; }
            set { SetProperty(ref _autoSuggestBox2Suggestions, value); }
        }

        public string? AutoSuggestBox1Text
        {
            get { return _autoSuggestBox1Text; }
            set
            {
                if (value != _autoSuggestBox1Text)
                {
                    SetProperty(ref _autoSuggestBox1Text, value);
                    if (_originalAutoSuggestBox1Suggestions != null && value != null)
                    {
                        var searchResult = _originalAutoSuggestBox1Suggestions.Where(x => IsMatch(x, value));
                        AutoSuggestBox1Suggestions = new ObservableCollection<string>(searchResult);
                    }
                }
            }
        }

        public string? AutoSuggestBox2Text
        {
            get { return _autoSuggestBox2Text; }
            set
            {
                if (value != _autoSuggestBox2Text)
                {
                    SetProperty(ref _autoSuggestBox2Text, value);
                    if (_originalAutoSuggestBox2Suggestions != null && value != null)
                    {
                        var searchResult = _originalAutoSuggestBox2Suggestions.Where(x => IsMatch(x.Key, value));
                        AutoSuggestBox2Suggestions = new ObservableCollection<KeyValuePair<string, Brush>>(searchResult);
                    }
                }
            }
        }




        public ICommand SetPassword1FromViewModelCommand { get; }
        public ICommand SetPassword2FromViewModelCommand { get; }

        public FieldsViewModel()
        {
            SetPassword1FromViewModelCommand = new AnotherCommandImplementation(_ => Password1 = "Set from ViewModel!");
            SetPassword2FromViewModelCommand = new AnotherCommandImplementation(_ => Password2 = "Set from ViewModel!");

            InitializeData();
        }
        private void InitializeData()
        {
            _originalAutoSuggestBox1Suggestions = new List<string>()
            {
                "Burger", "Fries", "Shake", "Lettuce"
            };

            _originalAutoSuggestBox2Suggestions = new List<KeyValuePair<string, Brush>>(GetColors());

            AutoSuggestBox1Suggestions = new ObservableCollection<string>(_originalAutoSuggestBox1Suggestions);
        }

        private bool IsMatch(string item, string currentText)
        {
            return item.ToLower().Contains(currentText.ToLower());
        }

        private IEnumerable<KeyValuePair<string, Brush>> GetColors()
        {
            return typeof(Colors)
                .GetProperties()
                .Where(prop =>
                    typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop =>
                    new KeyValuePair<string, Brush>(prop.Name, GenerateColorBrush(prop.GetValue(null))));
        }

        private SolidColorBrush GenerateColorBrush(object? prop)
        {
            if (prop is Color color)
                return new SolidColorBrush(color);
            return new SolidColorBrush(Colors.White);
        }
    }
}

public class FieldsTestObject : ViewModelBase
{
    private string? _name;
    private string? _content;

    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string? Content
    {
        get => _content;
        set => SetProperty(ref _content, value);
    }
}
