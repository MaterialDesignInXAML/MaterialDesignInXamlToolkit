using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public class CustomBaseColorTheme : ResourceDictionary, IBaseTheme
    {
        private Color? _primaryColor = Colors.Purple;
        public Color? PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                }
            }
        }

        private Color? _secondaryColor = Colors.Lime;
        public Color? SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                if (_secondaryColor != value)
                {
                    _secondaryColor = value;
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


        public CustomBaseColorTheme()
        {
            SetTheme();
        }

        public void SetTheme()
        {
            //if (PrimaryColor is Color primaryColor &&
            //    SecondaryColor is Color secondaryColor)
            //{
                var theme = Theme.Create(this, _primaryColor ?? Colors.Purple, _secondaryColor ?? Colors.Lime);
                ApplyTheme(theme);
            //}
        }

        protected virtual void ApplyTheme(ITheme theme)
        {
            this.SetTheme(theme);
        }


    }
}