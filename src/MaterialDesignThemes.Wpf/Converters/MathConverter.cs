using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public sealed class MathConverter : IValueConverter
{
    public static readonly MathConverter AddInstance = new() { Operation = MathOperation.Add };
    public static readonly MathConverter SubtractInstance = new() { Operation = MathOperation.Subtract };
    public static readonly MathConverter MultiplyInstance = new() { Operation = MathOperation.Multiply };
    public static readonly MathConverter DivideInstance = new() { Operation = MathOperation.Divide };
    public static readonly MathConverter PowInstance = new() { Operation = MathOperation.Pow };

    public double Offset { get; set; }
    public MathOperation Operation { get; set; }

    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        try
        {
            double value1 = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
            double value2 = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
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
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}
