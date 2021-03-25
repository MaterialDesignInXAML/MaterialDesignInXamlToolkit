using System.Windows;
using System.Windows.Controls;

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

        public static void SetOnlyShowFocusVisualWhileDragging(Slider slider, bool value)
            => slider.SetValue(OnlyShowFocusVisualWhileDraggingProperty, value);

        public static bool GetOnlyShowFocusVisualWhileDragging(Slider element)
            => (bool)element.GetValue(OnlyShowFocusVisualWhileDraggingProperty);
    }
}
