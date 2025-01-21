using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public class CheckBoxAssist
{
    private const double DefaultCheckBoxSize = 18.0;

    #region AttachedProperty : CheckBoxSizeProperty
    public static readonly DependencyProperty CheckBoxSizeProperty = DependencyProperty.RegisterAttached(
        "CheckBoxSize",
        typeof(double),
        typeof(CheckBoxAssist),
        new PropertyMetadata(DefaultCheckBoxSize)
    );

    public static double GetCheckBoxSize(CheckBox element) => (double)element.GetValue(CheckBoxSizeProperty);

    public static void SetCheckBoxSize(CheckBox element, double checkBoxSize) => element.SetValue(CheckBoxSizeProperty, checkBoxSize);
    #endregion



    #region AttachedProperty : CheckBoxForegroundProperty
    public static readonly DependencyProperty CheckBoxForegroundProperty = DependencyProperty.RegisterAttached(
        "CheckBoxForeground",
        typeof(Brush),
        typeof(CheckBoxAssist),
        null
    );

    public static Brush GetCheckBoxForeground(CheckBox element) => (Brush)element.GetValue(CheckBoxForegroundProperty);

    public static void SetCheckBoxForeground(CheckBox element, Brush checkBoxForeground) =>
        element.SetValue(CheckBoxForegroundProperty, checkBoxForeground);
    #endregion
}
