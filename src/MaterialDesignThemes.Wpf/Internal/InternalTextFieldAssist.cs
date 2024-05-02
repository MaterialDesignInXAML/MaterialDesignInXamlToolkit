namespace MaterialDesignThemes.Wpf.Internal;

public static class InternalTextFieldAssist
{
    /// <summary>
    /// Used by text field "wrappers" (i.e. controls hosting a text field and decorating on top of it) to signal to the text field that the mouse is over it,
    /// when in fact it is over a sibling (i.e. something in the wrapper) which is visually placed on top of the text field.
    /// </summary>
    public static readonly DependencyProperty IsMouseOverProperty = DependencyProperty.RegisterAttached(
        "IsMouseOver", typeof(bool), typeof(InternalTextFieldAssist), new PropertyMetadata(default(bool)));
    public static void SetIsMouseOver(DependencyObject element, bool value) => element.SetValue(IsMouseOverProperty, value);
    public static bool GetIsMouseOver(DependencyObject element) => (bool)element.GetValue(IsMouseOverProperty);
}
