using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public static class SliderAssist
    {
        public static readonly DependencyProperty IsActiveTrackProperty = DependencyProperty.RegisterAttached(
            "IsActiveTrack",
            typeof(bool),
            typeof(SliderAssist),
            new PropertyMetadata(false));

        public static bool GetIsActiveTrack(RepeatButton repeatButton)
            => (bool)repeatButton.GetValue(IsActiveTrackProperty);

        public static void SetIsActiveTrack(RepeatButton repeatButton, bool value)
            => repeatButton.SetValue(IsActiveTrackProperty, value);
    }
}
