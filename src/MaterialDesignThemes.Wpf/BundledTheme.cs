using System.Windows.Media;
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
            if (SetField(ref _baseTheme, value))
            {
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
            if (SetField(ref _primaryColor, value))
            {
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
            if (SetField(ref _secondaryColor, value))
            {
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
            if (SetField(ref _colorAdjustment, value))
            {
                SetTheme();
            }
        }
    }

    private void SetTheme()
    {
        if (BaseTheme is not BaseTheme baseTheme ||
            PrimaryColor is not PrimaryColor primaryColor ||
            SecondaryColor is not SecondaryColor secondaryColor)
        {
            return;
        }

        // only perform the registry lookup if needed, and only once
        Lazy<Color?> accentColor = new(Theme.GetSystemAccentColor);

        Color colorPrimary = primaryColor == MaterialDesignColors.PrimaryColor.Inherit
                              ? (accentColor.Value ?? default)
                              : SwatchHelper.Lookup[(MaterialDesignColor)primaryColor];

        Color colorSecondary = secondaryColor == MaterialDesignColors.SecondaryColor.Inherit
                              ? (accentColor.Value ?? default)
                              : SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor];

        Theme theme = Theme.Create(baseTheme, colorPrimary, colorSecondary);
        theme.ColorAdjustment = ColorAdjustment;

        ApplyTheme(theme);
    }

    protected static bool SetField<T>(ref T field, T value)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        return true;
    }

    protected virtual void ApplyTheme(Theme theme) =>
        this.SetTheme(theme);
}
