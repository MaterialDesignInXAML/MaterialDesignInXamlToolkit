namespace MaterialDesignDemo.Domain
{
    public class FieldsViewModel : ViewModelBase
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
            get => _name;
            set => SetProperty(ref _name, value);
        }

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

        public ICommand SetPassword1FromViewModelCommand { get; }
        public ICommand SetPassword2FromViewModelCommand { get; }

        public FieldsViewModel()
        {
            SetPassword1FromViewModelCommand = new AnotherCommandImplementation(_ => Password1 = "Set from ViewModel!");
            SetPassword2FromViewModelCommand = new AnotherCommandImplementation(_ => Password2 = "Set from ViewModel!");
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
}
