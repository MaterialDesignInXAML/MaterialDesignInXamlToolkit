using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignDemo.Shared.Converters;

public class BrushToHexConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush brush) return null;

        static string lowerHexString(int i) => i.ToString("X2").ToLower();
        string hex = lowerHexString(brush.Color.R) +
                  lowerHexString(brush.Color.G) +
                  lowerHexString(brush.Color.B);
        return "#" + hex;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
