using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class DoubleToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (Dock)parameter switch
            {
                Dock.Left => new Thickness((double)value, 0, 0, 0),
                Dock.Top => new Thickness(0, (double)value, 0, 0),
                Dock.Right => new Thickness(0, 0, (double)value, 0),
                Dock.Bottom => new Thickness(0, 0, 0, (double)value),
                _ => Binding.DoNothing
            };

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}