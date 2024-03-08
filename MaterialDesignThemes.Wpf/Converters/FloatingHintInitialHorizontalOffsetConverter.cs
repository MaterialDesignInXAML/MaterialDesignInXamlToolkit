using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintInitialHorizontalOffsetConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is null
            || values.Length < 9
            || values[0] is not double prefixWidth
            || values[1] is not Thickness prefixMargin
            || values[2] is not double suffixWidth
            || values[3] is not Thickness suffixMargin
            || values[4] is not PrefixSuffixVisibility prefixVisibility
            || values[5] is not PrefixSuffixVisibility suffixVisibility
            || values[6] is not PrefixSuffixHintBehavior prefixHintBehavior
            || values[7] is not PrefixSuffixHintBehavior suffixHintBehavior
            || values[8] is not HorizontalAlignment horizontalContentAlignment)
        {
            return 0;
        }
        return horizontalContentAlignment switch
        {
            HorizontalAlignment.Center => 0,
            HorizontalAlignment.Right => GetRightOffset(),
            _ => GetLeftOffset(),
        };

        double GetLeftOffset()
        {
            if (prefixVisibility == PrefixSuffixVisibility.VisibleWhenFocusedOrNonEmpty)
            {
                return 0;
            }
            return prefixHintBehavior switch
            {
                PrefixSuffixHintBehavior.AlignWithPrefixSuffix => -(prefixWidth + prefixMargin.Right),
                _ => 0,
            };
        }

        double GetRightOffset()
        {
            if (suffixVisibility == PrefixSuffixVisibility.VisibleWhenFocusedOrNonEmpty)
            {
                return 0;
            }
            return suffixHintBehavior switch
            {
                PrefixSuffixHintBehavior.AlignWithPrefixSuffix => suffixWidth + suffixMargin.Right,
                _ => 0,
            };
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
