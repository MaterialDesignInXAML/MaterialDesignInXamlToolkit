using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class VisualFeedbackAssist
    {
        public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached(
            "ClipToBounds", typeof (bool), typeof (VisualFeedbackAssist), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetClipToBounds(DependencyObject element, bool value)
        {
            element.SetValue(ClipToBoundsProperty, value);
        }

        public static bool GetClipToBounds(DependencyObject element)
        {
            return (bool) element.GetValue(ClipToBoundsProperty);
        }   
    }
}