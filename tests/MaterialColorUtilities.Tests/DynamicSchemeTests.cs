using System.Runtime.Serialization;

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
}
