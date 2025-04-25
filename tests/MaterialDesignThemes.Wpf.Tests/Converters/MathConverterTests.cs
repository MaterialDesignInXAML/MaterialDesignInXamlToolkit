using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf.Converters;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class MathConverterTests
{
    [Test]
    [EnumData]
    public async Task EnumValues_AreAllHandled(MathOperation operation)
    {
        MathConverter converter = new()
        {
            Operation = operation
        };

        await Assert.That(converter.Convert(1.0, null, 1.0, CultureInfo.CurrentUICulture) is double).IsTrue();
    }

    [Test]
    public async Task NoneDoubleArguments_ShouldReturnDoNothing()
    {
        MathConverter converter = new();
        object? actual1 = converter.Convert("", null, 1.0, CultureInfo.CurrentUICulture);
        await Assert.That(actual1).IsEqualTo(Binding.DoNothing);
        object? actual2 = converter.Convert(1.0, null, "", CultureInfo.CurrentUICulture);
        await Assert.That(actual2).IsEqualTo(Binding.DoNothing);
    }
}
