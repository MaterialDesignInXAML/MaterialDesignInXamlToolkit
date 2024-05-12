using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class MathConverterTests
{
    [Theory]
    [EnumData]
    public void EnumValues_AreAllHandled(MathOperation operation)
    {
        MathConverter converter = new ()
        {
            Operation = operation
        };

        Assert.True(converter.Convert(1.0, null, 1.0, CultureInfo.CurrentUICulture) is double);
    }
}
