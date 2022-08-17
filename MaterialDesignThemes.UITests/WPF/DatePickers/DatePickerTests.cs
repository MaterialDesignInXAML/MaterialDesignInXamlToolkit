using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.DatePickers
{
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
            var button = await stackPanel.GetElement<Button>("Button");

            // Act
            await button.MoveCursorTo();
            await Task.Delay(50);   // Wait for the visual change
            var inactiveBorderThickness = await datePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);
            await datePickerTextBox.MoveCursorTo();
            await Task.Delay(50);   // Wait for the visual change
            var hoverBorderThickness = await datePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);
            await datePickerTextBox.LeftClick();
            await Task.Delay(50);   // Wait for the visual change
            var focusedBorderThickness = await datePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);

            // TODO: It would be cool if a validation error could be set via XAMLTest without the need for the Binding and ValidationRules elements in the XAML above.
            await datePicker.SetProperty(DatePicker.SelectedDateProperty, DateTime.Now);
            await Task.Delay(50);   // Wait for the visual change
            var withErrorBorderThickness = await datePickerTextBox.GetProperty<Thickness>(Control.BorderThicknessProperty);

            // Assert
            Assert.Equal(expectedInactiveBorderThickness, inactiveBorderThickness);
            Assert.Equal(expectedActiveBorderThickness, hoverBorderThickness);
            Assert.Equal(expectedActiveBorderThickness, focusedBorderThickness);
            Assert.Equal(expectedActiveBorderThickness, withErrorBorderThickness);

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
}
