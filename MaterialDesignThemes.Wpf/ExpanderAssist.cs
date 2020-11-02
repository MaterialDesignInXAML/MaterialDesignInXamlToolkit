using System;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class ExpanderAssist
    {
        private static readonly Thickness DefaultHorizontalHeaderPadding = new Thickness(24, 12, 24, 12);
        private static readonly Thickness DefaultVerticalHeaderPadding = new Thickness(12, 24, 12, 24);

        #region AttachedProperty : HorizontalHeaderPaddingProperty
        public static readonly DependencyProperty HorizontalHeaderPaddingProperty
            = DependencyProperty.RegisterAttached("HorizontalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(DefaultHorizontalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

        public static Thickness GetHorizontalHeaderPadding(Expander element)
            => (Thickness)element.GetValue(HorizontalHeaderPaddingProperty);
        public static void SetHorizontalHeaderPadding(Expander element, Thickness value)
            => element.SetValue(HorizontalHeaderPaddingProperty, value);
        #endregion

        #region AttachedProperty : VerticalHeaderPaddingProperty
        public static readonly DependencyProperty VerticalHeaderPaddingProperty
            = DependencyProperty.RegisterAttached("VerticalHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(DefaultVerticalHeaderPadding, FrameworkPropertyMetadataOptions.Inherits));

        public static Thickness GetVerticalHeaderPadding(Expander element)
            => (Thickness)element.GetValue(VerticalHeaderPaddingProperty);
        public static void SetVerticalHeaderPadding(Expander element, Thickness value)
            => element.SetValue(VerticalHeaderPaddingProperty, value);
        #endregion

        #region AttachedProperty : HeaderFontSizeProperty
        public static readonly DependencyProperty HeaderFontSizeProperty
            = DependencyProperty.RegisterAttached("HeaderFontSize", typeof(double), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(15.0));

        public static double GetHeaderFontSize(Expander element)
            => (double)element.GetValue(HeaderFontSizeProperty);
        public static void SetHeaderFontSize(Expander element, double value)
            => element.SetValue(HeaderFontSizeProperty, value);
        #endregion
    }
}