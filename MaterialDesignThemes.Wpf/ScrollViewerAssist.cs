using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class ScrollViewerAssist
    {
        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.RegisterAttached(
            "SyncHorizontalOffset", typeof(double), typeof(ScrollViewerAssist), new PropertyMetadata(default(double), OnSyncHorizontalOffsetChanged));

        private static void OnSyncHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            scrollViewer?.ScrollToHorizontalOffset((double)e.NewValue);
        }

        public static void SetSyncHorizontalOffset(DependencyObject element, double value)
        {
            element.SetValue(HorizontalOffsetProperty, value);
        }

        public static double GetSyncHorizontalOffset(DependencyObject element)
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
    }
}