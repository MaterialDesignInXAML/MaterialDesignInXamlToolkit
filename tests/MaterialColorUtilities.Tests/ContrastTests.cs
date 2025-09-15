namespace MaterialColorUtilities.Tests;

public sealed class ContrastTests
{
    [Test]
    [DisplayName("ratioOfTones_outOfBoundsInput")]
    public async Task RatioOfTones_OutOfBounds_ReturnsMax()
    {
        var actual = Contrast.RatioOfTones(-10.0, 110.0);
        await Assert.That(actual).IsEqualTo(21.0).Within(0.001);
    }

    [Test]
    [DisplayName("lighter_impossibleRatioErrors")]
    public async Task Lighter_ImpossibleRatio_ReturnsError()
    {
        var actual = Contrast.Lighter(tone: 90.0, ratio: 10.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("lighter_outOfBoundsInputAboveErrors")]
    public async Task Lighter_ToneAboveBounds_ReturnsError()
    {
        var actual = Contrast.Lighter(tone: 110.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("lighter_outOfBoundsInputBelowErrors")]
    public async Task Lighter_ToneBelowBounds_ReturnsError()
    {
        var actual = Contrast.Lighter(tone: -10.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("lighterUnsafe_returnsMaxTone")]
    public async Task LighterUnsafe_ReturnsMaxToneOnFailure()
    {
        var actual = Contrast.LighterUnsafe(tone: 100.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(100.0).Within(0.001);
    }

    [Test]
    [DisplayName("darker_impossibleRatioErrors")]
    public async Task Darker_ImpossibleRatio_ReturnsError()
    {
        var actual = Contrast.Darker(tone: 10.0, ratio: 20.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("darker_outOfBoundsInputAboveErrors")]
    public async Task Darker_ToneAboveBounds_ReturnsError()
    {
        var actual = Contrast.Darker(tone: 110.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("darker_outOfBoundsInputBelowErrors")]
    public async Task Darker_ToneBelowBounds_ReturnsError()
    {
        var actual = Contrast.Darker(tone: -10.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(-1.0).Within(0.001);
    }

    [Test]
    [DisplayName("darkerUnsafe_returnsMinTone")]
    public async Task DarkerUnsafe_ReturnsMinToneOnFailure()
    {
        var actual = Contrast.DarkerUnsafe(tone: 0.0, ratio: 2.0);
        await Assert.That(actual).IsEqualTo(0.0).Within(0.001);
    }
}
