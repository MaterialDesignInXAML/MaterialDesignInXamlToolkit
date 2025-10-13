using System.Windows.Media;

namespace MaterialColorUtilities.Tests;

public sealed class ColorUtilsTests
{
    private static IEnumerable<double> Range(double start, double stop, int caseCount)
    {
        if (caseCount <= 1)
        {
            yield return start;
            yield break;
        }

        var stepSize = (stop - start) / (caseCount - 1);
        for (var i = 0; i < caseCount; i++)
        {
            yield return start + stepSize * i;
        }
    }

    private static IReadOnlyList<int> RgbRange() => Range(0.0, 255.0, 8).Select(d => (int)Math.Round(d)).ToArray();

    private static IReadOnlyList<int> FullRgbRange() => Enumerable.Range(0, 256).ToArray();

    [Test]
    [DisplayName("range_integrity")]
    public async Task Range_Integrity()
    {
        // Dart: final range = _range(3.0, 9999.0, 1234);
        var range = Range(3.0, 9999.0, 1234).ToArray();

        // Dart: expect(range[i], closeTo(3 + 8.1070559611 * i, 1e-5));
        for (var i = 0; i < 1234; i++)
        {
            var expected = 3 + 8.1070559611 * i;
            await Assert.That(range[i]).IsEqualTo(expected).Within(1e-5);
        }
    }

    [Test]
    [DisplayName("argbFromRgb returns correct values")]
    public async Task ArgbFromRgb_KnownVectors()
    {
        // black
        await Assert.That(ColorUtils.ArgbFromRgb(0, 0, 0)).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(ColorUtils.ArgbFromRgb(0, 0, 0)).IsEqualTo(unchecked((int)4278190080));

        // white
        await Assert.That(ColorUtils.ArgbFromRgb(255, 255, 255)).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(ColorUtils.ArgbFromRgb(255, 255, 255)).IsEqualTo(unchecked((int)4294967295));

        // random color
        await Assert.That(ColorUtils.ArgbFromRgb(50, 150, 250)).IsEqualTo(unchecked((int)0xFF3296FA));
        await Assert.That(ColorUtils.ArgbFromRgb(50, 150, 250)).IsEqualTo(unchecked((int)4281505530));
    }

    [Test]
    [DisplayName("y_to_lstar_to_y")]
    public async Task Y_To_Lstar_To_Y_RoundTrip()
    {
        foreach (var y in Range(0, 100, 1001))
        {
            var l = ColorUtils.LstarFromY(y);
            var y2 = ColorUtils.YFromLstar(l);
            await Assert.That(y2).IsEqualTo(y).Within(1e-5);
        }
    }

    [Test]
    [DisplayName("lstar_to_y_to_lstar")]
    public async Task Lstar_To_Y_To_Lstar_RoundTrip()
    {
        foreach (var lstar in Range(0, 100, 1001))
        {
            var y = ColorUtils.YFromLstar(lstar);
            var l2 = ColorUtils.LstarFromY(y);
            await Assert.That(l2).IsEqualTo(lstar).Within(1e-5);
        }
    }

    [Test]
    [DisplayName("yFromLstar numeric vectors")]
    public async Task YFromLstar_Vectors()
    {
        async Task Check(double lstar, double expected) =>
            await Assert.That(ColorUtils.YFromLstar(lstar)).IsEqualTo(expected).Within(1e-5);

        await Check(0.0, 0.0);
        await Check(0.1, 0.0110705);
        await Check(0.2, 0.0221411);
        await Check(0.3, 0.0332116);
        await Check(0.4, 0.0442822);
        await Check(0.5, 0.0553528);
        await Check(1.0, 0.1107056);
        await Check(2.0, 0.2214112);
        await Check(3.0, 0.3321169);
        await Check(4.0, 0.4428225);
        await Check(5.0, 0.5535282);
        await Check(8.0, 0.8856451);
        await Check(10.0, 1.1260199);
        await Check(15.0, 1.9085832);
        await Check(20.0, 2.9890524);
        await Check(25.0, 4.4154767);
        await Check(30.0, 6.2359055);
        await Check(40.0, 11.2509737);
        await Check(50.0, 18.4186518);
        await Check(60.0, 28.1233342);
        await Check(70.0, 40.7494157);
        await Check(80.0, 56.6812907);
        await Check(90.0, 76.3033539);
        await Check(95.0, 87.6183294);
        await Check(99.0, 97.4360239);
        await Check(100.0, 100.0);
    }

