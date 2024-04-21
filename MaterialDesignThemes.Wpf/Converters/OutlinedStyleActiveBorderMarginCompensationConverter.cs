using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class OutlinedStyleActiveBorderMarginCompensationConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not [Thickness baseThickness, Thickness thicknessToSubtract])
        {
            return default(Thickness);
        }

        return new Thickness(
            baseThickness.Left - thicknessToSubtract.Left,
            baseThickness.Top - thicknessToSubtract.Top,
            baseThickness.Right - thicknessToSubtract.Right,
            baseThickness.Bottom - thicknessToSubtract.Bottom);
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
