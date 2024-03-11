using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintMarginConverter : IMultiValueConverter
{
    private static readonly object EmptyThickness = new Thickness(0);
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null
            || values.Length < 6
            || values[0] is not double prefixWidth
            || values[1] is not Thickness prefixMargin
            || values[2] is not double suffixWidth
            || values[3] is not Thickness suffixMargin
            || values[4] is not PrefixSuffixVisibility prefixVisibility
            || values[5] is not PrefixSuffixVisibility suffixVisibility)
        {
            return EmptyThickness;
        }
        return new Thickness(GetLeftMargin(), 0, GetRightMargin(), 0);

        double GetLeftMargin()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.AlwaysVisible => prefixWidth + prefixMargin.Right,
                _ => 0,
            };
        }

        double GetRightMargin()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.AlwaysVisible => suffixWidth + suffixMargin.Left,
                _ => 0,
            };
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
