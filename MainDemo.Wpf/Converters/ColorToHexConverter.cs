using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignDemo.Converters
{
    public class ColorToHexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            string lowerHexString(int i) => i.ToString("X2").ToLower();
            var color = (Color)value;
            var hex = lowerHexString(color.R) +
                      lowerHexString(color.G) +
                      lowerHexString(color.B);
            return "#" + hex;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string hexString = value as string;

            if (!hexString.StartsWith("#"))
            {
                hexString = hexString.Insert(0, "#");
            }

            return ColorConverter.ConvertFromString(hexString);
        }
    }
}
