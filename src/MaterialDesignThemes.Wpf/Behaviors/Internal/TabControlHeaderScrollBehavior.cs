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

    public static readonly DependencyProperty TabScrollDirectionProperty =
        DependencyProperty.RegisterAttached("TabScrollDirection", typeof(TabScrollDirection),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(TabScrollDirection.Unknown));
    public static TabScrollDirection GetTabScrollDirection(DependencyObject obj) => (TabScrollDirection)obj.GetValue(TabScrollDirectionProperty);
    public static void SetTabScrollDirection(DependencyObject obj, TabScrollDirection value) => obj.SetValue(TabScrollDirectionProperty, value);

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

    private double? _desiredScrollStart;
    private bool _isAnimatingScroll;

    private void OnTabChanged(object sender, SelectionChangedEventArgs e)
    {
        TabControl tabControl = (TabControl)sender;

        if (e.AddedItems.Count > 0)
        {
            _desiredScrollStart = AssociatedObject.ContentHorizontalOffset;
            SetTabScrollDirection(tabControl, (IsMovingForward() ? TabScrollDirection.Forward : TabScrollDirection.Backward));
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

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject is { } ao)
        {
            ao.ScrollChanged -= AssociatedObject_ScrollChanged;
        }
    }

    private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        Debug.WriteLine($"ContentHorizontalOffset: {AssociatedObject.ContentHorizontalOffset}, HorizontalOffset: {e.HorizontalOffset}, HorizontalChange: {e.HorizontalChange}, ViewportWidth: {e.ViewportWidth}, ExtentWidth: {e.ExtentWidth}");
        if (_isAnimatingScroll || _desiredScrollStart is not { } desiredOffsetStart) return;

        double originalValue = desiredOffsetStart;
        double newValue = e.HorizontalOffset;

        _isAnimatingScroll = true;
        AssociatedObject.ScrollToHorizontalOffset(originalValue);
        Debug.WriteLine($"Initiating animated scroll from {originalValue} to {newValue}. Change is: {e.HorizontalChange}");
        DoubleAnimation scrollAnimation = new(originalValue, newValue, new Duration(TimeSpan.FromMilliseconds(250)));
        scrollAnimation.Completed += (_, _) =>
        {
            Debug.WriteLine("Animation completed");
            _desiredScrollStart = null;
            _isAnimatingScroll = false;
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
