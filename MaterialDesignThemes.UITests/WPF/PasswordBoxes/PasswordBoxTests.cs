using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.PasswordBox;

namespace MaterialDesignThemes.UITests.WPF.PasswordBoxes
{
    public class PasswordBoxTests : TestBase
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

        [Fact]
        [Description("PR 2828 and Issue 2930")]
        public async Task RevealPasswordBox_WithBoundPasswordProperty_RespectsThreeWayBinding()
        {
            await using var recorder = new TestRecorder(App);

            await App.InitializeWithMaterialDesign();
            IWindow window = await App.CreateWindow<BoundPasswordBoxWindow>();
            var userControl = await window.GetElement<BoundPasswordBox>();
            await userControl.SetProperty(nameof(BoundPasswordBox.UseRevealStyle), true);
            var passwordBox = await userControl.GetElement<PasswordBox>("PasswordBox");
            var clearTextPasswordTextBox = await passwordBox.GetElement<TextBox>("RevealPasswordTextBox");
            var revealPasswordButton = await passwordBox.GetElement<ToggleButton>("RevealPasswordButton");
            
            // Act 1 (Update in PasswordBox updates VM and RevealPasswordTextBox)
            await passwordBox.SendKeyboardInput($"1");
            string? boundText1 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
            string? password1 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
            string? clearTextPassword1 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

            // Act 2 (Update in RevealPasswordTextBox updates PasswordBox and VM)
            await revealPasswordButton.LeftClick();
            await Task.Delay(100);   // Wait for the "clear text TextBox" to become visible
            await clearTextPasswordTextBox.SendKeyboardInput($"2");
            string? boundText2 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
            string? password2 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
            string? clearTextPassword2 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

            // Act 3 (Update in VM updates PasswordBox and RevealPasswordTextBox)
            await userControl.SetProperty(nameof(BoundPasswordBox.ViewModelPassword), "3");
            string? boundText3 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
            string? password3 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));
            string? clearTextPassword3 = await clearTextPasswordTextBox.GetProperty<string>(TextBox.TextProperty);

            // Assert
            Assert.Equal("1", boundText1);
            Assert.Equal("1", password1);
            Assert.Equal("1", clearTextPassword1);

            Assert.Equal("12", boundText2);
            Assert.Equal("12", password2);
            Assert.Equal("12", clearTextPassword2);

            Assert.Equal("3", boundText3);
            Assert.Equal("3", password3);
            Assert.Equal("3", clearTextPassword3);

            recorder.Success();
        }

        [Fact]
        [Description("Issue 2930")]
        public async Task PasswordBox_WithBoundPasswordProperty_RespectsBinding()
        {
            await using var recorder = new TestRecorder(App);

            await App.InitializeWithMaterialDesign();
            IWindow window = await App.CreateWindow<BoundPasswordBoxWindow>();
            var userControl = await window.GetElement<BoundPasswordBox>();
            await userControl.SetProperty(nameof(BoundPasswordBox.UseRevealStyle), false);
            var passwordBox = await userControl.GetElement<PasswordBox>("PasswordBox");

            // Act 1 (Update in PasswordBox updates VM)
            await passwordBox.SendKeyboardInput($"1");
            string? boundText1 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
            string? password1 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));

            // Act 2 (Update in VM updates PasswordBox)
            await userControl.SetProperty(nameof(BoundPasswordBox.ViewModelPassword), "2");
            string? boundText2 = await userControl.GetProperty<string>(nameof(BoundPasswordBox.ViewModelPassword));
            string? password2 = await passwordBox.GetProperty<string>(nameof(PasswordBox.Password));

            // Assert
            Assert.Equal("1", boundText1);
            Assert.Equal("1", password1);

            Assert.Equal("2", boundText2);
            Assert.Equal("2", password2);

            recorder.Success();
        } 
    }
}
