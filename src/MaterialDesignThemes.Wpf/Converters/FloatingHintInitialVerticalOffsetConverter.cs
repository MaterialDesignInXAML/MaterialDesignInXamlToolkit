using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

/// <summary>
/// This converter is used to apply an initial vertical offset (downwards) of the floating hint in the case where the
/// <see cref="SmartHint.FloatingTarget"/> is taller than the <see cref="SmartHint"/> itself. This is typically the case
/// if a fixed (large) height is applied to the host control (e.g. <see cref="TextBox"/> or similar). In these cases the
/// hint should not float directly on top of the <see cref="SmartHint.FloatingTarget"/>, but rather be pushed down to sit
/// on top of the text inside the <see cref="SmartHint.FloatingTarget"/>.
///
/// There is an edge case that need to be dealt with, which is when the host element allows for text to wrap (i.e. in
/// <see cref="TextBox"/> based templates). In this case, we need to take the  number of text rows/line count into account
/// in the calculation.
/// </summary>
public class FloatingHintInitialVerticalOffsetConverter : IMultiValueConverter
{
    public static readonly FloatingHintInitialVerticalOffsetConverter Instance = new();

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double contentHostHeight, double hintHeight, int lineCount])
        {
            double offsetMultiplier = 0;
            if (lineCount > 1)
            {
                // Edge case where there are multiple rows of text so we need to calculate how far the hint should be pushed down.
                // If there are 2 rows, we need to reduce the offset by 0.5*height, 3 rows should reduce by 1*height, 4 rows should reduce by 1.5*height, etc.
                offsetMultiplier = lineCount / 2.0 - 0.5;
            }
            // Set an initial offset in order to push the hint down to where the actual text is displayed.
            // The value is clamped to be >= 0 which is needed for TextBoxes where a vertical scrollbar is needed (i.e. more lines
            // that are actually visible on screen) to avoid moving the hint further away than the actual viewport.
            return Math.Max(0, (contentHostHeight - hintHeight) / 2 - (offsetMultiplier * hintHeight));
        }
        return 0.0;
    }

    public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
