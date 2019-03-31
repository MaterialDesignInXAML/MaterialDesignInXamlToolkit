using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ExpanderAssist
    {
        public static readonly DependencyProperty LeftHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "LeftHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetLeftHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(LeftHeaderPaddingProperty, value);
        }

        public static Thickness GetLeftHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(LeftHeaderPaddingProperty);
        }

        public static readonly DependencyProperty RightHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "RightHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetRightHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(RightHeaderPaddingProperty, value);
        }

        public static Thickness GetRightHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(RightHeaderPaddingProperty);
        }

        public static readonly DependencyProperty UpHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "UpHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetUpHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(UpHeaderPaddingProperty, value);
        }

        public static Thickness GetUpHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(UpHeaderPaddingProperty);
        }

        public static readonly DependencyProperty DownHeaderPaddingProperty =
            DependencyProperty.RegisterAttached(
                "DownHeaderPadding", typeof(Thickness), typeof(ExpanderAssist),
                new FrameworkPropertyMetadata(new Thickness(0, 0, 0, 0), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetDownHeaderPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(DownHeaderPaddingProperty, value);
        }

        public static Thickness GetDownHeaderPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(DownHeaderPaddingProperty);
        }
    }
}
