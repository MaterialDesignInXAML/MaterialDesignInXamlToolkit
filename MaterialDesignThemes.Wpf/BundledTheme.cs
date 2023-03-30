using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf;

public class BundledTheme : ResourceDictionary, IMaterialDesignThemeDictionary
{
    private BaseTheme? _baseTheme;
    public BaseTheme? BaseTheme
    {
        get => _baseTheme;
        set
        {
            if (_baseTheme != value)
            {
                _baseTheme = value;
                SetTheme();
            }
        }
    }

    private PrimaryColor? _primaryColor;
    public PrimaryColor? PrimaryColor
    {
        get => _primaryColor;
        set
        {
            if (_primaryColor != value)
            {
                _primaryColor = value;
                SetTheme();
            }
        }
    }

    private SecondaryColor? _secondaryColor;
    public SecondaryColor? SecondaryColor
    {
        get => _secondaryColor;
        set
        {
            if (_secondaryColor != value)
            {
                _secondaryColor = value;
                SetTheme();
            }
        }
    }

    private ColorAdjustment? _colorAdjustment;
    public ColorAdjustment? ColorAdjustment
    {
        get => _colorAdjustment;
        set
        {
            if (_colorAdjustment != value)
            {
                _colorAdjustment = value;
                SetTheme();
            }
        }
    }

    private void SetTheme()
    {
        if (BaseTheme is BaseTheme baseTheme &&
            PrimaryColor is PrimaryColor primaryColor &&
            SecondaryColor is SecondaryColor secondaryColor)
        {
            Theme theme = Theme.Create(baseTheme,
                SwatchHelper.Lookup[(MaterialDesignColor)primaryColor],
                SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor]);
            theme.ColorAdjustment = ColorAdjustment;

            ApplyTheme(theme);
        }
    }

    protected virtual void ApplyTheme(Theme theme) =>
        this.SetTheme(theme);
}
