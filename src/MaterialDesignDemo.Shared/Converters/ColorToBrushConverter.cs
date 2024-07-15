using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignDemo.Shared.Converters;

[ValueConversion(typeof(Color), typeof(Brush))]
public sealed class ColorToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            SolidColorBrush rv = new(color);
            rv.Freeze();
            return rv;
        }
        return Binding.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush brush)
        {
            return brush.Color;
        }
        return default(Color);
    }
}
