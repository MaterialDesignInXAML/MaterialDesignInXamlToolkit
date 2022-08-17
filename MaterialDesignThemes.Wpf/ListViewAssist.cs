using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ListViewAssist
    {
        public static readonly DependencyProperty ListViewItemPaddingProperty = DependencyProperty.RegisterAttached(
            "ListViewItemPadding",
            typeof(Thickness),
            typeof(ListViewAssist),
            new FrameworkPropertyMetadata(new Thickness(8, 8, 8, 8), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetListViewItemPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(ListViewItemPaddingProperty, value);
        }

        public static Thickness GetListViewItemPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(ListViewItemPaddingProperty);
        }

        public static readonly DependencyProperty HeaderRowBackgroundProperty = DependencyProperty.RegisterAttached(
            "HeaderRowBackground",
            typeof(Brush),
            typeof(ListViewAssist),
            new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetHeaderRowBackground(DependencyObject element, Brush value)
        {
            element.SetValue(HeaderRowBackgroundProperty, value);
        }

        public static Brush GetHeaderRowBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(HeaderRowBackgroundProperty);
        }
    }
}
