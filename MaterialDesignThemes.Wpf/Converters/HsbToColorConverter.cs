using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class HsbToColorConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Hsb hsb) return new SolidColorBrush(hsb.ToColor());
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush) return brush.Color.ToHsb();
            return Binding.DoNothing;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var h = (double)values[0];
            var s = (double)values[1];
            var b = (double)values[2];

            return new SolidColorBrush(new Hsb(h, s, b).ToColor());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
