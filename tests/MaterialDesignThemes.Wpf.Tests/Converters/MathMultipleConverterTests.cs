using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class MathMultipleConverterTests
{
    [Theory]
    [EnumData]
    public void EnumValues_AreAllHandled(MathOperation operation)
    {
        MathMultipleConverter converter = new ()
        {
            Operation = operation
        };

        Assert.True(converter.Convert([1.0, 1.0], null, null, CultureInfo.CurrentUICulture) is double);
    }
}
