using System;
using System.ComponentModel;
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

            var stackPanel = await LoadXaml<StackPanel>($@"
<StackPanel>
    <DatePicker SelectedDate=""{DateTime.Today:d}"" materialDesign:TextFieldAssist.HasClearButton=""True""/>
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
    }
}
