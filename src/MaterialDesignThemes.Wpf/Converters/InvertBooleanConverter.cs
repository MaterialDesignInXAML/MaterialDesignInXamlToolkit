namespace MaterialDesignThemes.Wpf.Converters;

public sealed class InvertBooleanConverter : BooleanConverter<bool>
{
    public static readonly InvertBooleanConverter Instance = new();

    public InvertBooleanConverter()
        : base(false, true)
    {
    }
}
