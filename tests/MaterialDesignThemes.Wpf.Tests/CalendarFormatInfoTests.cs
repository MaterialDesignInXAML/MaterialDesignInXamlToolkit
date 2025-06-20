using System.Globalization;

namespace MaterialDesignThemes.Wpf.Tests;

[NotInParallel(nameof(CalendarFormatInfo))]
[TestExecutor<STAThreadExecutor>]
public class CalendarFormatInfoTests
{
    [Test]
    [Arguments("en-US", "MMMM yyyy", "yyyy", "ddd,", "MMM d")]
#if NET5_0_OR_GREATER
    [Arguments("fr-CA", "MMMM yyyy", "yyyy", "ddd", "d MMM")]
    [Arguments("ja-JP", "yyyy年M月", "yyyy年", "M月d日", "dddd")]
#else
    [Arguments("fr-CA", "MMMM, yyyy", "yyyy", "ddd", "d MMM")]
    [Arguments("ja-JP", "yyyy'年'M'月'", "yyyy年", "M月d日", "dddd")]
#endif
    public async Task TestFromCultureInfo(string cultureName, string yearMonth, string componentThree, string componentTwo, string componentOne)
    {
        var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));

        await Assert.That(result.YearMonthPattern).IsEqualTo(yearMonth);

        await Assert.That(result.ComponentOnePattern).IsEqualTo(componentOne);
        await Assert.That(result.ComponentTwoPattern).IsEqualTo(componentTwo);
        await Assert.That(result.ComponentThreePattern).IsEqualTo(componentThree);
    }

    [Test]
    [Arguments("", "ddd", "", true)]
    [Arguments("ddd", "ddd", "", true)]
    [Arguments("dddd", "ddd", "", true)]
    [Arguments("dddd ", "ddd", "", true)]
    [Arguments("dddd,", "ddd", ",", true)]
    [Arguments("dddd ,", "ddd", ",", true)]
    [Arguments("dddd','", "ddd", ",", true)]
    [Arguments("dddd' , '", "ddd", ",", true)]
    [Arguments("ddddd", "ddd", "", true)]
    [Arguments("ddddd,", "ddd", "", true)]
    [Arguments("Xddd", "ddd", "", false)]
    [Arguments("Xdddd", "ddd", "", false)]
    [Arguments("X ddd", "ddd", "", false)]
    [Arguments("X dddd", "ddd", "", false)]
    [Arguments("X, ddd", "ddd", ",", false)]
    [Arguments("X,ddd", "ddd", ",", false)]
    [Arguments("X','ddd", "ddd", ",", false)]
    [Arguments("X' , 'ddd", "ddd", ",", false)]
    [Arguments("Xddddd", "ddd", "", false)]
    [Arguments("X,ddddd", "ddd", "", false)]
    public async Task CanParseDayOfWeek(string s, string pattern, string separator, bool isFirst)
    {
        var result = CalendarFormatInfo.DayOfWeekStyle.Parse(s);

        await Assert.That(result.Pattern).IsEqualTo(pattern);
        await Assert.That(result.Separator).IsEqualTo(separator);
        await Assert.That(result.IsFirst).IsEqualTo(isFirst);
    }

    [Test]
    public async Task SettingYearPattern()
    {
        const string cultureName = "en-001";
        CalendarFormatInfo.SetYearPattern(cultureName, "A");
        var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
        await Assert.That(result.ComponentThreePattern).IsEqualTo("A");

        CalendarFormatInfo.SetYearPattern(cultureName, null);
        result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
        await Assert.That(result.ComponentThreePattern).IsEqualTo("yyyy");
    }

    [Test]
    public async Task SettingYearPatternOfMultipleCultures()
    {
        string[] cultureNames = { "en-001", "en-150" };
        CalendarFormatInfo.SetYearPattern(cultureNames, "B");
        foreach (var cultureName in cultureNames)
        {
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            await Assert.That(result.ComponentThreePattern).IsEqualTo("B");
        }
    }

    [Test]
    public async Task SettingDayOfWeekStyle()
    {
        const string cultureName = "en-001";
        CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
        var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
        await Assert.That(result.ComponentOnePattern).IsEqualTo("d MMM");
        await Assert.That(result.ComponentTwoPattern).IsEqualTo("Z@");

        CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Y", "@", false));
        result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
        await Assert.That(result.ComponentOnePattern).IsEqualTo("Y");
        await Assert.That(result.ComponentTwoPattern).IsEqualTo("d MMM@");
    }

    [Test]
    public async Task SettingDayOfWeekStyleOfMultipleCultures()
    {
        string[] cultureNames = { "en-001", "en-150" };
        CalendarFormatInfo.SetDayOfWeekStyle(cultureNames, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
        foreach (string cultureName in cultureNames)
        {
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            await Assert.That(result.ComponentOnePattern).IsEqualTo("d MMM");
            await Assert.That(result.ComponentTwoPattern).IsEqualTo("Z@");
        }
    }

    [Test]
    public async Task ResettingDayOfWeekStyle()
    {
        const string cultureName = "en-001";
        CalendarFormatInfo.SetDayOfWeekStyle(cultureName, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
        CalendarFormatInfo.ResetDayOfWeekStyle(cultureName);
        var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
        await Assert.That(result.ComponentOnePattern).IsEqualTo("d MMM");
        await Assert.That(result.ComponentTwoPattern).IsEqualTo("ddd,");
    }

    [Test]
    public async Task ResettingDayOfWeekStyleOfMultipleCultures()
    {
        string[] cultureNames = { "en-001", "en-150" };
        CalendarFormatInfo.SetDayOfWeekStyle(cultureNames, new CalendarFormatInfo.DayOfWeekStyle("Z", "@", true));
        CalendarFormatInfo.ResetDayOfWeekStyle(cultureNames);
        foreach (string cultureName in cultureNames)
        {
            var result = CalendarFormatInfo.FromCultureInfo(CultureInfo.GetCultureInfo(cultureName));
            await Assert.That(result.ComponentOnePattern).IsEqualTo("d MMM");
            await Assert.That(result.ComponentTwoPattern).IsEqualTo("ddd,");
        }
    }
}
