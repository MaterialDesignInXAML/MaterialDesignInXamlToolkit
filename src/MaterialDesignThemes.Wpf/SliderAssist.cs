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

    #region Issue3628
    internal static readonly DependencyProperty FocusParentSliderOnClickProperty =
            DependencyProperty.RegisterAttached(
                "FocusParentSliderOnClick",
                typeof(bool),
                typeof(SliderAssist),
                new PropertyMetadata(false, OnFocusParentSliderOnClickChanged));

    internal static bool GetFocusParentSliderOnClick(DependencyObject obj) =>
        (bool)obj.GetValue(FocusParentSliderOnClickProperty);

    internal static void SetFocusParentSliderOnClick(DependencyObject obj, bool value) =>
        obj.SetValue(FocusParentSliderOnClickProperty, value);

    private static void OnFocusParentSliderOnClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RepeatButton repeatButton)
        {
            if ((bool)e.NewValue)
            {
                repeatButton.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                                        (MouseButtonEventHandler)RepeatButton_PreviewMouseLeftButtonDown,
                                        true);
            }
            else
            {
                repeatButton.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                                           (MouseButtonEventHandler)RepeatButton_PreviewMouseLeftButtonDown);
            }
        }
    }

    private static void RepeatButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is DependencyObject repeatButton)
        {
            var slider = TreeHelper.FindParent<Slider>(repeatButton);
            slider?.Focus();
        }
    }
    #endregion
}
