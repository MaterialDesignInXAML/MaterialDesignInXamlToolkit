using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public class CheckBoxAssist
    {
        public static readonly DependencyProperty CheckBoxSizeProperty = DependencyProperty.RegisterAttached(
            "CheckBoxSize", typeof(double), typeof(CheckBoxAssist), new PropertyMetadata(18d));

        public static void SetCheckBoxSize(DependencyObject element, double value) => element.SetValue(CheckBoxSizeProperty, value);
        public static double GetCheckBoxSize(DependencyObject element) => (double) element.GetValue(CheckBoxSizeProperty);
    }
}