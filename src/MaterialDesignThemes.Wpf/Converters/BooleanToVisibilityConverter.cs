namespace MaterialDesignThemes.Wpf.Converters;

public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
{
    public static readonly BooleanToVisibilityConverter Instance = new();
    public static readonly BooleanToVisibilityConverter InverseInstance = new() { FalseValue = Visibility.Visible, TrueValue = Visibility.Hidden };

    public BooleanToVisibilityConverter() :
        base(Visibility.Visible, Visibility.Collapsed)
    { }
}
