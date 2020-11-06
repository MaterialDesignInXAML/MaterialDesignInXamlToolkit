using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    /// <summary>
    /// Help us format the content of a header button in a calendar.
    /// </summary>
    /// <remarks>
    /// Expected items, in the following order:
    ///     1) DateTime Calendar.DisplayDate
    ///     2) DateTime? Calendar.SelectedDate
    /// </remarks>
    public class CalendarDateCoalesceConverter : IMultiValueConverter
    {
        public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values?.Length != 2) throw new ArgumentException("Must specify two values", "values");
            if (values[0] is not DateTime) throw new ArgumentException($"First value should be a {nameof(DateTime)}", "values");
            if (values[1] is not null && values[1] is not DateTime) throw new ArgumentException($"Second value should be null or a {nameof(DateTime)}", "values");

            var selectedDate = (DateTime?)values[1];

            return selectedDate ?? values[0];
        }

        public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => null;
    }
}