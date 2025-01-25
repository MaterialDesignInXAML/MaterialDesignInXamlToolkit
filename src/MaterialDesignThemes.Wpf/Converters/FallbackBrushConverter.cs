using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class FallbackBrushConverter : IMultiValueConverter
{
    public static readonly FallbackBrushConverter Instance = new();

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values?.OfType<SolidColorBrush>()
            .FirstOrDefault(x => x.Color != default && x.Color != Colors.Transparent);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
