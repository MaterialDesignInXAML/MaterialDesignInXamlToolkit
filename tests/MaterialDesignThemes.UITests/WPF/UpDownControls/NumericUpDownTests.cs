using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.UpDownControls;


namespace MaterialDesignThemes.UITests.WPF.UpDownControls;


public class NumericUpDownTests : TestBase
{
    [Test]
    public async Task NumericButtons_IncreaseAndDecreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await Assert.That(await textBox.GetText()).IsEqualTo("1");
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("2");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(2);
        });

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("1");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);
        });

        recorder.Success();
    }

    [Test]
    public async Task NumericButtons_WithMaximum_DisablesPlusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" Maximum="2" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("2");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(2);
        });

        await Assert.That(await plusButton.GetIsEnabled()).IsFalse();

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("1");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);
        });

        await Assert.That(await plusButton.GetIsEnabled()).IsTrue();

        recorder.Success();
    }

    [Test]
    public async Task NumericButtons_WithMinimum_DisablesMinusButton()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" Minimum="1" />
        """);
        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("1");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);
        });

        await Assert.That(await minusButton.GetIsEnabled()).IsFalse();

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("2");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(2);
        });

        await Assert.That(await minusButton.GetIsEnabled()).IsTrue();

        recorder.Success();
    }

    [Test]
    public async Task MaxAndMinAssignments_CoerceValueToBeInRange()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" />
        """);

        await numericUpDown.SetMaximum(1);
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);

        await numericUpDown.SetMinimum(3);
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(3);
        await Assert.That(await numericUpDown.GetMaximum()).IsEqualTo(3);

        await numericUpDown.SetMaximum(2);
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(3);
        await Assert.That(await numericUpDown.GetMinimum()).IsEqualTo(3);
        await Assert.That(await numericUpDown.GetMaximum()).IsEqualTo(3);

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
          <materialDesign:NumericUpDown />
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
        await Assert.That(await textBox.GetIsFocused()).IsFalse();
        await Assert.That(await part_textBox.GetIsFocused()).IsTrue();

        recorder.Success();
    }

    [Test]
    [Skip("Needs XAMLTest 1.2.3 or later")]
    public async Task NumericUpDown_ValueSetGreaterThanMaximum_CoercesToMaximum()
    {
        await using var recorder = new TestRecorder(App);

        //Arrange
        var userControl = await LoadUserControl<BoundNumericUpDown>();
        var numericUpDown = await userControl.GetElement<NumericUpDown>();
        var buttonToFocus = await userControl.GetElement<Button>("btnToFocus");

        //Set the current value to the maximum
        await numericUpDown.SetMaximum(10);
        await numericUpDown.SetValue(10);

        //Act
        //Input a number greater than the maximum
        await numericUpDown.MoveKeyboardFocus();
        await numericUpDown.SendKeyboardInput($"99");

        await buttonToFocus.MoveKeyboardFocus();

        //Assert
        //The value and bound property in the VM should be set to the maximum
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(10);
        var viewModel = await userControl.GetProperty<BoundNumericUpDownViewModel>(nameof(BoundNumericUpDown.ViewModel));
        await Assert.That(viewModel?.Value).IsEqualTo(10);

        recorder.Success();
    }

    [Test]
    [Arguments(1, false, true)]
    [Arguments(5, true, true)]
    [Arguments(10, true, false)]
    [Description("Issue 3796")]
    public async Task NumericUpDown_WhenValueEqualsMinimum_DisableButtons(int value,
        bool decreaseEnabled, bool increaseEnabled)
    {
        await using var recorder = new TestRecorder(App);
        //Arrange
        var numericUpDown = await LoadXaml<NumericUpDown>($"""
        <materialDesign:NumericUpDown Value="{value}" Maximum="10" Minimum="1" />
        """);
        var increaseButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var decreaseButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");

        //Act

        bool increaseButtonEnabled = await increaseButton.GetIsEnabled();
        bool decreaseButtonEnabled = await decreaseButton.GetIsEnabled();

        //Assert
        await Assert.That(increaseButtonEnabled).IsEqualTo(increaseEnabled);
        await Assert.That(decreaseButtonEnabled).IsEqualTo(decreaseEnabled);

        recorder.Success();
    }
}
