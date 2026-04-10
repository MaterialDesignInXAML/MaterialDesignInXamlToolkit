using System.ComponentModel;
using System.Globalization;
using System.Threading;


namespace MaterialDesignThemes.UITests.WPF.UpDownControls;

public class DecimalUpDownTests: TestBase
{
    [Test]
    [Arguments("en-US")]
    [Arguments("da-DK")]
    [Arguments("fa-IR")]
    [Arguments("ja-JP")]
    [Arguments("zh-CN")]
    public async Task NumericButtons_IncreaseAndDecreaseValue(string culture)
    {
        await using var recorder = new TestRecorder(App);

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="1" ValueStep="0.1" />
        """);
        await App.RemoteExecute<string>(SetCulture, [culture]);

        var plusButton = await numericUpDown.GetElement<RepeatButton>("PART_IncreaseButton");
        var minusButton = await numericUpDown.GetElement<RepeatButton>("PART_DecreaseButton");
        var textBox = await numericUpDown.GetElement<TextBox>("PART_TextBox");

        await Assert.That(await textBox.GetText()).IsEqualTo("1");
        await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);

        await plusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo(Convert.ToString(1.1, CultureInfo.GetCultureInfo(culture)));
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo((decimal)1.1);
        });

        await minusButton.LeftClick();
        await Wait.For(async () =>
        {
            await Assert.That(await textBox.GetText()).IsEqualTo("1");
            await Assert.That(await numericUpDown.GetValue()).IsEqualTo(1);
        });

        recorder.Success();

        static void SetCulture(Application _, string culture)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        }
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

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="2" Minimum="1" />
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

        var numericUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Value="2" />
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
          <materialDesign:DecimalUpDown />
        </StackPanel>
        """);

        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");
        var part_textBox = await stackPanel.GetElement<TextBox>("PART_TextBox");

        // Act
        await textBox.MoveKeyboardFocus();
        await Task.Delay(50, TestContext.Current!.CancellationToken);
        await textBox.SendInput(new KeyboardInput(Key.Tab));
        await Task.Delay(50, TestContext.Current.CancellationToken);

        // Assert
        await Assert.That(await textBox.GetIsFocused()).IsFalse();
        await Assert.That(await part_textBox.GetIsFocused()).IsTrue();

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
        await Assert.That(await textBox.GetText()).IsEqualTo("30");
        await Assert.That(await decimalUpDown.GetValue()).IsEqualTo(2.5m);

        recorder.Success();
    }

    [Test]
    [Description("Issue 3781")]
    [Arguments("30", 2.5)]
    [Arguments("abc", 2.5)]
    [Arguments("2a", 2)]
    public async Task LostFocusWhenTextIsInvalid_RevertsToOriginalValue(string inputText, decimal expectedValue)
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

        await Assert.That(await textBox.GetText()).IsEqualTo(expectedValue.ToString());
        await Assert.That(await decimalUpDown.GetValue()).IsEqualTo(expectedValue);

        recorder.Success();
    }

    [Test]
    public async Task UpDownButtonsVisibility_Collapsed_RemovesReservedPadding()
    {
        await using var recorder = new TestRecorder(App);

        Thickness originalPadding = new(4, 5, 6, 7);
        var decimalUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Padding="4,5,6,7"
                                      Width="160"
                                      UpDownButtonsVisibility="Collapsed" />
        """);
        var buttonsHost = await decimalUpDown.GetElement<StackPanel>("ButtonsHost");
        var textBox = await decimalUpDown.GetElement<TextBox>("PART_TextBox");

        await Wait.For(async () =>
        {
            await Assert.That(await buttonsHost.RemoteExecute(GetButtonsHostVisibility)).IsEqualTo(Visibility.Collapsed);
            await Assert.That(await buttonsHost.RemoteExecute(GetButtonsHostActualWidth)).IsEqualTo(0d);
            await Assert.That(await textBox.RemoteExecute(GetTextBoxPadding)).IsEqualTo(originalPadding);
        });

        recorder.Success();
    }

    [Test]
    public async Task UpDownButtonsVisibility_Hidden_PreservesReservedPadding()
    {
        await using var recorder = new TestRecorder(App);

        Thickness originalPadding = new(4, 5, 6, 7);
        var decimalUpDown = await LoadXaml<DecimalUpDown>("""
        <materialDesign:DecimalUpDown Padding="4,5,6,7"
                                      Width="160"
                                      UpDownButtonsVisibility="Hidden" />
        """);
        var buttonsHost = await decimalUpDown.GetElement<StackPanel>("ButtonsHost");
        var textBox = await decimalUpDown.GetElement<TextBox>("PART_TextBox");

        await Wait.For(async () =>
        {
            double buttonsWidth = await buttonsHost.RemoteExecute(GetButtonsHostActualWidth);
            Thickness textBoxPadding = await textBox.RemoteExecute(GetTextBoxPadding);

            await Assert.That(await buttonsHost.RemoteExecute(GetButtonsHostVisibility)).IsEqualTo(Visibility.Hidden);
            await Assert.That(buttonsWidth).IsGreaterThan(0d);
            await Assert.That(textBoxPadding.Left).IsEqualTo(originalPadding.Left);
            await Assert.That(textBoxPadding.Top).IsEqualTo(originalPadding.Top);
            await Assert.That(textBoxPadding.Bottom).IsEqualTo(originalPadding.Bottom);
            await Assert.That(textBoxPadding.Right).IsEqualTo(originalPadding.Right + buttonsWidth);
        });

        recorder.Success();
    }

    private static Visibility GetButtonsHostVisibility(StackPanel stackPanel) => stackPanel.Visibility;

    private static double GetButtonsHostActualWidth(StackPanel stackPanel) => stackPanel.ActualWidth;

    private static Thickness GetTextBoxPadding(TextBox textBox) => textBox.Padding;
}
