namespace MaterialColorUtilities.Tests;

public sealed class SchemeMonochromeTests
{
    private static readonly int Blue = unchecked((int)0xFF0000FF);

    [Test]
    [DisplayName("Monochrome spec (dark theme)")]
    public async Task DarkTheme_MonochromeSpec()
    {
        var mdc = new MaterialDynamicColors();
        var scheme = new SchemeMonochrome(
            sourceColorHct: Hct.FromInt(Blue),
            isDark: true,
            contrastLevel: 0.0
        );

        await Assert.That(mdc.Primary.GetHct(scheme).Tone).IsEqualTo(100).Within(1);
        await Assert.That(mdc.OnPrimary.GetHct(scheme).Tone).IsEqualTo(10).Within(1);
        await Assert.That(mdc.PrimaryContainer.GetHct(scheme).Tone).IsEqualTo(85).Within(1);
        await Assert.That(mdc.OnPrimaryContainer.GetHct(scheme).Tone).IsEqualTo(0).Within(1);

        await Assert.That(mdc.Secondary.GetHct(scheme).Tone).IsEqualTo(80).Within(1);
        await Assert.That(mdc.OnSecondary.GetHct(scheme).Tone).IsEqualTo(10).Within(1);
        await Assert.That(mdc.SecondaryContainer.GetHct(scheme).Tone).IsEqualTo(30).Within(1);
        await Assert.That(mdc.OnSecondaryContainer.GetHct(scheme).Tone).IsEqualTo(90).Within(1);

        await Assert.That(mdc.Tertiary.GetHct(scheme).Tone).IsEqualTo(90).Within(1);
        await Assert.That(mdc.OnTertiary.GetHct(scheme).Tone).IsEqualTo(10).Within(1);
        await Assert.That(mdc.TertiaryContainer.GetHct(scheme).Tone).IsEqualTo(60).Within(1);
        await Assert.That(mdc.OnTertiaryContainer.GetHct(scheme).Tone).IsEqualTo(0).Within(1);
    }

    [Test]
    [DisplayName("Monochrome spec (light theme)")]
    public async Task LightTheme_MonochromeSpec()
    {
        var mdc = new MaterialDynamicColors();
        var scheme = new SchemeMonochrome(
            sourceColorHct: Hct.FromInt(Blue),
            isDark: false,
            contrastLevel: 0.0
        );

        await Assert.That(mdc.Primary.GetHct(scheme).Tone).IsEqualTo(0).Within(1);
        await Assert.That(mdc.OnPrimary.GetHct(scheme).Tone).IsEqualTo(90).Within(1);
        await Assert.That(mdc.PrimaryContainer.GetHct(scheme).Tone).IsEqualTo(25).Within(1);
        await Assert.That(mdc.OnPrimaryContainer.GetHct(scheme).Tone).IsEqualTo(100).Within(1);

        await Assert.That(mdc.Secondary.GetHct(scheme).Tone).IsEqualTo(40).Within(1);
        await Assert.That(mdc.OnSecondary.GetHct(scheme).Tone).IsEqualTo(100).Within(1);
        await Assert.That(mdc.SecondaryContainer.GetHct(scheme).Tone).IsEqualTo(85).Within(1);
        await Assert.That(mdc.OnSecondaryContainer.GetHct(scheme).Tone).IsEqualTo(10).Within(1);

        await Assert.That(mdc.Tertiary.GetHct(scheme).Tone).IsEqualTo(25).Within(1);
        await Assert.That(mdc.OnTertiary.GetHct(scheme).Tone).IsEqualTo(90).Within(1);
        await Assert.That(mdc.TertiaryContainer.GetHct(scheme).Tone).IsEqualTo(49).Within(1);
        await Assert.That(mdc.OnTertiaryContainer.GetHct(scheme).Tone).IsEqualTo(100).Within(1);
    }
}
