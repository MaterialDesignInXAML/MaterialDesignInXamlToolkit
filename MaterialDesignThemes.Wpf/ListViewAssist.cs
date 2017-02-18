using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ListViewAssist
    {
        public static readonly DependencyProperty ListViewItemPaddingProperty = DependencyProperty.RegisterAttached(
            "ListViewItemPadding",
            typeof(Thickness),
            typeof(ListViewAssist),
            new FrameworkPropertyMetadata(new Thickness(13, 8, 8, 8), FrameworkPropertyMetadataOptions.Inherits));

        public static void SetListViewItemPadding(DependencyObject element, Thickness value)
        {
            element.SetValue(ListViewItemPaddingProperty, value);
        }

        public static Thickness GetListViewItemPadding(DependencyObject element)
        {
            return (Thickness)element.GetValue(ListViewItemPaddingProperty);
        }
    }
}
