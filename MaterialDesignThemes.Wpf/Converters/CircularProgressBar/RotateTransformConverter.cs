using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.CircularProgressBar
{
    public class RotateTransformConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0].ExtractDouble();
            var minimum = values[1].ExtractDouble();
            var maximum = values[2].ExtractDouble();

            if (new[] { value, minimum, maximum }.AnyNan())
                return Binding.DoNothing;

            var percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);

            return 360*percent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}