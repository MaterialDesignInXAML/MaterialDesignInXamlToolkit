using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

/// <summary>
/// Converts a <see cref="Severity"/> to its theme brush via <see cref="SeverityBrushProvider"/>.
/// </summary>
public class SeverityToBrushConverter : IValueConverter
{
    public static readonly SeverityToBrushConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Severity severity && SeverityBrushProvider.GetBrush(severity) is { } brush
            ? brush
            : DependencyProperty.UnsetValue;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
