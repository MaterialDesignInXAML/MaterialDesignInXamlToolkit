using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class MathMultipleConverterTests
{
    [Test]
    [EnumData]
    public async Task EnumValues_AreAllHandled(MathOperation operation)
    {
        MathMultipleConverter converter = new()
        {
            Operation = operation
        };

        await Assert.That(converter.Convert([1.0, 1.0], null, null, CultureInfo.CurrentUICulture) is double).IsTrue();
    }
}
