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
            switch (floatingAlignment)
            {
                case FloatingHintHorizontalAlignment.Inherit:
                    alignment = restingAlignment;
                    break;
                case FloatingHintHorizontalAlignment.Left:
                    alignment = HorizontalAlignment.Left;
                    break;
                case FloatingHintHorizontalAlignment.Center:
                    alignment = HorizontalAlignment.Center;
                    break;
                case FloatingHintHorizontalAlignment.Right:
                    alignment = HorizontalAlignment.Right;
                    break;
                case FloatingHintHorizontalAlignment.Stretch:
                    alignment = HorizontalAlignment.Stretch;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        switch (alignment)
        {
            case HorizontalAlignment.Right:
                return FloatRight();
            case HorizontalAlignment.Center:
                return FloatCenter();
            default:
                return FloatLeft();
        }

        Thickness FloatLeft()
        {
            if (restingAlignment == HorizontalAlignment.Center)
            {
                // Animate from center to left
                double offset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                return new Thickness(offset - offset * scale, 0, 0, 0);
            }
            if (restingAlignment == HorizontalAlignment.Right)
            {
                // Animate from right to left
                double offset = Math.Max(0, availableWidth - desiredWidth);
                return new Thickness(offset - offset * scale, 0, 0, 0);
            }
            return new Thickness(0);
        }

        Thickness FloatCenter()
        {
            if (restingAlignment == HorizontalAlignment.Left || restingAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to center
                double offset = Math.Max(0, (availableWidth - desiredWidth * scaleMultiplier) / 2);
                return new Thickness(offset * scale, 0, 0, 0);
            }
            if (restingAlignment == HorizontalAlignment.Right)
            {
                // Animate from right to center
                double startOffset = Math.Max(0, availableWidth - desiredWidth);
                double endOffset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                double endOffsetDelta = startOffset - endOffset;
                return new Thickness(endOffset + endOffsetDelta * (1 - scale), 0, 0, 0);
            }
            return new Thickness(Math.Max(0, availableWidth - desiredWidth * scaleMultiplier) / 2, 0, 0, 0);
        }

        Thickness FloatRight()
        {
            if (restingAlignment == HorizontalAlignment.Left || restingAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to right
                double offset = Math.Max(0, availableWidth - desiredWidth * scaleMultiplier);
                return new Thickness(offset * scale, 0, 0, 0);
            }
            if (restingAlignment == HorizontalAlignment.Center)
            {
                // Animate from center to right
                double startOffset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                double endOffsetDelta = Math.Max(0, availableWidth - desiredWidth * scaleMultiplier) - startOffset;
                return new Thickness(startOffset + endOffsetDelta * scale, 0, 0, 0);
            }
            return new Thickness(Math.Max(0, availableWidth - desiredWidth * scaleMultiplier), 0, 0, 0);
        }
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
