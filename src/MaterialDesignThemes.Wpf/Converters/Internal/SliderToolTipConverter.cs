using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class SliderToolTipConverter : IMultiValueConverter
{
    public static readonly SliderToolTipConverter Instance = new();
    public object? Convert(object?[]? values, Type? targetType, object? parameter, CultureInfo? culture)
    {
        if (values?.Length >= 2 && values[1] is string format && !string.IsNullOrEmpty(format))
        {
            try
            {
                return string.Format(culture, format, values[0]);
            }
            catch (FormatException) { }
        }
        if (values?.Length >= 1 && targetType is not null)
        {
            return System.Convert.ChangeType(values[0], targetType, culture);
        }
        return DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
