using System;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.UITestCases.TestCases
{
    public class DialogHostTests : UITestCasesBase
    {
        public DialogHostTests(ITestOutputHelper output) 
            : base(output)
        { }

        [Fact]
        public async Task WhenDialogHostIsShown_OverlayPreventsClicking()
        {
            var mainWindow = new MainWindow(Driver);
            AppiumWebElement testCase = mainWindow.GetTestCase("DialogHost/ShowsDialog");
            testCase.Click();
            mainWindow.ExecuteButton.Click();

            WindowsElement resultTextBlock = Driver.FindElementByAccessibilityId("ResultTextBlock")!;

            Assert.Equal("Clicks: 0", resultTextBlock.Text);

            WindowsElement testOverlayButton = Driver.FindElementByName("TestOverlayButton")!;
            testOverlayButton.Click();

            Assert.Equal("Clicks: 1", resultTextBlock.Text);

            WindowsElement showDialogButton = Driver.FindElementByName("ShowDialogButton")!;
            showDialogButton.Click();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            testOverlayButton.Click();
            Assert.Equal("Clicks: 1", resultTextBlock.Text);

            WindowsElement closeDialogButton = Driver.FindElementByName("CloseDialogButton")!;
            closeDialogButton.Click();
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            testOverlayButton.Click();
            Assert.Equal("Clicks: 2", resultTextBlock.Text);
        }
    }
}
