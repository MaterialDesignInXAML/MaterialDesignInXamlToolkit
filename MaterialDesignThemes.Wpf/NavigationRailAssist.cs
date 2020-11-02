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
    }
}