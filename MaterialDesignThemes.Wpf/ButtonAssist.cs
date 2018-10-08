using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonAssist
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(double), typeof(ButtonAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetCornerRadius(DependencyObject element, double value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static double GetCornerRadius(DependencyObject element)
        {
            return (double)element.GetValue(CornerRadiusProperty);
        }
    }
}
