using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class TextBoxHorizontalScrollBarWidthConverter : IMultiValueConverter
{
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [double contentHostWidth, Visibility verticalScrollBarVisibility])
        {
            return Math.Max(0, contentHostWidth - (verticalScrollBarVisibility == Visibility.Visible ? SystemParameters.VerticalScrollBarWidth : 0));
        }
        return double.NaN;
    }

    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
