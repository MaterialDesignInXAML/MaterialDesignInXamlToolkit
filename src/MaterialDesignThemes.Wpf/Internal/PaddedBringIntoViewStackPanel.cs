
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

    static PaddedBringIntoViewStackPanel()
        => EventManager.RegisterClassHandler(typeof(PaddedBringIntoViewStackPanel),
            FrameworkElement.RequestBringIntoViewEvent,
            new RequestBringIntoViewEventHandler(OnRequestBringIntoView));

    private static void OnRequestBringIntoView(object sender, RoutedEventArgs e)
    {
        var panel = (PaddedBringIntoViewStackPanel)sender;
        if (!panel.UseHeaderPadding)
            return;

        if (e.OriginalSource is FrameworkElement child && child != panel)
        {
            e.Handled = true;

            double offset = panel.ScrollDirection switch
            {
                TabScrollDirection.Backward => -panel.HeaderPadding,
                TabScrollDirection.Forward => panel.HeaderPadding,
                _ => 0
            };
            var point = child.TranslatePoint(new Point(), panel);
            var newTargetRect = new Rect(new Point(point.X + offset, point.Y), child.RenderSize);
            panel.BringIntoView(newTargetRect);
        }
    }
}
