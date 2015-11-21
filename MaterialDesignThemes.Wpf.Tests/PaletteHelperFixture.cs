using Moq;
using NSubstitute;
using Rhino.Mocks;
using Xunit;
using MockRepository = Rhino.Mocks.MockRepository;

namespace MaterialDesignThemes.Wpf.Tests
{    
    /// <summary>
    /// Proves that PaletteHelper is mockable, thus allowing TDD for view models which may want to change 
    /// an application's palette.
    /// </summary>
    /// <remarks>
    /// This is not an exhaustive test of the class itself.
    /// </remarks>
    public class PaletteHelperFixture
    {
        [Fact]
        public void IsMockableWithRhino()
        {
            var paletteHelper = MockRepository.GenerateStub<PaletteHelper>();

            paletteHelper.SetLightDark(true);

            paletteHelper.AssertWasCalled(ph => ph.SetLightDark(true));
        }

        [Fact]
        public void IsMockableWithMoq()
        {
            var mock = new Mock<PaletteHelper>();            

            mock.Object.SetLightDark(true);

            mock.Verify(ph => ph.SetLightDark(true));
        }

        [Fact]
        public void IsMockableWithNSubstitute()
        {
            var mock = Substitute.For<PaletteHelper>();

            mock.SetLightDark(true);

            mock.Received(1).SetLightDark(true);
        }
    }
}
