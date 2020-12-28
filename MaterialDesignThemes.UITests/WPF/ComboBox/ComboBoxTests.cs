using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ComboBox
{
    public class ComboBoxTests : TestBase
    {
        public ComboBoxTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml(@"
<StackPanel>
    <ComboBox
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement("/ComboBox");
            IVisualElement helpTextBlock = await comboBox.GetElement("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetProperty<double>(TextBlock.FontSizeProperty.Name);

            Assert.Equal(20, fontSize);
            recorder.Success();
        }

        [Fact]
        [Description("Pull Request 2192")]
        public async Task OnFilledComboBoxHelperTextFontSize_ChangesHelperTextFontSize()
        {
            await using var recorder = new TestRecorder(App);

            var stackPanel = await LoadXaml(@"
<StackPanel>
    <ComboBox
        Style=""{StaticResource MaterialDesignFilledComboBox}""
        materialDesign:HintAssist.HelperTextFontSize=""20""/>
</StackPanel>");
            var comboBox = await stackPanel.GetElement("/ComboBox");
            IVisualElement helpTextBlock = await comboBox.GetElement("/Grid/Canvas/TextBlock");

            double fontSize = await helpTextBlock.GetProperty<double>(TextBlock.FontSizeProperty.Name);

            Assert.Equal(20, fontSize);
            recorder.Success();
        }
    }
}
