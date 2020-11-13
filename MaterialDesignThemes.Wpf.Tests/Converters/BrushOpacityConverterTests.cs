using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.Wpf.Converters;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests.Converters
{
    public class BrushOpacityConverterTests
    {
        [Theory]
        [InlineData("0.16", 0.16)]
        public void AllCultureParseParameterCorrectly(object parameter, double expectedOpacity)
        {
            var converter = new BrushOpacityConverter();
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                var inputBrush = new SolidColorBrush { Color = Colors.Red };
                var brush = (SolidColorBrush?)converter.Convert(inputBrush, typeof(Brush), parameter, culture);
                Assert.Equal(expectedOpacity, brush?.Opacity);
            }
        }
    }
}
