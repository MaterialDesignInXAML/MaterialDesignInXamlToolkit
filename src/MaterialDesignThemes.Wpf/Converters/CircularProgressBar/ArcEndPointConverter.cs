using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.CircularProgressBar;

public class ArcEndPointConverter : IMultiValueConverter
{
    public static readonly ArcEndPointConverter Instance = new();

    /// <summary>
    /// CircularProgressBar draws two arcs to support a full circle at 100 %.
    /// With one arc at 100 % the start point is identical the end point, so nothing is drawn.
    /// Midpoint at half of current percentage is the endpoint of the first arc
    /// and the start point of the second arc.
    /// </summary>
    public const string ParameterMidPoint = "MidPoint";

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        double actualWidth = values[0].ExtractDouble();
        double value = values[1].ExtractDouble();
        double minimum = values[2].ExtractDouble();
        double maximum = values[3].ExtractDouble();

        if (new[] { actualWidth, value, minimum, maximum }.AnyNan())
            return Binding.DoNothing;

        if (values.Length == 5)
        {
            double fullIndeterminateScaling = values[4].ExtractDouble();
            if (!double.IsNaN(fullIndeterminateScaling) && fullIndeterminateScaling > 0.0)
            {
                value = (maximum - minimum) * fullIndeterminateScaling;
            }
        }

        double percent = maximum <= minimum ? 1.0 : (value - minimum) / (maximum - minimum);
        if (Equals(parameter, ParameterMidPoint))
            percent /= 2;

        double degrees = 360 * percent;
        double radians = degrees * (Math.PI / 180);

        var centre = new Point(actualWidth / 2, actualWidth / 2);
        double hypotenuseRadius = (actualWidth / 2);

        double adjacent = Math.Cos(radians) * hypotenuseRadius;
        double opposite = Math.Sin(radians) * hypotenuseRadius;

        return new Point(centre.X + opposite, centre.Y - adjacent);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
