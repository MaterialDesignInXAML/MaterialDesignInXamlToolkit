using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public enum CalendarOrientation
    {
        Vertical,
        Horizontal
    }
    public static class CalendarAssist
    {
        #region Header Visibility
        public static readonly DependencyProperty IsHeaderVisibleProperty = DependencyProperty.RegisterAttached(
            "IsHeaderVisible", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(true));

        public static bool GetIsHeaderVisible(DependencyObject element) => (bool)element.GetValue(IsHeaderVisibleProperty);
        public static void SetIsHeaderVisible(DependencyObject element, bool value) => element.SetValue(IsHeaderVisibleProperty, value);
        #endregion

        #region HeaderBackground

        public static readonly DependencyProperty HeaderBackgroundProperty = DependencyProperty.RegisterAttached(
            "HeaderBackground", typeof(Brush), typeof(CalendarAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetHeaderBackground(DependencyObject element) => (Brush)element.GetValue(HeaderBackgroundProperty);
        public static void SetHeaderBackground(DependencyObject element, Brush value) => element.SetValue(HeaderBackgroundProperty, value);
        #endregion

        #region HeaderForeground
        public static readonly DependencyProperty HeaderForegroundProperty = DependencyProperty.RegisterAttached(
            "HeaderForeground", typeof(Brush), typeof(CalendarAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetHeaderForeground(DependencyObject element) => (Brush)element.GetValue(HeaderForegroundProperty);
        public static void SetHeaderForeground(DependencyObject element, Brush value) => element.SetValue(HeaderForegroundProperty, value);
        #endregion

        #region SelectionColor
        public static readonly DependencyProperty SelectionColorProperty = DependencyProperty.RegisterAttached(
            "SelectionColor", typeof(Brush), typeof(CalendarAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetSelectionColor(DependencyObject element) => (Brush)element.GetValue(SelectionColorProperty);
        public static void SetSelectionColor(DependencyObject element, Brush value) => element.SetValue(SelectionColorProperty, value);
        #endregion

        #region SelectionForegroundColor
        public static readonly DependencyProperty SelectionForegroundColorProperty = DependencyProperty.RegisterAttached(
            "SelectionForegroundColor", typeof(Brush), typeof(CalendarAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetSelectionForegroundColor(DependencyObject element) => (Brush)element.GetValue(SelectionForegroundColorProperty);
        public static void SetSelectionForegroundColor(DependencyObject element, Brush value) => element.SetValue(SelectionForegroundColorProperty, value);
        #endregion

        #region Orientation
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.RegisterAttached(
            "Orientation", typeof(CalendarOrientation), typeof(CalendarAssist), new FrameworkPropertyMetadata(default(CalendarOrientation)));

        public static CalendarOrientation GetOrientation(DependencyObject element) => (CalendarOrientation)element.GetValue(OrientationProperty);
        public static void SetOrientation(DependencyObject element, CalendarOrientation value) => element.SetValue(OrientationProperty, value);
        #endregion
    }
}