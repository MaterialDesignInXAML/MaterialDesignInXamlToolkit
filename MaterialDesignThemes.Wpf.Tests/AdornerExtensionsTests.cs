namespace MaterialDesignThemes.Wpf.Tests;

public class AdornerExtensionsTests : IClassFixture<AdornerExtensionsTests.AdornerFixture>
{
    private readonly AdornerFixture _testFixture;

    public AdornerExtensionsTests(AdornerFixture fixture)
    {
        _testFixture = fixture;
    }

    [Fact]
    public void AddAdornerNullArgumentThrowsException()
    {
        Assert.Throws<ArgumentNullException>(
            () => _testFixture._testElement.AddAdorner<BottomDashedLineAdorner>(null!));
    }

    [Fact]
    public void AddAdornerToElementWithoutAdornerLayerThrowsException()
    {
        Assert.Throws<InvalidOperationException>(
            () => _testFixture._testElement.AddAdorner(new BottomDashedLineAdorner(_testFixture._testElement)));
    }

    #region Class : AdornerFixture
    public class AdornerFixture : IDisposable
    {
        public UIElement _testElement;
        public AdornerFixture()
        {
            _testElement = new UIElement();
        }

        public void Dispose()
        {
            // No cleanup required
        }
    }
    #endregion
}
