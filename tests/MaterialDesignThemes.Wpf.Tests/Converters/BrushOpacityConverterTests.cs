using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class BrushOpacityConverterTests
{
    [Theory]
    [InlineData("0.16", 0.16)]
    public void AllCultureParseParameterCorrectly(object parameter, double expectedOpacity)
    {
        var converter = new BrushOpacityConverter();
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            SolidColorBrush inputBrush = new () { Color = Colors.Red };
            var brush = (SolidColorBrush?)converter.Convert(inputBrush, typeof(Brush), parameter, culture);
            Assert.Equal(expectedOpacity, brush?.Opacity);
        }
    }
}
