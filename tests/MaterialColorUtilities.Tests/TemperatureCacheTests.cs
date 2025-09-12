namespace MaterialColorUtilities.Tests;

public sealed class TemperatureCacheTests
{
    [Test]
    [DisplayName("TemperatureCache.rawTemperature")]
    public async Task Raw_Temperature()
    {
        var blueTemp  = TemperatureCache.RawTemperature(Hct.FromInt(unchecked((int)0xFF0000FF)));
        var redTemp   = TemperatureCache.RawTemperature(Hct.FromInt(unchecked((int)0xFFFF0000)));
        var greenTemp = TemperatureCache.RawTemperature(Hct.FromInt(unchecked((int)0xFF00FF00)));
        var whiteTemp = TemperatureCache.RawTemperature(Hct.FromInt(unchecked((int)0xFFFFFFFF)));
        var blackTemp = TemperatureCache.RawTemperature(Hct.FromInt(unchecked((int)0xFF000000)));

        await Assert.That(blueTemp ).IsEqualTo(-1.393).Within(0.001);
        await Assert.That(redTemp  ).IsEqualTo( 2.351).Within(0.001);
        await Assert.That(greenTemp).IsEqualTo(-0.267).Within(0.001);
        await Assert.That(whiteTemp).IsEqualTo(-0.5  ).Within(0.001);
        await Assert.That(blackTemp).IsEqualTo(-0.5  ).Within(0.001);
    }

    [Test]
    [DisplayName("TemperatureCache.inputRelativeTemperature")]
    public async Task Relative_Temperature()
    {
        var blue  = Hct.FromInt(unchecked((int)0xFF0000FF));
        var red   = Hct.FromInt(unchecked((int)0xFFFF0000));
        var green = Hct.FromInt(unchecked((int)0xFF00FF00));
        var white = Hct.FromInt(unchecked((int)0xFFFFFFFF));
        var black = Hct.FromInt(unchecked((int)0xFF000000));

        var blueTemp  = new TemperatureCache(blue ).GetRelativeTemperature(blue);
        var redTemp   = new TemperatureCache(red  ).GetRelativeTemperature(red);
        var greenTemp = new TemperatureCache(green).GetRelativeTemperature(green);
        var whiteTemp = new TemperatureCache(white).GetRelativeTemperature(white);
        var blackTemp = new TemperatureCache(black).GetRelativeTemperature(black);

        await Assert.That(blueTemp ).IsEqualTo(0.000).Within(0.001);
        await Assert.That(redTemp  ).IsEqualTo(1.000).Within(0.001);
        await Assert.That(greenTemp).IsEqualTo(0.467).Within(0.001);
        await Assert.That(whiteTemp).IsEqualTo(0.500).Within(0.001);
        await Assert.That(blackTemp).IsEqualTo(0.500).Within(0.001);
    }

    [Test]
    [DisplayName("TemperatureCache.complement")]
    public async Task Complement()
    {
        var blueComp  = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF0000FF))).GetComplement().Argb;
        var redComp   = new TemperatureCache(Hct.FromInt(unchecked((int)0xFFFF0000))).GetComplement().Argb;
        var greenComp = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF00FF00))).GetComplement().Argb;
        var whiteComp = new TemperatureCache(Hct.FromInt(unchecked((int)0xFFFFFFFF))).GetComplement().Argb;
        var blackComp = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF000000))).GetComplement().Argb;

        await Assert.That(blueComp ).IsEqualTo(unchecked((int)0xFF9D0002));
        await Assert.That(redComp  ).IsEqualTo(unchecked((int)0xFF007BFC));
        await Assert.That(greenComp).IsEqualTo(unchecked((int)0xFFFFD2C9));
        await Assert.That(whiteComp).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(blackComp).IsEqualTo(unchecked((int)0xFF000000));
    }

    [Test]
    [DisplayName("TemperatureCache.analogous")]
    public async Task Analogous()
    {
        // Blue
        var blueAnalogous = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF0000FF)))
            .GetAnalogousColors()
            .Select(h => h.Argb)
            .ToList();

        await Assert.That(blueAnalogous[0]).IsEqualTo(unchecked((int)0xFF00590C));
        await Assert.That(blueAnalogous[1]).IsEqualTo(unchecked((int)0xFF00564E));
        await Assert.That(blueAnalogous[2]).IsEqualTo(unchecked((int)0xFF0000FF));
        await Assert.That(blueAnalogous[3]).IsEqualTo(unchecked((int)0xFF6700CC));
        await Assert.That(blueAnalogous[4]).IsEqualTo(unchecked((int)0xFF81009F));

        // Red
        var redAnalogous = new TemperatureCache(Hct.FromInt(unchecked((int)0xFFFF0000)))
            .GetAnalogousColors()
            .Select(h => h.Argb)
            .ToList();

        await Assert.That(redAnalogous[0]).IsEqualTo(unchecked((int)0xFFF60082));
        await Assert.That(redAnalogous[1]).IsEqualTo(unchecked((int)0xFFFC004C));
        await Assert.That(redAnalogous[2]).IsEqualTo(unchecked((int)0xFFFF0000));
        await Assert.That(redAnalogous[3]).IsEqualTo(unchecked((int)0xFFD95500));
        await Assert.That(redAnalogous[4]).IsEqualTo(unchecked((int)0xFFAF7200));

        // Green
        var greenAnalogous = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF00FF00)))
            .GetAnalogousColors()
            .Select(h => h.Argb)
            .ToList();

        await Assert.That(greenAnalogous[0]).IsEqualTo(unchecked((int)0xFFCEE900));
        await Assert.That(greenAnalogous[1]).IsEqualTo(unchecked((int)0xFF92F500));
        await Assert.That(greenAnalogous[2]).IsEqualTo(unchecked((int)0xFF00FF00));
        await Assert.That(greenAnalogous[3]).IsEqualTo(unchecked((int)0xFF00FD6F));
        await Assert.That(greenAnalogous[4]).IsEqualTo(unchecked((int)0xFF00FAB3));

        // Black → all black
        var blackAnalogous = new TemperatureCache(Hct.FromInt(unchecked((int)0xFF000000)))
            .GetAnalogousColors()
            .Select(h => h.Argb)
            .ToList();

        await Assert.That(blackAnalogous[0]).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(blackAnalogous[1]).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(blackAnalogous[2]).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(blackAnalogous[3]).IsEqualTo(unchecked((int)0xFF000000));
        await Assert.That(blackAnalogous[4]).IsEqualTo(unchecked((int)0xFF000000));

        // White → all white
        var whiteAnalogous = new TemperatureCache(Hct.FromInt(unchecked((int)0xFFFFFFFF)))
            .GetAnalogousColors()
            .Select(h => h.Argb)
            .ToList();

        await Assert.That(whiteAnalogous[0]).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(whiteAnalogous[1]).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(whiteAnalogous[2]).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(whiteAnalogous[3]).IsEqualTo(unchecked((int)0xFFFFFFFF));
        await Assert.That(whiteAnalogous[4]).IsEqualTo(unchecked((int)0xFFFFFFFF));
    }
}
