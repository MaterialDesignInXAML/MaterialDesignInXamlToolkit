using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf.Converters
{
    public sealed class CalendarYearMonthConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            long ticks = long.MaxValue;
            foreach (var value in values)
            {
                if (value is DateTime dt)
                    ticks = dt.Ticks;
                else if (value is XmlLanguage language)
                    culture = language.GetSpecificCulture();
            }
            if (ticks == long.MaxValue)
                return null;

            return new DateTime(ticks).ToString(CalendarFormatInfo.FromCultureInfo(culture).YearMonthPattern, culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
