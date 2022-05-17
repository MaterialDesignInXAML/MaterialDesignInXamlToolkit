using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public static class SliderAssist
    {
        public static readonly DependencyProperty OnlyShowFocusVisualWhileDraggingProperty
            = DependencyProperty.RegisterAttached(
                "OnlyShowFocusVisualWhileDragging",
                typeof(bool),
                typeof(SliderAssist),
                new PropertyMetadata(false));

        public static void SetOnlyShowFocusVisualWhileDragging(RangeBase element, bool value)
            => element.SetValue(OnlyShowFocusVisualWhileDraggingProperty, value);

        public static bool GetOnlyShowFocusVisualWhileDragging(RangeBase element)
            => (bool)element.GetValue(OnlyShowFocusVisualWhileDraggingProperty);

        public static readonly DependencyProperty ToolTipFormatProperty
            = DependencyProperty.RegisterAttached(
                "ToolTipFormat",
                typeof(string),
                typeof(SliderAssist),
                new PropertyMetadata(null));

        public static void SetToolTipFormat(RangeBase element, string value)
            => element.SetValue(ToolTipFormatProperty, value);

        public static string GetToolTipFormat(RangeBase element)
            => (string)element.GetValue(ToolTipFormatProperty);
    }
}
