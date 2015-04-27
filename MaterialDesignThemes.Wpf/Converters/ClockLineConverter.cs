using System;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
	public class ClockLineConverter : IValueConverter
	{
		public ClockDisplayMode DisplayMode { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var time = (DateTime) value;
			return DisplayMode == ClockDisplayMode.Hours
				? (time.Hour > 13 ? time.Hour - 12 : time.Hour)*(360/12)
				: (time.Minute == 0 ? 60 : time.Minute)*(360/60);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}	
}