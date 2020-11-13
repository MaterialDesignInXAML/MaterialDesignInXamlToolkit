using System.Windows;

namespace MaterialDesignDemo
{
    public static class ButtonAssist
    {
        public static readonly DependencyProperty UniformCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "UniformCornerRadius", typeof(double), typeof(ButtonAssist), new PropertyMetadata(2.0, OnUniformCornerRadius));

        private static void OnUniformCornerRadius(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(d, new CornerRadius((double)e.NewValue));

        public static void SetUniformCornerRadius(DependencyObject element, double value)
            => element.SetValue(UniformCornerRadiusProperty, value);

        public static double GetUniformCornerRadius(DependencyObject element)
            => (double)element.GetValue(UniformCornerRadiusProperty);
    }
}