using System.ComponentModel;
using MaterialDesignThemes.UITests.Samples.UpDownControls;

namespace MaterialDesignThemes.UITests.WPF.UpDownControls;


public class NumericUpDownTests(ITestOutputHelper output) : TestBase(output)
{
    [Fact]
    public async Task NumericButtons_IncreaseAndDecreaseValue()
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" />
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" Maximum="2" />
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" Minimum="1" />
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" />
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
        Assert.False(await textBox.GetIsFocused());
        Assert.True(await part_textBox.GetIsFocused());

        recorder.Success();
    }

    [Fact(Skip = "Needs XAMLTest 1.2.3 or later")]
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
        Assert.Equal(10, await numericUpDown.GetValue());
        var viewModel = await userControl.GetProperty<BoundNumericUpDownViewModel>(nameof(BoundNumericUpDown.ViewModel));
        Assert.Equal(10, viewModel?.Value);

        recorder.Success();
    }

    [Theory]
    [InlineData(1, false, true)]
    [InlineData(5, true, true)]
    [InlineData(10, true, false)]
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
        Assert.Equal(increaseEnabled, increaseButtonEnabled);
        Assert.Equal(decreaseEnabled, decreaseButtonEnabled);

        recorder.Success();
    }
}
