using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class NullableToVisibilityConverter : IValueConverter
{
    public static readonly NullableToVisibilityConverter CollapsedInstance = new() { NullValue = Visibility.Collapsed, NotNullValue = Visibility.Visible };
    public static readonly NullableToVisibilityConverter NotCollapsedInstance = new() { NullValue = Visibility.Visible, NotNullValue = Visibility.Collapsed };

    public static readonly NullableToVisibilityConverter HiddenInstance = new() { NullValue = Visibility.Hidden, NotNullValue = Visibility.Visible };
    public static readonly NullableToVisibilityConverter NotHiddenInstance = new() { NullValue = Visibility.Visible, NotNullValue = Visibility.Hidden };

    public Visibility NullValue { get; set; } = Visibility.Collapsed;
    public Visibility NotNullValue { get; set; } = Visibility.Visible;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value == null ? NullValue : NotNullValue;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
