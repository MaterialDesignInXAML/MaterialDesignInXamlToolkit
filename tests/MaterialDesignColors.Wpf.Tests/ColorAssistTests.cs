using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignColors.Wpf.Tests;

public class ColorAssistTests
{
    [Test]
    public async Task EnsureContrastRatio_AdjustsColor()
    {
        var background = Color.FromRgb(0xFA, 0xFA, 0xFA);
        var foreground = Color.FromRgb(0xFF, 0xC1, 0x07);

        var adjusted = foreground.EnsureContrastRatio(background, 3.0f);

        double contrastRatio = adjusted.ContrastRatio(background);
        await Assert.That(contrastRatio).IsGreaterThanOrEqualTo(2.9);
        await Assert.That(contrastRatio).IsLessThanOrEqualTo(3.1);
    }
}
