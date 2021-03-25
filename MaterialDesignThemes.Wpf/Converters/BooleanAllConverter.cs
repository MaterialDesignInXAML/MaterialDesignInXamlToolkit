using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class BooleanAllConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => values.OfType<bool>().All(b => b);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
