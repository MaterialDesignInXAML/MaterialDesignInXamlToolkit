using System.Threading.Tasks;
using XamlTest;
using Xunit;
using Xunit.Abstractions;
using Controls = System.Windows.Controls;

namespace MaterialDesignThemes.UITests.WPF.PasswordBox
{
    public class PasswordBoxTests: TestBase
    {
        public PasswordBoxTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        public async Task OnClearButtonShown_LayoutDoesNotChange()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            var stackPanel = await LoadXaml(@"
<StackPanel>
    <PasswordBox materialDesign:TextFieldAssist.HasClearButton=""True""/>
    <TextBox/>
</StackPanel>");
            var passwordBox = await stackPanel.GetElement("/PasswordBox");
            var textBox = await stackPanel.GetElement("/TextBox");

            var initialRect = await textBox.GetCoordinates();

            //Act
            await passwordBox.SetProperty<string>(nameof(Controls.PasswordBox.Password), "x");

            //Assert
            var rect = await textBox.GetCoordinates();
            Assert.Equal(initialRect, rect);

            recorder.Success();
        }
    }
}
