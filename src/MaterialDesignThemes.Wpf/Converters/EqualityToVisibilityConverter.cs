using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class EqualityToVisibilityConverter : IValueConverter
{
    public Visibility EqualityValue {get; init; } = Visibility.Visible;
    public Visibility InequalityValue {get; init; } = Visibility.Collapsed;

    public static readonly EqualityToVisibilityConverter Instance = new();
    public static readonly EqualityToVisibilityConverter VisibleOnIneneqaulityInstance = new() { EqualityValue = Visibility.Collapsed, InequalityValue = Visibility.Visible };

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value.Equals(parameter)) return EqualityValue;

        return InequalityValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
