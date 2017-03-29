using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Helper properties for working with text fields.
    /// </summary>
    public static class TextFieldAssist
    {
        /// <summary>
        /// The text box scroll viewer margin property
        /// </summary>
        public static readonly DependencyProperty TextBoxScrollViewerMarginProperty = DependencyProperty.RegisterAttached(
            "TextBoxScrollViewerMargin",
            typeof(Thickness),
            typeof(TextFieldAssist),
            new FrameworkPropertyMetadata(new Thickness(double.NegativeInfinity), FrameworkPropertyMetadataOptions.Inherits, TextBoxScrollViewerMarginPropertyChangedCallback));

        /// <summary>
        /// Sets the text box scroll viewer margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetTextBoxScrollViewerMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(TextBoxScrollViewerMarginProperty, value);
        }

        /// <summary>
        /// Gets the text box scroll viewer margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static Thickness GetTextBoxScrollViewerMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(TextBoxScrollViewerMarginProperty);
        }

        /// <summary>
        /// The text box view margin property
        /// </summary>
        public static readonly DependencyProperty TextBoxViewMarginProperty = DependencyProperty.RegisterAttached(
            "TextBoxViewMargin",
            typeof(Thickness),
            typeof(TextFieldAssist),
            new FrameworkPropertyMetadata(new Thickness(double.NegativeInfinity), FrameworkPropertyMetadataOptions.Inherits, TextBoxViewMarginPropertyChangedCallback));

        /// <summary>
        /// Sets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetTextBoxViewMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(TextBoxViewMarginProperty, value);
        }

        /// <summary>
        /// Gets the text box view margin.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The <see cref="Thickness" />.
        /// </returns>
        public static Thickness GetTextBoxViewMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(TextBoxViewMarginProperty);
        }


        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static readonly DependencyProperty DecorationVisibilityProperty = DependencyProperty.RegisterAttached(
            "DecorationVisibility", typeof(Visibility), typeof(TextFieldAssist), new PropertyMetadata(default(Visibility)));

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        public static void SetDecorationVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(DecorationVisibilityProperty, value);
        }

        /// <summary>
        /// Controls the visibility of the underline decoration.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Visibility GetDecorationVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(DecorationVisibilityProperty);
        }

        #region Methods

        /// <summary>
        /// Applies the text box scroll viewer margin.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="margin">The margin.</param>
        private static void ApplyTextBoxScrollViewerMargin(Control textBox, Thickness margin)
        {
            if (margin.Equals(new Thickness(double.NegativeInfinity)))
            {
                return;
            }

            var frameworkElement = textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer;
            if (frameworkElement != null)
            {
                frameworkElement.Margin = margin;
            }
        }

        /// <summary>
        /// Applies the text box view margin.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="margin">The margin.</param>
        private static void ApplyTextBoxViewMargin(Control textBox, Thickness margin)
        {
            if (margin.Equals(new Thickness(double.NegativeInfinity)))
            {
                return;
            }

            var frameworkElement = (textBox.Template.FindName("PART_ContentHost", textBox) as ScrollViewer)?.Content as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Margin = margin;
            }
        }

        /// <summary>
        /// The text box scroll viewer margin property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The dependency property changed event args.</param>
        private static void TextBoxScrollViewerMarginPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Control; //could be a text box or password box
            if (box == null)
            {
                return;
            }

            if (box.IsLoaded)
            {
                ApplyTextBoxScrollViewerMargin(box, (Thickness)dependencyPropertyChangedEventArgs.NewValue);
            }

            box.Loaded += (sender, args) =>
            {
                var textBox = (Control)sender;
                ApplyTextBoxScrollViewerMargin(textBox, GetTextBoxScrollViewerMargin(textBox));
            };
        }

        /// <summary>
        /// The text box view margin property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="dependencyPropertyChangedEventArgs">The dependency property changed event args.</param>
        private static void TextBoxViewMarginPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var box = dependencyObject as Control; //could be a text box or password box
            if (box == null)
            {
                return;
            }

            if (box.IsLoaded)
            {
                ApplyTextBoxViewMargin(box, (Thickness)dependencyPropertyChangedEventArgs.NewValue);
            }

            box.Loaded += (sender, args) =>
            {
                var textBox = (Control)sender;
                ApplyTextBoxViewMargin(textBox, GetTextBoxViewMargin(textBox));
            };
        }

        #endregion
    }
}
