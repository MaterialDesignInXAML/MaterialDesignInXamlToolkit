using System.Diagnostics;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors.Internal;

public class TabControlHeaderScrollBehavior : Behavior<ScrollViewer>
{
    public TabControl TabControl
    {
        get => (TabControl)GetValue(TabControlProperty);
        set => SetValue(TabControlProperty, value);
    }

    public static readonly DependencyProperty TabControlProperty =
        DependencyProperty.Register(nameof(TabControl), typeof(TabControl),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(null, OnTabControlChanged));

    private static void OnTabControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (TabControlHeaderScrollBehavior)d;
        if (e.OldValue is TabControl oldTabControl)
        {
            oldTabControl.SelectionChanged -= behavior.OnTabChanged;
        }
        if (e.NewValue is TabControl newTabControl)
        {
            newTabControl.SelectionChanged += behavior.OnTabChanged;
        }
    }

    private void OnTabChanged(object sender, SelectionChangedEventArgs e)
    {
        TabControl tabControl = (TabControl)sender;

        if (e.AddedItems.Count > 0)
        {
            // First we detect if we are moving forward or backward through the tabs
            // If we are moving forward we leave the padding on the right
            // If we are moving backward we leave the padding on the left
            // If we didn't have a prior selection we assume we are moving forward
            bool movingForward = IsMovingForward();

            var newTab = e.AddedItems[e.AddedItems.Count - 1];

            //TODO: Can we do better than queue and hope?
            // https://source.dot.net/#PresentationFramework/System/Windows/Controls/ScrollViewer.cs,2448
            // Try to grab the event, and provide my own retangle offseting the orignal one.
            AssociatedObject.Dispatcher.BeginInvoke(() =>
            {
                if (tabControl.SelectedItem != newTab)
                    return;

                //TODO: Do we need to scroll?

                var toOffset = AssociatedObject.ContentHorizontalOffset + (movingForward ? 52 : -52);
                Debug.WriteLine($"Scrolling to {toOffset} from {AssociatedObject.ContentHorizontalOffset}");
                AssociatedObject.ScrollToHorizontalOffset(toOffset);
            }, System.Windows.Threading.DispatcherPriority.Background);
        }

        bool IsMovingForward()
        {
            if (e.RemovedItems.Count == 0) return true;

            var previousIndex = GetItemIndex(e.RemovedItems[0]);
            var nextIndex = GetItemIndex(e.AddedItems[^1]);

            return nextIndex > previousIndex;
        }

        int GetItemIndex(object? item)
        {
            return tabControl.Items.IndexOf(item);
        }
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
    }

    private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        Debug.WriteLine($"HorizontalOffset: {e.HorizontalOffset}, ViewportWidth: {e.ViewportWidth}, ExtentWidth: {e.ExtentWidth}");
    }
}
