using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace MaterialDesignThemes.Wpf
{
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

        public static readonly DependencyProperty SupportHorizontalScrollProperty = DependencyProperty.RegisterAttached(
            "SupportHorizontalScroll", typeof(bool), typeof(ScrollViewerAssist), new PropertyMetadata(false, OnSupportHorizontalScrollChanged));

        private static void OnSupportHorizontalScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Based on: https://blog.walterlv.com/post/handle-horizontal-scrolling-of-touchpad-en.html
            if (d is ScrollViewer scrollViewer)
            {
                if ((bool)e.NewValue)
                {
                    OnLoaded(scrollViewer, sv =>
                    {
                        if (GetSupportHorizontalScroll(sv))
                        {
                            RegisterHook(sv);
                        }
                    });
                }
                else
                {
                    OnLoaded(scrollViewer, sv =>
                    {
                        if (!GetSupportHorizontalScroll(sv))
                        {
                            RemoveHook(sv);
                        }
                    });
                }
            }

            static void OnLoaded(ScrollViewer scrollViewer, Action<ScrollViewer> doOnLoaded)
            {
                if(scrollViewer.IsLoaded)
                {
                    doOnLoaded(scrollViewer);
                }
                else
                {
                    RoutedEventHandler? onLoaded = null;
                    onLoaded = (_, _) =>
                    {
                        scrollViewer.Loaded -= onLoaded;
                        doOnLoaded(scrollViewer);
                    };
                    scrollViewer.Loaded += onLoaded;
                }
            }

            static void RemoveHook(ScrollViewer scrollViewer)
            {
                if (scrollViewer.GetValue(HorizontalScrollHookProperty) is HwndSourceHook hook &&
                    PresentationSource.FromVisual(scrollViewer) is HwndSource source)
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

        public static void SetSupportHorizontalScroll(DependencyObject element, bool value)
            => element.SetValue(SupportHorizontalScrollProperty, value);

        public static bool GetSupportHorizontalScroll(DependencyObject element)
            => (bool)element.GetValue(SupportHorizontalScrollProperty);

    }
}