using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ClockItemIsCheckedConverter : IValueConverter
    {
        private readonly Func<DateTime> _currentTimeGetter;
        private readonly ClockDisplayMode _displayMode;
        private readonly bool _is24Hours;

        public ClockItemIsCheckedConverter(Func<DateTime> currentTimeGetter, ClockDisplayMode displayMode, bool is24Hours)
        {
            _currentTimeGetter = currentTimeGetter ?? throw new ArgumentNullException(nameof(currentTimeGetter));
            _displayMode = displayMode;
            _is24Hours = is24Hours;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime)value;
            var i = (int)parameter;

            int converted = 0;
            if (_displayMode == ClockDisplayMode.Hours)
                converted = MassageHour(dateTime.Hour, _is24Hours);
            else if (_displayMode == ClockDisplayMode.Minutes)
                converted = MassageMinuteSecond(dateTime.Minute);
            else
                converted = MassageMinuteSecond(dateTime.Second);
            return converted == i;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value != true) return Binding.DoNothing;

            var currentTime = _currentTimeGetter();

            return new DateTime(
                currentTime.Year, currentTime.Month, currentTime.Day,
                (_displayMode == ClockDisplayMode.Hours) ? ReverseMassageHour((int)parameter, currentTime, _is24Hours) : currentTime.Hour,
                (_displayMode == ClockDisplayMode.Minutes) ? ReverseMassageMinuteSecond((int)parameter) : currentTime.Minute,
                (_displayMode == ClockDisplayMode.Seconds) ? ReverseMassageMinuteSecond((int)parameter) : currentTime.Second);
        }

        private static int MassageHour(int val, bool is24Hours)
        {
            if (is24Hours)
            {
                return val == 0 ? 24 : val;
            }

            if (val == 0) return 12;
            if (val > 12) return val - 12;
            return val;
        }

        private static int MassageMinuteSecond(int val)
        {
            return val == 0 ? 60 : val;
        }

        private static int ReverseMassageHour(int val, DateTime currentTime, bool is24Hours)
        {
            if (is24Hours)
            {
                return val == 24 ? 0 : val;
            }

            return currentTime.Hour < 12
                ? (val == 12 ? 0 : val)
                : (val == 12 ? 12 : val + 12);
        }

        private static int ReverseMassageMinuteSecond(int val)
        {
            return val == 60 ? 0 : val;
        }
    }
}
