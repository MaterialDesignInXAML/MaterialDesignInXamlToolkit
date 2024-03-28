using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintInitialHorizontalOffsetConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double prefixWidth, Thickness prefixMargin, double suffixWidth, Thickness suffixMargin, PrefixSuffixVisibility prefixVisibility, PrefixSuffixVisibility suffixVisibility, PrefixSuffixHintBehavior prefixHintBehavior, PrefixSuffixHintBehavior suffixHintBehavior, HorizontalAlignment horizontalContentAlignment])
        {
            return horizontalContentAlignment switch
            {
                HorizontalAlignment.Center => 0,
                HorizontalAlignment.Right => GetRightOffset(),
                _ => GetLeftOffset(),
            };
        }
        return 0;

        double GetLeftOffset()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when prefixHintBehavior == PrefixSuffixHintBehavior.AlignWithText =>
                    prefixWidth + prefixMargin.Right,
                PrefixSuffixVisibility.Always
                    when prefixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix =>
                    -(prefixWidth + prefixMargin.Right),
                _ => 0
            };
        }

        double GetRightOffset()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when suffixHintBehavior == PrefixSuffixHintBehavior.AlignWithText =>
                    -(suffixWidth + suffixMargin.Left),
                PrefixSuffixVisibility.Always
                    when suffixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix =>
                    suffixWidth + suffixMargin.Left,
                _ => 0
            };
        }
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
