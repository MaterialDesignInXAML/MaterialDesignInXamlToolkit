using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Converters;

public class IsDarkConverter : IValueConverter
{
    public static readonly IsDarkConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            SolidColorBrush brush => brush.Color,
            Color c => c,
            _ => (Color?)null
        } is Color color
            ? color.IsDarkColor()
            : Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
