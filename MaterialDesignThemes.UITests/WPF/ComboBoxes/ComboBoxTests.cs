using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MaterialDesignThemes.Wpf;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ComboBoxes
{
    public class ComboBoxTests : TestBase
    {
        public ComboBoxTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <ComboBox
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
            var helpTextBlock = await comboBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetFontSize();

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnFilledComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <ComboBox
        Style=""{StaticResource MaterialDesignFilledComboBox}""
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
            var helpTextBlock = await comboBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetFontSize();

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Issue 2340")]
        public async Task OnEditableComboBoxOpenDown_TextBoxMaintainsItsPosition()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel HorizontalAlignment=""Center"" VerticalAlignment=""Bottom"">
    <ComboBox
        materialDesign:HintAssist.Hint=""Search""
        IsEditable=""True"">
        <ComboBoxItem Content=""Apple"" />
        <ComboBoxItem Content=""Banana"" />
        <ComboBoxItem Content=""Pear"" />
        <ComboBoxItem Content=""Orange"" />
    </ComboBox>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");

            var textBox = await comboBox.GetElement<TextBox>("PART_EditableTextBox");
            var toggleButton = await comboBox.GetElement<ToggleButton>("/ToggleButton");

            Rect closedCoordinates = await textBox.GetCoordinates();
            await toggleButton.LeftClick();
            await Task.Delay(300);
            await Wait.For(async () =>
            {
                Rect openCoordinates = await textBox.GetCoordinates();
                Vector distance = openCoordinates.TopLeft - closedCoordinates.TopLeft;
                Assert.True(distance.Length <= 1);
            });

            recorder.Success();
        }

        [Fact]
        [Description("Issue 2340")]
        public async Task OnEditableComboBoxOpenUp_TextBoxMaintainsItsPosition()
        {
            await using var recorder = new TestRecorder(App);

            IWindow window = await LoadXamlWindow(@"
<StackPanel HorizontalAlignment=""Center"" VerticalAlignment=""Bottom"">
    <ComboBox
        materialDesign:HintAssist.Hint=""Search""
        IsEditable=""True"">
        <ComboBoxItem Content=""Apple"" />
        <ComboBoxItem Content=""Banana"" />
        <ComboBoxItem Content=""Pear"" />
        <ComboBoxItem Content=""Orange"" />
    </ComboBox>
</StackPanel>");
            var comboBox = await window.GetElement<ComboBox>("/ComboBox");

            var textBox = await comboBox.GetElement<TextBox>("PART_EditableTextBox");
            var toggleButton = await comboBox.GetElement<ToggleButton>("/ToggleButton");

            //Push the window near the bottom of the screen
            Rect windowCoords = await window.GetCoordinates();
            Screen screen = Screen.FromPoint(windowCoords.BottomLeft);
            var amountToMove = screen.Bounds.Height - windowCoords.Bottom - 50;
            await window.SetTop(windowCoords.Top + amountToMove);

            Rect closedCoordinates = await textBox.GetCoordinates();
            await toggleButton.LeftClick();
            await Task.Delay(300);
            await Wait.For(async () =>
            {
                Rect openCoordinates = await textBox.GetCoordinates();
                Vector distance = openCoordinates.TopLeft - closedCoordinates.TopLeft;
                Assert.True(distance.Length <= 1);
            });

            recorder.Success();
        }
    }
}
