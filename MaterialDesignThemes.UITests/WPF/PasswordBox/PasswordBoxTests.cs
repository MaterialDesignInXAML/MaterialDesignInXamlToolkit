using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
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
</StackPanel>");
            var passwordBox = await stackPanel.GetElement("/PasswordBox");

            var initialRect = await passwordBox.GetCoordinates();

            //Act
            await passwordBox.SetProperty(nameof(Controls.PasswordBox.Password), "x");

            //Assert
            var rect = await passwordBox.GetCoordinates();
            Assert.Equal(initialRect, rect);

            recorder.Success();
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnPasswordBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml(@"
<StackPanel>
    <PasswordBox materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var timePicker = await stackPanel.GetElement("/PasswordBox");
            IVisualElement helpTextBlock = await timePicker.GetElement("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetProperty<double>(TextBlock.FontSizeProperty.Name);

            Assert.Equal(20, fontSize);
            recorder.Success();
        }
    }
}
