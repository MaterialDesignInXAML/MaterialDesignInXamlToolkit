using System.Globalization;
using MaterialDesignThemes.Wpf.Converters.Internal;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class AutomationPropertiesNameConverterTests
{
    [Test]
    public async Task Convert_StringValue_ReturnsOriginalString()
    {
        var converter = AutomationPropertiesNameConverter.Instance;
        string input = "Test String";
        
        var result = converter.Convert(input, typeof(string), null, CultureInfo.InvariantCulture);
        
        await Assert.That(result).IsEqualTo(input);
    }

    [Test]
    public async Task Convert_NullValue_ReturnsEmptyString()
    {
        var converter = AutomationPropertiesNameConverter.Instance;
        
        var result = converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture);
        
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Convert_NonStringValue_ReturnsEmptyString()
    {
        var converter = AutomationPropertiesNameConverter.Instance;
        int input = 42;
        
        var result = converter.Convert(input, typeof(string), null, CultureInfo.InvariantCulture);
        
        await Assert.That(result).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task Convert_EmptyString_ReturnsEmptyString()
    {
        var converter = AutomationPropertiesNameConverter.Instance;
        string input = string.Empty;
        
        var result = converter.Convert(input, typeof(string), null, CultureInfo.InvariantCulture);
        
        await Assert.That(result).IsEqualTo(string.Empty);
    }
}
