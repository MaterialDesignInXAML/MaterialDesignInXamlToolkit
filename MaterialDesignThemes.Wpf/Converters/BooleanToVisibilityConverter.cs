using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is Boolean)
                return (Boolean)value ? TrueValue : FalseValue;

            if (parameter is Boolean)
                return (Boolean)parameter ? TrueValue : FalseValue;

            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;

            if (Equals(value, FalseValue))
                return false;

            if (Equals(parameter, TrueValue))
                return true;

            if (Equals(parameter, FalseValue))
                return false;

            return null;
        }
    }
}
