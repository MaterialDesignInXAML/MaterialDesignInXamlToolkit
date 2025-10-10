namespace MaterialColorUtilities.Tests;

public sealed class HctTests
{
    [Test]
    [Skip("Takes a long time to run")]
    [DisplayName("HCT preserves original color for all opaque ARGB values")]
    public async Task Hct_Preserves_Original_Color_For_All_Opaque_ARGB()
    {
        // Iterate from 0xFF000000 to 0xFFFFFFFF inclusive (all opaque colors).
        uint argb = 0xFF000000u;
        while (true)
        {
            int argbInt = unchecked((int)argb);

            var hct = Hct.FromInt(argbInt);
            var reconstructedArgb = Hct.From(hct.Hue, hct.Chroma, hct.Tone).Argb;

            await Assert.That(reconstructedArgb).IsEqualTo(argbInt);

            if (argb == 0xFFFFFFFFu)
                break;

            argb++;
        }
    }

    private const int Black   = unchecked((int)0xFF000000);
    private const int White   = unchecked((int)0xFFFFFFFF);
    private const int Red     = unchecked((int)0xFFFF0000);
    private const int Green   = unchecked((int)0xFF00FF00);
    private const int Blue    = unchecked((int)0xFF0000FF);
    private const int MidGray = unchecked((int)0xFF777777);

    private static bool ColorIsOnBoundary(int argb) =>
        ColorUtils.RedFromArgb(argb) == 0   || ColorUtils.RedFromArgb(argb) == 255 ||
        ColorUtils.GreenFromArgb(argb) == 0 || ColorUtils.GreenFromArgb(argb) == 255 ||
        ColorUtils.BlueFromArgb(argb) == 0  || ColorUtils.BlueFromArgb(argb) == 255;

    [Test]
    [DisplayName("Hct equality/hash basics (by ARGB)")]
    public async Task Hct_Equality_Hash_Basics_ByArgb()
    {
        var a = Hct.FromInt(123);
        var b = Hct.FromInt(123);

        // Equality by ARGB value
        await Assert.That(a.Argb).IsEqualTo(b.Argb);

        // Hash by ARGB value
        await Assert.That(a.Argb.GetHashCode()).IsEqualTo(b.Argb.GetHashCode());
    }

    [Test]
    [DisplayName("CAM16 conversions are reflexive via viewed(sRGB)")]
    public async Task Conversions_Are_Reflexive()
    {
        var cam = Cam16.FromInt(Red);

        // pass an empty buffer again (API requires the arg)
        var xyz = cam.XyzInViewingConditions(ViewingConditions.DEFAULT);
        var color = ColorUtils.ArgbFromXyz(xyz[0], xyz[1], xyz[2]);

        await Assert.That(color).IsEqualTo(Red);
    }

    [Test] public async Task Y_Midgray() => await Assert.That(ColorUtils.YFromLstar(50.0)).IsEqualTo(18.418).Within(0.001);
    [Test] public async Task Y_Black()   => await Assert.That(ColorUtils.YFromLstar(0.0)).IsEqualTo(0.0).Within(0.001);
    [Test] public async Task Y_White()   => await Assert.That(ColorUtils.YFromLstar(100)).IsEqualTo(100.0).Within(0.001);

    [Test]
    [DisplayName("CAM16 red metrics")]
    public async Task Cam_Red()
    {
        var cam = Cam16.FromInt(Red);
        await Assert.That(cam.GetJ()).IsEqualTo(46.445).Within(0.001);
        await Assert.That(cam.GetChroma()).IsEqualTo(113.357).Within(0.001);
        await Assert.That(cam.GetHue()).IsEqualTo(27.408).Within(0.001);
        await Assert.That(cam.GetM()).IsEqualTo(89.494).Within(0.001);
        await Assert.That(cam.GetS()).IsEqualTo(91.889).Within(0.001);
        await Assert.That(cam.GetQ()).IsEqualTo(105.988).Within(0.001);
    }

