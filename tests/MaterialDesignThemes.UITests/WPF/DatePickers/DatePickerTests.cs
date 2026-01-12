using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using MaterialDesignThemes.UITests.WPF.TextBoxes;


namespace MaterialDesignThemes.UITests.WPF.DatePickers;

public class DatePickerTests : TestBase
{
    [Test]
    [Description("Pull Request 2192")]
    public async Task OnDatePickerHelperTextFontSize_ChangesHelperTextFontSize()
    {
        var stackPanel = await LoadXaml<StackPanel>(@"
<StackPanel>
    <DatePicker materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var datePickerTextBox = await datePicker.GetElement<TextBox>("PART_TextBox");
        var helpTextBlock = await datePickerTextBox.GetElement<TextBlock>("/Grid/Canvas/TextBlock");

        double fontSize = await helpTextBlock.GetFontSize();

        await Assert.That(fontSize).IsEqualTo(20);
    }

    [Test]
    [Description("Issue 2495")]
    public async Task OnDatePicker_WithClearButton_ClearsSelectedDate()
    {
        string dateString = DateTime.Today.ToString("d", CultureInfo.GetCultureInfoByIetfLanguageTag("en-US"));
        var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <DatePicker SelectedDate=""{dateString}"" materialDesign:TextFieldAssist.HasClearButton=""True""/>
</StackPanel>");
        var datePicker = await stackPanel.GetElement<DatePicker>("/DatePicker");
        var clearButton = await datePicker.GetElement<Button>("PART_ClearButton");

        DateTime? selectedDate = await datePicker.GetSelectedDate();

        await Assert.That(selectedDate).IsNotNull();

        await clearButton.LeftClick();

        await Wait.For(async () =>
        {
            selectedDate = await datePicker.GetSelectedDate();
            await Assert.That(selectedDate).IsNull();
        });
    }

    [Test]
    [Description("Issue 3369")]
    public async Task OnDatePicker_WithClearButton_ClearsSelectedUncommittedText()
    {
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
        await Assert.That(await datePickerTextBox.GetText()).IsEqualTo("invalid date");

        // Act & Assert
        await Wait.For(async () => {
            await clearButton.LeftClick();
            await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);

            string? text = await datePickerTextBox.GetText();
            await Assert.That(text).IsNull();
        });
    }

