using System.Threading.Tasks;
using System.Windows.Media;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.Button
{
    public class RaisedButtonTests : TestBase
    {
        public RaisedButtonTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public async Task OnLoad_ThemeBrushesSet()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement button = await LoadXaml(@"<Button Content=""Button"" />");
            Color midColor = await GetThemeColor("PrimaryHueMidBrush");

            //Act
            Color? color = await button.GetBackgroundColor();

            //Assert
            Assert.Equal(midColor, color);

            recorder.Success();
        }
    }
}
