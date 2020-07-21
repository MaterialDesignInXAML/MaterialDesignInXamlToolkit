using System.Threading.Tasks;
using System.Windows.Media;
using XAMLTest;
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
    }
}
