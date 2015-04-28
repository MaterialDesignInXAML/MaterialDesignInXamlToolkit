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
		private readonly ClockDisplayMode _displayMode;

		public ClockItemIsCheckedConverter(Func<DateTime> currentTimeGetter, ClockDisplayMode displayMode)
		{
			if (currentTimeGetter == null) throw new ArgumentNullException("currentTimeGetter");

			_currentTimeGetter = currentTimeGetter;
			_displayMode = displayMode;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var dateTime = (DateTime) value;
			var i = (int) parameter;
			return (_displayMode == ClockDisplayMode.Hours ? MassageHour(dateTime.Hour) : MassageMinute(dateTime.Minute)) == i;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((bool)value != true) return Binding.DoNothing;

			var currentTime = _currentTimeGetter();

			return new DateTime(
				currentTime.Year, currentTime.Month, currentTime.Day, 
				(_displayMode == ClockDisplayMode.Hours) ? ReverseMassageHour((int)parameter, currentTime) : currentTime.Hour,
				(_displayMode == ClockDisplayMode.Minutes) ? ReverseMassageMinute((int)parameter) : currentTime.Minute, 
				currentTime.Second);
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

		private static int ReverseMassageHour(int val, DateTime currentTime)
		{
			return currentTime.Hour < 12 
				? (val == 12 ? 0 : val)
				: (val == 12 ? 12 : val + 12);
		}

		private static int ReverseMassageMinute(int val)
		{
			return val == 60 ? 0 : val;
		}
	}
}
