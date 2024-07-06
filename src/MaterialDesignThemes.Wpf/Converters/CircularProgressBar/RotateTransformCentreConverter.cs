using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.CircularProgressBar;

public class RotateTransformCentreConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>        
        (double)value / 2; //value == actual width

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
}
