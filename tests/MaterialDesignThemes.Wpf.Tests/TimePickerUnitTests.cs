using System.ComponentModel;
using System.Globalization;

namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class TimePickerUnitTests
{
    public static TimePicker CreateElement()
    {
        TimePicker timePicker = new();
        timePicker.ApplyDefaultStyle();
        return timePicker;
    }

    [Test]
    [Description("Issue 1691")]
    public async Task DontOverwriteDate()
    {
        TimePicker timePicker = CreateElement();

        var expectedDate = new DateTime(2000, 1, 1, 20, 0, 0);

        timePicker.SelectedTime = expectedDate;

        await Assert.That(timePicker.SelectedTime).IsEqualTo(expectedDate);
    }

    [Test]
    [MethodDataSource(nameof(GetDisplaysExpectedTextData))]
    public async Task DisplaysExpectedText(CultureInfo culture, DatePickerFormat format, bool is24Hour, bool withSeconds,
        DateTime selectedTime, string expectedText)
    {
        var timePicker = CreateElement();
        timePicker.Language = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
        timePicker.SelectedTimeFormat = format;
        timePicker.Is24Hours = is24Hour;
        timePicker.WithSeconds = withSeconds;
        timePicker.SelectedTime = selectedTime;

        await Assert.That(timePicker.Text).IsEqualTo(expectedText);
    }

    [Test]
    [MethodDataSource(nameof(GetParseLocalizedTimeStringData))]
    public async Task CanParseLocalizedTimeString(CultureInfo culture, DatePickerFormat format, bool is24Hour, bool withSeconds,
        string timeString, DateTime expectedTime)
    {
        var timePicker = CreateElement();
        timePicker.Language = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
        timePicker.SelectedTimeFormat = format;
        timePicker.Is24Hours = is24Hour;
        timePicker.WithSeconds = withSeconds;
        timePicker.SelectedTime = DateTime.MinValue;

        var textBox = timePicker.FindVisualChild<TextBox>(TimePicker.TextBoxPartName);
        textBox.Text = timeString;
        textBox.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));

        await Assert.That(timePicker.SelectedTime).IsEqualTo(expectedTime);
    }

    public static IEnumerable<Func<(CultureInfo culture,
         DatePickerFormat format,
         bool is24Hour,
         bool withSeconds,
         string timeString,
         DateTime expectedTime)>> GetParseLocalizedTimeStringData()
    {
        //for now just using the same set of data to make sure we can go both directions.
        foreach ((CultureInfo culture, DatePickerFormat format, bool is24Hour, bool withSeconds, DateTime date, string timeString) in GetDisplaysExpectedTextData().Select(x => x()))
        {
            //Convert the date to Today
            var newDate = DateTime.MinValue.AddHours(date.Hour).AddMinutes(date.Minute).AddSeconds(withSeconds ? date.Second : 0);

            if (!is24Hour && date.Hour > 12 &&
                (string.IsNullOrEmpty(culture.DateTimeFormat.AMDesignator) ||
                string.IsNullOrEmpty(culture.DateTimeFormat.PMDesignator)))
            {
                //Because there is no AM/PM designator, 12 hour times will be treated as AM
                newDate = newDate.AddHours(-12);
            }

            //Invert the order of the parameters.
            yield return () => (culture, format, is24Hour, withSeconds, timeString, newDate);
        }
    }

    public static IEnumerable<Func<
        (CultureInfo culture,
         DatePickerFormat format,
         bool is24Hour,
         bool withSeconds,
         DateTime dateTime,
         string expectedText)
        >> GetDisplaysExpectedTextData()
    {
        //AM intentionally picks values with only a single digit to verify the DatePickerFormat is applied
        var am = new DateTime(2000, 1, 1, 3, 5, 9);
        //PM intentionally picks two digit values greater than 12 to ensure the 24 hour format is applied
        var pm = new DateTime(2000, 1, 1, 16, 30, 25);

        //Invariant culture
        foreach (var data in GetDisplaysExpectedTextDataForCulture(CultureInfo.InvariantCulture, am,
            "3:05 AM", "3:05:09 AM", //12 hour short
            "03:05 AM", "03:05:09 AM", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(CultureInfo.InvariantCulture, pm,
            "4:30 PM", "4:30:25 PM", //12 hour short
            "04:30 PM", "04:30:25 PM", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }

        //US English
        var usEnglish = CultureInfo.GetCultureInfo("en-US");
        foreach (var data in GetDisplaysExpectedTextDataForCulture(usEnglish, am,
            "3:05 AM", "3:05:09 AM", //12 hour short
            "03:05 AM", "03:05:09 AM", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(usEnglish, pm,
            "4:30 PM", "4:30:25 PM", //12 hour short
            "04:30 PM", "04:30:25 PM", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }

        //Spain Spanish
        var spainSpanish = CultureInfo.GetCultureInfo("es-ES");
#if NET5_0_OR_GREATER
        foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, am,
            "3:05 a. m.", "3:05:09 a. m.", //12 hour short
            "03:05 a. m.", "03:05:09 a. m.", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, pm,
            "4:30 p. m.", "4:30:25 p. m.", //12 hour short
            "04:30 p. m.", "04:30:25 p. m.", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }
#else
        foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, am,
            "3:05", "3:05:09", //12 hour short
            "03:05", "03:05:09", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, pm,
            "4:30", "4:30:25", //12 hour short
            "04:30", "04:30:25", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }
#endif

        //Iran Farsi fa-IR
        var iranFarsi = CultureInfo.GetCultureInfo("fa-IR");
#if NET5_0_OR_GREATER
        foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, am,
            "3:05 قبل‌ازظهر", "3:05:09 قبل‌ازظهر", //12 hour short
            "03:05 قبل‌ازظهر", "03:05:09 قبل‌ازظهر", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, pm,
            "4:30 بعدازظهر", "4:30:25 بعدازظهر", //12 hour short
            "04:30 بعدازظهر", "04:30:25 بعدازظهر", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }
#else
        foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, am,
            "3:05 ق.ظ", "3:05:09 ق.ظ", //12 hour short
            "03:05 ق.ظ", "03:05:09 ق.ظ", //12 hour long
            "3:05", "3:05:09", //24 hour short
            "03:05", "03:05:09")) //24 hour long
        {
            yield return () => data;
        }
        foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, pm,
            "4:30 ب.ظ", "4:30:25 ب.ظ", //12 hour short
            "04:30 ب.ظ", "04:30:25 ب.ظ", //12 hour long
            "16:30", "16:30:25", //24 hour short
            "16:30", "16:30:25")) //24 hour long
        {
            yield return () => data;
        }
