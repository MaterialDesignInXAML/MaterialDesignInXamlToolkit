using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignColors.WpfExample.ProvingGroundStuff
{
    namespace MaterialDesignColors.WpfExample.ProvingGroundStuff.CicularProgressBar
    {

        public class StartPointConverter : IValueConverter
        {
            [Obsolete]
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is double && ((double) value > 0.0))
                {
                    return new Point((double)value / 2, 0);
                }

                return new Point();
            }

            [Obsolete]
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return Binding.DoNothing;
            }

        }        

        public class ArcSizeConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is double && ((double)value > 0.0))
                {
                    return new Size((double)value / 2, (double)value / 2);
                }

                return new Point();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return Binding.DoNothing;
            }
        }

        public class ArcEndPointConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var actualWidth = (double) values[0];
                var value = (double) values[1];
                var minimum = (double) values[2];
                var maximum = (double) values[3];                

                var percent = maximum <= minimum ? 1.0 : (value - minimum)/(maximum - minimum);
                var degrees = 360*percent;
                var radians = degrees*(Math.PI/180);

                var centre = new Point(actualWidth/2, actualWidth/2);
                var hypotenuseRadius = (actualWidth/2);

                var adjacent = Math.Cos(radians)*hypotenuseRadius;
                var opposite = Math.Sin(radians)*hypotenuseRadius;

                return new Point(centre.X + opposite, centre.Y - adjacent);
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class LargeArcConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var value = (double)values[0];
                var minimum = (double)values[1];
                var maximum = (double)values[2];

                var percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);

                return percent > 0.5;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class RotateTransformConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                var value = (double)values[0];
                var minimum = (double)values[1];
                var maximum = (double)values[2];

                var percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);

                return 360*percent;
            }

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class RotateTransformCentreConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                //value == actual width
                return (double) value/2;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return Binding.DoNothing;
            }
        }
    }
}
