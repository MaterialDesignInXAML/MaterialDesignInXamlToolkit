using System.ComponentModel;

namespace MaterialDesignThemes.UITests.WPF.UpDownControls;

public class DecimalUpDownTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public async Task NumericButtons_IncreaseAndDecreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        Assert.Equal("1", await textBox.GetText());
        Assert.Equal(1, await numericUpDown.GetValue());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });

        recorder.Success();
    }

    [Fact]
    public async Task NumericButtons_WithMaximum_DisablesPlusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="1" Maximum="2" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        Assert.False(await plusButton.GetIsEnabled());

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });

        Assert.True(await plusButton.GetIsEnabled());

        recorder.Success();
    }

    [Fact]
    public async Task NumericButtons_WithMinimum_DisablesMinusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="2" Minimum="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("1", await textBox.GetText());
            Assert.Equal(1, await numericUpDown.GetValue());
        });

        Assert.False(await minusButton.GetIsEnabled());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            Assert.Equal("2", await textBox.GetText());
            Assert.Equal(2, await numericUpDown.GetValue());
        });

        Assert.True(await minusButton.GetIsEnabled());

        recorder.Success();
    }

    [Fact]
    public async Task MaxAndMinAssignments_CoerceValueToBeInRange()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="2" />
        """);

        await numericUpDown.SetMaximum(1);
        Assert.Equal(1, await numericUpDown.GetValue());

        await numericUpDown.SetMinimum(3);
        Assert.Equal(3, await numericUpDown.GetValue());
        Assert.Equal(3, await numericUpDown.GetMaximum());

        await numericUpDown.SetMaximum(2);
        Assert.Equal(3, await numericUpDown.GetValue());
        Assert.Equal(3, await numericUpDown.GetMinimum());
        Assert.Equal(3, await numericUpDown.GetMaximum());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3654")]
    public async Task InternalTextBoxIsFocused_WhenGettingKeyboardFocus()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>("""
        <StackPanel>
          <TextBox />
          <materialDesign:DecimalUpDown />
        </StackPanel>
        """);

        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");
        var part_textBox = await stackPanel.GetElement<TextBox>("PART_TextBox");

        // Act
        await textBox.MoveKeyboardFocus();
        await Task.Delay(50);
        await textBox.SendInput(new KeyboardInput(Key.Tab));
        await Task.Delay(50);

        // Assert
        Assert.False(await textBox.GetIsFocused());
        Assert.True(await part_textBox.GetIsFocused());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3781")]
    public async Task IncreaseButtonClickWhenTextIsAboveMaximum_DoesNotIncreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>("""
        <StackPanel>
          <materialDesign:DecimalUpDown Minimum="-2.5" Maximum="2.5" Value="2.5" />
          <Button Content="AlternateFocusElement" />
        </StackPanel>
        """);
        var decimalUpDown = await stackPanel.GetElement<DecimalUpDown>();
        var textBox = await decimalUpDown.GetElement<TextBox>("PART_TextBox");
        var plusButton = await decimalUpDown.GetElement<RepeatButton>("PART_IncreaseButton");

        var button = await stackPanel.GetElement<Button>();

        await textBox.MoveKeyboardFocus();
        await textBox.SendKeyboardInput($"{ModifierKeys.Control}{Key.A}{ModifierKeys.None}30");
        await plusButton.LeftClick();

        //NB: Because the focus has not left the up down control, we don't expect the text to change
        Assert.Equal("30", await textBox.GetText());
        Assert.Equal(2.5m, await decimalUpDown.GetValue());

        recorder.Success();
    }

    [Theory]
    [Description("Issue 3781")]
    [InlineData("30")]
    [InlineData("abc")]
    [InlineData("2a")]
    public async Task LostFocusWhenTextIsInvalid_RevertsToOriginalValue(string inputText)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>("""
        <StackPanel>
          <materialDesign:DecimalUpDown Minimum="-2.5" Maximum="2.5" Value="2.5" />
          <Button Content="AlternateFocusElement" />
        </StackPanel>
        """);
        var decimalUpDown = await stackPanel.GetElement<DecimalUpDown>();
        var textBox = await decimalUpDown.GetElement<TextBox>("PART_TextBox");

        var button = await stackPanel.GetElement<Button>();

        await textBox.MoveKeyboardFocus();
        await textBox.SendKeyboardInput($"{ModifierKeys.Control}{Key.A}{ModifierKeys.None}{inputText}");
        await button.MoveKeyboardFocus();

        Assert.Equal("2.5", await textBox.GetText());
        Assert.Equal(2.5m, await decimalUpDown.GetValue());

        recorder.Success();
    }
}
