using System.Windows.Media;
using MaterialDesignDemo.Shared.Domain;
using MaterialDesignThemes.Wpf;

namespace MaterialDesignDemo.Domain;

internal class SmartHintViewModel : ViewModelBase
{
    public static Point DefaultFloatingOffset { get; } = new(0, 0);
    public static FontFamily DefaultFontFamily = (FontFamily)new MaterialDesignFontExtension().ProvideValue(null!);

    private bool _floatHint = true;
    private FloatingHintHorizontalAlignment _selectedAlignment = FloatingHintHorizontalAlignment.Inherit;
    private FloatingHintHorizontalAlignment _selectedFloatingAlignment = FloatingHintHorizontalAlignment.Inherit;
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
    private VerticalAlignment _selectedVerticalAlignment = VerticalAlignment.Stretch;
    private double _selectedLeadingIconSize = 20;
    private double _selectedTrailingIconSize = 20;
    private VerticalAlignment _selectedIconVerticalAlignment = VerticalAlignment.Center;
    private string? _prefixText = "pre";
    private string? _suffixText = "suf";
    private double _selectedFontSize = double.NaN;
    private FontFamily? _selectedFontFamily = DefaultFontFamily;
    private bool _controlsEnabled = true;
    private bool _rippleOnFocus = false;
    private bool _textBoxAcceptsReturn = false;
    private bool _textBoxIsReadOnly = false;
    private int _maxLength;
    private PrefixSuffixVisibility _selectedPrefixVisibility = PrefixSuffixVisibility.WhenFocusedOrNonEmpty;
    private PrefixSuffixHintBehavior _selectedPrefixHintBehavior = PrefixSuffixHintBehavior.AlignWithPrefixSuffix;
    private PrefixSuffixVisibility _selectedSuffixVisibility = PrefixSuffixVisibility.WhenFocusedOrNonEmpty;
    private PrefixSuffixHintBehavior _selectedSuffixHintBehavior = PrefixSuffixHintBehavior.AlignWithPrefixSuffix;
    private bool _newSpecHighlightingEnabled;
    private ScrollBarVisibility _selectedVerticalScrollBarVisibility = ScrollBarVisibility.Auto;
    private ScrollBarVisibility _selectedHorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
    private Thickness _outlineStyleBorderThickness = new(1);
    private Thickness _outlineStyleActiveBorderThickness = new(2);
    private TextWrapping _textBoxTextWrapping = TextWrapping.Wrap;
    private double _selectedMaxWidth = 200;

    public IEnumerable<FloatingHintHorizontalAlignment> HorizontalAlignmentOptions { get; } = Enum.GetValues(typeof(FloatingHintHorizontalAlignment)).OfType<FloatingHintHorizontalAlignment>();
    public IEnumerable<double> FloatingScaleOptions { get; } = [0.25, 0.5, 0.75, 1.0, 1.25, 1.5, 1.75, 2.0];
    public IEnumerable<Point> FloatingOffsetOptions { get; } = [DefaultFloatingOffset, new Point(0, -25), new Point(0, -16), new Point(16, -16), new Point(-16, -16), new Point(0, -50), new Point(-50, -50), new Point(50, -50)];
    public IEnumerable<string> ComboBoxOptions { get; } = ["Option 1", "Option 2", "Option 3"];
    public IEnumerable<Thickness> CustomPaddingOptions { get; } = [new Thickness(0), new Thickness(5), new Thickness(10), new Thickness(15)];
    public IEnumerable<double> CustomHeightOptions { get; } = [double.NaN, 50, 75, 100, 150];
    public IEnumerable<VerticalAlignment> VerticalAlignmentOptions { get; } = Enum.GetValues(typeof(VerticalAlignment)).OfType<VerticalAlignment>();
    public IEnumerable<double> IconSizeOptions { get; } = [10.0, 15, 20, 30, 50, 75];
    public IEnumerable<double> FontSizeOptions { get; } = [double.NaN, 8, 12, 16, 20, 24, 28];
    public IEnumerable<FontFamily> FontFamilyOptions { get; } = new FontFamily[] { DefaultFontFamily }.Concat(Fonts.SystemFontFamilies.OrderBy(f => f.Source));
    public IEnumerable<PrefixSuffixVisibility> PrefixSuffixVisibilityOptions { get; } = Enum.GetValues(typeof(PrefixSuffixVisibility)).OfType<PrefixSuffixVisibility>();
    public IEnumerable<PrefixSuffixHintBehavior> PrefixSuffixHintBehaviorOptions { get; } = Enum.GetValues(typeof(PrefixSuffixHintBehavior)).OfType<PrefixSuffixHintBehavior>();
    public IEnumerable<ScrollBarVisibility> ScrollBarVisibilityOptions { get; } = Enum.GetValues(typeof(ScrollBarVisibility)).OfType<ScrollBarVisibility>();
    public IEnumerable<Thickness> CustomOutlineStyleBorderThicknessOptions { get; } = [new Thickness(1), new Thickness(2), new Thickness(3), new Thickness(4), new Thickness(5), new Thickness(6) ];
    public IEnumerable<TextWrapping> TextWrappingOptions { get; } = Enum.GetValues(typeof(TextWrapping)).OfType<TextWrapping>();
    public IEnumerable<double> MaxWidthOptions { get; } = [double.NaN, 200];
    public IEnumerable<string> AutoSuggestBoxSuggestions { get; } = ["alpha", "bravo", "charlie", "delta", "echo", "foxtrot", "golf", "hotel", "india", "juliette", "kilo", "lima"];

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

