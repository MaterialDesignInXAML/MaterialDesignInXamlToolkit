using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors;

internal class TextBoxHorizontalScrollBarBehavior : Behavior<ScrollViewer>
{
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

    private void TargetScrollBar_OnScroll(object sender, ScrollEventArgs e)
    {
        if (AssociatedObject is not { } ao) return;
        ao.ScrollToHorizontalOffset(e.NewValue);
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        => AssociatedObject.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (TargetScrollBar is not { } ts) return;

        ts.ViewportSize = AssociatedObject.ViewportWidth;
        ts.Maximum = AssociatedObject.ScrollableWidth;
    }

    private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (TargetScrollBar is not { } ts) return;

        ts.Value = AssociatedObject.HorizontalOffset;
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
