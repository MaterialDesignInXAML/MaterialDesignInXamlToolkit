using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using Xunit;

namespace MaterialDesignColors.Wpf.Tests
{
    public class ColorAssistTests
    {
        [Fact]
        public void EnsureContrastRatio_AdjustsColor()
        {
            var background = Color.FromRgb(0xFA, 0xFA, 0xFA);
            var foreground = Color.FromRgb(0xFF, 0xC1, 0x07);

            var adjusted = foreground.EnsureContrastRatio(background, 3.0f);

            double contrastRatio = adjusted.ContrastRatio(background);
            Assert.True(contrastRatio >= 3.0);
            Assert.True(contrastRatio <= 3.1);
        }
    }
}