    [Test]
    [DisplayName("CAM16 green metrics")]
    public async Task Cam_Green()
    {
        var cam = Cam16.FromInt(Green);
        await Assert.That(cam.GetJ()).IsEqualTo(79.331).Within(0.001);
        await Assert.That(cam.GetChroma()).IsEqualTo(108.410).Within(0.001);
        await Assert.That(cam.GetHue()).IsEqualTo(142.139).Within(0.001);
        await Assert.That(cam.GetM()).IsEqualTo(85.587).Within(0.001);
        await Assert.That(cam.GetS()).IsEqualTo(78.604).Within(0.001);
        await Assert.That(cam.GetQ()).IsEqualTo(138.520).Within(0.001);
    }

    [Test]
    [DisplayName("CAM16 blue metrics")]
    public async Task Cam_Blue()
    {
        var cam = Cam16.FromInt(Blue);
        await Assert.That(cam.GetJ()).IsEqualTo(25.465).Within(0.001);
        await Assert.That(cam.GetChroma()).IsEqualTo(87.230).Within(0.001);
        await Assert.That(cam.GetHue()).IsEqualTo(282.788).Within(0.001);
        await Assert.That(cam.GetM()).IsEqualTo(68.867).Within(0.001);
        await Assert.That(cam.GetS()).IsEqualTo(93.674).Within(0.001);
        await Assert.That(cam.GetQ()).IsEqualTo(78.481).Within(0.001);
    }

    [Test]
    [DisplayName("CAM16 black metrics")]
    public async Task Cam_Black()
    {
        var cam = Cam16.FromInt(Black);
        await Assert.That(cam.GetJ()).IsEqualTo(0.0).Within(0.001);
        await Assert.That(cam.GetChroma()).IsEqualTo(0.0).Within(0.001);
        await Assert.That(cam.GetHue()).IsEqualTo(0.0).Within(0.001);
        await Assert.That(cam.GetM()).IsEqualTo(0.0).Within(0.001);
        await Assert.That(cam.GetS()).IsEqualTo(0.0).Within(0.001);
        await Assert.That(cam.GetQ()).IsEqualTo(0.0).Within(0.001);
    }

    [Test]
    [DisplayName("CAM16 white metrics")]
    public async Task Cam_White()
    {
        var cam = Cam16.FromInt(White);
        await Assert.That(cam.GetJ()).IsEqualTo(100.0).Within(0.001);
        await Assert.That(cam.GetChroma()).IsEqualTo(2.869).Within(0.001);
        await Assert.That(cam.GetHue()).IsEqualTo(209.492).Within(0.001);
        await Assert.That(cam.GetM()).IsEqualTo(2.265).Within(0.001);
        await Assert.That(cam.GetS()).IsEqualTo(12.068).Within(0.001);
        await Assert.That(cam.GetQ()).IsEqualTo(155.521).Within(0.001);
    }

    [Test] public async Task GamutMap_Red()   => await AssertGamutMapIdentity(Red);
    [Test] public async Task GamutMap_Green() => await AssertGamutMapIdentity(Green);
    [Test] public async Task GamutMap_Blue()  => await AssertGamutMapIdentity(Blue);
    [Test] public async Task GamutMap_White() => await AssertGamutMapIdentity(White);
    // NOTE: The Dart “midgray” test body actually used `green`; keep parity by testing `green`.
    [Test] public async Task GamutMap_Midgray_ParityWithDart() => await AssertGamutMapIdentity(Green);

    private static async Task AssertGamutMapIdentity(int colorToTest)
    {
        var cam = Cam16.FromInt(colorToTest);
        var color = Hct.From(cam.GetHue(), cam.GetChroma(), ColorUtils.LstarFromArgb(colorToTest)).Argb;
        await Assert.That(color).IsEqualTo(colorToTest);
    }

