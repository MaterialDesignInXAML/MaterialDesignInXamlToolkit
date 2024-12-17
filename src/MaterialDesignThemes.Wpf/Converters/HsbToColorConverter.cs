using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Converters;

public class HsbToColorConverter : IValueConverter, IMultiValueConverter
{
    public static readonly HsbToColorConverter Instance = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Hsb hsb) return Binding.DoNothing;

        return new SolidColorBrush(hsb.ToColor());        
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush brush) return Binding.DoNothing;

        return brush.Color.ToHsb();        
    }

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is not [double hue, double saturation, double brightness]) return Binding.DoNothing;

        return new SolidColorBrush(new Hsb(hue, saturation, brightness).ToColor());
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
