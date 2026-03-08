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
        => element.SetValue(HorizontalOffsetProperty, value);

    internal static double GetSyncHorizontalOffset(DependencyObject element)
        => (double)element.GetValue(HorizontalOffsetProperty);

    public static readonly DependencyProperty IsAutoHideEnabledProperty = DependencyProperty.RegisterAttached(
        "IsAutoHideEnabled", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(default(bool)));

    public static void SetIsAutoHideEnabled(DependencyObject element, bool value)
        => element.SetValue(IsAutoHideEnabledProperty, value);

    public static bool GetIsAutoHideEnabled(DependencyObject element)
        => (bool)element.GetValue(IsAutoHideEnabledProperty);

    public static readonly DependencyProperty CornerRectangleVisibilityProperty = DependencyProperty.RegisterAttached(
        "CornerRectangleVisibility", typeof(Visibility), typeof(ScrollViewerAssist), new PropertyMetadata(default(Visibility)));

    public static void SetCornerRectangleVisibility(DependencyObject element, Visibility value)
        => element.SetValue(CornerRectangleVisibilityProperty, value);

    public static Visibility GetCornerRectangleVisibility(DependencyObject element)
        => (Visibility)element.GetValue(CornerRectangleVisibilityProperty);

    public static readonly DependencyProperty ShowSeparatorsProperty = DependencyProperty.RegisterAttached(
        "ShowSeparators", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(default(bool)));

    public static void SetShowSeparators(DependencyObject element, bool value)
        => element.SetValue(ShowSeparatorsProperty, value);

    public static bool GetShowSeparators(DependencyObject element)
        => (bool)element.GetValue(ShowSeparatorsProperty);

    public static readonly DependencyProperty PaddingModeProperty = DependencyProperty.RegisterAttached(
        "PaddingMode", typeof(PaddingMode), typeof(ScrollViewerAssist), new PropertyMetadata(PaddingMode.Content));

    public static void SetPaddingMode(DependencyObject element, PaddingMode value)
        => element.SetValue(PaddingModeProperty, value);

    public static PaddingMode GetPaddingMode(DependencyObject element)
        => (PaddingMode)element.GetValue(PaddingModeProperty);

    public static readonly DependencyProperty IgnorePaddingProperty = DependencyProperty.RegisterAttached(
    "IgnorePadding", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(true));

    public static void SetIgnorePadding(DependencyObject element, bool value) => element.SetValue(IgnorePaddingProperty, value);
    public static bool GetIgnorePadding(DependencyObject element) => (bool)element.GetValue(IgnorePaddingProperty);

    private static readonly DependencyProperty HorizontalScrollHookProperty = DependencyProperty.RegisterAttached(
        "HorizontalScrollHook", typeof(HwndSourceHook), typeof(ScrollViewerAssist), new PropertyMetadata(null));

    private static readonly DependencyProperty HorizontalScrollSourceProperty = DependencyProperty.RegisterAttached(
        "HorizontalScrollSource", typeof(HwndSource), typeof(ScrollViewerAssist), new PropertyMetadata(null));

    private static readonly DependencyProperty BubbleVerticalScrollHookProperty = DependencyProperty.RegisterAttached(
        "BubbleVerticalScrollHook", typeof(HwndSourceHook), typeof(ScrollViewerAssist), new PropertyMetadata(null));

    public static readonly DependencyProperty SupportHorizontalScrollProperty = DependencyProperty.RegisterAttached(
        "SupportHorizontalScroll", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(false, OnSupportHorizontalScrollChanged));

    private static void OnSupportHorizontalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        //Based on: https://blog.walterlv.com/post/handle-horizontal-scrolling-of-touchpad-en.html
        if (d is ScrollViewer sv)
        {
            if (sv.IsLoaded)
            {
                DoOnLoaded(sv);
            }
            else
            {
                RegisterForLoadedEvent(sv);
                RegisterForUnloadedEvent(sv);
            }
        }

        static void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                DoOnLoaded(sv);
            }
        }

        static void UnregisterForLoadedEvent(ScrollViewer sv)
        {
            WeakEventManager<ScrollViewer, RoutedEventArgs>.RemoveHandler(sv, nameof(ScrollViewer.Loaded), OnLoaded);
        }

        static void RegisterForLoadedEvent(ScrollViewer sv)
        {
            // Avoid double registrations
            UnregisterForLoadedEvent(sv);
            WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(sv, nameof(ScrollViewer.Loaded), OnLoaded);
        }

        static void UnregisterForUnloadedEvent(ScrollViewer sv)
        {
            WeakEventManager<ScrollViewer, RoutedEventArgs>.RemoveHandler(sv, nameof(ScrollViewer.Unloaded), OnUnloaded);
        }

        static void RegisterForUnloadedEvent(ScrollViewer sv)
        {
            // Avoid double registrations
            UnregisterForUnloadedEvent(sv);
            WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(sv, nameof(ScrollViewer.Unloaded), OnUnloaded);
        }

        static void DoOnLoaded(ScrollViewer sv)
        {
            if (GetSupportHorizontalScroll(sv))
            {
                RegisterForUnloadedEvent(sv);
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

        static void RemoveHook(ScrollViewer sv)
        {
            var source = PresentationSource.FromVisual(sv) as HwndSource;
            if (source is not null && sv.GetValue(HorizontalScrollHookProperty) is HwndSourceHook hook)
            {
                source.RemoveHook(hook);
                sv.SetValue(HorizontalScrollHookProperty, null);
            }
        }

        static void RegisterHook(ScrollViewer sv)
        {
            RemoveHook(sv);
            if (PresentationSource.FromVisual(sv) is HwndSource source)
            {
                HwndSourceHook hook = Hook;
                sv.SetValue(HorizontalScrollSourceProperty, source);
                sv.SetValue(HorizontalScrollHookProperty, hook);
                source.AddHook(hook);
            }

            IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                const int WM_MOUSEHWHEEL = 0x020E;
                const int WM_DESTROY = 0x0002;
                const int WM_NCDESTROY = 0x0082;
                switch (msg)
                {
                    case WM_MOUSEHWHEEL when sv.IsMouseOver:
                        int tilt = (short)((wParam.ToInt64() >> 16) & 0xFFFF);
                        sv.ScrollToHorizontalOffset(sv.HorizontalOffset + tilt);
                        return (IntPtr)1;
                    case WM_DESTROY:
                    case WM_NCDESTROY:
                        UnregisterForLoadedEvent(sv);
                        UnregisterForUnloadedEvent(sv);
                        RemoveHook(sv);
                        break;
                }
                return IntPtr.Zero;
            }
        }
    }

    public static readonly DependencyProperty BubbleVerticalScrollProperty = DependencyProperty.RegisterAttached(
        "BubbleVerticalScroll", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(false, OnBubbleVerticalScrollChanged));
    
    private static void OnBubbleVerticalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ScrollViewer sv)
        {
            if (sv.IsLoaded)
            {
                DoOnLoaded(sv);
            }
            else
            {
                RegisterForLoadedEvent(sv);
                RegisterForUnloadedEvent(sv);
            }
        }

        static void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is ScrollViewer sv)
            {
                DoOnLoaded(sv);
            }
        }

        static void UnregisterForLoadedEvent(ScrollViewer sv)
        {
            WeakEventManager<ScrollViewer, RoutedEventArgs>.RemoveHandler(sv, nameof(ScrollViewer.Loaded), OnLoaded);
        }

        static void RegisterForLoadedEvent(ScrollViewer sv)
        {
            // Avoid double registrations
            UnregisterForLoadedEvent(sv);
            WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(sv, nameof(ScrollViewer.Loaded), OnLoaded);
        }

        static void UnregisterForUnloadedEvent(ScrollViewer sv)
        {
            WeakEventManager<ScrollViewer, RoutedEventArgs>.RemoveHandler(sv, nameof(ScrollViewer.Unloaded), OnUnloaded);
        }

        static void RegisterForUnloadedEvent(ScrollViewer sv)
        {
            // Avoid double registrations
            UnregisterForUnloadedEvent(sv);
            WeakEventManager<ScrollViewer, RoutedEventArgs>.AddHandler(sv, nameof(ScrollViewer.Unloaded), OnUnloaded);
        }

        static void UnregisterForMouseWheelEvent(ScrollViewer sv)
        {
            sv.RemoveHandler(UIElement.MouseWheelEvent, (RoutedEventHandler)ScrollViewerOnMouseWheel);
        }

        static void RegisterForMouseWheelEvent(ScrollViewer sv)
        {
            // Avoid double registrations
            UnregisterForMouseWheelEvent(sv);
            sv.AddHandler(UIElement.MouseWheelEvent, (RoutedEventHandler)ScrollViewerOnMouseWheel, true);
        }

        static void DoOnLoaded(ScrollViewer sv)
        {
            if (GetBubbleVerticalScroll(sv))
            {
                RegisterForUnloadedEvent(sv);
                RegisterForMouseWheelEvent(sv);
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
                UnregisterForUnloadedEvent(sv);
                UnregisterForMouseWheelEvent(sv);
            }
        }

        static void RemoveHook(ScrollViewer sv)
        {
            var source = PresentationSource.FromVisual(sv) as HwndSource;
            if (source is not null && sv.GetValue(BubbleVerticalScrollHookProperty) is HwndSourceHook hook)
            {
                source.RemoveHook(hook);
                sv.SetValue(BubbleVerticalScrollHookProperty, null);
            }
        }

        static void RegisterHook(ScrollViewer sv)
        {
            RemoveHook(sv);
            if (PresentationSource.FromVisual(sv) is HwndSource source)
            {
                HwndSourceHook hook = Hook;
                source.AddHook(hook);
                sv.SetValue(BubbleVerticalScrollHookProperty, hook);
            }

            IntPtr Hook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                const int WM_DESTROY = 0x0002;
                const int WM_NCDESTROY = 0x0082;
                switch (msg)
                {
                    case WM_DESTROY:
                    case WM_NCDESTROY:
                        UnregisterForMouseWheelEvent(sv);
                        UnregisterForLoadedEvent(sv);
                        UnregisterForUnloadedEvent(sv);
                        RemoveHook(sv);
                        break;
                }
                return IntPtr.Zero;
            }
        }

        // This relay is only needed because the UIElement.AddHandler() has strict requirements for the signature of the passed Delegate
        static void ScrollViewerOnMouseWheel(object? sender, RoutedEventArgs e) => HandleMouseWheel(sender, (MouseWheelEventArgs)e);

        static void HandleMouseWheel(object? sender, MouseWheelEventArgs e)
        {
            if (sender is not ScrollViewer sv || sv.GetVisualAncestry().Skip(1).FirstOrDefault() is not UIElement parentUiElement)
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
