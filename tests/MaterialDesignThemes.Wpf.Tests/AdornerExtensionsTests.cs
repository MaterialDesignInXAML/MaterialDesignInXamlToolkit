namespace MaterialDesignThemes.Wpf.Tests;

[ClassDataSource<AdornerFixture>(Shared = SharedType.PerClass)]
public class AdornerExtensionsTests
{
    private readonly AdornerFixture _testFixture;

    public AdornerExtensionsTests(AdornerFixture fixture)
    {
        _testFixture = fixture;
    }

    [Test]
    public async Task AddAdornerNullArgumentThrowsException()
    {
        await Assert.That(() => _testFixture._testElement.AddAdorner<BottomDashedLineAdorner>(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task AddAdornerToElementWithoutAdornerLayerThrowsException()
    {
        await Assert.That(() => _testFixture._testElement.AddAdorner(new BottomDashedLineAdorner(_testFixture._testElement))).ThrowsExactly<InvalidOperationException>();
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
