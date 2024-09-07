using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public sealed class MathMultipleConverter : IMultiValueConverter
{
    public MathOperation Operation { get; set; }

    public object? Convert(object?[]? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is null || value.Length < 2 || value[0] is null || value[1] is null) return Binding.DoNothing;

        if (!double.TryParse(value[0]!.ToString(), out double value1) || !double.TryParse(value[1]!.ToString(), out double value2))
            return 0;

        return Operation switch
        {
            MathOperation.Add => value1 + value2,
            MathOperation.Divide => value1 / value2,
            MathOperation.Multiply => value1 * value2,
            MathOperation.Subtract => value1 - value2,
            MathOperation.Pow => Math.Pow(value1, value2),
            _ => Binding.DoNothing
        };
    }

    public object?[]? ConvertBack(object? value, Type[]? targetTypes, object? parameter, CultureInfo? culture)
        => throw new NotImplementedException();
}
