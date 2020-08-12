using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class CustomBaseColorTheme : ResourceDictionary, IBaseTheme
    {
        public bool DesignTime { get; set; } = false;

        private Color? _primaryColor;
        public Color? PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    if (!DesignTime) { SetTheme(); }
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
                    if (!DesignTime) { SetTheme(); }
                }
            }
        }

        public Color ValidationErrorColor  { get; set; } = (Color) ColorConverter.ConvertFromString("#f44336");
        public Color MaterialDesignBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignPaper  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFAFAFA");
        public Color MaterialDesignCardBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFFFFFFF");
        public Color MaterialDesignToolBarBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFF5F5F5");
        public Color MaterialDesignBody  { get; set; } = (Color) ColorConverter.ConvertFromString("#DD000000");
        public Color MaterialDesignBodyLight  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignColumnHeader  { get; set; } = (Color) ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignCheckBoxOff  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignCheckBoxDisabled  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFBDBDBD");
        public Color MaterialDesignTextBoxBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#89000000");
        public Color MaterialDesignDivider  { get; set; } = (Color) ColorConverter.ConvertFromString("#1F000000");
        public Color MaterialDesignSelection  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignToolForeground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF616161");
        public Color MaterialDesignToolBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFE0E0E0");
        public Color MaterialDesignFlatButtonClick  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFDEDEDE");
        public Color MaterialDesignFlatButtonRipple  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignToolTipBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#757575");
        public Color MaterialDesignChipBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#12000000");
        public Color MaterialDesignSnackbarBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF323232");
        public Color MaterialDesignSnackbarMouseOver  { get; set; } = (Color) ColorConverter.ConvertFromString("#FF464642");
        public Color MaterialDesignSnackbarRipple  { get; set; } = (Color) ColorConverter.ConvertFromString("#FFB6B6B6");
        public Color MaterialDesignTextFieldBoxBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#0F000000");
        public Color MaterialDesignTextFieldBoxHoverBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#14000000");
        public Color MaterialDesignTextFieldBoxDisabledBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#08000000");
        public Color MaterialDesignTextAreaBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#BC000000");
        public Color MaterialDesignTextAreaInactiveBorder  { get; set; } = (Color) ColorConverter.ConvertFromString("#0F000000");
        public Color MaterialDesignDataGridRowHoverBackground  { get; set; } = (Color) ColorConverter.ConvertFromString("#0A000000");

        public void SetTheme()
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

        static CustomBaseColorTheme FromDark()
        {
            CustomBaseColorTheme result = new CustomBaseColorTheme();
            result.PrimaryColor = Colors.Purple;
            result.SecondaryColor = Colors.Lime;
            result.ValidationErrorColor = (Color)ColorConverter.ConvertFromString("#f44336");
            result.MaterialDesignBackground = (Color)ColorConverter.ConvertFromString("#FF000000");
            result.MaterialDesignPaper = (Color)ColorConverter.ConvertFromString("#FF303030");
            result.MaterialDesignCardBackground = (Color)ColorConverter.ConvertFromString("#FF424242");
            result.MaterialDesignToolBarBackground = (Color)ColorConverter.ConvertFromString("#FF212121");
            result.MaterialDesignBody = (Color)ColorConverter.ConvertFromString("#DDFFFFFF");
            result.MaterialDesignBodyLight = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
            result.MaterialDesignColumnHeader = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
            result.MaterialDesignCheckBoxOff = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
            result.MaterialDesignCheckBoxDisabled = (Color)ColorConverter.ConvertFromString("#FF647076");
            result.MaterialDesignTextBoxBorder = (Color)ColorConverter.ConvertFromString("#89FFFFFF");
            result.MaterialDesignDivider = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
            result.MaterialDesignSelection = (Color)ColorConverter.ConvertFromString("#757575");
            result.MaterialDesignToolForeground = (Color)ColorConverter.ConvertFromString("#FF616161");
            result.MaterialDesignToolBackground = (Color)ColorConverter.ConvertFromString("#FFe0e0e0");
            result.MaterialDesignFlatButtonClick = (Color)ColorConverter.ConvertFromString("#19757575");
            result.MaterialDesignFlatButtonRipple = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
            result.MaterialDesignToolTipBackground = (Color)ColorConverter.ConvertFromString("#eeeeee");
            result.MaterialDesignChipBackground = (Color)ColorConverter.ConvertFromString("#FF2E3C43");
            result.MaterialDesignSnackbarBackground = (Color)ColorConverter.ConvertFromString("#FFCDCDCD");
            result.MaterialDesignSnackbarMouseOver = (Color)ColorConverter.ConvertFromString("#FFB9B9BD");
            result.MaterialDesignSnackbarRipple = (Color)ColorConverter.ConvertFromString("#FF494949");
            result.MaterialDesignTextFieldBoxBackground = (Color)ColorConverter.ConvertFromString("#1AFFFFFF");
            result.MaterialDesignTextFieldBoxHoverBackground = (Color)ColorConverter.ConvertFromString("#1FFFFFFF");
            result.MaterialDesignTextFieldBoxDisabledBackground = (Color)ColorConverter.ConvertFromString("#0DFFFFFF");
            result.MaterialDesignTextAreaBorder = (Color)ColorConverter.ConvertFromString("#BCFFFFFF");
            result.MaterialDesignTextAreaInactiveBorder = (Color)ColorConverter.ConvertFromString("#1AFFFFFF");
            result.MaterialDesignDataGridRowHoverBackground = (Color)ColorConverter.ConvertFromString("#14FFFFFF");
            return result;
        }

        static CustomBaseColorTheme FromLight()
        {
            CustomBaseColorTheme result = new CustomBaseColorTheme();
            result.PrimaryColor = Colors.Purple;
            result.SecondaryColor = Colors.Lime;
            result.ValidationErrorColor = (Color)ColorConverter.ConvertFromString("#f44336");
            result.MaterialDesignBackground = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            result.MaterialDesignPaper = (Color)ColorConverter.ConvertFromString("#FFFAFAFA");
            result.MaterialDesignCardBackground = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
            result.MaterialDesignToolBarBackground = (Color)ColorConverter.ConvertFromString("#FFF5F5F5");
            result.MaterialDesignBody = (Color)ColorConverter.ConvertFromString("#DD000000");
            result.MaterialDesignBodyLight = (Color)ColorConverter.ConvertFromString("#89000000");
            result.MaterialDesignColumnHeader = (Color)ColorConverter.ConvertFromString("#BC000000");
            result.MaterialDesignCheckBoxOff = (Color)ColorConverter.ConvertFromString("#89000000");
            result.MaterialDesignCheckBoxDisabled = (Color)ColorConverter.ConvertFromString("#FFBDBDBD");
            result.MaterialDesignTextBoxBorder = (Color)ColorConverter.ConvertFromString("#89000000");
            result.MaterialDesignDivider = (Color)ColorConverter.ConvertFromString("#1F000000");
            result.MaterialDesignSelection = (Color)ColorConverter.ConvertFromString("#FFDEDEDE");
            result.MaterialDesignToolForeground = (Color)ColorConverter.ConvertFromString("#FF616161");
            result.MaterialDesignToolBackground = (Color)ColorConverter.ConvertFromString("#FFE0E0E0");
            result.MaterialDesignFlatButtonClick = (Color)ColorConverter.ConvertFromString("#FFDEDEDE");
            result.MaterialDesignFlatButtonRipple = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
            result.MaterialDesignToolTipBackground = (Color)ColorConverter.ConvertFromString("#757575");
            result.MaterialDesignChipBackground = (Color)ColorConverter.ConvertFromString("#12000000");
            result.MaterialDesignSnackbarBackground = (Color)ColorConverter.ConvertFromString("#FF323232");
            result.MaterialDesignSnackbarMouseOver = (Color)ColorConverter.ConvertFromString("#FF464642");
            result.MaterialDesignSnackbarRipple = (Color)ColorConverter.ConvertFromString("#FFB6B6B6");
            result.MaterialDesignTextFieldBoxBackground = (Color)ColorConverter.ConvertFromString("#0F000000");
            result.MaterialDesignTextFieldBoxHoverBackground = (Color)ColorConverter.ConvertFromString("#14000000");
            result.MaterialDesignTextFieldBoxDisabledBackground = (Color)ColorConverter.ConvertFromString("#08000000");
            result.MaterialDesignTextAreaBorder = (Color)ColorConverter.ConvertFromString("#BC000000");
            result.MaterialDesignTextAreaInactiveBorder = (Color)ColorConverter.ConvertFromString("#0F000000");
            result.MaterialDesignDataGridRowHoverBackground = (Color)ColorConverter.ConvertFromString("#0A000000");
            return result;
        }


    }
}