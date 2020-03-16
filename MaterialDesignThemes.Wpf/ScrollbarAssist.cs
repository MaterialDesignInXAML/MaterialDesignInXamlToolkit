using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ScrollBarAssist
    {
        public static readonly DependencyProperty ButtonsVisibilityProperty =
            DependencyProperty.RegisterAttached("ButtonsVisibility", typeof(Visibility), typeof(ScrollBarAssist), new PropertyMetadata(Visibility.Visible));

        public static void SetButtonsVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(ButtonsVisibilityProperty, value);
        }

        public static Visibility GetButtonsVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(ButtonsVisibilityProperty);
        }

        public static readonly DependencyProperty ThumbCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "ThumbCornerRadius", typeof(CornerRadius), typeof(ScrollBarAssist), new PropertyMetadata(default(CornerRadius)));

        public static void SetThumbCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(ThumbCornerRadiusProperty, value);
        }

        public static CornerRadius GetThumbCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(ThumbCornerRadiusProperty);
        }

        public static readonly DependencyProperty ThumbWidthProperty = DependencyProperty.RegisterAttached(
            "ThumbWidth", typeof(double), typeof(ScrollBarAssist), new PropertyMetadata(double.NaN));

        public static void SetThumbWidth(DependencyObject element, double value)
        {
            element.SetValue(ThumbWidthProperty, value);
        }

        public static double GetThumbWidth(DependencyObject element)
        {
            return (double)element.GetValue(ThumbWidthProperty);
        }

        public static readonly DependencyProperty ThumbHeightProperty = DependencyProperty.RegisterAttached(
            "ThumbHeight", typeof(double), typeof(ScrollBarAssist), new PropertyMetadata(double.NaN));

        public static void SetThumbHeight(DependencyObject element, double value)
        {
            element.SetValue(ThumbHeightProperty, value);
        }

        public static double GetThumbHeight(DependencyObject element)
        {
            return (double)element.GetValue(ThumbHeightProperty);
        }
    }
}
