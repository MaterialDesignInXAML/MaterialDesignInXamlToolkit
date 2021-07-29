using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ListBoxItemAssist
    {

        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

        #region AttachedProperty : CornerRadiusProperty
        /// <summary>
        /// Controls the corner radius of the selection box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ListBoxItemAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion

        #region ShowSelection
        public static bool GetShowSelection(DependencyObject element)
            => (bool)element.GetValue(ShowSelectionProperty);
        public static void SetShowSelection(DependencyObject element, bool value)
            => element.SetValue(ShowSelectionProperty, value);

        public static readonly DependencyProperty ShowSelectionProperty =
            DependencyProperty.RegisterAttached("ShowSelection", typeof(bool), typeof(ListBoxItemAssist), new PropertyMetadata(true));
        #endregion
    }
}