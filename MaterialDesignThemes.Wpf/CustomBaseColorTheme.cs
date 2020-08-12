using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class CustomBaseColorTheme : ResourceDictionary, IBaseTheme
    {
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

        public Color ValidationErrorColor { get; set; } = (Color)ColorConverter.ConvertFromString("#f44336");
        public Color MaterialDesignBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FF000000");
        public Color MaterialDesignPaper { get; set; } = (Color)ColorConverter.ConvertFromString("#FF303030");
        public Color MaterialDesignCardBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FF424242");
        public Color MaterialDesignToolBarBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FF212121");
        public Color MaterialDesignBody { get; set; } = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
        public Color MaterialDesignBodyLight { get; set; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignColumnHeader { get; set; } = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
        public Color MaterialDesignCheckBoxOff { get; set; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignCheckBoxDisabled { get; set; } = (Color)ColorConverter.ConvertFromString("#FF647076");
        public Color MaterialDesignTextBoxBorder { get; set; } = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
        public Color MaterialDesignDivider { get; set; } = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
        public Color MaterialDesignSelection { get; set; } = (Color)ColorConverter.ConvertFromString("#757575");
        public Color MaterialDesignToolForeground { get; set; } = (Color)ColorConverter.ConvertFromString("#FF616161");
        public Color MaterialDesignToolBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FFe0e0e0");
        public Color MaterialDesignFlatButtonClick { get; set; } = (Color)ColorConverter.ConvertFromString("#19757575");
        public Color MaterialDesignFlatButtonRipple { get; set; } = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#eeeeee");
        public Color MaterialDesignChipBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FF2E3C43");
        public Color MaterialDesignSnackbarBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
        public Color MaterialDesignSnackbarMouseOver { get; set; } = (Color)ColorConverter.ConvertFromString("#FFB9B9BD");
        public Color MaterialDesignSnackbarRipple { get; set; } = (Color)ColorConverter.ConvertFromString("#FF494949");
        public Color MaterialDesignTextFieldBoxBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#1AFFFFFF");
        public Color MaterialDesignTextFieldBoxHoverBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
        public Color MaterialDesignTextFieldBoxDisabledBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#0DFFFFFF");
        public Color MaterialDesignTextAreaBorder { get; set; } = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
        public Color MaterialDesignTextAreaInactiveBorder { get; set; } = (Color)ColorConverter.ConvertFromString("#1AFFFFFF");
        public Color MaterialDesignDataGridRowHoverBackground { get; set; } = (Color)ColorConverter.ConvertFromString("#14FFFFFF");

        private void SetTheme()
        {
            if (PrimaryColor is Color primaryColor &&
                SecondaryColor is Color secondaryColor)
            {
                var theme = Theme.Create(this, primaryColor, secondaryColor);
                ApplyTheme(theme);
            }
        }

        protected virtual void ApplyTheme(ITheme theme)
        {
            this.SetTheme(theme);
        }
    }
}