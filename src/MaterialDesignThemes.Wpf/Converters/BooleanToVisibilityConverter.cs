namespace MaterialDesignThemes.Wpf.Converters;

public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility>
{
    public static readonly BooleanToVisibilityConverter CollapsedInstance = new() { FalseValue = Visibility.Collapsed, TrueValue = Visibility.Visible };
    public static readonly BooleanToVisibilityConverter NotCollapsedInstance = new() { FalseValue = Visibility.Visible, TrueValue = Visibility.Collapsed };

    public static readonly BooleanToVisibilityConverter HiddenInstance = new() { FalseValue = Visibility.Hidden, TrueValue = Visibility.Visible };
    public static readonly BooleanToVisibilityConverter NotHiddenInstance = new() { FalseValue = Visibility.Visible, TrueValue = Visibility.Hidden };
    
    public BooleanToVisibilityConverter() :
        base(Visibility.Visible, Visibility.Collapsed)
    { }
}
