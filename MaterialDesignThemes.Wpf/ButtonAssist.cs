using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Helper properties for working with for make round corner.
    /// </summary>
    public static class ButtonAssist
    {
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(ButtonAssist), new PropertyMetadata(new CornerRadius(2.0)));

        public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static CornerRadius GetCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }
    }
}
