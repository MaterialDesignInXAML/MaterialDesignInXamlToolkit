using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintMarginConverter : IMultiValueConverter
{
    public static readonly FloatingHintMarginConverter Instance = new();

    private static readonly object EmptyThickness = new Thickness(0);

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not
            [
                bool isFloatingHint,
                bool isKeyboardFocusWithin,
                bool isEditable,
                double prefixWidth,
                Thickness prefixMargin,
                double suffixWidth,
                Thickness suffixMargin,
                PrefixSuffixVisibility prefixVisibility,
                PrefixSuffixVisibility suffixVisibility
            ])
        {
            return EmptyThickness;
        }

        double prefixTotalWidth = prefixWidth > 0 ? prefixWidth + prefixMargin.Right : 0;
        double suffixTotalWidth = suffixWidth > 0 ? suffixWidth + suffixMargin.Left : 0;

        return new Thickness(GetLeftMargin(), 0, GetRightMargin(), 0);

        double GetLeftMargin()
        {
            return prefixVisibility switch
            {
                PrefixSuffixVisibility.Always => prefixWidth + prefixMargin.Right,
                _ => (isFloatingHint && isEditable) || (!isKeyboardFocusWithin && isEditable) ? 0 : prefixTotalWidth,
            };
        }

        double GetRightMargin()
        {
            return suffixVisibility switch
            {
                PrefixSuffixVisibility.Always => suffixWidth + suffixMargin.Left,
                _ => (isFloatingHint && isEditable) || (!isKeyboardFocusWithin && isEditable) ? 0 : suffixTotalWidth,
            };
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
