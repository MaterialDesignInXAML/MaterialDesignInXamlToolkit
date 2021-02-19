using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
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
    <TextBox VerticalAlignment=""Top""
             Text=""Some Text""
             materialDesign:TextFieldAssist.HasClearButton=""True"">
    </TextBox>
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement clearButton = await grid.GetElement("PART_ClearButton");

            await textBox.MoveKeyboardFocus();
            //Delay needed to accout for transition storyboard
            await Task.Delay(MaterialDesignTextBox.FocusedAimationTime);

            double initialHeight = await textBox.GetActualHeight();

            //Act
            await clearButton.Click();

            //Assert
            await Task.Delay(MaterialDesignTextBox.FocusedAimationTime);

            double height = await textBox.GetActualHeight();
            Assert.Equal(initialHeight, height);

            recorder.Success();
        }

        [Fact]
        [Description("Issue 1883")]
        public async Task OnClearButtonWithHintShown_ControlHeighDoesNotChange()
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
    </TextBox>
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
    <TextBox Style=""{StaticResource MaterialDesignFilledTextBox}""
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
            await Wait.For(async () => Assert.Equal(initialHeight, await textBox.GetActualHeight()));
            Rect rect = await textBox.GetCoordinates();
            Assert.Equal(initialRect, rect);
            recorder.Success();
        }

        [Fact]
        [Description("Issue 2002")]
        public async Task OnTextBoxDisabled_FloatingHintBackgroundIsOpaque()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Background=""Red"">
    <TextBox
        Style=""{StaticResource MaterialDesignOutlinedTextBox}""
        VerticalAlignment=""Top""
        Height=""100""
        Text=""Some content to force hint to float""
        IsEnabled=""false""
        Margin=""30""
        materialDesign:HintAssist.Hint=""This is a text area""/>
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            //textFieldGrid is the element just inside of the border
            IVisualElement textFieldGrid = await textBox.GetElement("grid");
            IVisualElement hintBackground = await textBox.GetElement("HintBackgroundBorder");

            Color background = await hintBackground.GetEffectiveBackground(textFieldGrid);

            Assert.Equal(255, background.A);
            recorder.Success();
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnTextBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox VerticalAlignment=""Top""
             Text=""Some Text""
             materialDesign:HintAssist.HelperTextFontSize=""20"">
    </TextBox>
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement helpTextBlock = await textBox.GetElement("/Grid/Canvas/TextBlock");
            
            double fontSize = await helpTextBlock.GetProperty<double>(TextBlock.FontSizeProperty.Name);

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Issue 2203")]
        public async Task OnOutlinedTextBox_FloatingHintOffsetWithinRange()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox
        Style=""{StaticResource MaterialDesignOutlinedTextBox}""
        VerticalAlignment=""Top""
        materialDesign:HintAssist.Hint=""This is a hint""
    />
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement hint = await textBox.GetElement("Hint");

            Point floatingOffset = await hint.GetProperty<Point>(SmartHint.FloatingOffsetProperty);

            Assert.Equal(0, floatingOffset.X);
            Assert.InRange(floatingOffset.Y, -22, -20);

            recorder.Success();
        }

        [Fact]
        public async Task CharacterCount_WithMaxLengthSet_IsDisplayed()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox
        MaxLength=""10""
    />
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement characterCounter = await textBox.GetElement("CharacterCounterTextBlock");

            Assert.Equal("0 / 10", await characterCounter.GetText());

            await textBox.SetText("12345");

            Assert.Equal("5 / 10", await characterCounter.GetText());

            recorder.Success();
        }

        [Fact]
        public async Task CharacterCount_WithoutMaxLengthSet_IsCollapsed()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox />
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement characterCounter = await textBox.GetElement("CharacterCounterTextBlock");

            Assert.False(await characterCounter.GetIsVisible());

            recorder.Success();
        }

        [Fact]
        public async Task CharacterCount_WithMaxLengthSetAndCharacterCounterVisibilityCollapsed_IsNotDisplayed()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement grid = await LoadXaml(@"
<Grid Margin=""30"">
    <TextBox
        MaxLength=""10""
        materialDesign:TextFieldAssist.CharacterCounterVisibility=""Collapsed""
    />
</Grid>");
            IVisualElement textBox = await grid.GetElement("/TextBox");
            IVisualElement characterCounter = await textBox.GetElement("CharacterCounterTextBlock");

            Assert.False(await characterCounter.GetIsVisible());

            recorder.Success();
        }
    }
}
