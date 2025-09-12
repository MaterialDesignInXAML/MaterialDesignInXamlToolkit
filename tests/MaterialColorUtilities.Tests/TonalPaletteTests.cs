namespace MaterialColorUtilities.Tests;

public sealed class TonalPaletteTests
{
    private static readonly int[] CommonTones = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 95, 99, 100];

    private static int[] BuildArgbList(TonalPalette p) => CommonTones.Select(p.Tone).ToArray();

    [Test]
    [DisplayName("[FromHueAndChroma] tones of blue")]
    public async Task FromHueAndChroma_Tones_Of_Blue()
    {
        var hctBlue = Hct.FromInt(unchecked((int)0xFF0000FF));
        var tones = TonalPalette.FromHueAndChroma(hctBlue.Hue, hctBlue.Chroma);

        await Assert.That(tones.Tone(0)).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(tones.Tone(10)).IsEqualTo(unchecked((int)0xFF00006E));
        await Assert.That(tones.Tone(20)).IsEqualTo(unchecked((int)0xFF0001AC));
        await Assert.That(tones.Tone(30)).IsEqualTo(unchecked((int)0xFF0000EF));
        await Assert.That(tones.Tone(40)).IsEqualTo(unchecked((int)0xFF343DFF));
        await Assert.That(tones.Tone(50)).IsEqualTo(unchecked((int)0xFF5A64FF));
        await Assert.That(tones.Tone(60)).IsEqualTo(unchecked((int)0xFF7C84FF));
        await Assert.That(tones.Tone(70)).IsEqualTo(unchecked((int)0xFF9DA3FF));
        await Assert.That(tones.Tone(80)).IsEqualTo(unchecked((int)0xFFBEC2FF));
        await Assert.That(tones.Tone(90)).IsEqualTo(unchecked((int)0xFFE0E0FF));
        await Assert.That(tones.Tone(95)).IsEqualTo(unchecked((int)0xFFF1EFFF));
        await Assert.That(tones.Tone(99)).IsEqualTo(unchecked((int)0xFFFFFBFF));
        await Assert.That(tones.Tone(100)).IsEqualTo(unchecked((int)0xFFFFFFFF));

        // Tone not in Dart's commonTones: 3
        await Assert.That(tones.Tone(3)).IsEqualTo(unchecked((int)0xFF00003C));
    }

    [Test]
    [DisplayName("[FromHueAndChroma] asList (common tones)")]
    public async Task FromHueAndChroma_AsList_CommonTones()
    {
        var hctBlue = Hct.FromInt(unchecked((int)0xFF0000FF));
        var tones = TonalPalette.FromHueAndChroma(hctBlue.Hue, hctBlue.Chroma);

        var expected = new[]
        {
            unchecked((int)0xFF000000),
            unchecked((int)0xFF00006E),
            unchecked((int)0xFF0001AC),
            unchecked((int)0xFF0000EF),
            unchecked((int)0xFF343DFF),
            unchecked((int)0xFF5A64FF),
            unchecked((int)0xFF7C84FF),
            unchecked((int)0xFF9DA3FF),
            unchecked((int)0xFFBEC2FF),
            unchecked((int)0xFFE0E0FF),
            unchecked((int)0xFFF1EFFF),
            unchecked((int)0xFFFFFBFF),
            unchecked((int)0xFFFFFFFF),
        };

        var actual = BuildArgbList(tones);

        // Compare element-wise
        await Assert.That(actual.Length).IsEqualTo(expected.Length);
        for (int i = 0; i < expected.Length; i++)
            await Assert.That(actual[i]).IsEqualTo(expected[i]);
    }

    [Test]
    [DisplayName("[FromHueAndChroma] equivalence by generated tones (==/hashCode analog)")]
    public async Task FromHueAndChroma_Equivalence_By_Tones()
    {
        var hctAB = Hct.FromInt(unchecked((int)0xFF0000FF));
        var tonesA = TonalPalette.FromHueAndChroma(hctAB.Hue, hctAB.Chroma);
        var tonesB = TonalPalette.FromHueAndChroma(hctAB.Hue, hctAB.Chroma);

        var hctC = Hct.FromInt(unchecked((int)0xFF123456));
        var tonesC = TonalPalette.FromHueAndChroma(hctC.Hue, hctC.Chroma);

        var listA = BuildArgbList(tonesA);
        var listB = BuildArgbList(tonesB);
        var listC = BuildArgbList(tonesC);

        // A equals B functionally
        for (int i = 0; i < listA.Length; i++)
            await Assert.That(listA[i]).IsEqualTo(listB[i]);

        // B differs from C at least at one tone
        bool anyDiff = listB.Where((v, i) => v != listC[i]).Any();
        await Assert.That(anyDiff).IsTrue();
    }

    [Test]
    [DisplayName("KeyColor: exact chroma is available")]
    public async Task KeyColor_Exact_Chroma()
    {
        var palette = TonalPalette.FromHueAndChroma(50.0, 60.0);
        var result = palette.GetKeyColor();

        await Assert.That(result.Hue).IsEqualTo(50.0).Within(10.0);
        await Assert.That(result.Chroma).IsEqualTo(60.0).Within(0.5);
        await Assert.That(result.Tone).IsGreaterThan(0.0);
        await Assert.That(result.Tone).IsLessThan(100.0);
    }

    [Test]
    [DisplayName("KeyColor: requesting unusually high chroma")]
    public async Task KeyColor_Unusually_High_Chroma()
    {
        // For Hue 149, chroma peak ~89.6 — result should approach peak if 200 requested.
        var palette = TonalPalette.FromHueAndChroma(149.0, 200.0);
        var result = palette.GetKeyColor();

        await Assert.That(result.Hue).IsEqualTo(149.0).Within(10.0);
        await Assert.That(result.Chroma).IsGreaterThan(89.0);
        await Assert.That(result.Tone).IsGreaterThan(0.0);
        await Assert.That(result.Tone).IsLessThan(100.0);
    }

    [Test]
    [DisplayName("KeyColor: requesting unusually low chroma")]
    public async Task KeyColor_Unusually_Low_Chroma()
    {
        var palette = TonalPalette.FromHueAndChroma(50.0, 3.0);
        var result = palette.GetKeyColor();

        await Assert.That(result.Hue).IsEqualTo(50.0).Within(10.0);
        await Assert.That(result.Chroma).IsEqualTo(3.0).Within(0.5);
        await Assert.That(result.Tone).IsEqualTo(50.0).Within(0.5);
    }
}
