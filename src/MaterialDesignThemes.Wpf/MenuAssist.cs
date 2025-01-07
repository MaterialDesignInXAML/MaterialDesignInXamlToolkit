namespace MaterialDesignThemes.Wpf;

public static class MenuAssist
{
    #region AttachedProperty : TopLevelMenuItemHeight
    public static readonly DependencyProperty TopLevelMenuItemHeightProperty
        = DependencyProperty.RegisterAttached(
            "TopLevelMenuItemHeight",
            typeof(double),
            typeof(MenuAssist));

    public static double GetTopLevelMenuItemHeight(DependencyObject element) => (double)element.GetValue(TopLevelMenuItemHeightProperty);
    public static void SetTopLevelMenuItemHeight(DependencyObject element, double value) => element.SetValue(TopLevelMenuItemHeightProperty, value);
    #endregion

    public static readonly DependencyProperty MenuItemsPresenterMarginProperty =
        DependencyProperty.RegisterAttached(
            "MenuItemsPresenterMargin",
            typeof(Thickness),
            typeof(MenuAssist),
            new FrameworkPropertyMetadata(new Thickness(0, 16, 0, 16), FrameworkPropertyMetadataOptions.Inherits));
    public static Thickness GetMenuItemsPresenterMargin(DependencyObject obj)
        => (Thickness)obj.GetValue(MenuItemsPresenterMarginProperty);
    public static void SetMenuItemsPresenterMargin(DependencyObject obj, Thickness value)
        => obj.SetValue(MenuItemsPresenterMarginProperty, value);
}
