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
            Thickness prefixTextMargin,
            bool isMouseOver,
            bool hasKeyboardFocus])
        {
            double iconMargin = leadingIconWidth > 0 ? leadingIconMargin.Left + leadingIconMargin.Right : 0;
            double prefixMargin = prefixTextWidth > 0 ? prefixTextMargin.Left + prefixTextMargin.Right : 0;
            double offset = leadingIconWidth + iconMargin + prefixTextWidth + prefixMargin;
            double bottomOffset = 0;
            double topOffset = 0;

            if (isMouseOver || hasKeyboardFocus)
            {
                offset -= 1;
                topOffset += 1;
                bottomOffset -= 1;
            }
            return new Thickness(offset, topOffset, 0, bottomOffset);
        }
        return new Thickness(0);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
