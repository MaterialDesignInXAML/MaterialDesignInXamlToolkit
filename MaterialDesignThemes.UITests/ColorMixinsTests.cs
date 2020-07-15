using System.Windows.Media;
using VTTests;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class ColorMixinsTests
    {
        [Fact]
        public void ContrastRatio()
        {
            var ratio = Colors.Black.ContrastRatio(Colors.White);

            //Actual value should be 21, allowing for floating point rounding errors
            Assert.True(ratio >= 20.9);
        }
    }
}
