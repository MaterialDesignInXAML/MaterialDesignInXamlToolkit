using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters;

// TODO: This converter can be deleted when the new SmartHint approach from TextBox style is applied throughout.
internal class FloatingHintOffsetCalculationConverter : IMultiValueConverter
{
    public object Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Length > 3 &&
            values[3] is Point floatingOffset &&
            IsType<Point>(targetType) &&
            floatingOffset != HintAssist.DefaultFloatingOffset)
        {
            return floatingOffset;
        }

        var fontFamily = (FontFamily)values[0]!;
        double fontSize = (double)values[1]!;
        double floatingScale = (double)values[2]!;

        double hintHeight = fontFamily.LineSpacing * fontSize;
        double floatingHintHeight = hintHeight * floatingScale;
        double offset = floatingHintHeight;

        // For the "outlined" styles, the hint should (by default) float to the outline; this needs a bit of calculation using additional input.
        if (values.Length == 7
            && values[4] is double parentActualHeight
            && values[5] is Thickness padding
            && values[6] is VerticalAlignment verticalContentAlignment)
        {
            switch (verticalContentAlignment)
            {
                case VerticalAlignment.Top:
                case VerticalAlignment.Stretch:
                    offset = (floatingHintHeight / 2) + padding.Top;
                    break;
                case VerticalAlignment.Center:
                    offset = (floatingHintHeight / 2) + (parentActualHeight - padding.Top) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    offset = (floatingHintHeight / 2) + parentActualHeight - padding.Top - padding.Bottom;
                    break;
            }
        }

        if (IsType<Point>(targetType))
        {
            return new Point(0, -offset);
        }

        if (IsType<Thickness>(targetType))
        {
            return new Thickness(0, offset, 0, 0);
        }

        throw new NotSupportedException(targetType.FullName);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotSupportedException();

    private bool IsType<T>(Type type) => type == typeof(T);
}
