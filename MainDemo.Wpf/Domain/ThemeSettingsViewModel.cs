using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Theming;
using Theme = MaterialDesignThemes.Wpf.Theming.Theme;

namespace MaterialDesignDemo.Domain
{
    public class ThemeSettingsViewModel : ViewModelBase
    {
        public ThemeSettingsViewModel()
        {
            ThemeManager themeManager = ThemeManager.GetApplicationThemeManager()!;
            Theme theme = themeManager.GetTheme();

            IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;

            _isColorAdjusted = theme.ColorAdjustment is not null;

            var colorAdjustment = theme.ColorAdjustment ?? new ColorAdjustment();
            _desiredContrastRatio = colorAdjustment.DesiredContrastRatio;
            _contrastValue = colorAdjustment.Contrast;
            _colorSelectionValue = colorAdjustment.Colors;

            themeManager.ThemeChanged += (_, e) =>
            {
                IsDarkTheme = e.NewTheme?.GetBaseTheme() == BaseTheme.Dark;
            };
        }

        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private bool _isColorAdjusted;
        public bool IsColorAdjusted
        {
            get => _isColorAdjusted;
            set
            {
                if (SetProperty(ref _isColorAdjusted, value))
                {
                    ModifyTheme(theme =>
                    {
                        if (theme is Theme internalTheme)
                        {
                            internalTheme.ColorAdjustment = value
                                ? new ColorAdjustment
                                {
                                    DesiredContrastRatio = DesiredContrastRatio,
                                    Contrast = ContrastValue,
                                    Colors = ColorSelectionValue
                                }
                                : null;
                        }
                    });
                }
            }
        }

        private float _desiredContrastRatio = 4.5f;
        public float DesiredContrastRatio
        {
            get => _desiredContrastRatio;
            set
            {
                if (SetProperty(ref _desiredContrastRatio, value))
                {
                    ModifyTheme(theme =>
                    {
                        if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                            internalTheme.ColorAdjustment.DesiredContrastRatio = value;
                    });
                }
            }
        }

        public IEnumerable<Contrast> ContrastValues => Enum.GetValues(typeof(Contrast)).Cast<Contrast>();

        private Contrast _contrastValue;
        public Contrast ContrastValue
        {
            get => _contrastValue;
            set
            {
                if (SetProperty(ref _contrastValue, value))
                {
                    ModifyTheme(theme =>
                    {
                        if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                            internalTheme.ColorAdjustment.Contrast = value;
                    });
                }
            }
        }

        public IEnumerable<ColorSelection> ColorSelectionValues => Enum.GetValues(typeof(ColorSelection)).Cast<ColorSelection>();

        private ColorSelection _colorSelectionValue;
        public ColorSelection ColorSelectionValue
        {
            get => _colorSelectionValue;
            set
            {
                if (SetProperty(ref _colorSelectionValue, value))
                {
                    ModifyTheme(theme =>
                    {
                        if (theme is Theme internalTheme && internalTheme.ColorAdjustment != null)
                            internalTheme.ColorAdjustment.Colors = value;
                    });
                }
            }
        }

        private static void ModifyTheme(Action<Theme> modificationAction)
        {
            ThemeManager themeManager = ThemeManager.GetApplicationThemeManager()!;
            Theme theme = themeManager.GetTheme();

            modificationAction?.Invoke(theme);

            themeManager.SetTheme(theme);
        }
    }
}
