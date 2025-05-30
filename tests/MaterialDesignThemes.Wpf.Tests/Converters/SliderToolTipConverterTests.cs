using System.Globalization;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class SliderToolTipConverterTests
{
    [Test]
    [Arguments(1.4)]
    [Arguments(-47.4)]
    [Arguments(128.678)]
    [Arguments(42)]
    public async Task SliderConverterTest(object value)
    {
        Wpf.Converters.Internal.SliderToolTipConverter converter = new();

        //test a valid case
        object? result = converter.Convert([value, "Test String Format {0}"], typeof(string), null, CultureInfo.CurrentCulture);
        await Assert.That(result).IsEqualTo($"Test String Format {value}");

        //test too many placeholders in format string
        result = converter.Convert([value, "{0} {1}"], typeof(string), null, CultureInfo.CurrentCulture);
        await Assert.That(result).IsEqualTo(value.ToString());

        result = converter.Convert([value, "{0} {1} {2}"], typeof(string), null, CultureInfo.CurrentCulture);
        await Assert.That(result).IsEqualTo(value.ToString());

        //test empty format string
        result = converter.Convert([value, ""], typeof(string), null, CultureInfo.CurrentCulture);
        await Assert.That(result).IsEqualTo(value.ToString());

        //test null format string
        result = converter.Convert([value, null], typeof(string), null, CultureInfo.CurrentCulture);
        await Assert.That(result).IsEqualTo(value.ToString());
    }
}
