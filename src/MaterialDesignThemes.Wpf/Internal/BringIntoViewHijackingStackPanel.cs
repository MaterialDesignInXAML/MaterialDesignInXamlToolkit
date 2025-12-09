
using MaterialDesignThemes.Wpf.Behaviors.Internal;

namespace MaterialDesignThemes.Wpf.Internal;

public class BringIntoViewHijackingStackPanel : StackPanel
{
    public TabScrollDirection TabScrollDirection
    {
        get => (TabScrollDirection)GetValue(TabScrollDirectionProperty);
        set => SetValue(TabScrollDirectionProperty, value);
    }

    public static readonly DependencyProperty TabScrollDirectionProperty =
        DependencyProperty.Register(nameof(TabScrollDirection), typeof(TabScrollDirection),
            typeof(BringIntoViewHijackingStackPanel), new PropertyMetadata(TabScrollDirection.Unknown));

    public BringIntoViewHijackingStackPanel()
        => AddHandler(FrameworkElement.RequestBringIntoViewEvent, new RoutedEventHandler(OnRequestBringIntoView), false);

    private void OnRequestBringIntoView(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement child && child != this)
        {
            e.Handled = true;
            double offset = TabScrollDirection switch {
                TabScrollDirection.Backward => -TabControlHeaderScrollBehavior.ScrollOffset,
                TabScrollDirection.Forward => TabControlHeaderScrollBehavior.ScrollOffset,
                _ => 0
            };
            var point = child.TranslatePoint(new Point(), this);
            var newTargetRect = new Rect(new Point(point.X + offset, point.Y), child.RenderSize);
            BringIntoView(newTargetRect);
        }
    }
}