    [Test]
    [Description("Issue 2737")]
    public async Task OutlinedDatePicker_RespectsActiveAndInactiveBorderThickness_WhenAttachedPropertiesAreSet()
    {
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
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);   // Wait for the visual change
        var inactiveBorderThickness = await textBoxOuterBorder.GetBorderThickness();
        await datePickerTextBox.MoveCursorTo();
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);   // Wait for the visual change
        var hoverBorderThickness = await textBoxOuterBorder.GetBorderThickness();
        await datePickerTextBox.LeftClick();
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);   // Wait for the visual change
        var focusedBorderThickness = await textBoxOuterBorder.GetBorderThickness();

        // TODO: It would be cool if a validation error could be set via XAMLTest without the need for the Binding and ValidationRules elements in the XAML above.
        await datePicker.SetProperty(DatePicker.SelectedDateProperty, DateTime.Now);
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);   // Wait for the visual change
        var withErrorBorderThickness = await textBoxOuterBorder.GetBorderThickness();

        // Assert
        await Assert.That(inactiveBorderThickness).IsEqualTo(expectedInactiveBorderThickness);
        await Assert.That(hoverBorderThickness).IsEqualTo(expectedActiveBorderThickness);
        await Assert.That(focusedBorderThickness).IsEqualTo(expectedActiveBorderThickness);
        await Assert.That(withErrorBorderThickness).IsEqualTo(expectedActiveBorderThickness);
    }

    [Test]
    [Arguments("MaterialDesignFloatingHintDatePicker", null)]
    [Arguments("MaterialDesignFloatingHintDatePicker", 5)]
    [Arguments("MaterialDesignFloatingHintDatePicker", 20)]
    [Arguments("MaterialDesignFilledDatePicker", null)]
    [Arguments("MaterialDesignFilledDatePicker", 5)]
    [Arguments("MaterialDesignFilledDatePicker", 20)]
    [Arguments("MaterialDesignOutlinedDatePicker", null)]
    [Arguments("MaterialDesignOutlinedDatePicker", 5)]
    [Arguments("MaterialDesignOutlinedDatePicker", 20)]
    public async Task DatePicker_WithHintAndHelperText_RespectsPadding(string styleName, int? padding)
    {
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

        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left)).IsCloseTo(0, tolerance);
        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - helperTextCoordinates.Value.Left)).IsCloseTo(0, tolerance);
    }

    [Test]
    [Arguments("MaterialDesignFloatingHintDatePicker", null)]
    [Arguments("MaterialDesignFloatingHintDatePicker", 5)]
    [Arguments("MaterialDesignFloatingHintDatePicker", 20)]
    [Arguments("MaterialDesignFilledDatePicker", null)]
    [Arguments("MaterialDesignFilledDatePicker", 5)]
    [Arguments("MaterialDesignFilledDatePicker", 20)]
    [Arguments("MaterialDesignOutlinedDatePicker", null)]
    [Arguments("MaterialDesignOutlinedDatePicker", 5)]
    [Arguments("MaterialDesignOutlinedDatePicker", 20)]
    public async Task DatePicker_WithHintAndValidationError_RespectsPadding(string styleName, int? padding)
    {
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

        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - hintCoordinates.Value.Left)).IsCloseTo(0, tolerance);
        await Assert.That(Math.Abs(contentHostCoordinates.Value.Left - errorViewerCoordinates.Value.Left)).IsCloseTo(0, tolerance);
    }

    [Test]
    [Description("Issue 3365")]
    public async Task DatePicker_WithOutlinedStyleAndNoCustomHintBackgroundSet_ShouldApplyDefaultBackgroundWhenFloated()
    {
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
        await Assert.That(await hintBackgroundGrid.GetBackgroundColor()).IsNull();

        // Act
        await datePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        await Assert.That(await hintBackgroundGrid.GetBackgroundColor()).IsEqualTo(defaultFloatedBackground);
    }

    [Test]
    [Description("Issue 3365")]
    [Arguments("MaterialDesignDatePicker")]
    [Arguments("MaterialDesignFloatingHintDatePicker")]
    [Arguments("MaterialDesignFilledDatePicker")]
    [Arguments("MaterialDesignOutlinedDatePicker")]
    public async Task DatePicker_WithCustomHintBackgroundSet_ShouldApplyHintBackground(string style)
    {
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
        await Assert.That(await hintBackgroundBorder.GetBackgroundColor()).IsEqualTo(Colors.Red);

        // Act
        await datePickerTextBox.MoveKeyboardFocus();

        // Assert (focused state)
        await Assert.That(await hintBackgroundBorder.GetBackgroundColor()).IsEqualTo(Colors.Red);
    }

    [Test]
    [Description("Issue 3547")]
    public async Task DatePicker_ShouldApplyIsMouseOverTriggers_WhenHoveringCalendarButton()
    {
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
        await Task.Delay(50, TestContext.Current!.Execution.CancellationToken);
        var datePickerTextBoxHoverThickness = await datePickerTextBoxBorder.GetBorderThickness();
        await datePickerTimeButton.MoveCursorTo();
        await Task.Delay(50, TestContext.Current.Execution.CancellationToken);
        var datePickerCalendarButtonHoverThickness = await datePickerTextBoxBorder.GetBorderThickness();

        // Assert
        await Assert.That(datePickerTextBoxHoverThickness).IsEqualTo(expectedThickness);
        await Assert.That(datePickerCalendarButtonHoverThickness).IsEqualTo(expectedThickness);
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
