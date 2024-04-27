using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

internal class DoubleToThicknessConverter : IValueConverter
{
    public double InitialOffset { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => (Dock)parameter switch
        {
            Dock.Left => new Thickness(InitialOffset + (double)value, 0, 0, 0),
            Dock.Top => new Thickness(0, InitialOffset + (double)value, 0, 0),
            Dock.Right => new Thickness(0, 0, InitialOffset + (double)value, 0),
            Dock.Bottom => new Thickness(0, 0, 0, InitialOffset + (double)value),
            _ => Binding.DoNothing
        };

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
