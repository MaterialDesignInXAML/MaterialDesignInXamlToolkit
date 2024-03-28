using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public sealed class MathConverter : IValueConverter
{
    public double Offset { get; set; }
    public MathOperation Operation { get; set; }

    public object? Convert(object? value, Type? targetType, object? parameter, CultureInfo? culture)
    {
        try
        {
            double value1 = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
            double value2 = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            switch (Operation)
            {
                case MathOperation.Add:
                    return value1 + value2 + Offset;
                case MathOperation.Divide:
                    return value1 / value2 + Offset;
                case MathOperation.Multiply:
                    return value1 * value2 + Offset;
                case MathOperation.Subtract:
                    return value1 - value2 + Offset;
                case MathOperation.Pow:
                    return Math.Pow(value1, value2) + Offset;
                default:
                    return Binding.DoNothing;
            }
        }
        catch (FormatException)
        {
            return Binding.DoNothing;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Binding.DoNothing;
}
