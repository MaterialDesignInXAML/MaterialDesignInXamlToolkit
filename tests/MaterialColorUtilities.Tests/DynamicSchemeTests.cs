using System.Windows.Media;

namespace MaterialColorUtilities.Tests;

public sealed class DynamicSchemeTests
{
    [Test]
    [DisplayName("0 length input")]
    public async Task ZeroLengthInput_NoRotation()
    {
        var hue = DynamicScheme.GetRotatedHue(
            Hct.From(43, 16, 16),
            [],
            []);

        await Assert.That(hue).IsEqualTo(43.0).Within(1.0);
    }

    [Test]
    [DisplayName("1 length input no rotation")]
    public async Task OneLengthInput_NoRotation()
    {
        var hue = DynamicScheme.GetRotatedHue(
            Hct.From(43, 16, 16),
            [0.0],
            [0.0]);

        await Assert.That(hue).IsEqualTo(43.0).Within(1.0);
    }

    [Test]
    [DisplayName("input length mismatch asserts")]
    public async Task InputLengthMismatch_Throws()
    {
        await Assert.That(() =>
            DynamicScheme.GetRotatedHue(
                Hct.From(43, 16, 16),
                [0.0, 1.0],   // 2 breakpoints
                [0.0] // 1 rotation
            )
        ).Throws<ArgumentException>();
    }

    [Test]
    [DisplayName("on boundary rotation correct")]
    public async Task OnBoundary_RotationApplied()
    {
        var hue = DynamicScheme.GetRotatedHue(
            Hct.From(43, 16, 16),
            [0.0, 42.0, 360.0],
            [0.0, 15.0, 0.0]);

        await Assert.That(hue).IsEqualTo(43.0 + 15.0).Within(1.0);
    }

