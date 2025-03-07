using System.ComponentModel;


namespace MaterialDesignThemes.UITests.WPF.UpDownControls;

public class DecimalUpDownTests(ITestOutputHelper output) : TestBase(output)
{
    [Test]
    public async Task NumericButtons_IncreaseAndDecreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await Assert.Equal("1", await textBox.GetText());
        await Assert.Equal(1, await numericUpDown.GetValue());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.Equal("2", await textBox.GetText());
            await Assert.Equal(2, await numericUpDown.GetValue());
        });

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.Equal("1", await textBox.GetText());
            await Assert.Equal(1, await numericUpDown.GetValue());
        });

        recorder.Success();
    }

    [Test]
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
            await Assert.Equal("2", await textBox.GetText());
            await Assert.Equal(2, await numericUpDown.GetValue());
        });

        await Assert.False(await plusButton.GetIsEnabled());

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.Equal("1", await textBox.GetText());
            await Assert.Equal(1, await numericUpDown.GetValue());
        });

        await Assert.True(await plusButton.GetIsEnabled());

        recorder.Success();
    }

    [Test]
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
            await Assert.Equal("1", await textBox.GetText());
            await Assert.Equal(1, await numericUpDown.GetValue());
        });

        await Assert.False(await minusButton.GetIsEnabled());

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.Equal("2", await textBox.GetText());
            await Assert.Equal(2, await numericUpDown.GetValue());
        });

        await Assert.True(await minusButton.GetIsEnabled());

        recorder.Success();
    }

    [Test]
    public async Task MaxAndMinAssignments_CoerceValueToBeInRange()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="2" />
        """);

        await numericUpDown.SetMaximum(1);
        await Assert.Equal(1, await numericUpDown.GetValue());

        await numericUpDown.SetMinimum(3);
        await Assert.Equal(3, await numericUpDown.GetValue());
        await Assert.Equal(3, await numericUpDown.GetMaximum());

        await numericUpDown.SetMaximum(2);
        await Assert.Equal(3, await numericUpDown.GetValue());
        await Assert.Equal(3, await numericUpDown.GetMinimum());
        await Assert.Equal(3, await numericUpDown.GetMaximum());

        recorder.Success();
    }

    [Test]
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
        await Assert.False(await textBox.GetIsFocused());
        await Assert.True(await part_textBox.GetIsFocused());

        recorder.Success();
    }

    [Test]
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
        await Assert.Equal("30", await textBox.GetText());
        await Assert.Equal(2.5m, await decimalUpDown.GetValue());

        recorder.Success();
    }

    [Test]
    [Description("Issue 3781")]
    [Arguments("30")]
    [Arguments("abc")]
    [Arguments("2a")]
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

        await Assert.Equal("2.5", await textBox.GetText());
        await Assert.Equal(2.5m, await decimalUpDown.GetValue());

        recorder.Success();
    }
}
