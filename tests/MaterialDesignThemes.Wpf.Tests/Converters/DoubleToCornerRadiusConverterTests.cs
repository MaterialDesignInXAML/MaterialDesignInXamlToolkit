using System.Globalization;
using MaterialDesignThemes.Wpf.Converters.Internal;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class DoubleToCornerRadiusConverterTests
{
    [Test]
    [Arguments(-0.16, 0.0)]
    [Arguments(0.16, 0.16)]
    [Arguments(5.0, 5.0)]
    public async Task AllCultureParseParameterCorrectly(object parameter, double expectedCornerRadius)
    {
        DoubleToCornerRadiusConverter converter = new();
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            var cornerRadius = (CornerRadius?)converter.Convert(parameter, typeof(CornerRadius), parameter, culture);

            await Assert.That(cornerRadius.Value.TopLeft).IsEqualTo(expectedCornerRadius);
            await Assert.That(cornerRadius.Value.TopRight).IsEqualTo(expectedCornerRadius);
            await Assert.That(cornerRadius.Value.BottomRight).IsEqualTo(expectedCornerRadius);
            await Assert.That(cornerRadius.Value.BottomLeft).IsEqualTo(expectedCornerRadius);
        }
    }
}
