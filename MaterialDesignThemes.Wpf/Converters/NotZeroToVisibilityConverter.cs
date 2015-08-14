using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class NotZeroToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val;
            double.TryParse((value ?? "").ToString(), out val);

            return Math.Abs(val) > 0.0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}