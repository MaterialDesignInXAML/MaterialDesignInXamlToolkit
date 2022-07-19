using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ComboBoxes;

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

    [Fact]
    [Description("Issue 2690")]
    public async Task OnEditableComboBox_WithDefaultContextMenu_ShowsCutCopyPaste()
    {
        await using var recorder = new TestRecorder(App);

        var comboBox = await LoadXaml<ComboBox>(@"
<ComboBox IsEditable=""True"" Width=""200"" Style=""{StaticResource MaterialDesignComboBox}"">
    <ComboBoxItem Content=""Select1"" />
    <ComboBoxItem>Select2</ComboBoxItem>
    <ComboBoxItem>Select3</ComboBoxItem>
    <ComboBoxItem>Select4</ComboBoxItem>
    <ComboBoxItem>Select5</ComboBoxItem>
</ComboBox>");

        await comboBox.RightClick();

        IVisualElement<ContextMenu>? contextMenu = await comboBox.GetContextMenu();
        Assert.True(contextMenu is not null, "No context menu set on the ComboBox");

        await AssertMenu("Cut");
        await AssertMenu("Copy");
        await AssertMenu("Paste");

        recorder.Success();

        async Task AssertMenu(string menuHeader)
        {
            var menuItem = await contextMenu!.GetElement(ElementQuery.PropertyExpression<MenuItem>(x => x.Header, menuHeader));
            Assert.True(menuItem is not null, $"{menuHeader} menu item not found");
        }
    }

    [Fact]
    [Description("Issue 2713")]
    public async Task OnEditableComboBox_ClickInTextArea_FocusesTextBox()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel Orientation=""Horizontal"">
    <ComboBox x:Name=""EditableComboBox"" IsEditable=""True"" Style=""{StaticResource MaterialDesignComboBox}"">
        <ComboBoxItem>Select1</ComboBoxItem>
        <ComboBoxItem>Select2</ComboBoxItem>
        <ComboBoxItem IsSelected=""True"">Select3</ComboBoxItem>
        <ComboBoxItem>Select4</ComboBoxItem>
        <ComboBoxItem>Select5</ComboBoxItem>
    </ComboBox>
    <Button x:Name=""Button"" />
</StackPanel>");

        var comboBox = await stackPanel.GetElement<ComboBox>("EditableComboBox");
        var editableTextBox = await comboBox.GetElement<TextBox>("PART_EditableTextBox");
        var button = await stackPanel.GetElement<Button>("Button");

        // Open the combobox initially
        await comboBox.LeftClick(Position.RightCenter);
        await Task.Delay(50);   // Allow a little time for the drop-down to open (and property to change)
        bool wasOpenAfterClickOnToggleButton = await comboBox.GetIsDropDownOpen();

        // Focus (i.e. click) another element
        await button.LeftClick();

        // Click the editable TextBox of the ComboBox
        await editableTextBox.LeftClick();
        await Task.Delay(50);   // Allow a little time for the drop-down to open (and property to change)
        bool wasOpenAfterClickOnEditableTextBox = await comboBox.GetIsDropDownOpen();
        bool textBoxHasFocus = await editableTextBox.GetIsFocused();
        bool textBoxHasKeyboardFocus = await editableTextBox.GetIsKeyboardFocused();

        Assert.True(wasOpenAfterClickOnToggleButton, "ComboBox should have opened drop down when clicking the toggle button");
        Assert.False(wasOpenAfterClickOnEditableTextBox, "ComboBox should not have opened drop down when clicking the editable TextBox");
        Assert.True(textBoxHasFocus, "Editable TextBox should have focus");
        Assert.True(textBoxHasKeyboardFocus, "Editable TextBox should have keyboard focus");

        recorder.Success();
    }
}