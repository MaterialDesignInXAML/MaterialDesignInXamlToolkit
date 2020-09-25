using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class CalendarAssist
    {
        public static readonly DependencyProperty IsHeaderVisibleProperty = DependencyProperty.RegisterAttached(
            "IsHeaderVisible", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(true));

        public static void SetIsHeaderVisible(DependencyObject element, bool value) => element.SetValue(IsHeaderVisibleProperty, value);
        public static bool GetIsHeaderVisible(DependencyObject element) => (bool)element.GetValue(IsHeaderVisibleProperty);
    }
}