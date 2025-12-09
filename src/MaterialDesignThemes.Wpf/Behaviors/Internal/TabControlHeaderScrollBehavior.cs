using System.Diagnostics;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignThemes.Wpf.Behaviors.Internal;

public class TabControlHeaderScrollBehavior : Behavior<ScrollViewer>
{
    public static readonly DependencyProperty TabScrollDirectionProperty =
        DependencyProperty.RegisterAttached("TabScrollDirection", typeof(TabScrollDirection), typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(TabScrollDirection.Unknown));
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

    private void OnTabChanged(object sender, SelectionChangedEventArgs e)
    {
        TabControl tabControl = (TabControl)sender;

        if (e.AddedItems.Count > 0)
        {
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
        Debug.WriteLine($"HorizontalOffset: {e.HorizontalOffset}, ViewportWidth: {e.ViewportWidth}, ExtentWidth: {e.ExtentWidth}");
    }
}

public enum TabScrollDirection
{
    Unknown,
    Backward,
    Forward
}
