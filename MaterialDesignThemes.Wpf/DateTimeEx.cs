using System;
using System.Globalization;
using System.Linq;

namespace MaterialDesignThemes.Wpf
{
    internal static class DateTimeEx
    {
        internal static DateTimeFormatInfo GetDateFormat(this CultureInfo culture)
        {
            if (culture is null) throw new ArgumentNullException(nameof(culture));

            if (culture.Calendar is GregorianCalendar || culture.Calendar is PersianCalendar)
            {
                return culture.DateTimeFormat;
            }

            GregorianCalendar? foundCal = null;
            foreach (var cal in culture.OptionalCalendars.OfType<GregorianCalendar>())
            {
                // Return the first Gregorian calendar with CalendarType == Localized 
                // Otherwise return the first Gregorian calendar
                if (foundCal is null)
                {
                    foundCal = cal;
                }

                if (cal.CalendarType != GregorianCalendarTypes.Localized) continue;

                foundCal = cal;
                break;
            }


            DateTimeFormatInfo? dtfi;
            if (foundCal is null)
            {
                // if there are no GregorianCalendars in the OptionalCalendars list, use the invariant dtfi 
                dtfi = ((CultureInfo)CultureInfo.InvariantCulture.Clone()).DateTimeFormat;
                dtfi.Calendar = new GregorianCalendar();
            }
            else
            {
                dtfi = ((CultureInfo)culture.Clone()).DateTimeFormat;
                dtfi.Calendar = foundCal;
            }

            return dtfi;
        }
    }
}
