using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintMarginConverter : IMultiValueConverter
{
    private static readonly object EmptyThickness = new Thickness(0);

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values is [double prefixWidth, Thickness prefixMargin, double suffixWidth, Thickness suffixMargin, PrefixSuffixVisibility prefixVisibility, PrefixSuffixVisibility suffixVisibility]
            ? new Thickness(GetLeftMargin(), 0, GetRightMargin(), 0)
            : EmptyThickness;

        double GetLeftMargin()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.Always => prefixWidth + prefixMargin.Right,
                _ => 0,
            };
        }

        double GetRightMargin()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.Always => suffixWidth + suffixMargin.Left,
                _ => 0,
            };
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
