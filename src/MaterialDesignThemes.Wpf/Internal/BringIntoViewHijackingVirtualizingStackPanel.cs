
namespace MaterialDesignThemes.Wpf.Internal;

public class BringIntoViewHijackingVirtualizingStackPanel : VirtualizingStackPanel
{
    public BringIntoViewHijackingVirtualizingStackPanel()
        => AddHandler(FrameworkElement.RequestBringIntoViewEvent, new RoutedEventHandler(OnRequestBringIntoView), false);

    private void OnRequestBringIntoView(object sender, RoutedEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement child && child != this)
        {
            e.Handled = true;
            var point = child.TranslatePoint(new Point(), this);
            var newTargetRect = new Rect(new Point(point.X + 52, point.Y), child.RenderSize);
            BringIntoView(newTargetRect);
        }
    }
}
