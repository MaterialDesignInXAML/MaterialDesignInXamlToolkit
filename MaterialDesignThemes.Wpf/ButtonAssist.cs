using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonAssist
    {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

        #region AttachedProperty : CornerRadiusProperty
        /// <summary>
        /// Controls the corner radius of the surrounding box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ButtonAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion
    }
}