using System.Windows;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
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
                ITheme theme = Theme.Create(baseTheme.GetBaseTheme(),
                    SwatchHelper.Lookup[(MaterialDesignColor)primaryColor],
                    SwatchHelper.Lookup[(MaterialDesignColor)secondaryColor]);

                ApplyTheme(theme);
            }
        }

        protected virtual void ApplyTheme(ITheme theme) =>
            this.SetTheme(theme, ColorAdjustment);
    }
}