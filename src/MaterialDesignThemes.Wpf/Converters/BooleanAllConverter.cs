using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class BooleanAllConverter : IMultiValueConverter
{
    public static readonly BooleanAllConverter Instance = new();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        => values.OfType<bool>().All(b => b);

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
