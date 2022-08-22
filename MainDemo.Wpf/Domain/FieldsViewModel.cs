namespace MaterialDesignDemo.Domain
{
    public class FieldsViewModel : ViewModelBase
    {
        private string? _name;
        private string? _name2;
        private string? _text1;
        private string? _text2;
        private string? _password1 = "password";
        private string? _password2 = "password";

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
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Password cannot be empty");
                SetProperty(ref _password1, value);
            }
        }

        public string? Password2
        {
            get => _password2;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Password cannot be empty");
                SetProperty(ref _password2, value);
            }
        }

        public FieldsTestObject TestObject => new() { Name = "Mr. Test" };
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
