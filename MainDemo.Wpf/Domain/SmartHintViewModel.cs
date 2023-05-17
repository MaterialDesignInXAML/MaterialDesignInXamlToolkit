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
        set
        {
            _floatHint = value;
            OnPropertyChanged();
        }
    }

    public FloatingHintHorizontalAlignment SelectedAlignment
    {
        get => _selectedAlignment;
        set
        {
            _selectedAlignment = value;
            OnPropertyChanged();
        }
    }

    public double SelectedFloatingScale
    {
        get => _selectedFloatingScale;
        set
        {
            _selectedFloatingScale = value;
            OnPropertyChanged();
        }
    }

    public Point SelectedFloatingOffset
    {
        get => _selectedFloatingOffset;
        set
        {
            _selectedFloatingOffset = value;
            OnPropertyChanged();
        }
    }

    public bool ShowClearButton
    {
        get => _showClearButton;
        set
        {
            _showClearButton = value;
            OnPropertyChanged();
        }
    }

    public bool ShowLeadingIcon
    {
        get => _showLeadingIcon;
        set
        {
            _showLeadingIcon = value;
            OnPropertyChanged();
        }
    }

    public bool ShowTrailingIcon
    {
        get => _showTrailingIcon;
        set
        {
            _showTrailingIcon = value;
            OnPropertyChanged();
        }
    }

    public string HintText
    {
        get => _hintText;
        set
        {
            _hintText = value;
            OnPropertyChanged();
        }
    }

    public string HelperText
    {
        get => _helperText;
        set
        {
            _helperText = value;
            OnPropertyChanged();
        }
    }

    public bool ApplyCustomPadding
    {
        get => _applyCustomPadding;
        set
        {
            _applyCustomPadding = value;
            OnPropertyChanged();
        }
    }

    public Thickness SelectedCustomPadding
    {
        get => _selectedCustomPadding;
        set
        {
            _selectedCustomPadding = value;
            OnPropertyChanged();
        }
    }

    public double SelectedCustomHeight
    {
        get => _selectedCustomHeight;
        set
        {
            _selectedCustomHeight = value;
            OnPropertyChanged();
        }
    }

    public VerticalAlignment SelectedVerticalAlignment
    {
        get => _selectedVerticalAlignment;
        set
        {
            _selectedVerticalAlignment = value;
            OnPropertyChanged();
        }
    }

    public double SelectedLeadingIconSize
    {
        get => _selectedLeadingIconSize;
        set
        {
            _selectedLeadingIconSize = value;
            OnPropertyChanged();
        }
    }

    public double SelectedTrailingIconSize
    {
        get => _selectedTrailingIconSize;
        set
        {
            _selectedTrailingIconSize = value;
            OnPropertyChanged();
        }
    }

    public string? PrefixText
    {
        get => _prefixText;
        set
        {
            _prefixText = value;
            OnPropertyChanged();
        }
    }

    public string? SuffixText
    {
        get => _suffixText;
        set
        {
            _suffixText = value;
            OnPropertyChanged();
        }
    }

    public double SelectedFontSize
    {
        get => _selectedFontSize;
        set
        {
            _selectedFontSize = value;
            OnPropertyChanged();
        }
    }

    public FontFamily? SelectedFontFamily
    {
        get => _selectedFontFamily;
        set
        {
            _selectedFontFamily = value;
            OnPropertyChanged();
        }
    }
}