    [Test]
    [DisplayName("lstarFromY numeric vectors")]
    public async Task LstarFromY_Vectors()
    {
        async Task Check(double y, double expected) =>
            await Assert.That(ColorUtils.LstarFromY(y)).IsEqualTo(expected).Within(1e-5);

        await Check(0.0, 0.0);
        await Check(0.1, 0.9032962);
        await Check(0.2, 1.8065925);
        await Check(0.3, 2.7098888);
        await Check(0.4, 3.6131851);
        await Check(0.5, 4.5164814);
        await Check(0.8856451, 8.0);
        await Check(1.0, 8.9914424);
        await Check(2.0, 15.4872443);
        await Check(3.0, 20.0438970);
        await Check(4.0, 23.6714419);
        await Check(5.0, 26.7347653);
        await Check(10.0, 37.8424304);
        await Check(15.0, 45.6341970);
        await Check(20.0, 51.8372115);
        await Check(25.0, 57.0754208);
        await Check(30.0, 61.6542222);
        await Check(40.0, 69.4695307);
        await Check(50.0, 76.0692610);
        await Check(60.0, 81.8381891);
        await Check(70.0, 86.9968642);
        await Check(80.0, 91.6848609);
        await Check(90.0, 95.9967686);
        await Check(95.0, 98.0335184);
        await Check(99.0, 99.6120372);
        await Check(100.0, 100.0);
    }

    [Test]
    [DisplayName("y continuity at l* = 8")]
    public async Task Y_Continuity()
    {
        const double epsilon = 1e-6;
        const double delta = 1e-8;
        var left = 8.0 - delta;
        var mid = 8.0;
        var right = 8.0 + delta;

        await Assert.That(ColorUtils.YFromLstar(left))
            .IsEqualTo(ColorUtils.YFromLstar(mid)).Within(epsilon);

        await Assert.That(ColorUtils.YFromLstar(right))
            .IsEqualTo(ColorUtils.YFromLstar(mid)).Within(epsilon);
    }

    [Test]
    [DisplayName("rgb -> xyz -> rgb roundtrip (approx)")]
    public async Task Rgb_To_Xyz_To_Rgb_Roundtrip()
    {
        var range = RgbRange();
        foreach (var r in range)
        foreach (var g in range)
        foreach (var b in range)
        {
            var argb = ColorUtils.ArgbFromRgb(r, g, b);
            var xyz = ColorUtils.XyzFromArgb(argb);
            var converted = ColorUtils.ArgbFromXyz(xyz[0], xyz[1], xyz[2]);

            await Assert.That((double)ColorUtils.RedFromArgb(converted))
                .IsEqualTo(r).Within(1.5);
            await Assert.That((double)ColorUtils.GreenFromArgb(converted))
                .IsEqualTo(g).Within(1.5);
            await Assert.That((double)ColorUtils.BlueFromArgb(converted))
                .IsEqualTo(b).Within(1.5);
        }
    }

    [Test]
    [DisplayName("rgb -> lab -> rgb roundtrip (approx)")]
    public async Task Rgb_To_Lab_To_Rgb_Roundtrip()
    {
        var range = RgbRange();
        foreach (var r in range)
        foreach (var g in range)
        foreach (var b in range)
        {
            var argb = ColorUtils.ArgbFromRgb(r, g, b);
            var lab = ColorUtils.LabFromArgb(argb);
            var converted = ColorUtils.ArgbFromLab(lab[0], lab[1], lab[2]);

            await Assert.That((double)ColorUtils.RedFromArgb(converted))
                .IsEqualTo(r).Within(1.5);
            await Assert.That((double)ColorUtils.GreenFromArgb(converted))
                .IsEqualTo(g).Within(1.5);
            await Assert.That((double)ColorUtils.BlueFromArgb(converted))
                .IsEqualTo(b).Within(1.5);
        }
    }

