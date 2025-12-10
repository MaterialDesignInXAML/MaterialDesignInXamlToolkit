namespace MaterialDesignThemes.Wpf;

public enum TabControlHeaderBehavior
{
    Scrolling,
    Wrapping
}
public static class TabAssist
{
    public static readonly DependencyProperty HasFilledTabProperty = DependencyProperty.RegisterAttached(
        "HasFilledTab", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static void SetHasFilledTab(DependencyObject element, bool value)
        => element.SetValue(HasFilledTabProperty, value);

    public static bool GetHasFilledTab(DependencyObject element)
        => (bool)element.GetValue(HasFilledTabProperty);

    public static readonly DependencyProperty HasUniformTabWidthProperty = DependencyProperty.RegisterAttached(
        "HasUniformTabWidth", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static void SetHasUniformTabWidth(DependencyObject element, bool value)
        => element.SetValue(HasUniformTabWidthProperty, value);

    public static bool GetHasUniformTabWidth(DependencyObject element)
        => (bool)element.GetValue(HasUniformTabWidthProperty);

    public static readonly DependencyProperty HeaderPanelMarginProperty = DependencyProperty.RegisterAttached(
        "HeaderPanelMargin", typeof(Thickness), typeof(TabAssist), new PropertyMetadata(default(Thickness)));

    public static void SetHeaderPanelMargin(DependencyObject element, Thickness value)
        => element.SetValue(HeaderPanelMarginProperty, value);

    public static Thickness GetHeaderPanelMargin(DependencyObject element)
        => (Thickness)element.GetValue(HeaderPanelMarginProperty);

    public static Visibility GetBindableIsItemsHost(DependencyObject obj)
        => (Visibility)obj.GetValue(BindableIsItemsHostProperty);

    public static void SetBindableIsItemsHost(DependencyObject obj, Visibility value)
        => obj.SetValue(BindableIsItemsHostProperty, value);

    public static readonly DependencyProperty BindableIsItemsHostProperty =
        DependencyProperty.RegisterAttached("BindableIsItemsHost", typeof(Visibility), typeof(TabAssist), new PropertyMetadata(Visibility.Collapsed, OnBindableIsItemsHostChanged));

    private static void OnBindableIsItemsHostChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Panel panel)
        {
            panel.IsItemsHost = (Visibility)e.NewValue == Visibility.Visible;
        }
    }

    public static Cursor GetTabHeaderCursor(DependencyObject obj)
        => (Cursor)obj.GetValue(TabHeaderCursorProperty);

    public static void SetTabHeaderCursor(DependencyObject obj, Cursor value)
        => obj.SetValue(TabHeaderCursorProperty, value);

    public static readonly DependencyProperty TabHeaderCursorProperty =
        DependencyProperty.RegisterAttached("TabHeaderCursor", typeof(Cursor), typeof(TabAssist), new PropertyMetadata(Cursors.Hand));

    public static TabControlHeaderBehavior GetHeaderBehavior(DependencyObject obj)
        => (TabControlHeaderBehavior)obj.GetValue(HeaderBehaviorProperty);

    public static void SetHeaderBehavior(DependencyObject obj, TabControlHeaderBehavior value)
        => obj.SetValue(HeaderBehaviorProperty, value);

    public static readonly DependencyProperty HeaderBehaviorProperty =
        DependencyProperty.RegisterAttached("HeaderBehavior", typeof(TabControlHeaderBehavior), typeof(TabAssist),
            new PropertyMetadata(TabControlHeaderBehavior.Scrolling));

    public static bool GetAnimateTabScrolling(DependencyObject obj)
        => (bool)obj.GetValue(AnimateTabScrollingProperty);

    public static void SetAnimateTabScrolling(DependencyObject obj, bool value)
        => obj.SetValue(AnimateTabScrollingProperty, value);

    public static readonly DependencyProperty AnimateTabScrollingProperty =
        DependencyProperty.RegisterAttached("AnimateTabScrolling", typeof(bool), typeof(TabAssist), new PropertyMetadata(false));

    public static double GetTabScrollOffset(DependencyObject obj)
        => (double)obj.GetValue(TabScrollOffsetProperty);

    public static void SetTabScrollOffset(DependencyObject obj, double value)
        => obj.SetValue(TabScrollOffsetProperty, value);

    public static readonly DependencyProperty TabScrollOffsetProperty =
        DependencyProperty.RegisterAttached("TabScrollOffset", typeof(double),
            typeof(TabAssist), new PropertyMetadata(0d));
}
