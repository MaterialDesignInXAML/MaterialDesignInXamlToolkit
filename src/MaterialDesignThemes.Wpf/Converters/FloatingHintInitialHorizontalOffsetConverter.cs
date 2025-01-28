using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintInitialHorizontalOffsetConverter : IMultiValueConverter
{
    public static readonly FloatingHintInitialHorizontalOffsetConverter Instance = new();

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not
            [
                double prefixWidth,
                Thickness prefixMargin,
                double suffixWidth,
                Thickness suffixMargin,
                PrefixSuffixVisibility prefixVisibility,
                PrefixSuffixVisibility suffixVisibility,
                PrefixSuffixHintBehavior prefixHintBehavior,
                PrefixSuffixHintBehavior suffixHintBehavior,
                HorizontalAlignment horizontalContentAlignment,
                bool isEditable,
            ])
        {
            return 0D;
        }

        return horizontalContentAlignment switch
        {
            HorizontalAlignment.Center => 0D,
            HorizontalAlignment.Right => GetRightOffset(),
            _ => GetLeftOffset(),
        };

        double GetLeftOffset()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when prefixHintBehavior == PrefixSuffixHintBehavior.AlignWithText && isEditable =>
                    prefixWidth + prefixMargin.Right,
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when prefixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix && !isEditable && prefixWidth > 0D =>
                    -(prefixWidth + prefixMargin.Right),
                PrefixSuffixVisibility.Always
                    when prefixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix =>
                    -(prefixWidth + prefixMargin.Right),
                _ => 0D
            };
        }

        double GetRightOffset()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when suffixHintBehavior == PrefixSuffixHintBehavior.AlignWithText && isEditable =>
                    -(suffixWidth + suffixMargin.Left),
                PrefixSuffixVisibility.WhenFocusedOrNonEmpty
                    when suffixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix && !isEditable && suffixWidth > 0D =>
                    suffixWidth + suffixMargin.Left,
                PrefixSuffixVisibility.Always
                    when suffixHintBehavior == PrefixSuffixHintBehavior.AlignWithPrefixSuffix =>
                    suffixWidth + suffixMargin.Left,
                _ => 0D
            };
        }
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
