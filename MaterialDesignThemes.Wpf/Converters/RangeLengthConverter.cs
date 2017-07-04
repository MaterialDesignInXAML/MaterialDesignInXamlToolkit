using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class RangeLengthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 4 || values.Any(v => v == null))
                return Binding.DoNothing;

            if (!double.TryParse(values[0].ToString(), out double min)
                || !double.TryParse(values[1].ToString(), out double max)
                || !double.TryParse(values[2].ToString(), out double value)
                || !double.TryParse(values[3].ToString(), out double containerLength))

                return Binding.DoNothing;

            var percent =  (value - min) / (max - min);
            var length = percent * containerLength;

            return length;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
