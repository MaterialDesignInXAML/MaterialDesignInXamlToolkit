using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintInitialVerticalOffsetConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null
            || values.Length != 2
            || values[0] is not double contentHostHeight
            || values[1] is not double hintHeight)
        {
            return 0;
        }
        return (contentHostHeight - hintHeight) / 2;
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
