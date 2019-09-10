using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class CustomColorTheme : ResourceDictionary
    {
        private BaseTheme? _baseTheme;
        public BaseTheme? BaseTheme
        {
            get { return _baseTheme; }
            set
            {
                if (_baseTheme != value)
                {
                    _baseTheme = value;
                    SetTheme();
                }
            }
        }

        private Color? _primaryColor;
        public Color? PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    SetTheme();
                }
            }
        }

        private Color? _secondaryColor;
        public Color? SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                if (_secondaryColor != value)
                {
                    _secondaryColor = value;
                    SetTheme();
                }
            }
        }

        private void SetTheme()
        {
            if (BaseTheme is BaseTheme baseTheme &&
                PrimaryColor is Color primaryColor &&
                SecondaryColor is Color secondaryColor)
            {
                var theme = Theme.Create(baseTheme.GetBaseTheme(), primaryColor, secondaryColor);
                ApplyTheme(theme);
            }
        }

        protected virtual void ApplyTheme(ITheme theme)
        {
            this.SetTheme(theme);
        }
    }
}