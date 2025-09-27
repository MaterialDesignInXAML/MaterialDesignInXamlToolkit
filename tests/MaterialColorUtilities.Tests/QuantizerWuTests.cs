namespace MaterialColorUtilities.Tests;

public sealed class QuantizerWuTests
{
    private const int Red   = unchecked((int)0xFFFF0000);
    private const int Green = unchecked((int)0xFF00FF00);
    private const int Blue  = unchecked((int)0xFF0000FF);
    private const int MaxColors = 256;

    [Test]
    [DisplayName("1R (presence)")]
    public async Task OneRed_Presence()
    {
        var wu = new QuantizerWu();
        var result = wu.Quantize([Red], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
    }

    [Test]
    [DisplayName("1Rando")]
    public async Task OneRandom()
    {
        var wu = new QuantizerWu();
        var argb = unchecked((int)0xFF141216);
        var result = wu.Quantize([argb], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(argb);
    }

    [Test]
    [DisplayName("1R (exact)")]
    public async Task OneRed_Exact()
    {
        var wu = new QuantizerWu();
        var result = wu.Quantize([Red], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Red);
    }

    [Test]
    [DisplayName("1G")]
    public async Task OneGreen()
    {
        var wu = new QuantizerWu();
        var result = wu.Quantize([Green], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Green);
    }

    [Test]
    [DisplayName("1B")]
    public async Task OneBlue()
    {
        var wu = new QuantizerWu();
        var result = wu.Quantize([Blue], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("5B")]
    public async Task FiveBlue()
    {
        var wu = new QuantizerWu();
        var pixels = new[] { Blue, Blue, Blue, Blue, Blue };
        var result = wu.Quantize(pixels, MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        await Assert.That(colors.Count).IsEqualTo(1);
        await Assert.That(colors[0]).IsEqualTo(Blue);
    }

    [Test]
    [DisplayName("2R 3G")]
    public async Task TwoRed_ThreeGreen()
    {
        var wu = new QuantizerWu();
        var pixels = new[] { Red, Red, Green, Green, Green };
        var result = wu.Quantize(pixels, MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        // Set membership
        await Assert.That(colors.ToHashSet().Count).IsEqualTo(2);
        await Assert.That(colors.Contains(Green)).IsTrue();
        await Assert.That(colors.Contains(Red)).IsTrue();

        // Dart asserts order: [green, red]
        await Assert.That(colors[0]).IsEqualTo(Green);
        await Assert.That(colors[1]).IsEqualTo(Red);
    }

    [Test]
    [DisplayName("1R 1G 1B")]
    public async Task OneRed_OneGreen_OneBlue()
    {
        var wu = new QuantizerWu();
        var result = wu.Quantize([Red, Green, Blue], MaxColors);
        var colors = result.ColorToCount.Keys.ToList();

        // Set membership
        await Assert.That(colors.ToHashSet().Count).IsEqualTo(3);
        await Assert.That(colors.Contains(Blue)).IsTrue();
        await Assert.That(colors.Contains(Red)).IsTrue();
        await Assert.That(colors.Contains(Green)).IsTrue();

        // Dart asserts order: [blue, red, green]
        await Assert.That(colors[0]).IsEqualTo(Blue);
        await Assert.That(colors[1]).IsEqualTo(Red);
        await Assert.That(colors[2]).IsEqualTo(Green);
    }
}
