using System;
using System.Windows;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class UIElementExtensionsTests : IClassFixture<UIElementExtensionsTests.ArdornerFixture>
    {
        private readonly ArdornerFixture testFixture;

        public UIElementExtensionsTests(ArdornerFixture fixture)
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

        #region Class : ArdornerFixture
        public class ArdornerFixture : IDisposable
        {
            public UIElement testElement;
            public ArdornerFixture()
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