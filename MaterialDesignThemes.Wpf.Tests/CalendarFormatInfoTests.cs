using System.Globalization;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class CalendarFormatInfoTests
    {
        [StaTheory]
        [InlineData("en-US", "MMMM yyyy", "yyyy", "ddd,", "MMM d")]
#if NET5_0_OR_GREATER
        [InlineData("fr-CA", "MMMM yyyy", "yyyy", "ddd", "d MMM")]
        [InlineData("ja-JP", "yyyy年M月", "yyyy年", "M月d日", "dddd")]
#else
        [InlineData("fr-CA", "MMMM, yyyy", "yyyy", "ddd", "d MMM")]
        [InlineData("ja-JP", "yyyy'年'M'月'", "yyyy年", "M月d日", "dddd")]
#endif
        public void TestFromCultureInfo(string cultureName, string yearMonth, string componentThree, string componentTwo, string componentOne)
        {
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));

            Assert.Equal(yearMonth, result.YearMonthPattern);
            Assert.Equal(componentOne, result.ComponentOnePattern);
            Assert.Equal(componentTwo, result.ComponentTwoPattern);
            Assert.Equal(componentThree, result.ComponentThreePattern);
        }

        [Theory]
        [InlineData("", "ddd", "", true)]
        [InlineData("ddd", "ddd", "", true)]
        [InlineData("dddd", "ddd", "", true)]
        [InlineData("dddd ", "ddd", "", true)]
        [InlineData("dddd,", "ddd", ",", true)]
        [InlineData("dddd ,", "ddd", ",", true)]
        [InlineData("dddd','", "ddd", ",", true)]
        [InlineData("dddd' , '", "ddd", ",", true)]
        [InlineData("ddddd", "ddd", "", true)]
        [InlineData("ddddd,", "ddd", "", true)]
        [InlineData("Xddd", "ddd", "", false)]
        [InlineData("Xdddd", "ddd", "", false)]
        [InlineData("X ddd", "ddd", "", false)]
        [InlineData("X dddd", "ddd", "", false)]
        [InlineData("X, ddd", "ddd", ",", false)]
        [InlineData("X,ddd", "ddd", ",", false)]
        [InlineData("X','ddd", "ddd", ",", false)]
        [InlineData("X' , 'ddd", "ddd", ",", false)]
        [InlineData("Xddddd", "ddd", "", false)]
        [InlineData("X,ddddd", "ddd", "", false)]
        public void CanParseDayOfWeek(string s, string pattern, string separator, bool isFirst)
        {
            var result = CalendarFormatInfo.DayOfWeekStyle.Parse(s);

            Assert.Equal(pattern, result.Pattern);
            Assert.Equal(separator, result.Separator);
            Assert.Equal(isFirst, result.IsFirst);
        }

        [StaFact]
        public void SettingYearPattern()
        {
            const string cultureName = "en-001";
            CalendarFormatInfo.SetYearPattern(cultureName, "A");
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            Assert.Equal("A", result.ComponentThreePattern);

            CalendarFormatInfo.SetYearPattern(cultureName, null);
            result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            Assert.Equal("yyyy", result.ComponentThreePattern);
        }

        [StaFact]
        public void SettingYearPatternOfMultipleCultures()
        {
            string[] cultureNames = { "en-001", "en-150" };
            CalendarFormatInfo.SetYearPattern(cultureNames, "B");
            foreach (var cultureName in cultureNames)
            {
                var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
                Assert.Equal("B", result.ComponentThreePattern);
            }
        }

        [StaFact]
        public void SettingDayOfWeekStyle()
        {
            const string cultureName = "en-001";
            CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            Assert.Equal("d MMM", result.ComponentOnePattern);
            Assert.Equal("Z@", result.ComponentTwoPattern);

            CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Y", "@", false));
            result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            Assert.Equal("Y", result.ComponentOnePattern);
            Assert.Equal("d MMM@", result.ComponentTwoPattern);
        }

        [StaFact]
        public void SettingDayOfWeekStyleOfMultipleCultures()
        {
            string[] cultureNames = { "en-001", "en-150" };
            CalendarFormatInfo.SetDayOfWeekStyle(cultureNames, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
            foreach (var cultureName in cultureNames)
            {
                var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
                Assert.Equal("d MMM", result.ComponentOnePattern);
                Assert.Equal("Z@", result.ComponentTwoPattern);
            }
        }

        [StaFact]
        public void ResettingDayOfWeekStyle()
        {
            const string cultureName = "en-001";
            CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
            CalendarFormatInfo.ResetDayOfWeekStyle(cultureName);
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            Assert.Equal("d MMM", result.ComponentOnePattern);
            Assert.Equal("ddd,", result.ComponentTwoPattern);
        }

        [StaFact]
        public void ResettingDayOfWeekStyleOfMultipleCultures()
        {
            string[] cultureNames = { "en-001", "en-150" };
            CalendarFormatInfo.SetDayOfWeekStyle(cultureNames, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
            CalendarFormatInfo.ResetDayOfWeekStyle(cultureNames);
            foreach (var cultureName in cultureNames)
            {
                var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
                Assert.Equal("d MMM", result.ComponentOnePattern);
                Assert.Equal("ddd,", result.ComponentTwoPattern);
            }
        }
    }
}
