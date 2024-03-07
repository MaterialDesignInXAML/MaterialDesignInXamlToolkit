using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain;

internal class SmartHintViewModel : ViewModelBase
{
    public static Point DefaultFloatingOffset { get; } = new(0, -16);
    public static FontFamily DefaultFontFamily = (FontFamily)new MaterialDesignFontExtension().ProvideValue(null!);

    private bool _floatHint = true;
    private FloatingHintHorizontalAlignment _selectedAlignment = FloatingHintHorizontalAlignment.Inherit;
    private double _selectedFloatingScale = 0.75;
    private bool _showClearButton = true;
    private bool _showLeadingIcon = true;
    private bool _showTrailingIcon = true;
    private string _hintText = "Hint text";
    private string _helperText = "Helper text";
    private Point _selectedFloatingOffset = DefaultFloatingOffset;
    private bool _applyCustomPadding;
    private Thickness _selectedCustomPadding = new(5);
    private double _selectedCustomHeight = double.NaN;
    private VerticalAlignment _selectedVerticalAlignment = VerticalAlignment.Center;
    private double _selectedLeadingIconSize = 20;
    private double _selectedTrailingIconSize = 20;
    private string? _prefixText;
    private string? _suffixText;
    private double _selectedFontSize = double.NaN;
    private FontFamily? _selectedFontFamily = DefaultFontFamily;
    private bool _controlsEnabled = true;
    private bool _rippleOnFocus = false;
    private bool _textBoxAcceptsReturn = false;
    private int _maxLength;

    public IEnumerable<FloatingHintHorizontalAlignment> HorizontalAlignmentOptions { get; } = Enum.GetValues(typeof(FloatingHintHorizontalAlignment)).OfType<FloatingHintHorizontalAlignment>();
    public IEnumerable<double> FloatingScaleOptions { get; } = new[] {0.25, 0.5, 0.75, 1.0};
    public IEnumerable<Point> FloatingOffsetOptions { get; } = new[] { DefaultFloatingOffset, new Point(0, -25), new Point(16, -16), new Point(-16, -16), new Point(0, -50) };
    public IEnumerable<string> ComboBoxOptions { get; } = new[] {"Option 1", "Option 2", "Option 3"};
    public IEnumerable<Thickness> CustomPaddingOptions { get; } = new [] { new Thickness(0), new Thickness(5), new Thickness(10), new Thickness(15) };
    public IEnumerable<double> CustomHeightOptions { get; } = new[] { double.NaN, 50, 75, 100 };
    public IEnumerable<VerticalAlignment> VerticalAlignmentOptions { get; } = (VerticalAlignment[])Enum.GetValues(typeof(VerticalAlignment));
    public IEnumerable<double> IconSizeOptions { get; } = new[] { 10.0, 15, 20, 30, 50, 75 };
    public IEnumerable<double> FontSizeOptions { get; } = new[] { double.NaN, 8, 12, 16, 20, 24, 28 };
    public IEnumerable<FontFamily> FontFamilyOptions { get; } = new FontFamily[] { DefaultFontFamily }.Concat(Fonts.SystemFontFamilies.OrderBy(f => f.Source));

    public bool FloatHint
    {
        get => _floatHint;
        set => SetProperty(ref _floatHint, value);
    }

    public FloatingHintHorizontalAlignment SelectedAlignment
    {
        get => _selectedAlignment;
        set => SetProperty(ref _selectedAlignment, value);
    }

    public double SelectedFloatingScale
    {
        get => _selectedFloatingScale;
        set => SetProperty(ref _selectedFloatingScale, value);
    }

    public Point SelectedFloatingOffset
    {
        get => _selectedFloatingOffset;
        set => SetProperty(ref _selectedFloatingOffset, value);
    }

    public bool ShowClearButton
    {
        get => _showClearButton;
        set => SetProperty(ref _showClearButton, value);
    }

    public bool ShowLeadingIcon
    {
        get => _showLeadingIcon;
        set => SetProperty(ref _showLeadingIcon, value);
    }

    public bool ShowTrailingIcon
    {
        get => _showTrailingIcon;
        set => SetProperty(ref _showTrailingIcon, value);
    }

    public string HintText
    {
        get => _hintText;
        set => SetProperty(ref _hintText, value);
    }

    public string HelperText
    {
        get => _helperText;
        set => SetProperty(ref _helperText, value);
    }

    public bool ApplyCustomPadding
    {
        get => _applyCustomPadding;
        set => SetProperty(ref _applyCustomPadding, value);
    }

    public Thickness SelectedCustomPadding
    {
        get => _selectedCustomPadding;
        set => SetProperty(ref _selectedCustomPadding, value);
    }

    public double SelectedCustomHeight
    {
        get => _selectedCustomHeight;
        set => SetProperty(ref _selectedCustomHeight, value);
    }

    public VerticalAlignment SelectedVerticalAlignment
    {
        get => _selectedVerticalAlignment;
        set => SetProperty(ref _selectedVerticalAlignment, value);
    }

    public double SelectedLeadingIconSize
    {
        get => _selectedLeadingIconSize;
        set => SetProperty(ref _selectedLeadingIconSize, value);
    }

    public double SelectedTrailingIconSize
    {
        get => _selectedTrailingIconSize;
        set => SetProperty(ref _selectedTrailingIconSize, value);
    }

    public string? PrefixText
    {
        get => _prefixText;
        set => SetProperty(ref _prefixText, value);
    }

    public string? SuffixText
    {
        get => _suffixText;
        set => SetProperty(ref _suffixText, value);
    }

    public double SelectedFontSize
    {
        get => _selectedFontSize;
        set => SetProperty(ref _selectedFontSize, value);
    }

    public FontFamily? SelectedFontFamily
    {
        get => _selectedFontFamily;
        set => SetProperty(ref _selectedFontFamily, value);
    }

    public bool ControlsEnabled
    {
        get => _controlsEnabled;
        set => SetProperty(ref _controlsEnabled, value);
    }

    public bool RippleOnFocus
    {
        get => _rippleOnFocus;
        set => SetProperty(ref _rippleOnFocus, value);
    }

    public bool TextBoxAcceptsReturn
    {
        get => _textBoxAcceptsReturn;
        set => SetProperty(ref _textBoxAcceptsReturn, value);
    }

    public bool ShowCharacterCounter
    {
        get => MaxLength > 0;
        set => MaxLength = value == true ? 50 : 0;
    }

    public int MaxLength
    {
        get => _maxLength;
        set
        {
            SetProperty(ref _maxLength, value);
            OnPropertyChanged(nameof(ShowCharacterCounter));
        }
    }
}
