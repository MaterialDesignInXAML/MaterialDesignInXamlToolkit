using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public sealed class MathMultipleConverter : IMultiValueConverter
{
    public static readonly MathMultipleConverter AddInstance = new() { Operation = MathOperation.Add };
    public static readonly MathMultipleConverter SubtractInstance = new() { Operation = MathOperation.Subtract };
    public static readonly MathMultipleConverter MultiplyInstance = new() { Operation = MathOperation.Multiply };
    public static readonly MathMultipleConverter DivideInstance = new() { Operation = MathOperation.Divide };
    public static readonly MathMultipleConverter PowInstance = new() { Operation = MathOperation.Pow };

    public MathOperation Operation { get; set; }

    public object? Convert(object?[]? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (value is not [double value1, double value2]) return Binding.DoNothing;

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
