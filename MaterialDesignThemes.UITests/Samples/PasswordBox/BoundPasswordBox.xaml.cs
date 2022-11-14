namespace MaterialDesignThemes.UITests.Samples.PasswordBox;

public partial class BoundPasswordBox
{
    

    public string? ViewModelPassword
    {
        get => ((BoundPasswordBoxViewModel) DataContext).Password;
        set => ((BoundPasswordBoxViewModel) DataContext).Password = value;
    }


    private bool _useRevealStyle;
    public bool UseRevealStyle
    {
        get => _useRevealStyle;
        set
        {
            _useRevealStyle = value;
            if (_useRevealStyle)
            {
                PasswordBox.Style = (Style)PasswordBox.FindResource("MaterialDesignFloatingHintRevealPasswordBox");
            }
            else
            {
                PasswordBox.ClearValue(StyleProperty);
            }

        }
    }

    public BoundPasswordBox()
    {
        DataContext = new BoundPasswordBoxViewModel();
        InitializeComponent();
    }
}
