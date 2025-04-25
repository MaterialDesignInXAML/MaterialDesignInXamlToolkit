using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests.Converters;

public sealed class BrushOpacityConverterTests
{
    [Test]
    [Arguments("0.16", 0.16)]
    public async Task AllCultureParseParameterCorrectly(object parameter, double expectedOpacity)
    {
        var converter = new BrushOpacityConverter();
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            SolidColorBrush inputBrush = new() { Color = Colors.Red };
            var brush = (SolidColorBrush?)converter.Convert(inputBrush, typeof(Brush), parameter, culture);
            await Assert.That(brush?.Opacity).IsEqualTo(expectedOpacity);
        }
    }
}
