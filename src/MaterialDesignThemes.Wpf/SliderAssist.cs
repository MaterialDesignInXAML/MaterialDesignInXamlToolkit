using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

public static class SliderAssist
{
    public static readonly DependencyProperty HideActiveTrackProperty
        = DependencyProperty.RegisterAttached(
            "HideActiveTrack",
            typeof(bool),
            typeof(SliderAssist),
            new PropertyMetadata(false));

    public static bool GetHideActiveTrack(DependencyObject element)
        => (bool)element.GetValue(HideActiveTrackProperty);
    public static void SetHideActiveTrack(DependencyObject element, bool value)
        => element.SetValue(HideActiveTrackProperty, value);

    public static readonly DependencyProperty OnlyShowFocusVisualWhileDraggingProperty
        = DependencyProperty.RegisterAttached(
            "OnlyShowFocusVisualWhileDragging",
            typeof(bool),
            typeof(SliderAssist),
            new PropertyMetadata(false));

    public static bool GetOnlyShowFocusVisualWhileDragging(RangeBase element)
        => (bool)element.GetValue(OnlyShowFocusVisualWhileDraggingProperty);

    public static void SetOnlyShowFocusVisualWhileDragging(RangeBase element, bool value)
        => element.SetValue(OnlyShowFocusVisualWhileDraggingProperty, value);

    public static readonly DependencyProperty ToolTipFormatProperty
        = DependencyProperty.RegisterAttached(
            "ToolTipFormat",
            typeof(string),
            typeof(SliderAssist),
            new PropertyMetadata(null));

    public static string GetToolTipFormat(RangeBase element)
        => (string)element.GetValue(ToolTipFormatProperty);

    public static void SetToolTipFormat(RangeBase element, string value)
        => element.SetValue(ToolTipFormatProperty, value);

    // Fix for Issue3628
    public static readonly DependencyProperty FocusSliderOnClickProperty =
            DependencyProperty.RegisterAttached(
                "FocusSliderOnClick",
                typeof(bool),
                typeof(SliderAssist),
                new PropertyMetadata(false, OnFocusSliderOnClickChanged));

    public static bool GetFocusSliderOnClick(RangeBase obj) =>
        (bool)obj.GetValue(FocusSliderOnClickProperty);

    public static void SetFocusSliderOnClick(RangeBase obj, bool value) =>
        obj.SetValue(FocusSliderOnClickProperty, value);

    private static void OnFocusSliderOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Slider slider)
        {
            if ((bool)e.NewValue)
            {
                slider.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                                 (MouseButtonEventHandler)Slider_PreviewMouseLeftButtonDown,
                                        true);
            }
            else
            {
                slider.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                                    (MouseButtonEventHandler)Slider_PreviewMouseLeftButtonDown);
            }
        }
    }

    private static void Slider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Slider slider)
        {
            slider.Focus();
        }
    }
}
