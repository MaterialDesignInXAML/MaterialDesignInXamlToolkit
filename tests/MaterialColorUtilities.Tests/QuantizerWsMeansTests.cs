namespace MaterialColorUtilities.Tests;

public sealed class QuantizerWsMeansTests
{
    private const int Red      = unchecked((int)0xFFFF0000);
    private const int Green    = unchecked((int)0xFF00FF00);
    private const int Blue     = unchecked((int)0xFF0000FF);
    private const int OneRando = unchecked((int)0xFF141216);
    private const int MaxColors = 256;

    private static int[] NoStartingClusters => [];

    [Test]
    [DisplayName("1Rando")]
    public async Task OneRandomColor()
    {
        var result = QuantizerWsMeans.Quantize([OneRando], NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(OneRando);
    }

    [Test]
    [DisplayName("1R (presence)")]
    public async Task OneRed_Presence()
    {
        var result = QuantizerWsMeans.Quantize([Red], NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
    }

    [Test]
    [DisplayName("1R (exact)")]
    public async Task OneRed_Exact()
    {
        var result = QuantizerWsMeans.Quantize([Red], NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Red);
    }

    [Test]
    [DisplayName("1G")]
    public async Task OneGreen()
    {
        var result = QuantizerWsMeans.Quantize([Green], NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Green);
    }

    [Test]
    [DisplayName("1B")]
    public async Task OneBlue()
    {
        var result = QuantizerWsMeans.Quantize([Blue], NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("5B")]
    public async Task FiveBlue()
    {
        var pixels = new[] { Blue, Blue, Blue, Blue, Blue };
        var result = QuantizerWsMeans.Quantize(pixels, NoStartingClusters, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
        await Assert.That(result[Blue]).IsEqualTo(5);
    }
}