#endif

    }

    private static IEnumerable<
        (CultureInfo culture,
         DatePickerFormat format,
         bool is24Hour,
         bool withSeconds,
         DateTime dateTime,
         string expectedText)
        > GetDisplaysExpectedTextDataForCulture(CultureInfo culture,
        DateTime dateTime,
        string short12Hour, string short12HourWithSeconds,
        string long12Hour, string long12HourWithSeconds,
        string short24Hour, string short24HourWithSeconds,
        string long24Hour, string long24HourWithSeconds)
    {
        yield return (culture, DatePickerFormat.Short, false, false, dateTime, short12Hour);
        yield return (culture, DatePickerFormat.Short, false, true, dateTime, short12HourWithSeconds);
        yield return (culture, DatePickerFormat.Long, false, false, dateTime, long12Hour);
        yield return (culture, DatePickerFormat.Long, false, true, dateTime, long12HourWithSeconds);
        yield return (culture, DatePickerFormat.Short, true, false, dateTime, short24Hour);
        yield return (culture, DatePickerFormat.Short, true, true, dateTime, short24HourWithSeconds);
        yield return (culture, DatePickerFormat.Long, true, false, dateTime, long24Hour);
        yield return (culture, DatePickerFormat.Long, true, true, dateTime, long24HourWithSeconds);
    }
}
