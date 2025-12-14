
using MaterialDesignThemes.Wpf.Behaviors.Internal;

namespace MaterialDesignThemes.Wpf.Internal;

public class PaddedBringIntoViewStackPanel : StackPanel
{
    public TabScrollDirection ScrollDirection
    {
        get => (TabScrollDirection)GetValue(ScrollDirectionProperty);
        set => SetValue(ScrollDirectionProperty, value);
    }

    public static readonly DependencyProperty ScrollDirectionProperty =
        DependencyProperty.Register(nameof(ScrollDirection), typeof(TabScrollDirection),
            typeof(PaddedBringIntoViewStackPanel), new PropertyMetadata(TabScrollDirection.Unknown));

    public double HeaderPadding
    {
        get => (double)GetValue(HeaderPaddingProperty);
        set => SetValue(HeaderPaddingProperty, value);
    }

    public static readonly DependencyProperty HeaderPaddingProperty =
        DependencyProperty.Register(nameof(HeaderPadding),
            typeof(double), typeof(PaddedBringIntoViewStackPanel), new PropertyMetadata(0d));

    public bool UseHeaderPadding
    {
        get => (bool)GetValue(UseHeaderPaddingProperty);
        set => SetValue(UseHeaderPaddingProperty, value);
    }

    public static readonly DependencyProperty UseHeaderPaddingProperty =
        DependencyProperty.Register(nameof(UseHeaderPadding), typeof(bool), typeof(PaddedBringIntoViewStackPanel), new PropertyMetadata(false));

    public PaddedBringIntoViewStackPanel()
        => AddHandler(FrameworkElement.RequestBringIntoViewEvent, new RoutedEventHandler(OnRequestBringIntoView), false);

    private void OnRequestBringIntoView(object sender, RoutedEventArgs e)
    {
        if (!UseHeaderPadding)
            return;

        if (e.OriginalSource is FrameworkElement child && child != this)
        {
            e.Handled = true;

            // TODO: Consider making the "ScrollDirection" a destructive read (i.e. reset the value once it is read) to avoid leaving a Backward/Forward value that may be misinterpreted at a later stage.
            double offset = ScrollDirection switch {
                TabScrollDirection.Backward => -HeaderPadding,
                TabScrollDirection.Forward => HeaderPadding,
                _ => 0
            };
            var point = child.TranslatePoint(new Point(), this);
            var newTargetRect = new Rect(new Point(point.X + offset, point.Y), child.RenderSize);
            BringIntoView(newTargetRect);
        }
    }
}
