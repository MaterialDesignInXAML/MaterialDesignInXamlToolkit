using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
namespace MaterialDesignThemes.UITests.WPF.TimePickers;

public class TimePickerTests : TestBase
{
    public TimePickerTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2020, 8, 10)]
    public async Task OnTextChangedIfSelectedTimeIsNonNull_DatePartDoesNotChange(int year, int month, int day)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker Language=""en-US"" />
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        await timePicker.SetSelectedTime(new DateTime(year, month, day));
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SetText("1:10 AM");

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(new DateTime(year, month, day, 1, 10, 0), actual);

        recorder.Success();
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2020, 8, 10)]
    public async Task OnLostFocusIfSelectedTimeIsNonNull_DatePartDoesNotChange(int year, int month, int day)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker Language=""en-US"" />
    <TextBox />
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        await timePicker.SetSelectedTime(new DateTime(year, month, day));
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SetText("1:10");
        await textBox.MoveKeyboardFocus();

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(new DateTime(year, month, day, 1, 10, 0), actual);

        recorder.Success();
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(2020, 8, 2)]
    public async Task OnClockPickedIfSelectedTimeIsNonNull_DatePartDoesNotChange(int year, int month, int day)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");

        await timePicker.SetSelectedTime(new DateTime(year, month, day));
        await timePicker.PickClock(1, 10);

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(new DateTime(year, month, day, 1, 10, 0), actual);

        recorder.Success();
    }

    [Fact]
    public async Task OnTextChangedIfSelectedTimeIsNull_DatePartWillBeToday()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker Language=""en-US"" />
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        var today = DateTime.Today;
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SetText("1:23 AM");

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(AdjustToday(today, actual).Add(new TimeSpan(1, 23, 0)), actual);

        recorder.Success();
    }

    [Fact]
    public async Task OnLostFocusIfSelectedTimeIsNull_DatePartWillBeToday()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker Language=""en-US"" />
    <TextBox />
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        var today = DateTime.Today;
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SetText("1:23");
        await textBox.MoveKeyboardFocus();

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(AdjustToday(today, actual).Add(new TimeSpan(1, 23, 0)), actual);

        recorder.Success();
    }

    [Fact]
    public async Task OnClockPickedIfSelectedTimeIsNull_DatePartWillBeToday()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");

        var today = DateTime.Today;
        await timePicker.PickClock(1, 23);

        var actual = await timePicker.GetSelectedTime();
        Assert.Equal(AdjustToday(today, actual).Add(new TimeSpan(1, 23, 0)), actual);

        recorder.Success();
    }

    private static DateTime AdjustToday(DateTime today, DateTime? adjustTo)
    {
        if (adjustTo.HasValue && today != adjustTo.Value.Date)
        {
            var tomorrow = today.AddDays(1);
            if (tomorrow == adjustTo.Value.Date)
                today = tomorrow;
        }
        return today;
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnLostFocusIfTimeHasBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
    <TextBox/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}");
        await textBox.MoveKeyboardFocus();

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:02 AM", actual);

        recorder.Success();
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnLostFocusIfTimeHasNotBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
    <TextBox/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var textBox = await stackPanel.GetElement<TextBox>("/TextBox");

        await timePicker.SetSelectedTime(new DateTime (2020, 8, 10, 1, 2, 0)); 
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}");
        await textBox.MoveKeyboardFocus();

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:02 AM", actual);

        recorder.Success();
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnEnterKeyDownIfTimeHasNotBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        await timePicker.SetSelectedTime(new DateTime (2020, 8, 10, 1, 2, 0)); 
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}{Key.Enter}");

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:02 AM", actual);

        recorder.Success();
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnEnterKeyDownIfTimeHasBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        await timePicker.SetSelectedTime(new DateTime (2020, 8, 10, 1, 3, 0)); 
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}{Key.Enter}");

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:02 AM", actual);

        recorder.Success();
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnTimePickedIfTimeHasBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        await timePicker.SetSelectedTime(new DateTime(2020, 8, 10, 1, 2, 0));
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}");
        await timePicker.PickClock(1, 3);

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:03 AM", actual);

        recorder.Success();
    }

    [Theory]
    [InlineData("1:2")]
    [InlineData("1:02")]
    [InlineData("1:02 AM")]
    public async Task OnTimePickedIfTimeHasNotBeenChanged_TextWillBeFormatted(string text)
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");

        await timePicker.SetSelectedTime(new DateTime(2020, 8, 10, 1, 2, 0));
        await timePickerTextBox.MoveKeyboardFocus();
        await timePickerTextBox.SendKeyboardInput($"{text}");
        await timePicker.PickClock(1, 2);

        var actual = await timePickerTextBox.GetText();
        Assert.Equal("1:02 AM", actual);

        recorder.Success();
    }

    [Fact]
    [Description("Pull Request 2192")]
    public async Task OnTimePickerHelperTextFontSize_ChangesHelperTextFontSize()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <materialDesign:TimePicker
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var helpTextBlock = await timePicker.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

        double fontSize = await helpTextBlock.GetFontSize();

        Assert.Equal(20, fontSize);
        recorder.Success();
    }

    [Fact]
    [Description("Issue 2737")]
    public async Task OutlinedTimePicker_RespectsActiveAndInactiveBorderThickness_WhenAttachedPropertiesAreSet()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var expectedInactiveBorderThickness = new Thickness(4, 3, 2, 1);
        var expectedActiveBorderThickness = new Thickness(1, 2, 3, 4);
        var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <materialDesign:TimePicker Style=""{{StaticResource MaterialDesignOutlinedTimePicker}}""
      materialDesign:TimePickerAssist.OutlinedBorderInactiveThickness=""{expectedInactiveBorderThickness}""
      materialDesign:TimePickerAssist.OutlinedBorderActiveThickness=""{expectedActiveBorderThickness}"">
      <materialDesign:TimePicker.Text>
        <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
          <Binding.ValidationRules>
            <local:OnlyTenOClockValidationRule ValidatesOnTargetUpdated=""True""/>
          </Binding.ValidationRules>
        </Binding>
      </materialDesign:TimePicker.Text>
    </materialDesign:TimePicker>
    <Button x:Name=""Button"" Content=""Some Button"" Margin=""0,20,0,0"" />
</StackPanel>", ("local", typeof(OnlyTenOClockValidationRule)));
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        await timePicker.SetProperty(TimePicker.TextProperty, "10:00");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var button = await stackPanel.GetElement<Button>("Button");

        // Act
        await button.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var inactiveBorderThickness = await timePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);
        await timePickerTextBox.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var hoverBorderThickness = await timePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);
        await timePickerTextBox.LeftClick();
        await Task.Delay(50);   // Wait for the visual change
        var focusedBorderThickness = await timePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);

        // TODO: It would be cool if a validation error could be set via XAMLTest without the need for the Binding and ValidationRules elements in the XAML above.
        await timePicker.SetProperty(TimePicker.TextProperty, "11:00");
        await Task.Delay(50);   // Wait for the visual change
        var withErrorBorderThickness = await timePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);

        // Assert
        Assert.Equal(expectedInactiveBorderThickness, inactiveBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, hoverBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, focusedBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, withErrorBorderThickness);

        recorder.Success();
    }
}

public class OnlyTenOClockValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        => value is not "10:00" ? new ValidationResult(false, "Only 10 o'clock allowed") : ValidationResult.ValidResult;
}