    [Test]
    [DisplayName("rotation > 360 wraps")]
    public async Task RotationGreaterThan360_Wraps()
    {
        var hue = DynamicScheme.GetRotatedHue(
            Hct.From(43, 16, 16),
            [0.0, 42.0, 360.0],
            [0.0, 480.0, 0.0]);

        // 43 + 480 = 523 -> 163 after sanitize/wrap
        await Assert.That(hue).IsEqualTo(163.0).Within(1.0);
    }

#if WPF
    /// <summary>
    /// Shows how te crate a theme from a primary color.
    /// </summary>
    [Test]
    public async Task CreateThemeFromColor()
    {
        var mdc = new MaterialDynamicColors();
        var primaryColorHct = Hct.FromInt(ColorUtils.ArgbFromColor(Color.FromArgb(0xff, 0x6a, 0x9c, 0x59)));
        var scheme = new SchemeContent(
            primaryColorHct,
            isDark: true,
            contrastLevel : 0.5,
            SpecVersion.Spec2025,
            Platform.Phone);

        scheme.ShouldSatisfyAllConditions(
            // Main Palettes
            () => mdc.PrimaryPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x52, 0x83, 0x43)),
            () => mdc.SecondaryPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x68, 0x7D, 0x5E)),
            () => mdc.TertiaryPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x2B, 0x9F, 0x94)),
            () => mdc.NeutralPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x75, 0x78, 0x71)),
            () => mdc.NeutralVariantPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x72, 0x79, 0x6C)),
            () => mdc.ErrorPaletteKeyColor.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xDE, 0x37, 0x30)),

            // Surfaces [S]
            () => mdc.Background.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x11, 0x14, 0x0F)),
            () => mdc.OnBackground.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xE1, 0xE3, 0xDB)),
            () => mdc.Surface.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x11, 0x14, 0x0F)),
            () => mdc.SurfaceDim.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x11, 0x14, 0x0F)),
            () => mdc.SurfaceBright.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x42, 0x45, 0x3F)),
            () => mdc.SurfaceContainerLowest.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x06, 0x08, 0x05)),
            () => mdc.SurfaceContainerLow.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x1B, 0x1E, 0x19)),
            () => mdc.SurfaceContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x26, 0x29, 0x23)),
            () => mdc.SurfaceContainerHigh.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x30, 0x33, 0x2E)),
            () => mdc.SurfaceContainerHighest.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x3C, 0x3F, 0x39)),
            () => mdc.OnSurface.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)),
            () => mdc.SurfaceVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x42, 0x49, 0x3E)),
            () => mdc.OnSurfaceVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xD8, 0xDF, 0xCF)),
            () => mdc.Outline.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xAD, 0xB4, 0xA6)),
            () => mdc.OutlineVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x8B, 0x93, 0x85)),
            () => mdc.InverseSurface.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xE1, 0xE3, 0xDB)),
            () => mdc.InverseOnSurface.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x28, 0x2B, 0x25)),
            () => mdc.Shadow.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),
            () => mdc.Scrim.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),
            () => mdc.SurfaceTint.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x9F, 0xD5, 0x8B)),

            // Primaries [P]
            () => mdc.Primary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xB5, 0xEB, 0x9F)),
            () => mdc.PrimaryDim?.GetColor(scheme).ShouldBe(Color.FromArgb(0x00, 0x00, 0x00, 0x00)),
            () => mdc.OnPrimary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x04, 0x2D, 0x00)),
            () => mdc.PrimaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x6B, 0x9D, 0x5A)),
            () => mdc.OnPrimaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),
            () => mdc.PrimaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xBB, 0xF1, 0xA5)),
            () => mdc.PrimaryFixedDim.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x9F, 0xD5, 0x8B)),
            () => mdc.OnPrimaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x01, 0x16, 0x00)),
            () => mdc.OnPrimaryFixedVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x10, 0x3F, 0x06)),
            () => mdc.InversePrimary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x24, 0x52, 0x18)),

            // Secondaries [Q]
            () => mdc.Secondary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xCC, 0xE3, 0xBF)),
            () => mdc.SecondaryDim?.GetColor(scheme).ShouldBe(Color.FromArgb(0x00, 0x00, 0x00, 0x00)),
            () => mdc.OnSecondary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x18, 0x2A, 0x12)),
            () => mdc.SecondaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x81, 0x97, 0x77)),
            () => mdc.OnSecondaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),

            // Secondary Fixed [QF]
            () => mdc.SecondaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xD2, 0xE9, 0xC5)),
            () => mdc.SecondaryFixedDim.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xB7, 0xCD, 0xAA)),
            () => mdc.OnSecondaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x05, 0x15, 0x02)),
            () => mdc.OnSecondaryFixedVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x28, 0x3B, 0x22)),

            // Tertiaries [T]
            () => mdc.Tertiary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x85, 0xEE, 0xE1)),
            () => mdc.TertiaryDim?.GetColor(scheme).ShouldBe(Color.FromArgb(0x00, 0x00, 0x00, 0x00)),
            () => mdc.OnTertiary.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x2B, 0x27)),
            () => mdc.TertiaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x2D, 0xA1, 0x96)),
            () => mdc.OnTertiaryContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),

            // Tertiary Fixed [TF]
            () => mdc.TertiaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x8B, 0xF5, 0xE8)),
            () => mdc.TertiaryFixedDim.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x6E, 0xD8, 0xCC)),
            () => mdc.OnTertiaryFixed.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x15, 0x12)),
            () => mdc.OnTertiaryFixedVariant.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x3E, 0x38)),

            // Errors [E]
            () => mdc.Error.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xFF, 0xD2, 0xCC)),
            () => mdc.ErrorDim?.GetColor(scheme).ShouldBe(Color.FromArgb(0x00, 0x00, 0x00, 0x00)),
            () => mdc.OnError.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x54, 0x00, 0x03)),
            () => mdc.ErrorContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xFF, 0x54, 0x49)),
            () => mdc.OnErrorContainer.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)),

            // Android-only
            () => mdc.ControlActivated.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x23, 0x51, 0x17)),
            () => mdc.ControlNormal.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0xC2, 0xC9, 0xBA)),
            () => mdc.ControlHighlight.GetColor(scheme).ShouldBe(Color.FromArgb(0x33, 0xFF, 0xFF, 0xFF)),
            () => mdc.TextPrimaryInverse.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x19, 0x1C, 0x17)),
            () => mdc.TextSecondaryAndTertiaryInverse.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x42, 0x49, 0x3E)),
            () => mdc.TextPrimaryInverseDisableOnly.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x19, 0x1C, 0x17)),
            () => mdc.TextSecondaryAndTertiaryInverseDisabled.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x19, 0x1C, 0x17)),
            () => mdc.TextHintInverse.GetColor(scheme).ShouldBe(Color.FromArgb(0xFF, 0x19, 0x1C, 0x17)));
        await Task.CompletedTask;
    }
#endif
}
