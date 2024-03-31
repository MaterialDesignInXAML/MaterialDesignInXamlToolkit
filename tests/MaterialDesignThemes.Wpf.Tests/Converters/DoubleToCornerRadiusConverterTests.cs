using System.Globalization;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public class DoubleToCornerRadiusConverterTests
{
    [Theory]
    [InlineData(-0.16, 0.0)]
    [InlineData(0.16, 0.16)]
    [InlineData(5.0, 5.0)]
    public void AllCultureParseParameterCorrectly(object parameter, double expectedCornerRadius)
    {
        var converter = new DoubleToCornerRadiusConverter();
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            var cornerRadius = (CornerRadius?)converter.Convert(parameter, typeof(CornerRadius), parameter, culture);

            Assert.Equal(expectedCornerRadius, cornerRadius.Value.TopLeft);
            Assert.Equal(expectedCornerRadius, cornerRadius.Value.TopRight);
            Assert.Equal(expectedCornerRadius, cornerRadius.Value.BottomRight);
            Assert.Equal(expectedCornerRadius, cornerRadius.Value.BottomLeft);
        }
    }
}
