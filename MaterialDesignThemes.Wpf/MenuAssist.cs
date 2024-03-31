using System.Windows.Media;

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
}
