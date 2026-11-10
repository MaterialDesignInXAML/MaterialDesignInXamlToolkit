namespace MaterialDesignThemes.Wpf;

public class CheckBoxAssist
{
    private const double DefaultCheckBoxSize = 18.0;

    #region AttachedProperty : CheckBoxSizeProperty
    public static readonly DependencyProperty CheckBoxSizeProperty
        = DependencyProperty.RegisterAttached("CheckBoxSize", typeof(double), typeof(CheckBoxAssist), new PropertyMetadata(DefaultCheckBoxSize));

    public static double GetCheckBoxSize(CheckBox element) => (double)element.GetValue(CheckBoxSizeProperty);
    public static void SetCheckBoxSize(CheckBox element, double checkBoxSize) => element.SetValue(CheckBoxSizeProperty, checkBoxSize);
    #endregion

    #region AttachedProperty : ContentPlacementProperty
    /// <summary>
    /// Where the content is placed relative to the check box glyph.
    /// </summary>
    public static readonly DependencyProperty ContentPlacementProperty
        = DependencyProperty.RegisterAttached("ContentPlacement", typeof(Dock), typeof(CheckBoxAssist), new PropertyMetadata(Dock.Right));

    public static Dock GetContentPlacement(CheckBox element)
        => (Dock)element.GetValue(ContentPlacementProperty);
    public static void SetContentPlacement(CheckBox element, Dock value)
        => element.SetValue(ContentPlacementProperty, value);
    #endregion
}
