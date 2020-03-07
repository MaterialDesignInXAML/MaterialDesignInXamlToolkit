using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    internal static class ScrollViewerAssist
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
    }
}