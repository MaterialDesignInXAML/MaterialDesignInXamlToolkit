using System.ComponentModel;
using MaterialDesignThemes.UITests.WPF.TextBoxes;

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
        await Wait.For(async () => await contextMenu!.GetIsVisible());
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

        // Open the ComboBox initially
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

    [Theory]
    [InlineData("MaterialDesignFloatingHintComboBox", null)]
    [InlineData("MaterialDesignFloatingHintComboBox", 5)]
    [InlineData("MaterialDesignFloatingHintComboBox", 20)]
    [InlineData("MaterialDesignFilledComboBox", null)]
    [InlineData("MaterialDesignFilledComboBox", 5)]
    [InlineData("MaterialDesignFilledComboBox", 20)]
    [InlineData("MaterialDesignOutlinedComboBox", null)]
    [InlineData("MaterialDesignOutlinedComboBox", 5)]
    [InlineData("MaterialDesignOutlinedComboBox", 20)]
    public async Task ComboBox_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var comboBox = await LoadXaml<ComboBox>($@"
<ComboBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"" />
");

        var contentHost = await comboBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await comboBox.GetElement<SmartHint>("Hint");
        var helperText = await comboBox.GetElement<TextBlock>("HelperTextTextBlock");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? helperTextCoordinates = await helperText.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintComboBox", null)]
    [InlineData("MaterialDesignFloatingHintComboBox", 5)]
    [InlineData("MaterialDesignFloatingHintComboBox", 20)]
    [InlineData("MaterialDesignFilledComboBox", null)]
    [InlineData("MaterialDesignFilledComboBox", 5)]
    [InlineData("MaterialDesignFilledComboBox", 20)]
    [InlineData("MaterialDesignOutlinedComboBox", null)]
    [InlineData("MaterialDesignOutlinedComboBox", 5)]
    [InlineData("MaterialDesignOutlinedComboBox", 20)]
    public async Task ComboBox_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var comboBox = await LoadXaml<ComboBox>($@"
<ComboBox {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
  <ComboBox.Text>
    <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
      <Binding.ValidationRules>
        <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
      </Binding.ValidationRules>
    </Binding>
  </ComboBox.Text>
</ComboBox>
", ("local", typeof(NotEmptyValidationRule)));

        var contentHost = await comboBox.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await comboBox.GetElement<SmartHint>("Hint");
        var errorViewer = await comboBox.GetElement<Border>("DefaultErrorViewer");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? errorViewerCoordinates = await errorViewer.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData(HorizontalAlignment.Left)]
    [InlineData(HorizontalAlignment.Right)]
    [InlineData(HorizontalAlignment.Center)]
    [InlineData(HorizontalAlignment.Stretch)]
    [Description("Issue 3433")]
    public async Task ComboBox_WithHorizontalContentAlignment_RespectsAlignment(HorizontalAlignment alignment)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>($"""
            <StackPanel>
              <ComboBox HorizontalContentAlignment="{alignment}">
                <ComboBoxItem Content="TEST" IsSelected="True" />
              </ComboBox>
            </StackPanel>
            """);
        var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
        var selectedItemPresenter = await comboBox.GetElement<ContentPresenter>("contentPresenter");

        Assert.Equal(alignment, await selectedItemPresenter.GetHorizontalAlignment());

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintComboBox", 0, 0, 0, 1)]
    [InlineData("MaterialDesignFilledComboBox", 0, 0, 0, 1)]
    [InlineData("MaterialDesignOutlinedComboBox", 2, 2, 2, 2)]
    [Description("Issue 3623")]
    public async Task ComboBox_BorderShouldDependOnAppliedStyle(string style, double left, double top, double right, double bottom)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>($$"""
             <StackPanel>
               <ComboBox Style="{StaticResource {{style}}}">
                 <ComboBoxItem Content="TEST" IsSelected="True" />
               </ComboBox>
             </StackPanel>
             """);
        var comboBox = await stackPanel.GetElement<ComboBox>("/ComboBox");
        var border = await comboBox.GetElement<Border>("OuterBorder");

        await comboBox.LeftClick();

        Thickness thickness = await border.GetBorderThickness();
        Assert.Equal(new Thickness(left, top, right, bottom), thickness);

        recorder.Success();
    }
}
