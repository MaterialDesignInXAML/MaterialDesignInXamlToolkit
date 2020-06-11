using System;
using System.Collections.Generic;
using System.Globalization;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Provides culture-specific information about the format of calendar.
    /// </summary>
    public class CalendarFormatInfo
    {
        /// <summary>
        /// Gets the custom format string for a year and month value.
        /// </summary>
        public string YearMonthPattern { get; }

        /// <summary>
        /// Gets the custom format string for a component one value.
        /// </summary>
        public string ComponentOnePattern { get; }

        /// <summary>
        /// Gets the custom format string for a component two value.
        /// </summary>
        public string ComponentTwoPattern { get; }

        /// <summary>
        /// Gets the custom format string for a component three value.
        /// </summary>
        public string ComponentThreePattern { get; }

        private const string ShortDayOfWeek = "ddd";
        private const string LongDayOfWeek = "dddd";

        private static readonly Dictionary<string, CalendarFormatInfo> _formatInfoCache = new Dictionary<string, CalendarFormatInfo>();
        private static readonly Dictionary<string, string> _cultureYearPatterns = new Dictionary<string, string>();
        private static readonly Dictionary<string, DayOfWeekStyle> _cultureDayOfWeekStyles = new Dictionary<string, DayOfWeekStyle>();

        private static readonly string[] JapaneseCultureNames = { "ja", "ja-JP" };
        private static readonly string[] ZhongwenCultureNames = { "zh", "zh-CN", "zh-Hans", "zh-Hans-HK", "zh-Hans-MO", "zh-Hant", "zh-HK", "zh-MO", "zh-SG", "zh-TW" };
        private static readonly string[] KoreanCultureNames = { "ko", "ko-KR", "ko-KP" };

        private const string CJKYearSuffix = "\u5e74";
        private const string KoreanYearSuffix = "\ub144";

        static CalendarFormatInfo()
        {
            SetYearPattern(JapaneseCultureNames, "yyyy" + CJKYearSuffix);
            SetYearPattern(ZhongwenCultureNames, "yyyy" + CJKYearSuffix);
            SetYearPattern(KoreanCultureNames, "yyyy" + KoreanYearSuffix);

            var dayOfWeekStyle = new DayOfWeekStyle(LongDayOfWeek, string.Empty, false);
            SetDayOfWeekStyle(JapaneseCultureNames, dayOfWeekStyle);
            SetDayOfWeekStyle(ZhongwenCultureNames, dayOfWeekStyle);
        }

        /// <summary>
        /// Sets the culture-specific custom format string for a year value. 
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to set the <paramref name="yearPattern"/> for.</param>
        /// <param name="yearPattern">The custom format string for a year value. If null, culture-specific custom format string for a year value is removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void SetYearPattern(string[] cultureNames, string yearPattern)
        {
            if (cultureNames == null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
                SetYearPattern(cultureName, yearPattern);
        }

        /// <summary>
        /// Sets the culture-specific custom format string for a year value.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to set the <paramref name="yearPattern"/> for.</param>
        /// <param name="yearPattern">The custom format string for a year value. If null, culture-specific custom format string for a year value is removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void SetYearPattern(string cultureName, string yearPattern)
        {
            if (cultureName == null)
                throw new ArgumentNullException(nameof(cultureName));

            if (yearPattern != null)
                _cultureYearPatterns[cultureName] = yearPattern;
            else
                _cultureYearPatterns.Remove(cultureName);
            DiscardFormatInfoCache(cultureName);
        }

        /// <summary>
        /// Sets the culture-specific day of week style.
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to set the <paramref name="dayOfWeekStyle"/> for.</param>
        /// <param name="dayOfWeekStyle">A <see cref="DayOfWeekStyle"/> to be set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void SetDayOfWeekStyle(string[] cultureNames, DayOfWeekStyle dayOfWeekStyle)
        {
            if (cultureNames == null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
                SetDayOfWeekStyle(cultureName, dayOfWeekStyle);
        }

        /// <summary>
        /// Sets the culture-specific day of week style.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to set the <paramref name="dayOfWeekStyle"/> for.</param>
        /// <param name="dayOfWeekStyle">A <see cref="DayOfWeekStyle"/> to be set.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void SetDayOfWeekStyle(string cultureName, DayOfWeekStyle dayOfWeekStyle)
        {
            if (cultureName == null)
                throw new ArgumentNullException(nameof(cultureName));

            _cultureDayOfWeekStyles[cultureName] = dayOfWeekStyle;
            DiscardFormatInfoCache(cultureName);
        }

        /// <summary>
        /// Resets the culture-specific day of week style to default value.
        /// </summary>
        /// <param name="cultureNames">An array of string that specify the name of culture to reset.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureNames"/> is null.</exception>
        public static void ResetDayOfWeekStyle(string[] cultureNames)
        {
            if (cultureNames == null)
                throw new ArgumentNullException(nameof(cultureNames));

            foreach (var cultureName in cultureNames)
                ResetDayOfWeekStyle(cultureName);
        }

        /// <summary>
        /// Resets the culture-specific day of week style to default value.
        /// </summary>
        /// <param name="cultureName">A string that specify the name of culture to reset.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cultureName"/> is null.</exception>
        public static void ResetDayOfWeekStyle(string cultureName)
        {
            if (cultureName == null)
                throw new ArgumentNullException(nameof(cultureName));

            if (_cultureDayOfWeekStyles.Remove(cultureName))
                DiscardFormatInfoCache(cultureName);
        }

        private static void DiscardFormatInfoCache(string cultureName)
        {
            _formatInfoCache.Remove(cultureName);
        }

        private CalendarFormatInfo(string yearMonthPattern, string componentOnePattern, string componentTwoPattern, string componentThreePattern)
        {
            YearMonthPattern = yearMonthPattern;
            ComponentOnePattern = componentOnePattern;
            ComponentTwoPattern = componentTwoPattern;
            ComponentThreePattern = componentThreePattern;
        }

        /// <summary>
        /// Creates a <see cref="CalendarFormatInfo"/> from the <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="cultureInfo">A <see cref="CultureInfo"/> that specifies the culture to get the date format.</param>
        /// <returns>The <see cref="CalendarFormatInfo"/> object that this method creates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="cultureInfo"/> is null.</exception>
        public static CalendarFormatInfo FromCultureInfo(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
                throw new ArgumentNullException(nameof(cultureInfo));

            CalendarFormatInfo calendarInfo;
            if (_formatInfoCache.TryGetValue(cultureInfo.Name, out calendarInfo))
                return calendarInfo;

            var dateTimeFormat = cultureInfo.DateTimeFormat;

            string yearPattern;
            if (!_cultureYearPatterns.TryGetValue(cultureInfo.Name, out yearPattern))
                yearPattern = "yyyy";

            DayOfWeekStyle dayOfWeekStyle;
            if (!_cultureDayOfWeekStyles.TryGetValue(cultureInfo.Name, out dayOfWeekStyle))
                dayOfWeekStyle = DayOfWeekStyle.Parse(dateTimeFormat.LongDatePattern);

            var monthDayPattern = dateTimeFormat.MonthDayPattern.Replace("MMMM", "MMM");
            if (dayOfWeekStyle.IsFirst)
                calendarInfo = new CalendarFormatInfo(dateTimeFormat.YearMonthPattern,
                                                       monthDayPattern,
                                                       dayOfWeekStyle.Pattern + dayOfWeekStyle.Separator,
                                                       yearPattern);
            else
                calendarInfo = new CalendarFormatInfo(dateTimeFormat.YearMonthPattern,
                                                       dayOfWeekStyle.Pattern,
                                                       monthDayPattern + dayOfWeekStyle.Separator,
                                                       yearPattern);

            _formatInfoCache[cultureInfo.Name] = calendarInfo;
            return calendarInfo;
        }

        /// <summary>
        /// Represents a day of week style.
        /// </summary>
        public struct DayOfWeekStyle
        {
            /// <summary>
            /// Gets the custom format string for a day of week value.
            /// </summary>
            public string Pattern { get; }

            /// <summary>
            /// Gets the string that separates MonthDay and DayOfWeek.
            /// </summary>
            public string Separator { get; }

            /// <summary>
            /// Gets a value indicating whether DayOfWeek is before MonthDay.
            /// </summary>
            public bool IsFirst { get; }

            private const char EthiopicWordspace = '\u1361';
            private const char EthiopicComma = '\u1363';
            private const char EthiopicColon = '\u1365';
            private const char ArabicComma = '\u060c';

            private static readonly string SeparatorChars =
              new string(new char[] { ',', ArabicComma, EthiopicWordspace, EthiopicComma, EthiopicColon });

            /// <summary>
            /// Initializes a new instance of the <see cref="DayOfWeekStyle"/> struct.
            /// </summary>
            /// <param name="pattern">A custom format string for a day of week value.</param>
            /// <param name="separator">A string that separates MonthDay and DayOfWeek.</param>
            /// <param name="isFirst">A value indicating whether DayOfWeek is before MonthDay.</param>
            public DayOfWeekStyle(string pattern, string separator, bool isFirst)
            {
                this.Pattern = pattern ?? string.Empty;
                this.Separator = separator ?? string.Empty;
                this.IsFirst = isFirst;
            }

            /// <summary>
            /// Extracts the <see cref="DayOfWeekStyle"/> from the date format string.
            /// </summary>
            /// <param name="s">the date format string.</param>
            /// <returns>The <see cref="DayOfWeekStyle"/> struct.</returns>
            /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
            public static DayOfWeekStyle Parse(string s)
            {
                if (s == null)
                    throw new ArgumentNullException(nameof(s));

                if (s.StartsWith(ShortDayOfWeek, StringComparison.Ordinal))
                {
                    var index = 3;
                    if (index < s.Length && s[index] == 'd')
                        index++;
                    for (; index < s.Length && IsSpace(s[index]); index++)
                        ;
                    var separator = index < s.Length && IsSeparator(s[index]) ? s[index].ToString() : string.Empty;
                    return new DayOfWeekStyle(ShortDayOfWeek, separator, true);
                }
                else if (s.EndsWith(ShortDayOfWeek, StringComparison.Ordinal))
                {
                    var index = s.Length - 4;
                    if (index >= 0 && s[index] == 'd')
                        index--;
                    for (; index >= 0 && IsSpace(s[index]); index--)
                        ;
                    var separator = index >= 0 && IsSeparator(s[index]) ? s[index].ToString() : string.Empty;
                    return new DayOfWeekStyle(ShortDayOfWeek, separator, false);
                }
                return new DayOfWeekStyle(ShortDayOfWeek, string.Empty, true);
            }

            private static bool IsSpace(char c) => c == ' ' || c == '\'';

            private static bool IsSeparator(char c) => SeparatorChars.IndexOf(c) >= 0;
        }
    }
}
