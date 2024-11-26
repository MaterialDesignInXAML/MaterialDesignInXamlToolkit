using System.Globalization;
using System.Windows.Data;
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

    [Fact]
    public void NoneDoubleArguments_ShouldReturnDoNothing()
    {
        MathConverter converter = new();
        object? actual1 = converter.Convert("", null, 1.0, CultureInfo.CurrentUICulture);
        Assert.Equal(Binding.DoNothing, actual1);
        object? actual2 = converter.Convert(1.0, null, "", CultureInfo.CurrentUICulture);
        Assert.Equal(Binding.DoNothing, actual2);
    }
}
