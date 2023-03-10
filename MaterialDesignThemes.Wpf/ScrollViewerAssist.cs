using System.Windows.Interop;

namespace MaterialDesignThemes.Wpf;

public static class ScrollViewerAssist
{
    internal static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached(
        "SyncHorizontalOffset", typeof(double), typeof(ScrollViewerAssist), new PropertyMetadata(default(double), OnSyncHorizontalOffsetChanged));

    private static void OnSyncHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var scrollViewer = d as ScrollViewer;
        scrollViewer?.ScrollToHorizontalOffset((double)e.NewValue);
    }

    internal static void SetSyncHorizontalOffset(DependencyObject element, double value)
    {
        element.SetValue(HorizontalOffsetProperty, value);
    }

    internal static double GetSyncHorizontalOffset(DependencyObject element)
    {
        return (double)element.GetValue(HorizontalOffsetProperty);
    }

    public static readonly DependencyProperty IsAutoHideEnabledProperty = DependencyProperty.RegisterAttached(
        "IsAutoHideEnabled", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(default(bool)));

    public static void SetIsAutoHideEnabled(DependencyObject element, bool value)
    {
        element.SetValue(IsAutoHideEnabledProperty, value);
    }

    public static bool GetIsAutoHideEnabled(DependencyObject element)
    {
        return (bool)element.GetValue(IsAutoHideEnabledProperty);
    }

    public static readonly DependencyProperty CornerRectangleVisibilityProperty = DependencyProperty.RegisterAttached(
        "CornerRectangleVisibility", typeof(Visibility), typeof(ScrollViewerAssist), new PropertyMetadata(default(Visibility)));

    public static void SetCornerRectangleVisibility(DependencyObject element, Visibility value)
    {
        element.SetValue(CornerRectangleVisibilityProperty, value);
    }

    public static Visibility GetCornerRectangleVisibility(DependencyObject element)
    {
        return (Visibility)element.GetValue(CornerRectangleVisibilityProperty);
    }

    public static readonly DependencyProperty ShowSeparatorsProperty = DependencyProperty.RegisterAttached(
        "ShowSeparators", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(default(bool)));

    public static void SetShowSeparators(DependencyObject element, bool value)
    {
        element.SetValue(ShowSeparatorsProperty, value);
    }

    public static bool GetShowSeparators(DependencyObject element)
    {
        return (bool)element.GetValue(ShowSeparatorsProperty);
    }

    public static readonly DependencyProperty IgnorePaddingProperty = DependencyProperty.RegisterAttached(
        "IgnorePadding", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(true));

    public static void SetIgnorePadding(DependencyObject element, bool value) => element.SetValue(IgnorePaddingProperty, value);
    public static bool GetIgnorePadding(DependencyObject element) => (bool)element.GetValue(IgnorePaddingProperty);

    private static readonly DependencyProperty HorizontalScrollHookProperty = DependencyProperty.RegisterAttached(
        "HorizontalScrollHook", typeof(HwndSourceHook), typeof(ScrollViewerAssist), new PropertyMetadata(null));

    private static readonly DependencyProperty HorizontalScrollSourceProperty = DependencyProperty.RegisterAttached(
        "HorizontalScrollSource", typeof(HwndSource), typeof(ScrollViewerAssist), new PropertyMetadata(null));

    public static readonly DependencyProperty SupportHorizontalScrollProperty = DependencyProperty.RegisterAttached(
        "SupportHorizontalScroll", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(false, OnSupportHorizontalScrollChanged));

    private static void OnSupportHorizontalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //Based on: https://blog.walterlv.com/post/handle-horizontal-scrolling-of-touchpad-en.html
        if (d is ScrollViewer scrollViewer)
        {
            if (scrollViewer.IsLoaded)
            {
                DoOnLoaded(scrollViewer);
            }
            else
            {
                WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(scrollViewer, nameof(ScrollViewer.Loaded), OnLoaded);
                WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(scrollViewer, nameof(ScrollViewer.Unloaded), OnUnloaded);
            }
        }

        static void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                DoOnLoaded(sv);
            }
        }

        static void DoOnLoaded(ScrollViewer sv)
        {
            if (GetSupportHorizontalScroll(sv))
            {
                RegisterHook(sv);
            }
            else
            {
                RemoveHook(sv);
            }
        }

        static void OnUnloaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                RemoveHook(sv);
            }
        }

        static void RemoveHook(ScrollViewer scrollViewer)
        {
            if (scrollViewer.GetValue(HorizontalScrollHookProperty) is HwndSourceHook hook &&
                scrollViewer.GetValue(HorizontalScrollSourceProperty) is HwndSource source)
            {
                source.RemoveHook(hook);
                scrollViewer.SetValue(HorizontalScrollHookProperty, null);
            }
        }

        static void RegisterHook(ScrollViewer scrollViewer)
        {
            RemoveHook(scrollViewer);
            if (PresentationSource.FromVisual(scrollViewer) is HwndSource source)
            {
                HwndSourceHook hook = Hook;
                scrollViewer.SetValue(HorizontalScrollSourceProperty, source);
                scrollViewer.SetValue(HorizontalScrollHookProperty, hook);
                source.AddHook(hook);
            }

            IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                const int WM_MOUSEHWHEEL = 0x020E;
                switch (msg)
                {
                    case WM_MOUSEHWHEEL:
                        int tilt = (short)((wParam.ToInt64() >> 16) & 0xFFFF);
                        OnMouseTilt(tilt);
                        return (IntPtr)1;
                }
                return IntPtr.Zero;
            }

            void OnMouseTilt(int tilt)
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + tilt);
            }
        }
    }

    public static readonly DependencyProperty BubbleVerticalScrollProperty = DependencyProperty.RegisterAttached(
        "BubbleVerticalScroll", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(false, OnBubbleVerticalScrollChanged));

    private static void OnBubbleVerticalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer scrollViewer)
        {
            if (scrollViewer.IsLoaded)
            {
                DoOnLoaded(scrollViewer);
            }
            else
            {
                WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(scrollViewer, nameof(ScrollViewer.Loaded), OnLoaded);
                WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(scrollViewer, nameof(ScrollViewer.Unloaded), OnUnloaded);
            }
        }

        static void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                DoOnLoaded(sv);
            }
        }

        static void DoOnLoaded(ScrollViewer sv)
        {
            if (GetBubbleVerticalScroll(sv))
            {
                RegisterHook(sv);
            }
            else
            {
                RemoveHook(sv);
            }
        }

        static void OnUnloaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                RemoveHook(sv);
            }
        }

        static void RemoveHook(ScrollViewer scrollViewer)
        {
            scrollViewer.RemoveHandler(UIElement.MouseWheelEvent, (RoutedEventHandler)ScrollViewerOnMouseWheel);
        }

        static void RegisterHook(ScrollViewer scrollViewer)
        {
            RemoveHook(scrollViewer);
            scrollViewer.AddHandler(UIElement.MouseWheelEvent, (RoutedEventHandler)ScrollViewerOnMouseWheel, true);
        }

        // This relay is only needed because the UIElement.AddHandler() has strict requirements for the signature of the passed Delegate
        static void ScrollViewerOnMouseWheel(object sender, RoutedEventArgs e) => HandleMouseWheel(sender, (MouseWheelEventArgs)e);

        static void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;

            if (scrollViewer.GetVisualAncestry().Skip(1).FirstOrDefault() is not UIElement parentUiElement)
                return;

            // Re-raise the mouse wheel event on the visual parent to bubble it upwards
            e.Handled = true;
            var mouseWheelEventArgs = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = UIElement.MouseWheelEvent,
                Source = sender
            };
            parentUiElement.RaiseEvent(mouseWheelEventArgs);
        }
    }

    public static void SetSupportHorizontalScroll(DependencyObject element, bool value)
        => element.SetValue(SupportHorizontalScrollProperty, value);

    public static bool GetSupportHorizontalScroll(DependencyObject element)
        => (bool)element.GetValue(SupportHorizontalScrollProperty);

    public static void SetBubbleVerticalScroll(DependencyObject element, bool value)
        => element.SetValue(BubbleVerticalScrollProperty, value);

    public static bool GetBubbleVerticalScroll(DependencyObject element)
        => (bool)element.GetValue(BubbleVerticalScrollProperty);
}
