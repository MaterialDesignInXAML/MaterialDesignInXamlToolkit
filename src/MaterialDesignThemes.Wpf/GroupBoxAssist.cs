namespace MaterialDesignThemes.Wpf;

public static class GroupBoxAssist
{
    private const double DefaultHeaderPadding = 9.0;

    #region AttachedProperty : HeaderPaddingProperty
    public static readonly DependencyProperty HeaderPaddingProperty
            = DependencyProperty.RegisterAttached("HeaderPadding", typeof(double), typeof(GroupBoxAssist), new PropertyMetadata(DefaultHeaderPadding));

    public static double GetHeaderPadding(GroupBox element) => (double)element.GetValue(HeaderPaddingProperty);
    public static void SetHeaderPadding(GroupBox element, double headerPadding) => element.SetValue(HeaderPaddingProperty, headerPadding);
    #endregion
}
