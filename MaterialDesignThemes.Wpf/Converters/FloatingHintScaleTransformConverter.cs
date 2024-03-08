using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintScaleTransformConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 3
            || values.Any(v => v == null)
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || !double.TryParse(values[1]!.ToString(), out double lower)
            || !double.TryParse(values[2]!.ToString(), out double upper))
        {
            return Transform.Identity;
        }
        double scalePercentage = upper + (lower - upper) * scale;
        return new ScaleTransform(scalePercentage, scalePercentage);
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
