using System.Globalization;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{
    public class SliderToolTipConverterTests
    {
        [Theory]
        [InlineData(1.4)]
        [InlineData(-47.4)]
        [InlineData(128.678)]
        [InlineData(42)]
        public void SliderConverterTest(object value)
        {
            Wpf.Converters.SliderToolTipConverter converter = new Wpf.Converters.SliderToolTipConverter();

            //test a valid case
            var result = converter.Convert(new object[] { value, "Test String Format {0}" }, typeof(string), null, CultureInfo.CurrentCulture);
            Assert.Equal($"Test String Format {value}", result);

            //test too many placeholders in format string
            result = converter.Convert(new object[] { value, "{0} {1}" }, typeof(string), null, CultureInfo.CurrentCulture);
            Assert.Equal(value.ToString(), result);

            result = converter.Convert(new object[] { value, "{0} {1} {2}" }, typeof(string), null, CultureInfo.CurrentCulture);
            Assert.Equal(value.ToString(), result);

            //test empty format string
            result = converter.Convert(new object[] { value, "" }, typeof(string), null, CultureInfo.CurrentCulture);
            Assert.Equal(value.ToString(), result);

            //test null format string
            result = converter.Convert(new object[] { value, null }, typeof(string), null, CultureInfo.CurrentCulture);
            Assert.Equal(value.ToString(), result);
        }
    }
}
