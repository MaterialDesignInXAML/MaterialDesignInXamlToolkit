using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public static class ListBoxItemAssist
{

    private static readonly CornerRadius DefaultCornerRadius = new(2.0);

    #region AttachedProperty : CornerRadiusProperty
    /// <summary>
    /// Controls the corner radius of the selection box.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty
        = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ListBoxItemAssist), new PropertyMetadata(DefaultCornerRadius));

    public static CornerRadius GetCornerRadius(DependencyObject element)
        => (CornerRadius)element.GetValue(CornerRadiusProperty);
    public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
    #endregion

    #region HoverBackground
    public static Brush? GetHoverBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(HoverBackgroundProperty);

    public static void SetHoverBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(HoverBackgroundProperty, value);

    public static readonly DependencyProperty HoverBackgroundProperty =
        DependencyProperty.RegisterAttached("HoverBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    #endregion HoverBackground

    #region SelectedFocusedBackground
    public static Brush? GetSelectedFocusedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(SelectedFocusedBackgroundProperty);

    public static void SetSelectedFocusedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(SelectedFocusedBackgroundProperty, value);

    public static readonly DependencyProperty SelectedFocusedBackgroundProperty =
        DependencyProperty.RegisterAttached("SelectedFocusedBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    #endregion SelectedFocusedBackground

    #region SelectedUnfocusedBackground
    public static Brush? GetSelectedUnfocusedBackground(DependencyObject obj)
        => (Brush?)obj.GetValue(SelectedUnfocusedBackgroundProperty);

    public static void SetSelectedUnfocusedBackground(DependencyObject obj, Brush? value)
        => obj.SetValue(SelectedUnfocusedBackgroundProperty, value);

    public static readonly DependencyProperty SelectedUnfocusedBackgroundProperty =
        DependencyProperty.RegisterAttached("SelectedUnfocusedBackground", typeof(Brush), typeof(ListBoxItemAssist), new PropertyMetadata(null));
    #endregion SelectedFocusedBackground

    #region ShowSelection
    public static bool GetShowSelection(DependencyObject element)
        => (bool)element.GetValue(ShowSelectionProperty);
    public static void SetShowSelection(DependencyObject element, bool value)
        => element.SetValue(ShowSelectionProperty, value);

    public static readonly DependencyProperty ShowSelectionProperty =
        DependencyProperty.RegisterAttached("ShowSelection", typeof(bool), typeof(ListBoxItemAssist), new PropertyMetadata(true));
    #endregion
}
