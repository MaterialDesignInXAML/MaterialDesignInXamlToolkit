using System;
using System.Windows;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class UIElementExtensionsTests : IClassFixture<UIElementExtensionsTests.AdornerFixture>
    {
        private readonly AdornerFixture testFixture;

        public UIElementExtensionsTests(AdornerFixture fixture)
        {
            testFixture = fixture;
        }

        [Fact]
        public void AddAdornerNullArgumentThrowsException()
        { 
            Assert.Throws<ArgumentNullException>(
                () => testFixture.testElement.AddAdorner<BottomDashedLineAdorner>(null!));
        }

        [Fact]
        public void AddAdornerToElementWithoutAdornerLayerThrowsException()
        {
            Assert.Throws<InvalidOperationException>(
                () => testFixture.testElement.AddAdorner(new BottomDashedLineAdorner(testFixture.testElement)));
        }

        #region Class : AdornerFixture
        public class AdornerFixture : IDisposable
        {
            public UIElement testElement;
            public AdornerFixture()
            {
                testElement = new UIElement();
            }

            public void Dispose()
            {
                // No cleanup required
            }
        }
        #endregion
    }
}