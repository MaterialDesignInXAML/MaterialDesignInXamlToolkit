using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
	internal class ClockItemIsCheckedConverter : IValueConverter
	{
		private readonly Func<DateTime> _currentTimeGetter;
		private readonly ClockDisplay _display;

		public ClockItemIsCheckedConverter(Func<DateTime> currentTimeGetter, ClockDisplay display)
		{
			if (currentTimeGetter == null) throw new ArgumentNullException(nameof(currentTimeGetter));

			_currentTimeGetter = currentTimeGetter;
			_display = display;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var dateTime = (DateTime) value;
			var i = (int) parameter;
			return (_display == ClockDisplay.Hours ? MassageHour(dateTime.Hour) : MassageMinute(dateTime.Minute)) == i;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value != true) return Binding.DoNothing;

			var currentTimeGetter = _currentTimeGetter();

			return new DateTime(
				currentTimeGetter.Year, currentTimeGetter.Month, currentTimeGetter.Day, 
				(_display == ClockDisplay.Hours) ? (int)parameter : currentTimeGetter.Hour,
				(_display == ClockDisplay.Minutes) ? ReverseMassageMinute((int)parameter) : currentTimeGetter.Minute, 
				currentTimeGetter.Second);
		}

		private static int MassageHour(int val)
		{
			if (val == 0) return 12;
			if (val > 12) return val - 12;
			return val;
		}

		private static int MassageMinute(int val)
		{
			return val == 0 ? 60 : val;
		}

		private static int ReverseMassageMinute(int val)
		{
			return val == 60 ? 0 : val;
		}
	}
}
