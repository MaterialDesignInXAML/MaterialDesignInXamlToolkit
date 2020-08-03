using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.TextBox
{
    public class TextBoxTests : TestBase
    {
        public TextBoxTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        [Description("Issue 1883")]
        public async Task OnClearButtonShown_ControlHeighDoesNotChange()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox Style=""{StaticResource MaterialDesignFloatingHintTextBox}""
        VerticalAlignment=""Top""
        materialDesign:TextFieldAssist.HasClearButton=""True""
        materialDesign:TextFieldAssist.PrefixText =""$""
        materialDesign:TextFieldAssist.SuffixText = ""mm"">
        <materialDesign:HintAssist.Hint>
            <StackPanel Orientation=""Horizontal"" Margin=""-2 0 0 0"">
                <materialDesign:PackIcon Kind=""AccessPoint"" />
                <TextBlock>WiFi</TextBlock>
            </StackPanel>
         </materialDesign:HintAssist.Hint >
    </TextBox >
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement clearButton = await grid.GetElement("PART_ClearButton");

            double initialHeight = await textBox.GetActualHeight();

            //Act
            await textBox.MoveKeyboardFocus();
            //Delay needed to accout for transition storyboard
            await Task.Delay(MaterialDesignTextBox.FocusedAimationTime);

            //Assert
            double height = await textBox.GetActualHeight();
            Assert.Equal(initialHeight, height);

            recorder.Success();
        }

        [Fact]
        [Description("Issue 1979")]
        public async Task OnTextCleared_MultilineTextBox()
        {
            await using var recorder = new TestRecorder(App);

            //Arrange
            IVisualElement grid = await LoadXaml(@"
<Grid>
    <TextBox Style=""{StaticResource MaterialDesignFilledTextFieldTextBox}""
             materialDesign:HintAssist.Hint=""Floating hint in a box""
             VerticalAlignment=""Top""/>
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");

            Rect initialRect = await textBox.GetCoordinates();
            double initialHeight = await textBox.GetActualHeight();

            await textBox.SetText($"Line 1{Environment.NewLine}Line 2");

            double twoLineHeight = await textBox.GetActualHeight();

            //Act
            await textBox.SetText("");

            //Assert
            double height = await textBox.GetActualHeight();
            Assert.Equal(initialHeight, height);
            Rect rect = await textBox.GetCoordinates();
            Assert.Equal(initialRect, rect);


            recorder.Success();
        }
    }
}
