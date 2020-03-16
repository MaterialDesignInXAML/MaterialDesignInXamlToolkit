using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class DrawerOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = value as double? ?? 0;
            if (double.IsInfinity(d) || double.IsNaN(d)) d = 0;

            var dock = (parameter is Dock) ? (Dock)parameter : Dock.Left;
            switch (dock)
            {
                case Dock.Top:
                    return new Thickness(0, 0 - d, 0, 0);
                case Dock.Bottom:
                    return new Thickness(0, 0, 0, 0 - d);
                case Dock.Right:
                    return new Thickness(0, 0, 0 - d, 0);
                case Dock.Left:
                default:
                    return new Thickness(0 - d, 0, 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
