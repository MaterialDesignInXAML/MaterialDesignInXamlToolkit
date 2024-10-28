using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.UITests.WPF.TextBoxes;

namespace MaterialDesignThemes.UITests.WPF.DatePickers;

public class DatePickerTests : TestBase
{
    public DatePickerTests(ITestOutputHelper output)
        : base(output)
    {
    }

    [Fact]
    [Description("Pull Request 2192")]
    public async Task OnDatePickerHelperTextFontSize_ChangesHelperTextFontSize()
    {
        await using var recorder = new TestRecorder(App);

        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <DatePicker materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await datePicker.GetElement<TextBox>("PART_TextBox");
        var helpTextBlock = await datePickerTextBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

        double fontSize = await helpTextBlock.GetFontSize();

        Assert.Equal(20, fontSize);
        recorder.Success();
    }

    [Fact]
    [Description("Issue 2495")]
    public async Task OnDatePicker_WithClearButton_ClearsSelectedDate()
    {
        await using var recorder = new TestRecorder(App);

        string dateString = DateTime.Today.ToString("d", CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
        var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <DatePicker SelectedDate=""{dateString}"" materialDesign:TextFieldAssist.HasClearButton=""True""/>
</StackPanel>");
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var clearButton = await datePicker.GetElement<Button>("PART_ClearButton");

        DateTime? selectedDate = await datePicker.GetSelectedDate();

        Assert.NotNull(selectedDate);

        await clearButton.LeftClick();

        await Wait.For(async () =>
        {
            selectedDate = await datePicker.GetSelectedDate();
            Assert.Null(selectedDate);
        });

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3369")]
    public async Task OnDatePicker_WithClearButton_ClearsSelectedUncommittedText()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel>
              <DatePicker materialDesign:TextFieldAssist.HasClearButton="True"/>
            </StackPanel>
            """);
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await stackPanel.GetElement<DatePickerTextBox>("/DatePickerTextBox");
        var clearButton = await datePicker.GetElement<Button>("PART_ClearButton");

        await datePickerTextBox.SendKeyboardInput($"invalid date");
        Assert.Equal("invalid date", await datePickerTextBox.GetText());

        // Act
        await clearButton.LeftClick();
        await Task.Delay(50);

        // Assert
        Assert.Null(await datePickerTextBox.GetText());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 2737")]
    public async Task OutlinedDatePicker_RespectsActiveAndInactiveBorderThickness_WhenAttachedPropertiesAreSet()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var expectedInactiveBorderThickness = new Thickness(4, 3, 2, 1);
        var expectedActiveBorderThickness = new Thickness(1, 2, 3, 4);
        var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <DatePicker Style=""{{StaticResource MaterialDesignOutlinedDatePicker}}""
      materialDesign:DatePickerAssist.OutlinedBorderInactiveThickness=""{expectedInactiveBorderThickness}""
      materialDesign:DatePickerAssist.OutlinedBorderActiveThickness=""{expectedActiveBorderThickness}"">
      <DatePicker.SelectedDate>
        <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
          <Binding.ValidationRules>
            <local:FutureDateValidationRule ValidatesOnTargetUpdated=""True""/>
          </Binding.ValidationRules>
        </Binding>
      </DatePicker.SelectedDate>
    </DatePicker>
    <Button x:Name=""Button"" Content=""Some Button"" Margin=""0,20,0,0"" />
</StackPanel>", ("local", typeof(FutureDateValidationRule)));
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        await datePicker.SetProperty(DatePicker.SelectedDateProperty, DateTime.Now.AddDays(1));
        var datePickerTextBox = await datePicker.GetElement<TextBox>("PART_TextBox");
        var textBoxOuterBorder = await datePickerTextBox.GetElement<Border>("OuterBorder");
        var button = await stackPanel.GetElement<Button>("Button");

        // Act
        await button.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var inactiveBorderThickness = await textBoxOuterBorder.GetBorderThickness();
        await datePickerTextBox.MoveCursorTo();
        await Task.Delay(50);   // Wait for the visual change
        var hoverBorderThickness = await textBoxOuterBorder.GetBorderThickness();
        await datePickerTextBox.LeftClick();
        await Task.Delay(50);   // Wait for the visual change
        var focusedBorderThickness = await textBoxOuterBorder.GetBorderThickness();

        // TODO: It would be cool if a validation error could be set via XAMLTest without the need for the Binding and ValidationRules elements in the XAML above.
        await datePicker.SetProperty(DatePicker.SelectedDateProperty, DateTime.Now);
        await Task.Delay(50);   // Wait for the visual change
        var withErrorBorderThickness = await textBoxOuterBorder.GetBorderThickness();

        // Assert
        Assert.Equal(expectedInactiveBorderThickness, inactiveBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, hoverBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, focusedBorderThickness);
        Assert.Equal(expectedActiveBorderThickness, withErrorBorderThickness);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintDatePicker", null)]
    [InlineData("MaterialDesignFloatingHintDatePicker", 5)]
    [InlineData("MaterialDesignFloatingHintDatePicker", 20)]
    [InlineData("MaterialDesignFilledDatePicker", null)]
    [InlineData("MaterialDesignFilledDatePicker", 5)]
    [InlineData("MaterialDesignFilledDatePicker", 20)]
    [InlineData("MaterialDesignOutlinedDatePicker", null)]
    [InlineData("MaterialDesignOutlinedDatePicker", 5)]
    [InlineData("MaterialDesignOutlinedDatePicker", 20)]
    public async Task DatePicker_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var datePicker = await LoadXaml<DatePicker>($@"
<DatePicker {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"" />
");

        var contentHost = await datePicker.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await datePicker.GetElement<SmartHint>("Hint");
        var helperText = await datePicker.GetElement<TextBlock>("HelperTextTextBlock");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? helperTextCoordinates = await helperText.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Theory]
    [InlineData("MaterialDesignFloatingHintDatePicker", null)]
    [InlineData("MaterialDesignFloatingHintDatePicker", 5)]
    [InlineData("MaterialDesignFloatingHintDatePicker", 20)]
    [InlineData("MaterialDesignFilledDatePicker", null)]
    [InlineData("MaterialDesignFilledDatePicker", 5)]
    [InlineData("MaterialDesignFilledDatePicker", 20)]
    [InlineData("MaterialDesignOutlinedDatePicker", null)]
    [InlineData("MaterialDesignOutlinedDatePicker", 5)]
    [InlineData("MaterialDesignOutlinedDatePicker", 20)]
    public async Task DatePicker_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
        await using var recorder = new TestRecorder(App);

        // FIXME: Tolerance needed because TextFieldAssist.TextBoxViewMargin is in play and slightly modifies the hint text placement in certain cases.
        const double tolerance = 1.5;

        string styleAttribute = $"Style=\"{{StaticResource {styleName}}}\"";
        string paddingAttribute = padding.HasValue ? $"Padding=\"{padding.Value}\"" : string.Empty;

        var datePicker = await LoadXaml<DatePicker>($@"
<DatePicker {styleAttribute} {paddingAttribute}
  materialDesign:HintAssist.Hint=""Hint text""
  materialDesign:HintAssist.HelperText=""Helper text""
  Width=""200"" VerticalAlignment=""Center"" HorizontalAlignment=""Center"">
  <DatePicker.Text>
    <Binding RelativeSource=""{{RelativeSource Self}}"" Path=""Tag"" UpdateSourceTrigger=""PropertyChanged"">
      <Binding.ValidationRules>
        <local:NotEmptyValidationRule ValidatesOnTargetUpdated=""True""/>
      </Binding.ValidationRules>
    </Binding>
  </DatePicker.Text>
</DatePicker>
", ("local", typeof(NotEmptyValidationRule)));

        var contentHost = await datePicker.GetElement<ScrollViewer>("PART_ContentHost");
        var hint = await datePicker.GetElement<SmartHint>("Hint");
        var errorViewer = await datePicker.GetElement<Border>("DefaultErrorViewer");

        Rect? contentHostCoordinates = await contentHost.GetCoordinates();
        Rect? hintCoordinates = await hint.GetCoordinates();
        Rect? errorViewerCoordinates = await errorViewer.GetCoordinates();

        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left), 0, tolerance);
        Assert.InRange(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left), 0, tolerance);

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3365")]
    public async Task DatePicker_WithOutlinedStyleAndNoCustomHintBackgroundSet_ShouldApplyDefaultBackgroundWhenFloated()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel>
            <DatePicker
              Style="{StaticResource MaterialDesignOutlinedDatePicker}"
              materialDesign:HintAssist.Hint="Hint text" />
            </StackPanel>
            """);
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await datePicker.GetElement<DatePickerTextBox>("/DatePickerTextBox");
        var hintBackgroundGrid = await datePicker.GetElement<Grid>("HintBackgroundGrid");

        var defaultFloatedBackground = await GetThemeColor("MaterialDesign.Brush.Background");

        // Assert (unfocused state)
        Assert.Null(await hintBackgroundGrid.GetBackgroundColor());

        // Act
        await datePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        Assert.Equal(defaultFloatedBackground, await hintBackgroundGrid.GetBackgroundColor());

        recorder.Success();
    }

