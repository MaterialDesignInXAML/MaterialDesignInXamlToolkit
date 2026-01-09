using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.UITests.Samples.Theme;

namespace MaterialDesignThemes.UITests.WPF.Theme;

public class ColorAdjustTests : TestBase
{
    public static IEnumerable<Func<PrimaryColor>> PrimaryColors()
    {
        return Enum.GetValues(typeof(PrimaryColor))
            .OfType<PrimaryColor>()
            .Select(x => new Func<PrimaryColor>(() => x));
    }

    [Test]
    [MethodDataSource(nameof(PrimaryColors))]
    public async Task PrimaryColor_AdjustToTheme(PrimaryColor primary)
    {
        await App.InitializeWithMaterialDesign(BaseTheme.Light, primary, colorAdjustment: new ColorAdjustment());

        IWindow window = await App.CreateWindow<ColorAdjustWindow>();

        Color? windowBackground = await window.GetBackgroundColor();

        var themeToggle = await window.GetElement<ToggleButton>("/ToggleButton");
        var largeText = await window.GetElement<TextBlock>("/TextBlock[0]");
        var smallText = await window.GetElement<TextBlock>("/TextBlock[1]");

        await AssertContrastRatio();

        await themeToggle.LeftClick();
        await Wait.For(async () => await window.GetBackgroundColor() != windowBackground);

        await AssertContrastRatio();
        async Task AssertContrastRatio()
        {
            const double tolerance = 0.1;
            Color? largeTextForeground = await largeText.GetForegroundColor();
            Color largeTextBackground = await largeText.GetEffectiveBackground();

            Color? smallTextForeground = await smallText.GetForegroundColor();
            Color smallTextBackground = await smallText.GetEffectiveBackground();

            double largeContrastRatio = ColorAssist.ContrastRatio(largeTextForeground.Value, largeTextBackground);
            await Assert.That(largeContrastRatio).IsGreaterThanOrEqualTo(MaterialDesignSpec.MinimumContrastLargeText - tolerance)
                .Because($"Large font contrast ratio '{largeContrastRatio}' does not meet material design spec {MaterialDesignSpec.MinimumContrastLargeText}");
            double smallContrastRatio = ColorAssist.ContrastRatio(smallTextForeground.Value, smallTextBackground);
            await Assert.That(smallContrastRatio).IsGreaterThanOrEqualTo(MaterialDesignSpec.MinimumContrastSmallText - tolerance).Because($"Small font contrast ratio '{smallContrastRatio}' does not meet material design spec {MaterialDesignSpec.MinimumContrastSmallText}");
        }
    }
}
