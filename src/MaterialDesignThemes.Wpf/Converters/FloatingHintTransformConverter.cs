using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

internal class FloatingHintTransformConverter : IMultiValueConverter
{
    public bool ApplyScaleTransform { get; set; } = true;
    public bool ApplyTranslateTransform { get; set; } = true;

    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values?.Length != 5
            || values.Any(v => v == null)
            || !double.TryParse(values[0]!.ToString(), out double scale)
            || !double.TryParse(values[1]!.ToString(), out double lower)
            || !double.TryParse(values[2]!.ToString(), out double upper)
            || values[3] is not Point floatingOffset
            || !double.TryParse(values[4]!.ToString(), out double initialVerticalOffset))
        {
            return Transform.Identity;
        }

        double result = upper + (lower - upper) * scale;

        var transformGroup = new TransformGroup();
        if (ApplyScaleTransform)
        {
            transformGroup.Children.Add(new ScaleTransform
            {
                ScaleX = result,
                ScaleY = result
            });
        }
        if (ApplyTranslateTransform)
        {
            /* As a consequence of Math.Min() which is used below to ensure the initial offset is respected (in filled style)
               the SmartHint will not be able to "float downwards". I believe this is acceptable though.
             */
            transformGroup.Children.Add(new TranslateTransform
            {
                X = scale * floatingOffset.X,
                Y = Math.Min(initialVerticalOffset, scale * floatingOffset.Y)
            });
        }
        return transformGroup;
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