    [Test]
    [DisplayName("rgb -> l* -> rgb (exact, via argbFromLstar)")]
    public async Task Rgb_To_Lstar_To_Rgb()
    {
        foreach (var component in FullRgbRange())
        {
            var argb = ColorUtils.ArgbFromRgb(component, component, component);
            var lstar = ColorUtils.LstarFromArgb(argb);
            var converted = ColorUtils.ArgbFromLstar(lstar);
            await Assert.That(converted).IsEqualTo(argb);
        }
    }

    [Test]
    [DisplayName("rgb -> l* -> y commutes with Y from XYZ")]
    public async Task Rgb_To_Lstar_To_Y_Commutes()
    {
        var range = RgbRange();
        foreach (var r in range)
        foreach (var g in range)
        foreach (var b in range)
        {
            var argb = ColorUtils.ArgbFromRgb(r, g, b);
            var lstar = ColorUtils.LstarFromArgb(argb);
            var y1 = ColorUtils.YFromLstar(lstar);
            var y2 = ColorUtils.XyzFromArgb(argb)[1];

            await Assert.That(y1).IsEqualTo(y2).Within(1e-5);
        }
    }

    [Test]
    [DisplayName("l* -> rgb -> y commutes (looser tol)")]
    public async Task Lstar_To_Rgb_To_Y_Commutes()
    {
        foreach (var lstar in Range(0, 100, 1001))
        {
            var argb = ColorUtils.ArgbFromLstar(lstar);
            var yFromRgb = ColorUtils.XyzFromArgb(argb)[1];
            var yFromL = ColorUtils.YFromLstar(lstar);

            await Assert.That(yFromRgb).IsEqualTo(yFromL).Within(1.0);
        }
    }

    [Test]
    [DisplayName("linearize -> delinearize is identity on 0..255")]
    public async Task Linearize_Delinearize_RoundTrip()
    {
        foreach (var c in FullRgbRange())
        {
            var lin = ColorUtils.Linearized(c);
            var converted = ColorUtils.Delinearized(lin);
            await Assert.That(converted).IsEqualTo(c);
        }
    }

    public static IEnumerable<Func<(int argb, Color color)>> TestColors()
    {
        yield return () => (unchecked((int)0xFFFF0000), Colors.Red);
        yield return () => (unchecked((int)0xFF00FF00), Colors.Lime);
        yield return () => (unchecked((int)0xFF0000FF), Colors.Blue);
        yield return () => (unchecked((int)0xFFFF00FF), Colors.Magenta);
        yield return () => (unchecked((int)0xFFFFFF00), Colors.Yellow);
        yield return () => (unchecked((int)0xFF00FFFF), Colors.Cyan);
        yield return () => (unchecked((int)0xFFFFFFFF), Colors.White);
        yield return () => (unchecked((int)0xFF000000), Colors.Black);
        yield return () => (unchecked((int)0x00FFFFFF), Colors.Transparent);
    }

    [Test]
    [DisplayName("colorFromArgb returns known Colors")]
    [MethodDataSource(nameof(TestColors))]
    public async Task ColorFromArgb_KnownColors(int argb, Color color)
    {
        var converted = ColorUtils.ColorFromArgb(argb);

        string result = $"{converted.A:x}{converted.R:x}{converted.G:x}{converted.B:X}";
        string expected = $"{color.A:x}{color.R:x}{color.G:x}{color.B:X}";

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [DisplayName("argbFromColor returns known ints")]
    [MethodDataSource(nameof(TestColors))]
    public async Task ArgbFromColor_KnownColors(int argb, Color color)
    {
        string result = ColorUtils.ArgbFromColor(color).ToString("X");
        string expected = argb.ToString("X");

        await Assert.That(result).IsEqualTo(expected);
    }
}