    public FloatingHintHorizontalAlignment SelectedFloatingAlignment
    {
        get => _selectedFloatingAlignment;
        set => SetProperty(ref _selectedFloatingAlignment, value);
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

    public VerticalAlignment SelectedIconVerticalAlignment
    {
        get => _selectedIconVerticalAlignment;
        set => SetProperty(ref _selectedIconVerticalAlignment, value);
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

    public bool TextBoxIsReadOnly
    {
        get => _textBoxIsReadOnly;
        set => SetProperty(ref _textBoxIsReadOnly, value);
    }

    public bool ShowCharacterCounter
    {
        get => MaxLength > 0;
        set => MaxLength = value ? 50 : 0;
    }

    public int MaxLength
    {
        get => _maxLength;
        set
        {
            if (SetProperty(ref _maxLength, value))
            {
                OnPropertyChanged(nameof(ShowCharacterCounter));
            }
        }
    }

    public PrefixSuffixVisibility SelectedPrefixVisibility
    {
        get => _selectedPrefixVisibility;
        set => SetProperty(ref _selectedPrefixVisibility, value);
    }

    public PrefixSuffixHintBehavior SelectedPrefixHintBehavior
    {
        get => _selectedPrefixHintBehavior;
        set => SetProperty(ref _selectedPrefixHintBehavior, value);
    }

    public PrefixSuffixVisibility SelectedSuffixVisibility
    {
        get => _selectedSuffixVisibility;
        set => SetProperty(ref _selectedSuffixVisibility, value);
    }

    public PrefixSuffixHintBehavior SelectedSuffixHintBehavior
    {
        get => _selectedSuffixHintBehavior;
        set => SetProperty(ref _selectedSuffixHintBehavior, value);
    }

    public bool NewSpecHighlightingEnabled
    {
        get => _newSpecHighlightingEnabled;
        set => SetProperty(ref _newSpecHighlightingEnabled, value);
    }

    public ScrollBarVisibility SelectedVerticalScrollBarVisibility
    {
        get => _selectedVerticalScrollBarVisibility;
        set => SetProperty(ref _selectedVerticalScrollBarVisibility, value);
    }

    public ScrollBarVisibility SelectedHorizontalScrollBarVisibility
    {
        get => _selectedHorizontalScrollBarVisibility;
        set => SetProperty(ref _selectedHorizontalScrollBarVisibility, value);
    }

    public Thickness OutlineStyleBorderThickness
    {
        get => _outlineStyleBorderThickness;
        set => SetProperty(ref _outlineStyleBorderThickness, value);
    }

    public Thickness OutlineStyleActiveBorderThickness
    {
        get => _outlineStyleActiveBorderThickness;
        set => SetProperty(ref _outlineStyleActiveBorderThickness, value);
    }

    public TextWrapping TextBoxTextWrapping
    {
        get => _textBoxTextWrapping;
        set => SetProperty(ref _textBoxTextWrapping, value);
    }

    public double SelectedMaxWidth
    {
        get => _selectedMaxWidth;
        set => SetProperty(ref _selectedMaxWidth, value);
    }
}
