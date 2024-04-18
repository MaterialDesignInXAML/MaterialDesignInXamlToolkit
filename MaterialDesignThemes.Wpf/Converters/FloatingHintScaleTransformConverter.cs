using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintScaleTransformConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not [double scale, double lower, double upper])
        {
            return Transform.Identity;
        }

        double scalePercentage = upper + (lower - upper) * scale;
        return new ScaleTransform(scalePercentage, scalePercentage);
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
