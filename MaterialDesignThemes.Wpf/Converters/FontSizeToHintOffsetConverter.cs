using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class FontSizeToHintOffsetConverter : IValueConverter, IMultiValueConverter
    {
        private static Point ToHintOffset(object value)
        {
            var fontSize = System.Convert.ToDouble(value);
            var hintOffset = fontSize / 2 + 12;
            return new Point(0, -hintOffset);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ToHintOffset(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var hintOffset = values.Length > 0 ? ToHintOffset(values[0]) : new Point(0, 0);
            if (values.Length >= 2 && values[1] is Point offset)
            {
                if (!(parameter is Point defaultOffset))
                    defaultOffset = new Point(0, 0);
                hintOffset += offset - defaultOffset;
            }
            return hintOffset;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
