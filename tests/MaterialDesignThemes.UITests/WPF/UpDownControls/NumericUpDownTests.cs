using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using Google.Protobuf.WellKnownTypes;
using MaterialDesignThemes.UITests.Samples.UpDownControls;


namespace MaterialDesignThemes.UITests.WPF.UpDownControls;


public class NumericUpDownTests(ITestOutputHelper output) : TestBase(output)
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="1" Maximum="2" />
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" Minimum="1" />
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

        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Value="2" />
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
        await Assert.False(await textBox.GetIsFocused());
        await Assert.True(await part_textBox.GetIsFocused());

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
        await Assert.Equal(10, await numericUpDown.GetValue());
        var viewModel = await userControl.GetProperty<BoundNumericUpDownViewModel>(nameof(BoundNumericUpDown.ViewModel));
        await Assert.Equal(10, viewModel?.Value);

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
        await Assert.Equal(increaseEnabled, increaseButtonEnabled);
        await Assert.Equal(decreaseEnabled, decreaseButtonEnabled);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3827")]
    public async Task NumericUpDown_WhenBindingUpdateTriggerIsPropertyChanged_ItUpdatesBeforeLoosingFocus()
    {
        await using var recorder = new TestRecorder(App);
        //Arrange
        var numericUpDown = await LoadXaml<NumericUpDown>("""
        <materialDesign:NumericUpDown Tag="{Binding Value, RelativeSource={RelativeSource Self}, UpdateSourceTrigger=PropertyChanged}" Maximum="10" Minimum="1" />
        """);
        
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");
        //Act
        await textBox.MoveKeyboardFocus();
        await textBox.SendKeyboardInput($"{ModifierKeys.Control}{Key.A}{ModifierKeys.None}4");

        //Act
        object? tag = await numericUpDown.GetTag();

        //Assert
        Assert.Equal("4", tag?.ToString());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3827")]
    public async Task NumericUpDown_WhenBindingUpdateTriggerIsLostFocus_ItDoesNotUpdateUntilItLoosesFocus()
    {
        await using var recorder = new TestRecorder(App);
        //Arrange
        var userControl = await LoadUserControl<BoundNumericUpDown>();
        var numericUpDown = await userControl.GetElement<NumericUpDown>();
        var buttonToFocus = await userControl.GetElement<Button>("btnToFocus");
        await numericUpDown.SetValue(2);

        static void SetBindingToLostFocus(NumericUpDown numericUpDown)
        {
            var binding = new Binding(nameof(NumericUpDown.Value))
            {
                Path = new(nameof(BoundNumericUpDownViewModel.Value)),
                UpdateSourceTrigger = UpdateSourceTrigger.LostFocus
            };
            BindingOperations.SetBinding(numericUpDown, NumericUpDown.ValueProperty, binding);
        }
        await numericUpDown.RemoteExecute(SetBindingToLostFocus);

        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        static int GetViewModelValue(NumericUpDown numericUpDown)
        {
            return ((BoundNumericUpDownViewModel)numericUpDown.DataContext).Value;
        }

        //Act
        await textBox.MoveKeyboardFocus();
        await textBox.SendKeyboardInput($"{ModifierKeys.Control}{Key.A}{ModifierKeys.None}4");

        //Act
        int valueBeforeLostFocus = await numericUpDown.RemoteExecute(GetViewModelValue);
        await textBox.SendKeyboardInput($"{Key.Tab}");
        int valueAfterLostFocus = await numericUpDown.RemoteExecute(GetViewModelValue);


        //Assert
        Assert.Equal("2", valueBeforeLostFocus.ToString());
        Assert.Equal("4", valueAfterLostFocus.ToString());

        recorder.Success();
    }
}
