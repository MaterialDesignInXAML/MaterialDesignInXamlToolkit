using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintContainerMarginConverter : IMultiValueConverter
{
    private static readonly object EmptyThickness = new Thickness(0);

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double scale, Thickness floatingMargin])
        {
            return floatingMargin with
            {
                Left = floatingMargin.Left * scale,
                Top = floatingMargin.Top * scale,
                Right = floatingMargin.Right * scale,
                Bottom = floatingMargin.Bottom * scale,
            };
        }
        return EmptyThickness;
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
