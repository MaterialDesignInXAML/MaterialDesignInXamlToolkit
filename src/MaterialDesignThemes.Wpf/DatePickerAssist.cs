namespace MaterialDesignThemes.Wpf;

[Obsolete("This class is obsolete and will be removed in a future version. Please use the TextBoxAssist equivalents instead. For OutlinedBorderInactiveThickness, simply use BorderThickness property instead.")]
public static class DatePickerAssist
{
    public static readonly DependencyProperty OutlinedBorderInactiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderInactiveThickness", typeof(Thickness), typeof(DatePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderInactiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderInactiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderInactiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderInactiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderInactiveThicknessProperty);

    public static readonly DependencyProperty OutlinedBorderActiveThicknessProperty = DependencyProperty.RegisterAttached(
        "OutlinedBorderActiveThickness", typeof(Thickness), typeof(DatePickerAssist), new FrameworkPropertyMetadata(Constants.DefaultOutlinedBorderActiveThickness, FrameworkPropertyMetadataOptions.Inherits));
    public static void SetOutlinedBorderActiveThickness(DependencyObject element, Thickness value) => element.SetValue(OutlinedBorderActiveThicknessProperty, value);
    public static Thickness GetOutlinedBorderActiveThickness(DependencyObject element) => (Thickness)element.GetValue(OutlinedBorderActiveThicknessProperty);
}
