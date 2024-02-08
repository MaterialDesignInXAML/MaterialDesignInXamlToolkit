using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

internal class FloatingHintTextBlockMarginConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 7 || values.Any(v => v == null)
            || values[0] is not FloatingHintHorizontalAlignment floatingAlignment
            || values[1] is not HorizontalAlignment restingAlignment
            || !double.TryParse(values[2]!.ToString(), out double desiredWidth)
            || !double.TryParse(values[3]!.ToString(), out double availableWidth)
            || !double.TryParse(values[4]!.ToString(), out double scale)
            || !double.TryParse(values[5]!.ToString(), out double lower)
            || !double.TryParse(values[6]!.ToString(), out double upper))
        {
            return Transform.Identity;
        }

        double scaleMultiplier = upper + (lower - upper) * scale;

        HorizontalAlignment alignment = restingAlignment;
        if (scale != 0)
        {
            alignment = floatingAlignment switch
            {
                FloatingHintHorizontalAlignment.Inherit => restingAlignment,
                FloatingHintHorizontalAlignment.Left => HorizontalAlignment.Left,
                FloatingHintHorizontalAlignment.Center => HorizontalAlignment.Center,
                FloatingHintHorizontalAlignment.Right => HorizontalAlignment.Right,
                FloatingHintHorizontalAlignment.Stretch => HorizontalAlignment.Stretch,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
        double leftThickness = alignment switch
        {
            HorizontalAlignment.Right => FloatRight(),
            HorizontalAlignment.Center => FloatCenter(),
            _ => FloatLeft(),
        };

        return new Thickness(Math.Round(leftThickness), 0, 0, 0);

        double FloatLeft()
        {
            if (restingAlignment == HorizontalAlignment.Center)
            {
                // Animate from center to left
                double offset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                return offset - offset * scale;
            }
            if (restingAlignment == HorizontalAlignment.Right)
            {
                // Animate from right to left
                double offset = Math.Max(0, availableWidth - desiredWidth);
                return offset - offset * scale;
            }
            return 0;
        }

        double FloatCenter()
        {
            if (restingAlignment == HorizontalAlignment.Left || restingAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to center
                double offset = Math.Max(0, (availableWidth - desiredWidth * scaleMultiplier) / 2);
                return offset * scale;
            }
            if (restingAlignment == HorizontalAlignment.Right)
            {
                // Animate from right to center
                double startOffset = Math.Max(0, availableWidth - desiredWidth);
                double endOffset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                double endOffsetDelta = startOffset - endOffset;
                return endOffset + endOffsetDelta * (1 - scale);
            }
            return Math.Max(0, availableWidth - desiredWidth * scaleMultiplier) / 2;
        }

        double FloatRight()
        {
            if (restingAlignment == HorizontalAlignment.Left || restingAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to right
                double offset = Math.Max(0, availableWidth - desiredWidth * scaleMultiplier);
                return offset * scale;
            }
            if (restingAlignment == HorizontalAlignment.Center)
            {
                // Animate from center to right
                double startOffset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                double endOffsetDelta = Math.Max(0, availableWidth - desiredWidth * scaleMultiplier) - startOffset;
                return startOffset + endOffsetDelta * scale;
            }
            return Math.Max(0, availableWidth - desiredWidth * scaleMultiplier);
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
