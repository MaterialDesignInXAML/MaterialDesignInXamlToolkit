using System;
using System.Windows.Media;
using MaterialDesignColors;

namespace MaterialDesignThemes.Wpf
{
    public class Theme : ITheme
    {
        public static IBaseTheme Light { get; } = new MaterialDesignLightTheme();
        public static IBaseTheme Dark { get; } = new MaterialDesignDarkTheme();

        public static Theme Create(IBaseTheme baseTheme, Color primary, Color accent)
        {
            if (baseTheme == null) throw new ArgumentNullException(nameof(baseTheme));
            var theme = new Theme();

            theme.SetBaseTheme(baseTheme);
            theme.SetPrimaryColor(primary);
            theme.SetAccentColor(accent);

            return theme;
        }

        public PairedColor SecondaryLight { get; set; }
        public PairedColor SecondaryMid { get; set; }
        public PairedColor SecondaryDark { get; set; }

        public PairedColor PrimaryLight { get; set; }
        public PairedColor PrimaryMid { get; set; }
        public PairedColor PrimaryDark { get; set; }

        public Color ValidationError { get; set; }
        public Color Background { get; set; }
        public Color Paper { get; set; }
        public Color CardBackground { get; set; }
        public Color ToolBarBackground { get; set; }
        public Color Body { get; set; }
        public Color BodyLight { get; set; }
        public Color ColumnHeader { get; set; }
        public Color CheckBoxOff { get; set; }
        public Color CheckBoxDisabled { get; set; }
        public Color Divider { get; set; }
        public Color Selection { get; set; }
        public Color FlatButtonClick { get; set; }
        public Color FlatButtonRipple { get; set; }
        public Color ToolTipBackground { get; set; }
        public Color ChipBackground { get; set; }
        public Color SnackbarBackground { get; set; }
        public Color SnackbarMouseOver { get; set; }
        public Color SnackbarRipple { get; set; }
        public Color TextBoxBorder { get; set; }
        public Color TextFieldBoxBackground { get; set; }
        public Color TextFieldBoxHoverBackground { get; set; }
        public Color TextFieldBoxDisabledBackground { get; set; }
        public Color TextAreaBorder { get; set; }
        public Color TextAreaInactiveBorder { get; set; }
    }
}