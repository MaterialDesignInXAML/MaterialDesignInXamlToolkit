using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class MathMultipleConverterTests
{
    [Test]
    [MatrixDataSource]
    public async Task EnumValues_AreAllHandled(
        [EnumData<MathOperation>]MathOperation operation)
    {
        MathMultipleConverter converter = new()
        {
            Operation = operation
        };

        await Assert.That(converter.Convert([1.0, 1.0], null, null, CultureInfo.CurrentUICulture) is double).IsTrue();
    }
}
