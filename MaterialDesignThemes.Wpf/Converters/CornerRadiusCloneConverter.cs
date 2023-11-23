using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class CornerRadiusCloneConverter : IValueConverter
{
    public double? FixedTopLeft { get; set; }
    public double? FixedTopRight { get; set; }
    public double? FixedBottomLeft { get; set; }
    public double? FixedBottomRight { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is CornerRadius cornerRadius)
        {
            return new CornerRadius(
                FixedTopLeft ?? cornerRadius.TopLeft,
                FixedTopRight ?? cornerRadius.TopRight,
                FixedBottomRight ?? cornerRadius.BottomRight,
                FixedBottomLeft ?? cornerRadius.BottomLeft);
        }
        return new CornerRadius();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
