using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

public class FloatingHintTranslateTransformConverter : IMultiValueConverter
{
    public static readonly FloatingHintTranslateTransformConverter Instance = new();
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is not [double scale, double lower, double upper, SmartHint hint, Point floatingOffset, double yOffset, ..])
        {
            return Transform.Identity;
        }

        // Back-compatible behavior, fall back to using the non-nullable floatingOffset if it has a non-default value
        if (hint.FloatingTarget is null || floatingOffset != HintAssist.DefaultFloatingOffset)
        {
            /* As a consequence of Math.Min() which is used below to ensure the initial offset is respected (in filled style)
               the SmartHint will not be able to "float downwards". I believe this is acceptable though.
             */
            return new TranslateTransform
            {
                X = scale * floatingOffset.X,
                Y = Math.Min(hint.InitialVerticalOffset, scale * floatingOffset.Y)
            };
        }
        return new TranslateTransform
        {
            X = GetFloatingTargetHorizontalOffset() * scale,
            Y = GetFloatingTargetVerticalOffset() * scale
        };

        double GetFloatingTargetHorizontalOffset()
        {
            return hint.InitialHorizontalOffset + hint.HorizontalContentAlignment switch
            {
                HorizontalAlignment.Center => 0,
                HorizontalAlignment.Right => hint.FloatingMargin.Right,
                _ => -hint.FloatingMargin.Left,
            };
        }

        double GetFloatingTargetVerticalOffset()
        {
            double offset = yOffset;
            offset += hint.InitialVerticalOffset;
            offset -= hint.ActualHeight;

            double scalePercentage = upper + (lower - upper) * scale;
            offset += hint.FloatingAlignment switch
            {
                VerticalAlignment.Top => hint.ActualHeight - hint.ActualHeight * upper * scalePercentage,
                VerticalAlignment.Bottom => hint.ActualHeight * upper * (1 - scalePercentage),
                _ => hint.ActualHeight * upper * (1 - scalePercentage) / 2,
            };
            return offset;
        }
    }

    public object[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
