﻿using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        [Description("Issue 2495")]
        public async Task OnComboBox_WithClearButton_ClearsSelection()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <ComboBox materialDesign:HintAssist.Hint=""OS""
              materialDesign:TextFieldAssist.HasClearButton=""True""
              SelectedIndex=""1"">
        <ComboBoxItem Content=""Android"" />
        <ComboBoxItem Content=""iOS"" />
        <ComboBoxItem Content=""Linux"" />
        <ComboBoxItem Content=""Windows"" />
    </ComboBox>
</StackPanel>");
            var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
            var clearButton = await comboBox.GetElement<Button>("PART_ClearButton");

            int? selectedIndex = await comboBox.GetSelectedIndex();
            object? text = await comboBox.GetText();

            Assert.True(selectedIndex >= 0);
            Assert.NotNull(text);

            await clearButton.LeftClick();

            await Wait.For(async () =>
            {
                text = await comboBox.GetText();
                Assert.Null(text);
                selectedIndex = await comboBox.GetSelectedIndex();
                Assert.False(selectedIndex >= 0);
            });

            recorder.Success();
        }
    }
}
