using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignDemo.Shared.Converters;

public sealed class IsTransparentConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Color? color = value switch
        {
            Color c => c,
            SolidColorBrush brush => brush.Color,
            _ => null
        };
        return color == Colors.Transparent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue &&
            boolValue)
        {
            return Colors.Transparent;
        }
        return Binding.DoNothing;
    }
}
