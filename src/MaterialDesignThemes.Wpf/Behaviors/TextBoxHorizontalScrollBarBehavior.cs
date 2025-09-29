using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

internal class TextBoxHorizontalScrollBarBehavior : Behavior<ScrollViewer>
{
    private ScrollBar? _builtInScrollBar;

    public static readonly DependencyProperty TargetScrollBarProperty =
        DependencyProperty.Register(nameof(TargetScrollBar), typeof(ScrollBar), typeof(TextBoxHorizontalScrollBarBehavior), new PropertyMetadata(null, TargetScrollBarChanged));
    public ScrollBar TargetScrollBar
    {
        get => (ScrollBar)GetValue(TargetScrollBarProperty);
        set => SetValue(TargetScrollBarProperty, value);
    }

    private static void TargetScrollBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TextBoxHorizontalScrollBarBehavior b = (TextBoxHorizontalScrollBarBehavior)d;

        if (e.OldValue is ScrollBar oldValue)
        {
            oldValue.Scroll -= b.TargetScrollBar_OnScroll;
        }
        if (e.NewValue is ScrollBar newValue)
        {
            newValue.Scroll += b.TargetScrollBar_OnScroll;
        }
    }

    public static readonly DependencyProperty TargetScrollBarVisibilityProperty =
        DependencyProperty.Register(nameof(TargetScrollBarVisibility), typeof(ScrollBarVisibility), typeof(TextBoxHorizontalScrollBarBehavior), new PropertyMetadata(ScrollBarVisibility.Auto));
    public ScrollBarVisibility TargetScrollBarVisibility
    {
        get => (ScrollBarVisibility)GetValue(TargetScrollBarVisibilityProperty);
        set => SetValue(TargetScrollBarVisibilityProperty, value);
    }

    private void TargetScrollBar_OnScroll(object sender, ScrollEventArgs e)
    {
        if (AssociatedObject is not { } ao) return;
        ao.ScrollToHorizontalOffset(e.NewValue);
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        AssociatedObject.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        _builtInScrollBar = AssociatedObject.FindChild<ScrollBar>("PART_HorizontalScrollBar");
    }

    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (TargetScrollBar is not { } ts || _builtInScrollBar is null) return;

        ts.ViewportSize = AssociatedObject.ViewportWidth;
        ts.Value = AssociatedObject.HorizontalOffset;
        ts.Maximum = _builtInScrollBar.Maximum;
        UpdateTargetScrollBarVisibility(_builtInScrollBar.Maximum > 0);
    }

    private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (TargetScrollBar is not { } ts || _builtInScrollBar is null) return;

        ts.Value = AssociatedObject.HorizontalOffset;
        ts.Maximum = _builtInScrollBar.Maximum;
        UpdateTargetScrollBarVisibility(_builtInScrollBar.Maximum > 0);
    }

    private void UpdateTargetScrollBarVisibility(bool showIfRequired)
    {
        if (TargetScrollBar is not { } ts) return;

        AssociatedObject.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        ts.Visibility = TargetScrollBarVisibility switch
        {
            ScrollBarVisibility.Hidden or ScrollBarVisibility.Disabled => Visibility.Collapsed,
            ScrollBarVisibility.Visible => Visibility.Visible,
            _ => showIfRequired ? Visibility.Visible : Visibility.Collapsed,
        };
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += AssociatedObject_Loaded;
        AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject is { } ao)
        {
            ao.Loaded -= AssociatedObject_Loaded;
            ao.SizeChanged -= AssociatedObject_SizeChanged;
            ao.ScrollChanged -= AssociatedObject_ScrollChanged;
        }
        base.OnDetaching();
    }
}
