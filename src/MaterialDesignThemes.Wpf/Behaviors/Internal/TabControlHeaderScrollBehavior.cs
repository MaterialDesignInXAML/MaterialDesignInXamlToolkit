using System.Diagnostics;
using System.Windows.Media.Animation;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors.Internal;

public class TabControlHeaderScrollBehavior : Behavior<ScrollViewer>
{
    public static readonly DependencyProperty CustomHorizontalOffsetProperty =
    DependencyProperty.RegisterAttached("CustomHorizontalOffset", typeof(double),
        typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(0d, CustomHorizontalOffsetChanged));
    public static double GetCustomHorizontalOffset(DependencyObject obj) => (double)obj.GetValue(CustomHorizontalOffsetProperty);
    public static void SetCustomHorizontalOffset(DependencyObject obj, double value) => obj.SetValue(CustomHorizontalOffsetProperty, value);
    private static void CustomHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var scrollViewer = (ScrollViewer)d;
        scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
    }

    public static readonly DependencyProperty ScrollDirectionProperty =
        DependencyProperty.RegisterAttached("ScrollDirection", typeof(TabScrollDirection),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(TabScrollDirection.Unknown));
    public static TabScrollDirection GetScrollDirection(DependencyObject obj) => (TabScrollDirection)obj.GetValue(ScrollDirectionProperty);
    public static void SetScrollDirection(DependencyObject obj, TabScrollDirection value) => obj.SetValue(ScrollDirectionProperty, value);

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
            oldTabControl.SizeChanged -= behavior.OnTabControlSizeChanged;
            oldTabControl.PreviewKeyDown -= behavior.OnTabControlPreviewKeyDown;
        }
        if (e.NewValue is TabControl newTabControl)
        {
            newTabControl.SelectionChanged += behavior.OnTabChanged;
            newTabControl.SizeChanged += behavior.OnTabControlSizeChanged;
            newTabControl.PreviewKeyDown += behavior.OnTabControlPreviewKeyDown;
        }
    }

    public FrameworkElement ScrollableContent
    {
        get => (FrameworkElement)GetValue(ScrollableContentProperty);
        set => SetValue(ScrollableContentProperty, value);
    }

    public static readonly DependencyProperty ScrollableContentProperty =
        DependencyProperty.Register(nameof(ScrollableContent), typeof(FrameworkElement),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(null, OnScrollableContentChanged));

    private static void OnScrollableContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (TabControlHeaderScrollBehavior)d;
        behavior.AddPaddingToScrollableContentIfWiderThanViewPort();
    }

    private double? _desiredScrollStart;
    private bool _isAnimatingScroll;

    private void OnTabChanged(object sender, SelectionChangedEventArgs e)
    {
        var tabControl = (TabControl)sender;

        if (e.AddedItems.Count > 0)
        {
            _desiredScrollStart = AssociatedObject.ContentHorizontalOffset;
            SetScrollDirection(tabControl, (IsMovingForward() ? TabScrollDirection.Forward : TabScrollDirection.Backward));

            // In case the TabItem has focusable content, the FrameworkElement.RequestBringIntoView won't fire. The lines below ensures that it fires.
            var tab = tabControl.ItemContainerGenerator.ContainerFromItem(e.AddedItems[0]) as TabItem;
            tab?.BringIntoView();
        }

        bool IsMovingForward()
        {
            if (e.RemovedItems.Count == 0) return true;
            int previousIndex = GetItemIndex(e.RemovedItems[0]);
            int nextIndex = GetItemIndex(e.AddedItems[^1]);
            return nextIndex > previousIndex;
        }

        int GetItemIndex(object? item) => tabControl.Items.IndexOf(item);
    }

    private void OnTabControlSizeChanged(object sender, SizeChangedEventArgs _) => AddPaddingToScrollableContentIfWiderThanViewPort();
    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs _) => AddPaddingToScrollableContentIfWiderThanViewPort();

    private void AddPaddingToScrollableContentIfWiderThanViewPort()
    {
        if (TabAssist.GetUseHeaderPadding(TabControl) == false)
            return;
        if (ScrollableContent is null)
            return;

        if (ScrollableContent.ActualWidth > TabControl.ActualWidth)
        {
            double offset = TabAssist.GetHeaderPadding(TabControl);
            ScrollableContent.Margin = new(offset, 0, offset, 0);
        }
        else
        {
            ScrollableContent.Margin = new();
        }
    }

    private void OnTabControlPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (!_isAnimatingScroll)
            return;

        if (e.Key is Key.Left or Key.Right or Key.Home or Key.End or Key.PageUp or Key.PageDown or Key.Tab)
            e.Handled = true;
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
        AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        Dispatcher.BeginInvoke(() => AddPaddingToScrollableContentIfWiderThanViewPort());
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject is { } ao)
        {
            ao.ScrollChanged -= AssociatedObject_ScrollChanged;
            ao.SizeChanged -= AssociatedObject_SizeChanged;
        }
    }

    private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (TabAssist.GetUseHeaderPadding(TabControl) == false)
            return;
        TimeSpan duration = TabAssist.GetScrollDuration(TabControl);
        if (duration == TimeSpan.Zero)
            return;
        if ( _isAnimatingScroll || _desiredScrollStart is not { } desiredOffsetStart)
            return;

        double originalValue = desiredOffsetStart;
        double newValue = e.HorizontalOffset;
        _isAnimatingScroll = true;

        // HACK: Temporarily disable user interaction while the animated scroll is ongoing. This prevents the double-click of a tab stopping the animation prematurely.
        bool originalIsHitTestVisibleValue = TabControl.IsHitTestVisible;
        TabControl.SetCurrentValue(FrameworkElement.IsHitTestVisibleProperty, false);

        AssociatedObject.ScrollToHorizontalOffset(originalValue);
        DoubleAnimation scrollAnimation = new(originalValue, newValue, new Duration(duration));
        scrollAnimation.Completed += (_, _) =>
        {
            _desiredScrollStart = null;
            _isAnimatingScroll = false;

            // HACK: Set the hit test visibility back to its original value
            TabControl.SetCurrentValue(FrameworkElement.IsHitTestVisibleProperty, originalIsHitTestVisibleValue);
        };
        AssociatedObject.BeginAnimation(TabControlHeaderScrollBehavior.CustomHorizontalOffsetProperty, scrollAnimation);
    }
}

public enum TabScrollDirection
{
    Unknown,
    Backward,
    Forward
}
