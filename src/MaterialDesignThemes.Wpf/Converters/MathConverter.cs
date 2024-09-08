using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public sealed class MathConverter : IValueConverter
{
    public double Offset { get; set; }
    public MathOperation Operation { get; set; }

    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is not double value1 || parameter is not double value2) return Binding.DoNothing;

        return Operation switch
        {
            MathOperation.Add => value1 + value2 + Offset,
            MathOperation.Divide => value1 / value2 + Offset,
            MathOperation.Multiply => value1 * value2 + Offset,
            MathOperation.Subtract => value1 - value2 + Offset,
            MathOperation.Pow => Math.Pow(value1, value2) + Offset,
            _ => Binding.DoNothing,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}
