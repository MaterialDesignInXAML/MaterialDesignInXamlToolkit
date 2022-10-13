using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Theming;
using Theme = MaterialDesignThemes.Wpf.Theming.Theme;

namespace MaterialDesignDemo.Domain;

internal class ColorToolViewModel : ViewModelBase
{
    private readonly ThemeManager _themeManager = ThemeManager.GetApplicationThemeManager()!;

    private ColorScheme _activeScheme;
    public ColorScheme ActiveScheme
    {
        get => _activeScheme;
        set
        {
            if (_activeScheme != value)
            {
                _activeScheme = value;
                OnPropertyChanged();
            }
        }
    }

    private Color? _selectedColor;
    public Color? SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (_selectedColor != value)
            {
                _selectedColor = value;
                OnPropertyChanged();

                // if we are triggering a change internally its a hue change and the colors will match
                // so we don't want to trigger a custom color change.
                var currentSchemeColor = ActiveScheme switch
                {
                    ColorScheme.Primary => _primaryColor,
                    ColorScheme.Secondary => _secondaryColor,
                    ColorScheme.PrimaryForeground => _primaryForegroundColor,
                    ColorScheme.SecondaryForeground => _secondaryForegroundColor,
                    _ => throw new NotSupportedException($"{ActiveScheme} is not a handled ColorScheme.. Ye daft programmer!")
                };

                if (_selectedColor != currentSchemeColor && value is Color color)
                {
                    ChangeCustomColor(color);
                }
            }
        }
    }

    public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

    public ICommand ChangeCustomHueCommand { get; }

    public ICommand ChangeHueCommand { get; }
    public ICommand ChangeToPrimaryCommand { get; }
    public ICommand ChangeToSecondaryCommand { get; }
    public ICommand ChangeToPrimaryForegroundCommand { get; }
    public ICommand ChangeToSecondaryForegroundCommand { get; }

    public ICommand ToggleBaseCommand { get; }

        private void ApplyBase(bool isDark)
        {
            var theme = _themeManager.GetTheme();
            Theme baseTheme = isDark ? Theme.Dark : Theme.Light;
            theme.SetBaseTheme(baseTheme);
            _themeManager.SetTheme(theme);
        }

    public ColorToolViewModel()
    {
        ToggleBaseCommand = new AnotherCommandImplementation(o => ApplyBase((bool)o!));
        ChangeHueCommand = new AnotherCommandImplementation(ChangeHue);
        ChangeCustomHueCommand = new AnotherCommandImplementation(ChangeCustomColor);
        ChangeToPrimaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Primary));
        ChangeToSecondaryCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.Secondary));
        ChangeToPrimaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.PrimaryForeground));
        ChangeToSecondaryForegroundCommand = new AnotherCommandImplementation(o => ChangeScheme(ColorScheme.SecondaryForeground));


        var theme = _themeManager.GetTheme();

        _primaryColor = theme.PrimaryMid.Color;
        _secondaryColor = theme.SecondaryMid.Color;

        SelectedColor = _primaryColor;
    }

    private void ChangeCustomColor(object? obj)
    {
        var color = (Color)obj!;

            if (ActiveScheme == ColorScheme.Primary)
            {
                _themeManager.ChangePrimaryColor(color);
                _primaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _themeManager.ChangeSecondaryColor(color);
                _secondaryColor = color;
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SetPrimaryForegroundToSingleColor(color);
                _primaryForegroundColor = color;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SetSecondaryForegroundToSingleColor(color);
                _secondaryForegroundColor = color;
            }
        }
        else if (ActiveScheme == ColorScheme.Secondary)
        {
            _paletteHelper.ChangeSecondaryColor(color);
            _secondaryColor = color;
        }
        else if (ActiveScheme == ColorScheme.PrimaryForeground)
        {
            SetPrimaryForegroundToSingleColor(color);
            _primaryForegroundColor = color;
        }
        else if (ActiveScheme == ColorScheme.SecondaryForeground)
        {
            SetSecondaryForegroundToSingleColor(color);
            _secondaryForegroundColor = color;
        }
    }

    private void ChangeScheme(ColorScheme scheme)
    {
        ActiveScheme = scheme;
        if (ActiveScheme == ColorScheme.Primary)
        {
            SelectedColor = _primaryColor;
        }
        else if (ActiveScheme == ColorScheme.Secondary)
        {
            SelectedColor = _secondaryColor;
        }
        else if (ActiveScheme == ColorScheme.PrimaryForeground)
        {
            SelectedColor = _primaryForegroundColor;
        }
        else if (ActiveScheme == ColorScheme.SecondaryForeground)
        {
            SelectedColor = _secondaryForegroundColor;
        }
    }

    private Color? _primaryColor;

    private Color? _secondaryColor;

    private Color? _primaryForegroundColor;

    private Color? _secondaryForegroundColor;

    private void ChangeHue(object? obj)
    {
        var hue = (Color)obj!;

            SelectedColor = hue;
            if (ActiveScheme == ColorScheme.Primary)
            {
                _themeManager.ChangePrimaryColor(hue);
                _primaryColor = hue;
                _primaryForegroundColor = _themeManager.GetTheme().PrimaryMid.GetForegroundColor();
            }
            else if (ActiveScheme == ColorScheme.Secondary)
            {
                _themeManager.ChangeSecondaryColor(hue);
                _secondaryColor = hue;
                _secondaryForegroundColor = _themeManager.GetTheme().SecondaryMid.GetForegroundColor();
            }
            else if (ActiveScheme == ColorScheme.PrimaryForeground)
            {
                SetPrimaryForegroundToSingleColor(hue);
                _primaryForegroundColor = hue;
            }
            else if (ActiveScheme == ColorScheme.SecondaryForeground)
            {
                SetSecondaryForegroundToSingleColor(hue);
                _secondaryForegroundColor = hue;
            }
        }
        else if (ActiveScheme == ColorScheme.Secondary)
        {
            var theme = _themeManager.GetTheme();

    private void SetPrimaryForegroundToSingleColor(Color color)
    {
        ITheme theme = _paletteHelper.GetTheme();

            _themeManager.SetTheme(theme);
        }


    private void SetSecondaryForegroundToSingleColor(Color color)
    {
        var theme = _themeManager.GetTheme();

        theme.SecondaryLight = new ColorPair(theme.SecondaryLight.Color, color);
        theme.SecondaryMid = new ColorPair(theme.SecondaryMid.Color, color);
        theme.SecondaryDark = new ColorPair(theme.SecondaryDark.Color, color);

            _themeManager.SetTheme(theme);
    }
}
