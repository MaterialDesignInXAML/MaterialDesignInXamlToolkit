using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.DatePicker
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

            var stackPanel = await LoadXaml(@"
<StackPanel>
    <DatePicker materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var timePicker = await stackPanel.GetElement("/DatePicker");
            IVisualElement datePickerTextBox = await timePicker.GetElement("PART_TextBox");
            IVisualElement helpTextBlock = await datePickerTextBox.GetElement("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetProperty<double>(TextBlock.FontSizeProperty.Name);

            Assert.Equal(20, fontSize);
            recorder.Success();
        }
    }
}
