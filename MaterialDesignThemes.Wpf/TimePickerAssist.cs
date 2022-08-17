namespace MaterialDesignThemes.Wpf;

public static class TimePickerAssist
{
    private static Thickness DefaultOutlinedBorderInactiveThickness { get; } = new(1);
    private static Thickness DefaultOutlinedBorderActiveThickness { get; } = new(2);

    public static readonly DependencyProperty OutlinedBorderInactiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderInactiveThickness", typeof(Thickness), typeof(TimePickerAssist), new FrameworkPropertyMetadata(DefaultOutlinedBorderInactiveThickness, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetOutlinedBorderInactiveThickness(DependencyObject element, Thickness value)
    {
        element.SetValue(OutlinedBorderInactiveThicknessProperty, value);
    }

    public static Thickness GetOutlinedBorderInactiveThickness(DependencyObject element)
    {
        return (Thickness)element.GetValue(OutlinedBorderInactiveThicknessProperty);
    }

    public static readonly DependencyProperty OutlinedBorderActiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderActiveThickness", typeof(Thickness), typeof(TimePickerAssist), new FrameworkPropertyMetadata(DefaultOutlinedBorderActiveThickness, FrameworkPropertyMetadataOptions.Inherits));

    public static void SetOutlinedBorderActiveThickness(DependencyObject element, Thickness value)
    {
        element.SetValue(OutlinedBorderActiveThicknessProperty, value);
    }

    public static Thickness GetOutlinedBorderActiveThickness(DependencyObject element)
    {
        return (Thickness)element.GetValue(OutlinedBorderActiveThicknessProperty);
    }
}