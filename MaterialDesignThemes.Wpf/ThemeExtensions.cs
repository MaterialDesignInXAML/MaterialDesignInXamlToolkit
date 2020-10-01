using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public static class ThemeExtensions
    {
        internal static ColorPair ToPairedColor(this Hue hue)
            => new ColorPair(hue.Color, hue.Foreground);

        internal static void SetPalette(this ITheme theme, Palette palette)
        {
            List<Hue> allHues = palette.PrimarySwatch.PrimaryHues.ToList();

            Hue lightHue = allHues[palette.PrimaryLightHueIndex];
            Hue midHue = allHues[palette.PrimaryMidHueIndex];
            Hue darkHue = allHues[palette.PrimaryDarkHueIndex];

            theme.PrimaryLight = lightHue.ToPairedColor();
            theme.PrimaryMid = midHue.ToPairedColor();
            theme.PrimaryDark = darkHue.ToPairedColor();
        }

        public static IBaseTheme GetBaseTheme(this BaseTheme baseTheme)
        {
            switch (baseTheme)
            {
                case BaseTheme.Dark: return Theme.Dark;
                case BaseTheme.Light: return Theme.Light;
                case BaseTheme.Inherit: return Theme.GetSystemTheme() switch
                    {
                        BaseTheme.Dark => Theme.Dark,
                        _ => Theme.Light
                    };
                default: throw new InvalidOperationException();
            }
        }

        public static BaseTheme GetBaseTheme(this ITheme theme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            var foreground = theme.Background.ContrastingForegroundColor();
            return foreground == Colors.Black ? BaseTheme.Light : BaseTheme.Dark;
        }

        public static void SetBaseTheme(this ITheme theme, IBaseTheme baseTheme)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.ValidationError = baseTheme.MaterialDesignValidationErrorColor;
            theme.Background = baseTheme.MaterialDesignBackground;
            theme.Paper = baseTheme.MaterialDesignPaper;
            theme.CardBackground = baseTheme.MaterialDesignCardBackground;
            theme.ToolBarBackground = baseTheme.MaterialDesignToolBarBackground;
            theme.Body = baseTheme.MaterialDesignBody;
            theme.BodyLight = baseTheme.MaterialDesignBodyLight;
            theme.ColumnHeader = baseTheme.MaterialDesignColumnHeader;
            theme.CheckBoxOff = baseTheme.MaterialDesignCheckBoxOff;
            theme.CheckBoxDisabled = baseTheme.MaterialDesignCheckBoxDisabled;
            theme.Divider = baseTheme.MaterialDesignDivider;
            theme.Selection = baseTheme.MaterialDesignSelection;
            theme.ToolForeground = baseTheme.MaterialDesignToolForeground;
            theme.ToolBackground = baseTheme.MaterialDesignToolBackground;
            theme.FlatButtonClick = baseTheme.MaterialDesignFlatButtonClick;
            theme.FlatButtonRipple = baseTheme.MaterialDesignFlatButtonRipple;
            theme.ToolTipBackground = baseTheme.MaterialDesignToolTipBackground;
            theme.ChipBackground = baseTheme.MaterialDesignChipBackground;
            theme.SnackbarBackground = baseTheme.MaterialDesignSnackbarBackground;
            theme.SnackbarMouseOver = baseTheme.MaterialDesignSnackbarMouseOver;
            theme.SnackbarRipple = baseTheme.MaterialDesignSnackbarRipple;
            theme.TextBoxBorder = baseTheme.MaterialDesignTextBoxBorder;
            theme.TextFieldBoxBackground = baseTheme.MaterialDesignTextFieldBoxBackground;
            theme.TextFieldBoxHoverBackground = baseTheme.MaterialDesignTextFieldBoxHoverBackground;
            theme.TextFieldBoxDisabledBackground = baseTheme.MaterialDesignTextFieldBoxDisabledBackground;
            theme.TextAreaBorder = baseTheme.MaterialDesignTextAreaBorder;
            theme.TextAreaInactiveBorder = baseTheme.MaterialDesignTextAreaInactiveBorder;
            theme.DataGridRowHoverBackground = baseTheme.MaterialDesignDataGridRowHoverBackground;
        }

        public static void SetPrimaryColor(this ITheme theme, Color primaryColor)
        {
            if (theme is null) throw new ArgumentNullException(nameof(theme));

            theme.PrimaryLight = primaryColor.Lighten();
            theme.PrimaryMid = primaryColor;
            theme.PrimaryDark = primaryColor.Darken();
        }

        public static void SetSecondaryColor(this ITheme theme, Color accentColor)
        {
            if (theme == null) throw new ArgumentNullException(nameof(theme));
            theme.SecondaryLight = accentColor.Lighten();
            theme.SecondaryMid = accentColor;
            theme.SecondaryDark = accentColor.Darken();
        }
    }
}