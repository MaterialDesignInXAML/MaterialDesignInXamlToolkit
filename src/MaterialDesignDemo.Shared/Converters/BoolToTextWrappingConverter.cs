using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignDemo.Shared.Converters;

public class BoolToTextWrappingConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }
        return TextWrapping.Wrap;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