    [Test]
    [DisplayName("HCT returns a sufficiently close color")]
    public async Task Hct_Returns_Sufficiently_Close_Color()
    {
        for (int hue = 15; hue < 360; hue += 30)
        for (int chroma = 0; chroma <= 100; chroma += 10)
        for (int tone = 20; tone <= 80; tone += 10)
        {
            var desc = $"H{hue} C{chroma} T{tone}";
            var hct = Hct.From(hue, chroma, tone);

            if (chroma > 0)
            {
                await Assert.That(hct.Hue)
                    .IsEqualTo(hue).Within(4.0)
                    .Because($"Hue should be close for {desc}");
            }

            // chroma ∈ [0, chroma + 2.5]
            await Assert.That(hct.Chroma).IsGreaterThanOrEqualTo(0.0)
                .Because($"Chroma lower bound for {desc}");
            await Assert.That(hct.Chroma).IsLessThanOrEqualTo(chroma + 2.5)
                .Because($"Chroma should be close or less for {desc}");

            if (hct.Chroma < chroma - 2.5)
            {
                // Non-sRGB request should land on sRGB cube boundary
                await Assert.That(ColorIsOnBoundary(hct.Argb))
                    .IsTrue()
                    .Because($"Out-of-gamut {desc} should be on sRGB boundary, got 0x{hct.Argb:X8}");
            }

            await Assert.That(hct.Tone)
                .IsEqualTo(tone).Within(0.5)
                .Because($"Tone should be close for {desc}");
        }
    }

    [Test]
    [DisplayName("CAM16 to XYZ (without preallocated array)")]
    public async Task Cam16_To_Xyz_NoArray()
    {
        var cam = Cam16.FromInt(Red);

        // pass an empty buffer to indicate "no preallocated array"
        var xyz = cam.XyzInViewingConditions(ViewingConditions.DEFAULT);

        await Assert.That(xyz[0]).IsEqualTo(41.23).Within(0.01);
        await Assert.That(xyz[1]).IsEqualTo(21.26).Within(0.01);
        await Assert.That(xyz[2]).IsEqualTo(1.93 ).Within(0.01);
    }

    [Test]
    [DisplayName("CAM16 to XYZ (with preallocated array)")]
    public async Task Cam16_To_Xyz_WithArray()
    {
        var cam = Cam16.FromInt(Red);

        // Provide a length-3 buffer
        var buffer = new double[3];
        var xyz = cam.XyzInViewingConditions(ViewingConditions.DEFAULT, buffer);

        await Assert.That(xyz[0]).IsEqualTo(41.23).Within(0.01);
        await Assert.That(xyz[1]).IsEqualTo(21.26).Within(0.01);
        await Assert.That(xyz[2]).IsEqualTo(1.93 ).Within(0.01);

        // Optional: verify the implementation reused the provided array
        // await Assert.That(ReferenceEquals(xyz, buffer)).IsTrue();
    }

    [Test]
    [DisplayName("Color Relativity — red in black/white")]
    public async Task ColorRelativity_Red()
    {
        var hct = Hct.FromInt(Red);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFF9F5C51));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFFFF5D48));
    }

    [Test]
    [DisplayName("Color Relativity — green in black/white")]
    public async Task ColorRelativity_Green()
    {
        var hct = Hct.FromInt(Green);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFFACD69D));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFF8EFF77));
    }

    [Test]
    [DisplayName("Color Relativity — blue in black/white")]
    public async Task ColorRelativity_Blue()
    {
        var hct = Hct.FromInt(Blue);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFF343654));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFF3F49FF));
    }

    [Test]
    [DisplayName("Color Relativity — white in black/white")]
    public async Task ColorRelativity_White()
    {
        var hct = Hct.FromInt(White);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFFFFFFFF));
    }

    [Test]
    [DisplayName("Color Relativity — midgray in black/white")]
    public async Task ColorRelativity_MidGray()
    {
        var hct = Hct.FromInt(MidGray);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFF605F5F));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFF8E8E8E));
    }

    [Test]
    [DisplayName("Color Relativity — black in black/white")]
    public async Task ColorRelativity_Black()
    {
        var hct = Hct.FromInt(Black);
        var inBlack = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(0.0)).Argb;
        var inWhite = hct.InViewingConditions(ViewingConditions.DefaultWithBackgroundLstar(100.0)).Argb;

        await Assert.That(inBlack ).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(inWhite ).IsEqualTo(unchecked((int)0xFF000000));
    }
}
