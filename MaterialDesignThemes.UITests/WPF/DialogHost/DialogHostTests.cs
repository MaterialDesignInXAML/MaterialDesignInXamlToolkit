using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.UITests.Samples.DialogHost;
using XamlTest;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.WPF.DialogHost
{
    public class DialogHostTests : TestBase
    {
        public DialogHostTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task OnOpenDialog_OverlayCoversContent()
        {
            await using var recorder = new TestRecorder(App);

            IVisualElement dialogHost = await LoadUserControl<WithCounter>();
            IVisualElement overlay = await dialogHost.GetElement("PART_ContentCoverGrid");

            IVisualElement resultTextBlock = await dialogHost.GetElement("ResultTextBlock");
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 0");

            IVisualElement testOverlayButton = await dialogHost.GetElement("TestOverlayButton");
            await testOverlayButton.Click();
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");

            IVisualElement showDialogButton = await dialogHost.GetElement("ShowDialogButton");
            await showDialogButton.Click();

            IVisualElement closeDialogButton = await dialogHost.GetElement("CloseDialogButton");
            await Wait.For(async () => await closeDialogButton.GetIsVisible() == true);

            await testOverlayButton.Click();
            await Wait.For(async () => await resultTextBlock.GetText() == "Clicks: 1");
            await closeDialogButton.Click();

            await Wait.For(async () => await overlay.GetVisibility() != Visibility.Visible);
            await testOverlayButton.Click();
            await Wait.For(async () => Assert.Equal("Clicks: 2", await resultTextBlock.GetText()));
        }
    }
}