    [Theory]
    [Description("Issue 3365")]
    [InlineData("MaterialDesignDatePicker")]
    [InlineData("MaterialDesignFloatingHintDatePicker")]
    [InlineData("MaterialDesignFilledDatePicker")]
    [InlineData("MaterialDesignOutlinedDatePicker")]
    public async Task DatePicker_WithCustomHintBackgroundSet_ShouldApplyHintBackground(string style)
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        var stackPanel = await LoadXaml<StackPanel>($$"""
            <StackPanel>
              <DatePicker
                Style="{StaticResource {{style}}}"
                materialDesign:HintAssist.Hint="Hint text"
                materialDesign:HintAssist.Background="Red" />
            </StackPanel>
            """);
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await datePicker.GetElement<DatePickerTextBox>("/DatePickerTextBox");
        var hintBackgroundBorder = await datePicker.GetElement<Border>("HintBackgroundBorder");

        // Assert (unfocused state)
        Assert.Equal(Colors.Red, await hintBackgroundBorder.GetBackgroundColor());

        // Act
        await datePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        Assert.Equal(Colors.Red, await hintBackgroundBorder.GetBackgroundColor());

        recorder.Success();
    }

    [Fact]
    [Description("Issue 3547")]
    public async Task DatePicker_ShouldApplyIsMouseOverTriggers_WhenHoveringCalendarButton()
    {
        await using var recorder = new TestRecorder(App);

        // Arrange
        Thickness expectedThickness = Constants.DefaultOutlinedBorderActiveThickness;
        var stackPanel = await LoadXaml<StackPanel>("""
            <StackPanel>
              <DatePicker
                Style="{StaticResource MaterialDesignOutlinedDatePicker}" />
            </StackPanel>
            """);
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await datePicker.GetElement<DatePickerTextBox>("/DatePickerTextBox");
        var datePickerTextBoxBorder = await datePickerTextBox.GetElement<Border>("OuterBorder");
        var datePickerTimeButton = await datePicker.GetElement<Button>("PART_Button");

        // Act
        await datePickerTextBoxBorder.MoveCursorTo();
        await Task.Delay(50);
        var datePickerTextBoxHoverThickness = await datePickerTextBoxBorder.GetBorderThickness();
        await datePickerTimeButton.MoveCursorTo();
        await Task.Delay(50);
        var datePickerCalendarButtonHoverThickness = await datePickerTextBoxBorder.GetBorderThickness();

        // Assert
        Assert.Equal(expectedThickness, datePickerTextBoxHoverThickness);
        Assert.Equal(expectedThickness, datePickerCalendarButtonHoverThickness);

        recorder.Success();
    }
}

public class FutureDateValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        DateTime time;
        if (!DateTime.TryParse((value ?? "").ToString(),
                CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
                out time)) return new ValidationResult(false, "Invalid date");

        return time.Date <= DateTime.Now.Date
            ? new ValidationResult(false, "Future date required")
            : ValidationResult.ValidResult;
    }
}
