using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintContainerMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 2
            || values.Any(v => v == null)
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || values[1] is not Thickness floatingMargin)
        {
            return new Thickness(0);
        }

        return floatingMargin with
        {
            Left = floatingMargin.Left * scale,
            Top = floatingMargin.Top * scale,
            Right = floatingMargin.Right * scale,
            Bottom = floatingMargin.Bottom * scale,
        };
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
