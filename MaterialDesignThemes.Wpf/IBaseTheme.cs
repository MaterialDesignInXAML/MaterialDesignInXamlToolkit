using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public interface IBaseTheme
    {
        Color ValidationErrorColor { get; }
        Color MaterialDesignBackground { get; }
        Color MaterialDesignPaper { get; }
        Color MaterialDesignCardBackground { get; }
        Color MaterialDesignToolBarBackground { get; }
        Color MaterialDesignBody { get; }
        Color MaterialDesignBodyLight { get; }
        Color MaterialDesignColumnHeader { get; }
        Color MaterialDesignCheckBoxOff { get; }
        Color MaterialDesignCheckBoxDisabled { get; }
        Color MaterialDesignTextBoxBorder { get; }
        Color MaterialDesignDivider { get; }
        Color MaterialDesignSelection { get; }
        Color MaterialDesignFlatButtonClick { get; }
        Color MaterialDesignFlatButtonRipple { get; }
        Color MaterialDesignToolTipBackground { get; }
        Color MaterialDesignChipBackground { get; }
        Color MaterialDesignSnackbarBackground { get; }
        Color MaterialDesignSnackbarMouseOver { get; }
        Color MaterialDesignSnackbarRipple { get; }
        Color MaterialDesignTextFieldBoxBackground { get; }
        Color MaterialDesignTextFieldBoxHoverBackground { get; }
        Color MaterialDesignTextFieldBoxDisabledBackground { get; }
        Color MaterialDesignTextAreaBorder { get; }
        Color MaterialDesignTextAreaInactiveBorder { get; }
    }
}
