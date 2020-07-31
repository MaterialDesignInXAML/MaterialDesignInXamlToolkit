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

        #region Obsolete (Remove in next major rev)

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static readonly DependencyProperty LeftHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
        "LeftHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
        new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static void SetLeftHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(LeftHeaderPaddingProperty, value);
        }

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static Thickness GetLeftHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(LeftHeaderPaddingProperty);
        }

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static readonly DependencyProperty RightHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "RightHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static void SetRightHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(RightHeaderPaddingProperty, value);
        }

        [Obsolete("This will be removed in the next major version. Use HorizontalHeaderPaddingProperty instead.")]
        public static Thickness GetRightHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(RightHeaderPaddingProperty);
        }

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static readonly DependencyProperty UpHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "UpHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static void SetUpHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(UpHeaderPaddingProperty, value);
        }

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static Thickness GetUpHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(UpHeaderPaddingProperty);
        }

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static readonly DependencyProperty DownHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "DownHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static void SetDownHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(DownHeaderPaddingProperty, value);
        }

        [Obsolete("This will be removed in the next major version. Use VerticalHeaderPaddingProperty instead.")]
        public static Thickness GetDownHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(DownHeaderPaddingProperty);
        }

        #endregion
    }
}