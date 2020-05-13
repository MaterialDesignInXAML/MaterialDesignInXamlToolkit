using System;
using System.Windows;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests
{
    public class ArdornerAssistTests : IClassFixture<ArdornerAssistTests.ArdornerFixture>
    {
        private readonly ArdornerFixture testFixture;

        public ArdornerAssistTests(ArdornerFixture fixture)
        {
            testFixture = fixture;
        }

        [Fact]
        public void AddAdornerNullArgumentThrowsException()
        { 
            Assert.Throws<ArgumentNullException>(
                () => testFixture.testElement.AddAdornerOfType<BottomDashedLineAdorner>(null));
        }

        [Fact]
        public void AddAdornerToElementWithoutAdornerLayerThrowsException()
        {
            Assert.Throws<InvalidOperationException>(
                () => testFixture.testElement.AddAdornerOfType(new BottomDashedLineAdorner(testFixture.testElement)));
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