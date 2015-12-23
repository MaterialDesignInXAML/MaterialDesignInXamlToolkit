using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonAssist
    {
        #region Icon dependency property

        /// <summary>
        /// An attached dependency property which provides an
        /// icon for buttons.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(object), typeof(ButtonAssist), new FrameworkPropertyMetadata((ImageSource)null));

        /// <summary>
        /// Gets the <see cref="IconProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// object for arbitrary WPF elements.
        /// </summary>
        public static object GetIcone(DependencyObject obj)
        {
            return obj.GetValue(IconProperty);
        }

        /// <summary>
        /// Sets the attached <see cref="IconProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// object for arbitrary WPF elements.
        /// </summary>
        public static void SetIcon(DependencyObject obj, object value)
        {
            obj.SetValue(IconProperty, value);
        }

        #endregion
    }
}