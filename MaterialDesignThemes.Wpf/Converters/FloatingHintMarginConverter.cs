using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintMarginConverter : IMultiValueConverter
{
    private static readonly object EmptyThickness = new Thickness(0);

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values is [bool isFloatingHint, bool isKeyboardFocusWithin, double prefixWidth, Thickness prefixMargin, double suffixWidth, Thickness suffixMargin, PrefixSuffixVisibility prefixVisibility, PrefixSuffixVisibility suffixVisibility]
            ? new Thickness(GetLeftMargin(), 0, GetRightMargin(), 0)
            : EmptyThickness;

        double GetLeftMargin()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.Always => prefixWidth + prefixMargin.Right,
                _ => isFloatingHint || !isKeyboardFocusWithin ? 0 : prefixWidth + prefixMargin.Right,
            };
        }

        double GetRightMargin()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.Always => suffixWidth + suffixMargin.Left,
                _ => isFloatingHint || !isKeyboardFocusWithin ? 0 : suffixWidth + suffixMargin.Left,
            };
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
