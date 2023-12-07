using Shouldly;
using Xunit;

namespace MaterialDesignColors.Wpf.Fixture;

public class ResourceProviderFixture
{
    [Fact]
    public void ExcludesBlack()
    {
        var swatchesProvider = new SwatchesProvider();

        var containsBlack = swatchesProvider.Swatches.Any(
            swatch => string.Compare(swatch.Name, "Black", StringComparison.InvariantCultureIgnoreCase) == 0);

        containsBlack.ShouldBe(false);
    }

    [Fact]
    public void IncludesGrey()
    {
        var swatchesProvider = new SwatchesProvider();

        var containsBlack = swatchesProvider.Swatches.Any(
            swatch => string.Compare(swatch.Name, "Grey", StringComparison.InvariantCultureIgnoreCase) == 0);

        containsBlack.ShouldBe(true);
    }

    [Fact]
    public void BrownHasNoSecondary()
    {
        var swatchesProvider = new SwatchesProvider();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "brown");

        brownSwatch.SecondaryHues.ShouldNotBeNull();
        brownSwatch.SecondaryHues.Count().ShouldBe(0);
    }

    [Fact]
    public void BrownHasPrimaries()
    {
        var swatchesProvider = new SwatchesProvider();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "brown");

        brownSwatch.PrimaryHues.ShouldNotBeNull();
        brownSwatch.PrimaryHues.Count().ShouldBe(10);
    }

    [Fact]
    public void IndigoHasSecondaries()
    {
        var swatchesProvider = new SwatchesProvider();

        var brownSwatch = swatchesProvider.Swatches.Single(
            swatch => swatch.Name == "indigo");

        brownSwatch.SecondaryHues.ShouldNotBeNull();
        brownSwatch.SecondaryHues.Count().ShouldBe(4);
    }
}
