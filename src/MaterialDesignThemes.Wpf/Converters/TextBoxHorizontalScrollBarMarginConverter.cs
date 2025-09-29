using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class TextBoxHorizontalScrollBarMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [
            double leadingIconWidth,
            Thickness leadingIconMargin,
            double prefixTextWidth,
            Thickness prefixTextMargin])
        {
            double offset = leadingIconWidth + leadingIconMargin.Left + leadingIconMargin.Right + prefixTextWidth + prefixTextMargin.Left + prefixTextMargin.Right;
            return new Thickness(offset, 1, 0, 0);
        }
        return new Thickness(0);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
