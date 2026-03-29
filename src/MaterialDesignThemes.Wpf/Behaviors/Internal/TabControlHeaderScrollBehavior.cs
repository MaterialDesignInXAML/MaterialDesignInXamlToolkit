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
            oldTabControl.ItemContainerGenerator.ItemsChanged -= behavior.OnTabsChanged;
        }
        if (e.NewValue is TabControl newTabControl)
        {
            newTabControl.SelectionChanged += behavior.OnTabChanged;
            newTabControl.SizeChanged += behavior.OnTabControlSizeChanged;
            newTabControl.PreviewKeyDown += behavior.OnTabControlPreviewKeyDown;
            newTabControl.ItemContainerGenerator.ItemsChanged += behavior.OnTabsChanged;
        }
    }

    public double AdditionalHeaderPanelContentWidth
    {
        get => (double)GetValue(AdditionalHeaderPanelContentWidthProperty);
        set => SetValue(AdditionalHeaderPanelContentWidthProperty, value);
    }

    public static readonly DependencyProperty AdditionalHeaderPanelContentWidthProperty =
        DependencyProperty.Register(nameof(AdditionalHeaderPanelContentWidth), typeof(double),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(0d, AdditionalHeaderPanelContentWidthChanged));

    private static void AdditionalHeaderPanelContentWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (TabControlHeaderScrollBehavior)d;
        behavior.AddPaddingToScrollableContentIfWiderThanViewPort();
    }

    public double NavigationPanelLeftWidth
    {
        get => (double)GetValue(NavigationPanelLeftWidthProperty);
        set => SetValue(NavigationPanelLeftWidthProperty, value);
    }

    public static readonly DependencyProperty NavigationPanelLeftWidthProperty =
        DependencyProperty.Register(nameof(NavigationPanelLeftWidth), typeof(double),
            typeof(TabControlHeaderScrollBehavior), new PropertyMetadata(0d, NavigationPanelLeftWidthChanged));

    private static void NavigationPanelLeftWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var behavior = (TabControlHeaderScrollBehavior)d;
        behavior.AddPaddingToScrollableContentIfWiderThanViewPort();
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

    public ICommand NextTabCommand { get; }
    public ICommand PreviousTabCommand { get; }

    private double? _desiredScrollStart;
    private bool _isAnimatingScroll;

    public TabControlHeaderScrollBehavior()
    {
        NextTabCommand = new SimpleICommandImplementation(_ =>
        {
            if (TabControl is { } tabControl)
            {
                NavigationPanelBehavior behavior = TabAssist.GetNavigationPanelBehavior(tabControl);
                if (behavior == NavigationPanelBehavior.Scroll)
                {
                    _desiredScrollStart = AssociatedObject.ContentHorizontalOffset;
                    double newOffset = Math.Min(AssociatedObject.ContentHorizontalOffset + AssociatedObject.ActualWidth, AssociatedObject.ExtentWidth - AssociatedObject.ActualWidth - AssociatedObject.Margin.Left - AssociatedObject.Margin.Right);
                    AssociatedObject.ScrollToHorizontalOffset(newOffset);
                }
                else if (behavior == NavigationPanelBehavior.Select && TryGetNextTabIndex(tabControl, out int nextIndex))
                {
                    tabControl.SelectedIndex = nextIndex;
                }
            }
        }, CanNextTabCommandExecute);
        PreviousTabCommand = new SimpleICommandImplementation(_ =>
        {
            if (TabControl is { } tabControl)
            {
                NavigationPanelBehavior behavior = TabAssist.GetNavigationPanelBehavior(tabControl);
                if (behavior == NavigationPanelBehavior.Scroll)
                {
                    _desiredScrollStart = AssociatedObject.ContentHorizontalOffset;
                    AssociatedObject.ScrollToHorizontalOffset(AssociatedObject.ContentHorizontalOffset - AssociatedObject.ActualWidth);
                }
                else if (behavior == NavigationPanelBehavior.Select && TryGetPreviousTabIndex(tabControl, out int previousIndex))
                {
                    tabControl.SelectedIndex = previousIndex;
                }
            }
        }, CanPreviousTabCommandExecute);

        bool CanNextTabCommandExecute(object? _)
        {
            if (TabControl is not { } tabControl)
                return false;

            NavigationPanelBehavior behavior = TabAssist.GetNavigationPanelBehavior(tabControl);
            return behavior switch
            {
                NavigationPanelBehavior.Scroll => AssociatedObject.ContentHorizontalOffset < AssociatedObject.ExtentWidth - AssociatedObject.ActualWidth - AssociatedObject.Margin.Left - AssociatedObject.Margin.Right,
                NavigationPanelBehavior.Select => TryGetNextTabIndex(tabControl, out int _),
                _ => false
            };
        }

        bool CanPreviousTabCommandExecute(object? _)
        {
            if (TabControl is not { } tabControl)
                return false;

            NavigationPanelBehavior behavior = TabAssist.GetNavigationPanelBehavior(tabControl);
            return behavior switch
            {
                NavigationPanelBehavior.Scroll => AssociatedObject.ContentHorizontalOffset > 0,
                NavigationPanelBehavior.Select => TryGetPreviousTabIndex(tabControl, out int _),
                _ => false
            };
        }

        static bool TryGetNextTabIndex(TabControl tabControl, out int nextTabIndex)
        {
            nextTabIndex = -1;
            var nextTabs = GetEnabledTabItemIndices(tabControl, index => index > tabControl.SelectedIndex);
            if (nextTabs.Count > 0)
            {
                nextTabIndex = nextTabs.First();
                return true;
            }
            return false;
        }

        static bool TryGetPreviousTabIndex(TabControl tabControl, out int previousTabIndex)
        {
            previousTabIndex = -1;
            var previousTabs = GetEnabledTabItemIndices(tabControl, index => index < tabControl.SelectedIndex);
            if (previousTabs.Count > 0)
            {
                previousTabIndex = previousTabs.Last();
                return true;
            }
            return false;
        }

        static List<int> GetEnabledTabItemIndices(TabControl tabControl, Predicate<int> predicate) => [.. tabControl
            .Items
            .Cast<object>()
            .Select(item => (TabItem)tabControl.ItemContainerGenerator.ContainerFromItem(item))
            .Where(tab => tab != null && tab.IsEnabled)
            .Select(tab => tabControl.ItemContainerGenerator.IndexFromContainer(tab))
            .Where(tabIndex => predicate(tabIndex))];
    }

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

    private void OnTabsChanged(object sender, ItemsChangedEventArgs e)
        => AssociatedObject.Dispatcher.BeginInvoke(() => AddPaddingToScrollableContentIfWiderThanViewPort(), System.Windows.Threading.DispatcherPriority.Loaded);   // Defer execution until collection change is rendered
    private void OnTabControlSizeChanged(object sender, SizeChangedEventArgs _) => AddPaddingToScrollableContentIfWiderThanViewPort();
    private void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs _) => AddPaddingToScrollableContentIfWiderThanViewPort();

    private void AddPaddingToScrollableContentIfWiderThanViewPort()
    {
        if (TabAssist.GetUseHeaderPadding(TabControl) == false)
            return;
        if (ScrollableContent is null)
            return;

        if (ScrollableContent.ActualWidth > TabControl.ActualWidth - AdditionalHeaderPanelContentWidth)
        {
            double offset = TabAssist.GetHeaderPadding(TabControl);
            ScrollableContent.Margin = new(offset, 0, offset + AdditionalHeaderPanelContentWidth, 0);
            TabAssist.SetIsOverflowing(TabControl, true);
        }
        else
        {
            ScrollableContent.Margin = new();
            AssociatedObject.SetCurrentValue(TabControlHeaderScrollBehavior.CustomHorizontalOffsetProperty, 0d);
            TabAssist.SetIsOverflowing(TabControl, false);
        }
        AssociatedObject.Margin = new(NavigationPanelLeftWidth, 0, AdditionalHeaderPanelContentWidth, 0);
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
        ((SimpleICommandImplementation)PreviousTabCommand!).Refresh();
        ((SimpleICommandImplementation)NextTabCommand!).Refresh();

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
            ((SimpleICommandImplementation)PreviousTabCommand!).Refresh();
            ((SimpleICommandImplementation)NextTabCommand!).Refresh();

            _desiredScrollStart = null;
            _isAnimatingScroll = false;

            // HACK: Set the hit test visibility back to its original value
            TabControl.SetCurrentValue(FrameworkElement.IsHitTestVisibleProperty, originalIsHitTestVisibleValue);
        };
        AssociatedObject.BeginAnimation(TabControlHeaderScrollBehavior.CustomHorizontalOffsetProperty, scrollAnimation);
    }

    private class SimpleICommandImplementation : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;

        public SimpleICommandImplementation(Action<object?> execute)
            : this(execute, null)
        { }

        public SimpleICommandImplementation(Action<object?> execute, Func<object?, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Refresh() => CommandManager.InvalidateRequerySuggested();
    }
}

public enum TabScrollDirection
{
    Unknown,
    Backward,
    Forward
}
