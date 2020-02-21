using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class RemoveAlphaBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                Color background = Colors.White;
                if (parameter is Color color)
                {
                    background = color;
                }
                return new SolidColorBrush(RgbaToRgb(brush.Color, background));
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static Color RgbaToRgb(Color rgba, Color background)
        {
            double alpha = (double)rgba.A / byte.MaxValue;
            return Color.FromRgb(
                (byte)((1 - alpha) * background.R + alpha * rgba.R),
                (byte)((1 - alpha) * background.G + alpha * rgba.G),
                (byte)((1 - alpha) * background.B + alpha * rgba.B)
            );
        }
    }
}