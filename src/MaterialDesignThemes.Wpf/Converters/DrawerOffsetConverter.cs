using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class DrawerOffsetConverter : IValueConverter
{
    public static readonly DrawerOffsetConverter Instance = new();
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double d = value as double? ?? 0;
        if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

        Dock dock = (parameter is Dock) ? (Dock)parameter : Dock.Left;
        return dock switch
        {
            Dock.Top => new Thickness(0, 0 - d, 0, 0),
            Dock.Bottom => new Thickness(0, 0, 0, 0 - d),
            Dock.Right => new Thickness(0, 0, 0 - d, 0),
            _ => (object)new Thickness(0 - d, 0, 0, 0),
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
