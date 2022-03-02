using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.PasswordBoxes
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
            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <PasswordBox materialDesign:TextFieldAssist.HasClearButton=""True""/>
</StackPanel>");
            var passwordBox = await stackPanel.GetElement<PasswordBox>("/PasswordBox");

            var initialRect = await passwordBox.GetCoordinates();

            //Act
            await passwordBox.SetPassword("x");

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

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <PasswordBox materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var passwordBox = await stackPanel.GetElement<PasswordBox>("/PasswordBox");
            var helpTextBlock = await passwordBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetFontSize();

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Issue 2495")]
        public async Task OnPasswordBox_WithClearButton_ClearsPassword()
        {
            await using var recorder = new TestRecorder(App);

            var grid = await LoadXaml<Grid>(@"
<Grid Margin=""30"">
    <PasswordBox materialDesign:TextFieldAssist.HasClearButton=""True"" />
</Grid>");
            var passwordBox = await grid.GetElement<PasswordBox>("/PasswordBox");
            var clearButton = await passwordBox.GetElement<Button>("PART_ClearButton");

            await passwordBox.SendKeyboardInput($"Test");

            string? password = await passwordBox.GetPassword();

            Assert.NotNull(password);

            await clearButton.LeftClick();

            await Wait.For(async () =>
            {
                password = await passwordBox.GetPassword();
                Assert.Null(password);
            });

            recorder.Success();
        }
    }
}
