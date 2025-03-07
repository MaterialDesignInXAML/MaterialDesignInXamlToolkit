namespace MaterialDesignColors.Wpf.Tests;

public class ResourceProviderFixture
{
    [Test]
    public async Task ExcludesBlack()
    {
        SwatchesProvider swatchesProvider = new ();

        bool containsBlack = swatchesProvider.Swatches.Any(
            swatch => string.Compare(swatch.Name, "Black", StringComparison.InvariantCultureIgnoreCase) == 0);

        await Assert.That(containsBlack).IsFalse();
    }

    [Test]
    public async Task IncludesGrey()
    {
        SwatchesProvider swatchesProvider = new ();

        bool containsBlack = swatchesProvider.Swatches.Any(
            swatch => string.Compare(swatch.Name, "Grey", StringComparison.InvariantCultureIgnoreCase) == 0);

        await Assert.That(containsBlack).IsTrue();
    }

    [Test]
    public async Task BrownHasNoSecondary()
    {
        SwatchesProvider swatchesProvider = new ();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "brown");

        await Assert.That(brownSwatch.SecondaryHues).IsNotNull();
        await Assert.That(brownSwatch.SecondaryHues.Count).IsEqualTo(0);
    }

    [Test]
    public async Task BrownHasPrimaries()
    {
        SwatchesProvider swatchesProvider = new ();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "brown");

        await Assert.That(brownSwatch.PrimaryHues).IsNotNull();
        await Assert.That(brownSwatch.PrimaryHues.Count).IsEqualTo(10);
    }

    [Test]
    public async Task IndigoHasSecondaries()
    {
        SwatchesProvider swatchesProvider = new ();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "indigo");

        await Assert.That(brownSwatch.SecondaryHues).IsNotNull();
        await Assert.That(brownSwatch.SecondaryHues.Count).IsEqualTo(4);
    }
}
