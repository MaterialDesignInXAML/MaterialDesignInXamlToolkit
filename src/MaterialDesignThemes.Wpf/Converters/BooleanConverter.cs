using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class BooleanConverter<T>(T trueValue, T falseValue) : IValueConverter
{
    public T TrueValue { get; set; } = trueValue;
    public T FalseValue { get; set; } = falseValue;

    public virtual object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool boolValue && boolValue ? TrueValue : FalseValue;

    public virtual object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is T tValue && EqualityComparer<T>.Default.Equals(tValue, TrueValue);
}
