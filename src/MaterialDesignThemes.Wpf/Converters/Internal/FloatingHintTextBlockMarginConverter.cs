using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class FloatingHintTextBlockMarginConverter : IMultiValueConverter
{
    public static readonly FloatingHintTextBlockMarginConverter Instance = new();
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not
            [
                FloatingHintHorizontalAlignment restingAlignmentOverride,
                FloatingHintHorizontalAlignment floatingAlignment,
                HorizontalAlignment restingAlignment,
                double desiredWidth,
                double availableWidth,
                double scale,
                double lower,
                double upper
            ])
        {
            return Transform.Identity;
        }

        double scaleMultiplier = upper + (lower - upper) * scale;

        HorizontalAlignment restAlignment = restingAlignmentOverride switch
        {
            FloatingHintHorizontalAlignment.Inherit => restingAlignment,
            FloatingHintHorizontalAlignment.Left => HorizontalAlignment.Left,
            FloatingHintHorizontalAlignment.Center => HorizontalAlignment.Center,
            FloatingHintHorizontalAlignment.Right => HorizontalAlignment.Right,
            FloatingHintHorizontalAlignment.Stretch => HorizontalAlignment.Stretch,
            _ => throw new ArgumentOutOfRangeException(),
        };

        HorizontalAlignment floatAlignment = restAlignment;
        if (scale != 0)
        {
            floatAlignment = floatingAlignment switch
            {
                FloatingHintHorizontalAlignment.Inherit => restingAlignment,
                FloatingHintHorizontalAlignment.Left => HorizontalAlignment.Left,
                FloatingHintHorizontalAlignment.Center => HorizontalAlignment.Center,
                FloatingHintHorizontalAlignment.Right => HorizontalAlignment.Right,
                FloatingHintHorizontalAlignment.Stretch => HorizontalAlignment.Stretch,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
        double leftThickness = floatAlignment switch
        {
            HorizontalAlignment.Right => FloatRight(),
            HorizontalAlignment.Center => FloatCenter(),
            _ => FloatLeft(),
        };

        return new Thickness(Math.Round(leftThickness), 0, 0, 0);

        double FloatLeft()
        {
            if (restAlignment == HorizontalAlignment.Center)
            {
                // Animate from center to left
                double offset = Math.Max(0, (availableWidth - desiredWidth) / 2);
                return offset - offset * scale;
            }
            if (restAlignment == HorizontalAlignment.Right)
            {
                // Animate from right to left
                double offset = Math.Max(0, availableWidth - desiredWidth);
                return offset - offset * scale;
            }
            return 0;
        }

        double FloatCenter()
        {
            if (restAlignment == HorizontalAlignment.Left || restAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to center
                double offset = Math.Max(0, (availableWidth - desiredWidth * scaleMultiplier) / 2);
                return offset * scale;
            }
            if (restAlignment == HorizontalAlignment.Right)
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
            if (restAlignment == HorizontalAlignment.Left || restAlignment == HorizontalAlignment.Stretch)
            {
                // Animate from left to right
                double offset = Math.Max(0, availableWidth - desiredWidth * scaleMultiplier);
                return offset * scale;
            }
            if (restAlignment == HorizontalAlignment.Center)
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
