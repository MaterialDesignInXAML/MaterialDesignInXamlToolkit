using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class NavigationRailAssist
    {
        #region Property FloatingContent

        /// <summary>
        /// Floating Content (ex: Button) on navigation rail (optional)
        /// </summary>
        public static readonly DependencyProperty FloatingContentProperty = DependencyProperty.RegisterAttached(
            "FloatingContent", typeof(object), typeof(NavigationRailAssist), new PropertyMetadata(null));

        public static object GetFloatingContent(DependencyObject element) => (object)element.GetValue(FloatingContentProperty);
        public static void SetFloatingContent(DependencyObject element, object value) => element.SetValue(FloatingContentProperty, value);

        #endregion

        #region Property ShowSelectionBackground

        public static readonly DependencyProperty ShowSelectionBackgroundProperty = DependencyProperty.RegisterAttached(
            "ShowSelectionBackground", typeof(bool), typeof(NavigationRailAssist), new PropertyMetadata(false));

        public static object GetShowSelectionBackground(DependencyObject element) => (bool)element.GetValue(ShowSelectionBackgroundProperty);
        public static void SetShowSelectionBackground(DependencyObject element, bool value) => element.SetValue(ShowSelectionBackgroundProperty, value);

        #endregion

        #region Property SelectionCornerRadius

        public static readonly DependencyProperty SelectionUniformCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "SelectionUniformCornerRadius", typeof(CornerRadius), typeof(NavigationRailAssist), new PropertyMetadata(default(CornerRadius)));

        public static object GetSelectionUniformCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(SelectionUniformCornerRadiusProperty);
        public static void SetSelectionUniformCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(SelectionUniformCornerRadiusProperty, value);

        #endregion
    }
}