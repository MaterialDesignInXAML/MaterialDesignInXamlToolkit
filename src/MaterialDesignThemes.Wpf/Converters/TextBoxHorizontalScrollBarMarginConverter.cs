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
            bool hasKeyboardFocus,
            bool hasOutlinedTextField,
            Thickness normalBorder,
            Thickness activeBorder])
        {
            double iconMargin = leadingIconWidth > 0 ? leadingIconMargin.Left + leadingIconMargin.Right : 0;
            double prefixMargin = prefixTextWidth > 0 ? prefixTextMargin.Left + prefixTextMargin.Right : 0;
            double offset = leadingIconWidth + iconMargin + prefixTextWidth + prefixMargin;
            double bottomOffset = 0;
            double topOffset = 0;

            if (hasOutlinedTextField && (isMouseOver || hasKeyboardFocus))
            {
                double horizDelta = activeBorder.Left - normalBorder.Left;
                double vertDeltaTop = activeBorder.Top - normalBorder.Top;
                double vertDeltaBottom = activeBorder.Bottom - normalBorder.Bottom;
                offset -= horizDelta;
                topOffset += vertDeltaTop;
                bottomOffset -= vertDeltaBottom;
            }
            return new Thickness(offset, topOffset, 0, bottomOffset);
        }
        return new Thickness(0);
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
