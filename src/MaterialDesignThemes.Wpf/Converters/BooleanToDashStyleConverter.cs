using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class BooleanToDashStyleConverter : IValueConverter
{
    public DashStyle? TrueValue { get; set; }
    public DashStyle? FalseValue { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? TrueValue : FalseValue;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is DashStyle dashStyle && dashStyle == TrueValue;
}
