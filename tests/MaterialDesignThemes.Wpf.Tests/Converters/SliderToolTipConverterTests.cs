using System.Globalization;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class SliderToolTipConverterTests
{
    [Theory]
    [InlineData(1.4)]
    [InlineData(-47.4)]
    [InlineData(128.678)]
    [InlineData(42)]
    public void SliderConverterTest(object value)
    {

/* Unmerged change from project 'MaterialDesignThemes.Wpf.Tests (net8.0-windows)'
Before:
        Wpf.Converters.SliderToolTipConverter converter = new ();
After:
        SliderToolTipConverter converter = new ();
*/
        Wpf.Converters.Internal.SliderToolTipConverter converter = new ();

        //test a valid case
        object? result = converter.Convert([value, "Test String Format {0}"], typeof(string), null, CultureInfo.CurrentCulture);
        Assert.Equal($"Test String Format {value}", result);

        //test too many placeholders in format string
        result = converter.Convert([value, "{0} {1}"], typeof(string), null, CultureInfo.CurrentCulture);
        Assert.Equal(value.ToString(), result);

        result = converter.Convert([value, "{0} {1} {2}"], typeof(string), null, CultureInfo.CurrentCulture);
        Assert.Equal(value.ToString(), result);

        //test empty format string
        result = converter.Convert([value, ""], typeof(string), null, CultureInfo.CurrentCulture);
        Assert.Equal(value.ToString(), result);

        //test null format string
        result = converter.Convert([value, null], typeof(string), null, CultureInfo.CurrentCulture);
        Assert.Equal(value.ToString(), result);
    }
}
