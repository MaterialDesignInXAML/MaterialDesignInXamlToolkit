using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class HintAssist
    {
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
    }
}