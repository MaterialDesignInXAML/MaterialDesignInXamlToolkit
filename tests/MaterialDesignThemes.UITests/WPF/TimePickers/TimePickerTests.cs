using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.UITests.WPF.TextBoxes;

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

        await timePicker.SetSelectedTime(new DateTime(2020, 8, 10, 1, 2, 0));
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

        await timePicker.SetSelectedTime(new DateTime(2020, 8, 10, 1, 2, 0));
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

        await timePicker.SetSelectedTime(new DateTime(2020, 8, 10, 1, 3, 0));
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
        var textBoxOuterBorder = await timePickerTextBox.GetElement<Border>("OuterBorder");
        var button = await stackPanel.GetElement<Button>("Button");

        // Act
        await button.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var inactiveBorderThickness = await textBoxOuterBorder.GetBorderThickness();
        await timePickerTextBox.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var hoverBorderThickness = await textBoxOuterBorder.GetBorderThickness(); ;
        await timePickerTextBox.LeftClick();
        await Task.Delay(50);   // Wait for the visual change
        var focusedBorderThickness = await textBoxOuterBorder.GetBorderThickness(); ;

        // TODO: It would be cool if a validation error could be set via XAMLTest without the need for the Binding and ValidationRules elements in the XAML above.
        await timePicker.SetProperty(TimePicker.TextProperty, "11:00");
        await Task.Delay(50);   // Wait for the visual change
        var withErrorBorderThickness = await textBoxOuterBorder.GetBorderThickness(); ;

        // Assert
        Assert.Equal(expectedInactiveBorderThickness, inactiveBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, hoverBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, focusedBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, withErrorBorderThickness);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintTimePicker", null)]
    [InlineData("MaterialDesignFloatingHintTimePicker", 5)]
    [InlineData("MaterialDesignFloatingHintTimePicker", 20)]
    [InlineData("MaterialDesignFilledTimePicker", null)]
    [InlineData("MaterialDesignFilledTimePicker", 5)]
    [InlineData("MaterialDesignFilledTimePicker", 20)]
    [InlineData("MaterialDesignOutlinedTimePicker", null)]
    [InlineData("MaterialDesignOutlinedTimePicker", 5)]
    [InlineData("MaterialDesignOutlinedTimePicker", 20)]
    public async Task TimePicker_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var timePicker = await LoadXaml<TimePicker>($@"
<materialDesign:TimePicker {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"" />
");

        var contentHost = await timePicker.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await timePicker.GetElement<SmartHint>("Hint");
        var helperText = await timePicker.GetElement<TextBlock>("HelperTextTextBlock");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? helperTextCoordinates = await helperText.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintTimePicker", null)]
    [InlineData("MaterialDesignFloatingHintTimePicker", 5)]
    [InlineData("MaterialDesignFloatingHintTimePicker", 20)]
    [InlineData("MaterialDesignFilledTimePicker", null)]
    [InlineData("MaterialDesignFilledTimePicker", 5)]
    [InlineData("MaterialDesignFilledTimePicker", 20)]
    [InlineData("MaterialDesignOutlinedTimePicker", null)]
    [InlineData("MaterialDesignOutlinedTimePicker", 5)]
    [InlineData("MaterialDesignOutlinedTimePicker", 20)]
    public async Task TimePicker_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var timePicker = await LoadXaml<TimePicker>($@"
<materialDesign:TimePicker {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
  <materialDesign:TimePicker.Text>
    <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
      <Binding.ValidationRules>
        <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
      </Binding.ValidationRules>
    </Binding>
  </materialDesign:TimePicker.Text>
</materialDesign:TimePicker>
", ("local", typeof(NotEmptyValidationRule)));

        var contentHost = await timePicker.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await timePicker.GetElement<SmartHint>("Hint");
        var errorViewer = await timePicker.GetElement<Border>("DefaultErrorViewer");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? errorViewerCoordinates = await errorViewer.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3119")]
    public async Task TimePicker_WithClearButton_ClearButtonClearsSelectedTime()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
  <materialDesign:TimePicker
    SelectedTime=""08:30""
    materialDesign:TextFieldAssist.HasClearButton=""True"" />
</StackPanel>");
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var clearButton = await timePicker.GetElement<Button>("PART_ClearButton");
        Assert.NotNull(await timePicker.GetSelectedTime());

        // Act
        await clearButton.LeftClick();
        await Task.Delay(50);

        // Assert
        Assert.Null(await timePicker.GetSelectedTime());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3369")]
    public async Task TimePicker_WithClearButton_ClearButtonClearsUncommittedText()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>($"""
        <StackPanel>
          <materialDesign:TimePicker
             materialDesign:TextFieldAssist.HasClearButton="True" />
        </StackPanel>
        """);
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var clearButton = await timePicker.GetElement<Button>("PART_ClearButton");

        await timePickerTextBox.SendKeyboardInput($"invalid time");
        Assert.Equal("invalid time", await timePickerTextBox.GetText());

        // Act
        await clearButton.LeftClick();
        await Task.Delay(50);

        // Assert
        Assert.Null(await timePickerTextBox.GetText());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3365")]
    public async Task TimePicker_WithOutlinedStyleAndNoCustomHintBackgroundSet_ShouldApplyDefaultBackgroundWhenFloated()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel>
            <materialDesign:TimePicker
              Style="{StaticResource MaterialDesignOutlinedTimePicker}"
              materialDesign:HintAssist.Hint="Hint text" />
            </StackPanel>
            """);
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var hintBackgroundGrid = await timePicker.GetElement<Grid>("HintBackgroundGrid");

        var defaultBackground = Colors.Transparent; 
        var defaultFloatedBackground = await GetThemeColor("MaterialDesign.Brush.Background");

        // Assert (unfocused state)
        Assert.Equal(defaultBackground, await hintBackgroundGrid.GetBackgroundColor());

        // Act
        await timePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        Assert.Equal(defaultFloatedBackground, await hintBackgroundGrid.GetBackgroundColor());

        recorder.Success();
    }

    [Theory]
    [Description("Issue 3365")]
    [InlineData("MaterialDesignTimePicker")]
    [InlineData("MaterialDesignFloatingHintTimePicker")]
    [InlineData("MaterialDesignFilledTimePicker")]
    [InlineData("MaterialDesignOutlinedTimePicker")]
    public async Task TimePicker_WithCustomHintBackgroundSet_ShouldApplyHintBackground(string style)
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>($$"""
            <StackPanel>
              <materialDesign:TimePicker
                Style="{StaticResource {{style}}}"
                materialDesign:HintAssist.Hint="Hint text"
                materialDesign:HintAssist.Background="Red" />
            </StackPanel>
            """);
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var hintBackgroundBorder = await timePicker.GetElement<Border>("HintBackgroundBorder");

        // Assert (unfocused state)
        Assert.Equal(Colors.Red, await hintBackgroundBorder.GetBackgroundColor());

        // Act
        await timePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        Assert.Equal(Colors.Red, await hintBackgroundBorder.GetBackgroundColor());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3547")]
    public async Task TimePicker_ShouldApplyIsMouseOverTriggers_WhenHoveringTimeButton()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        Thickness expectedThickness = Constants.DefaultOutlinedBorderActiveThickness;
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel>
              <materialDesign:TimePicker
                Style="{StaticResource MaterialDesignOutlinedTimePicker}" />
            </StackPanel>
            """);
        var timePicker = await stackPanel.GetElement<TimePicker>("/TimePicker");
        var timePickerTextBox = await timePicker.GetElement<TimePickerTextBox>("/TimePickerTextBox");
        var timePickerTextBoxBorder = await timePickerTextBox.GetElement<Border>("OuterBorder");
        var timePickerTimeButton = await timePicker.GetElement<Button>("PART_Button");

        // Act
        await timePickerTextBoxBorder.MoveCursorTo();
        await Task.Delay(50);
        var timePickerTextBoxHoverThickness = await timePickerTextBoxBorder.GetBorderThickness();
        await timePickerTimeButton.MoveCursorTo();
        await Task.Delay(50);
        var timePickerTimeButtonHoverThickness = await timePickerTextBoxBorder.GetBorderThickness();

        // Assert
        Assert.Equal(expectedThickness, timePickerTextBoxHoverThickness);
        Assert.Equal(expectedThickness, timePickerTimeButtonHoverThickness);

        recorder.Success();
    }
}

public class OnlyTenOClockValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        => value is not "10:00" ? new ValidationResult(false, "Only 10 o'clock allowed") : ValidationResult.ValidResult;
}
