using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }

        public T TrueValue { get; set; }
        public T FalseValue { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue && boolValue ? TrueValue : FalseValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T tValue && EqualityComparer<T>.Default.Equals(tValue, TrueValue);
        }
    }
}
