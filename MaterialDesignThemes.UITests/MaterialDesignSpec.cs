using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;
using Xunit;

namespace MaterialDesignThemes.UITests;

public static class MaterialDesignSpec
{
    /// <summary>
    /// Small text 4.5:1 against the background
    /// https://www.material.io/design/usability/accessibility.html#color-and-contrast
    /// </summary>
    public const double MinimumContrastSmallText = 4.5;

    /// <summary>
    /// Large text (at 14 pt bold/18 pt regular and up) and graphics 3:1 against the background
    /// https://www.material.io/design/usability/accessibility.html#color-and-contrast
    /// </summary>
    public const double MinimumContrastLargeText = 3.0;

    public static void AssertContrastRatio(Color foreground, Color background, double minimumContrastRatio)
    {
        const double tolerance = 0.1;
        
        var ratio = ColorAssist.ContrastRatio(foreground, background);
        Assert.True(ratio >= minimumContrastRatio - tolerance, $"Contrast ratio '{ratio}' is less than {minimumContrastRatio} with a tolerance of 0.1");
    }
}
