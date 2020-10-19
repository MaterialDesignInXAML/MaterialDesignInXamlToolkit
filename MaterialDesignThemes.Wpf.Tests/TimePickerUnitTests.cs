using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{

    public class TimePickerUnitTests
    {
        private readonly TimePicker _timePicker;

        public TimePickerUnitTests()
        {
            _timePicker = new TimePicker();
            _timePicker.ApplyDefaultStyle();
        }

        [StaFact]
        [Description("Issue 1691")]
        public void DontOverwriteDate()
        {
            var expectedDate = new DateTime(2000, 1, 1, 20, 0, 0);

            _timePicker.SelectedTime = expectedDate;

            Assert.Equal(_timePicker.SelectedTime, expectedDate);
        }

        [StaTheory]
        [MemberData(nameof(GetDisplaysExpectedTextData))]
        public void DisplaysExpectedText(CultureInfo culture, DatePickerFormat format, bool is24Hour, bool withSeconds,
            DateTime? selectedTime, string expectedText)
        {
            _timePicker.Language = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
            _timePicker.SelectedTimeFormat = format;
            _timePicker.Is24Hours = is24Hour;
            _timePicker.WithSeconds = withSeconds;
            _timePicker.SelectedTime = selectedTime;

            Assert.Equal(expectedText, _timePicker.Text);
        }

        [StaTheory]
        [MemberData(nameof(GetParseLocalizedTimeStringData))]
        public void CanParseLocalizedTimeString(CultureInfo culture, DatePickerFormat format, bool is24Hour, bool withSeconds,
            string timeString, DateTime? expectedTime)
        {
            _timePicker.Language = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
            _timePicker.SelectedTimeFormat = format;
            _timePicker.Is24Hours = is24Hour;
            _timePicker.WithSeconds = withSeconds;
            _timePicker.SelectedTime = DateTime.MinValue;

            var textBox = _timePicker.FindVisualChild<TextBox>(TimePicker.TextBoxPartName);
            textBox.Text = timeString;
            textBox.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));

            Assert.Equal(expectedTime, _timePicker.SelectedTime);
        }

        public static IEnumerable<object[]> GetParseLocalizedTimeStringData()
        {
            //for now just using the same set of data to make sure we can go both directions.
            foreach (object[] data in GetDisplaysExpectedTextData())
            {
                var culture = (CultureInfo)data[0];
                bool is24Hour = (bool)data[2];
                var withSeconds = (bool)data[3];
                var date = (DateTime)data[4];
                var timeString = (string)data[5];

                //Convert the date to Today
                date = DateTime.MinValue.AddHours(date.Hour).AddMinutes(date.Minute).AddSeconds(withSeconds ? date.Second : 0);

                if (!is24Hour && date.Hour > 12 &&
                    (string.IsNullOrEmpty(culture.DateTimeFormat.AMDesignator) ||
                    string.IsNullOrEmpty(culture.DateTimeFormat.PMDesignator)))
                {
                    //Because there is no AM/PM designator, 12 hour times will be treated as AM
                    date = date.AddHours(-12);
                }

                //Invert the order of the parameters.
                data[5] = date;
                data[4] = timeString;


                yield return data;
            }
        }

        public static IEnumerable<object[]> GetDisplaysExpectedTextData()
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
                yield return data;
            }
            foreach (var data in GetDisplaysExpectedTextDataForCulture(CultureInfo.InvariantCulture, pm,
                "4:30 PM", "4:30:25 PM", //12 hour short
                "04:30 PM", "04:30:25 PM", //12 hour long
                "16:30", "16:30:25", //24 hour short
                "16:30", "16:30:25")) //24 hour long
            {
                yield return data;
            }

            //US English
            var usEnglish = CultureInfo.GetCultureInfo("en-US");
            foreach (var data in GetDisplaysExpectedTextDataForCulture(usEnglish, am,
                "3:05 AM", "3:05:09 AM", //12 hour short
                "03:05 AM", "03:05:09 AM", //12 hour long
                "3:05", "3:05:09", //24 hour short
                "03:05", "03:05:09")) //24 hour long
            {
                yield return data;
            }
            foreach (var data in GetDisplaysExpectedTextDataForCulture(usEnglish, pm,
                "4:30 PM", "4:30:25 PM", //12 hour short
                "04:30 PM", "04:30:25 PM", //12 hour long
                "16:30", "16:30:25", //24 hour short
                "16:30", "16:30:25")) //24 hour long
            {
                yield return data;
            }

            //Spain Spanish
            var spainSpanish = CultureInfo.GetCultureInfo("es-ES");
            foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, am,
                "3:05", "3:05:09", //12 hour short
                "03:05", "03:05:09", //12 hour long
                "3:05", "3:05:09", //24 hour short
                "03:05", "03:05:09")) //24 hour long
            {
                yield return data;
            }
            foreach (var data in GetDisplaysExpectedTextDataForCulture(spainSpanish, pm,
                "4:30", "4:30:25", //12 hour short
                "04:30", "04:30:25", //12 hour long
                "16:30", "16:30:25", //24 hour short
                "16:30", "16:30:25")) //24 hour long
            {
                yield return data;
            }

            //Iran Farsi fa-IR
            var iranFarsi = CultureInfo.GetCultureInfo("fa-IR");
            foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, am,
                "3:05 ق.ظ", "3:05:09 ق.ظ", //12 hour short
                "03:05 ق.ظ", "03:05:09 ق.ظ", //12 hour long
                "3:05", "3:05:09", //24 hour short
                "03:05", "03:05:09")) //24 hour long
            {
                yield return data;
            }
            foreach (var data in GetDisplaysExpectedTextDataForCulture(iranFarsi, pm,
                "4:30 ب.ظ", "4:30:25 ب.ظ", //12 hour short
                "04:30 ب.ظ", "04:30:25 ب.ظ", //12 hour long
                "16:30", "16:30:25", //24 hour short
                "16:30", "16:30:25")) //24 hour long
            {
                yield return data;
            }
        }

        private static IEnumerable<object[]> GetDisplaysExpectedTextDataForCulture(CultureInfo culture,
            DateTime dateTime,
            string short12Hour, string short12HourWithSeconds,
            string long12Hour, string long12HourWithSeconds,
            string short24Hour, string short24HourWithSeconds,
            string long24Hour, string long24HourWithSeconds)
        {
            yield return new object[]
            {
                culture,
                DatePickerFormat.Short,
                false,
                false,
                dateTime,
                short12Hour
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Short,
                false,
                true,
                dateTime,
                short12HourWithSeconds
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Long,
                false,
                false,
                dateTime,
                long12Hour
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Long,
                false,
                true,
                dateTime,
                long12HourWithSeconds
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Short,
                true,
                false,
                dateTime,
                short24Hour
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Short,
                true,
                true,
                dateTime,
                short24HourWithSeconds
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Long,
                true,
                false,
                dateTime,
                long24Hour
            };
            yield return new object[]
            {
                culture,
                DatePickerFormat.Long,
                true,
                true,
                dateTime,
                long24HourWithSeconds
            };
        }
    }
}