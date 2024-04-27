using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintContainerMarginConverter : IMultiValueConverter
{
    private static readonly object EmptyThickness = new Thickness(0);

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double scale, Thickness floatingMargin, double floatingScale])
        {
            return floatingMargin with
            {
                Left = (floatingMargin.Left * scale) / floatingScale,
                Top = (floatingMargin.Top * scale) / floatingScale,
                Right = (floatingMargin.Right * scale) / floatingScale,
                Bottom = (floatingMargin.Bottom * scale) / floatingScale
            };
        }
        return EmptyThickness;
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
