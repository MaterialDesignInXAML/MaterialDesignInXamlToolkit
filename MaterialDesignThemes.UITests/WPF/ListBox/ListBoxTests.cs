using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.ListBox
{
    public class ListBoxTests : TestBase
    {
        public ListBoxTests(ITestOutputHelper output)
            : base(output)
        { }

        [Fact]
        public async Task OnMouseOver_BackgroundIsSet()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement listBox = await LoadXaml(@"
<ListBox MinWidth=""200"">
    <ListBoxItem Content=""Item1"" />
    <ListBoxItem Content=""Item2"" />
    <ListBoxItem Content=""Item3"" />
    <ListBoxItem Content=""Item4"" />
</ListBox>
");

            IVisualElement listBoxItem = await listBox.GetElement("/ListBoxItem[2]");
            Assert.Equal("Item3", await listBoxItem.GetProperty<string>("Content"));
            IVisualElement mouseOverBorder = await listBoxItem.GetElement("MouseOverBorder");

            await listBox.MoveCursorToElement(Position.TopLeft);
            await Wait.For(async () => Assert.Equal(0.0, await mouseOverBorder.GetOpacity()));

            await mouseOverBorder.MoveCursorToElement();
            await Wait.For(async () =>
            {
                double opacity = await mouseOverBorder.GetOpacity();
                Output.WriteLine($"Got opacity {opacity}");
                Assert.Equal(0.1, opacity);
            });

            Color effectiveBackground = await mouseOverBorder.GetEffectiveBackground();
            Color foreground = await listBoxItem.GetForegroundColor();
            foreground = foreground.FlattenOnto(effectiveBackground);

            float contrastRatio = foreground.ContrastRatio(effectiveBackground);
            Assert.True(contrastRatio >= MaterialDesignSpec.MinimumContrastSmallText);

            recorder.Success();
        }

        [Theory]
        [Description("Issue 1994")]
        [InlineData("MaterialDesignFilterChipListBox")]
        [InlineData("MaterialDesignFilterChipPrimaryListBox")]
        [InlineData("MaterialDesignFilterChipAccentListBox")]
        [InlineData("MaterialDesignFilterChipOutlineListBox")]
        [InlineData("MaterialDesignFilterChipPrimaryOutlineListBox")]
        [InlineData("MaterialDesignFilterChipAccentOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipListBox")]
        [InlineData("MaterialDesignChoiceChipPrimaryListBox")]
        [InlineData("MaterialDesignChoiceChipAccentListBox")]
        [InlineData("MaterialDesignChoiceChipOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipPrimaryOutlineListBox")]
        [InlineData("MaterialDesignChoiceChipAccentOutlineListBox")]
        public async Task OnClickChoiceChipListBox_ChangesSelectedItem(string listBoxStyle)
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement listBox = await LoadXaml($@"
<ListBox x:Name=""ChipsListBox"" Style=""{{StaticResource {listBoxStyle}}}"">
    <ListBoxItem>Mercury</ListBoxItem>
    <ListBoxItem>Venus</ListBoxItem>
    <ListBoxItem>Earth</ListBoxItem>
    <ListBoxItem>Pluto</ListBoxItem>
</ListBox>
");
            IVisualElement earth = await listBox.GetElement("/ListBoxItem[2]");
            await earth.Click();

            await Wait.For(async () => Assert.Equal(2, await listBox.GetProperty<int>(nameof(Selector.SelectedIndex))));

            recorder.Success();
        }
    }
}
