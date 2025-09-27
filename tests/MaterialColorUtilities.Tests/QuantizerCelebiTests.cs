namespace MaterialColorUtilities.Tests;

public sealed class QuantizerCelebiTests
{
    private const int Red   = unchecked((int)0xFFFF0000);
    private const int Green = unchecked((int)0xFF00FF00);
    private const int Blue  = unchecked((int)0xFF0000FF);
    private const int MaxColors = 256;

    [Test]
    [DisplayName("1R")]
    public async Task OneRed()
    {
        var result = QuantizerCelebi.Quantize([Red], MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Red);
    }

    [Test]
    [DisplayName("1G")]
    public async Task OneGreen()
    {
        var result = QuantizerCelebi.Quantize([Green], MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Green);
    }

    [Test]
    [DisplayName("1B")]
    public async Task OneBlue()
    {
        var result = QuantizerCelebi.Quantize([Blue], MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("5B")]
    public async Task FiveBlue()
    {
        var pixels = new[] { Blue, Blue, Blue, Blue, Blue };
        var result = QuantizerCelebi.Quantize(pixels, MaxColors);
        var colors = result.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
        await Assert.That(result[Blue]).IsEqualTo(5);
    }

    [Test]
    [DisplayName("1R 1G 1B")]
    public async Task OneRedOneGreenOneBlue()
    {
        var result = QuantizerCelebi.Quantize([Red, Green, Blue], MaxColors);

        // Content (set) must contain exactly the three colors
        var set = result.Keys.ToHashSet();
        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set.Contains(Blue)).IsTrue();
        await Assert.That(set.Contains(Red)).IsTrue();
        await Assert.That(set.Contains(Green)).IsTrue();

        // Each should have count 1
        await Assert.That(result[Blue]).IsEqualTo(1);
        await Assert.That(result[Red]).IsEqualTo(1);
        await Assert.That(result[Green]).IsEqualTo(1);

        // If you want to mimic the Dart list-order assertion, sort by population desc, then by ARGB for ties:
        var ordered = result.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Select(kv => kv.Key).ToList();
        await Assert.That(ordered.ToHashSet().SetEquals([Blue, Red, Green])).IsTrue();
    }

    [Test]
    [DisplayName("2R 3G")]
    public async Task TwoRedThreeGreen()
    {
        var pixels = new[] { Red, Red, Green, Green, Green };
        var result = QuantizerCelebi.Quantize(pixels, MaxColors);

        // Exactly two colors present
        var set = result.Keys.ToHashSet();
        await Assert.That(set.Count).IsEqualTo(2);
        await Assert.That(set.Contains(Green)).IsTrue();
        await Assert.That(set.Contains(Red)).IsTrue();

        // Counts must reflect populations
        await Assert.That(result[Green]).IsEqualTo(3);
        await Assert.That(result[Red]).IsEqualTo(2);

        // Verify population ordering (green first)
        var ordered = result.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).ToList();
        await Assert.That(ordered[0]).IsEqualTo(Green);
        await Assert.That(ordered[1]).IsEqualTo(Red);
    }
}
