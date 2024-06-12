namespace MaterialDesignThemes.Wpf;

public static class GroupBoxAssist
{
    private static readonly Thickness DefaultHeaderPaddingThickness = new(9, 9, 9, 9);

    #region AttachedProperty : HeaderPaddingProperty
    public static readonly DependencyProperty HeaderPaddingProperty
            = DependencyProperty.RegisterAttached("HeaderPadding", typeof(Thickness), typeof(GroupBoxAssist), new PropertyMetadata(DefaultHeaderPaddingThickness));

    public static Thickness GetHeaderPadding(GroupBox element) => (Thickness)element.GetValue(HeaderPaddingProperty);
    public static void SetHeaderPadding(GroupBox element, Thickness headerPadding) => element.SetValue(HeaderPaddingProperty, headerPadding);
    #endregion
}
