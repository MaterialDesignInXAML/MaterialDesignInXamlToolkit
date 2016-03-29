using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class HintAssist
    {
        #region UseFloating

        public static readonly DependencyProperty UseFloatingProperty = DependencyProperty.RegisterAttached(
            "UseFloating",
            typeof(bool),
            typeof(HintAssist),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetUseFloating(DependencyObject element)
        {
            return (bool) element.GetValue(UseFloatingProperty);
        }

        public static void SetUseFloating(DependencyObject element, bool value)
        {
            element.SetValue(UseFloatingProperty, value);
        }

        #endregion

        #region Hint

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty HintProperty = DependencyProperty.RegisterAttached(
            "Hint",
            typeof(string),
            typeof(HintAssist),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the hint.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHint(DependencyObject element, string value)
        {
            element.SetValue(HintProperty, value);
        }

        /// <summary>
        /// Gets the hint.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public static string GetHint(DependencyObject element)
        {
            return (string)element.GetValue(HintProperty);
        }

        #endregion

        #region HintOpacity

        /// <summary>
        /// The hint opacity property
        /// </summary>
        public static readonly DependencyProperty HintOpacityProperty = DependencyProperty.RegisterAttached(
            "HintOpacity",
            typeof(double),
            typeof(HintAssist),
            new PropertyMetadata(.56));

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static double GetHintOpacityProperty(DependencyObject element)
        {
            return (double)element.GetValue(HintOpacityProperty);
        }

        /// <summary>
        /// Sets the hint opacity.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHintOpacity(DependencyObject element, double value)
        {
            element.SetValue(HintOpacityProperty, value);
        }

        #endregion
    }
}