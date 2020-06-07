using System;
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
        public void WhenDialogHostIsShown_OverlayPreventsClicking()
        {
            var mainWindow = new MainWindow(Driver);
            AppiumWebElement testCase = mainWindow.GetTestCase("DialogHost/ShowsDialog");
            testCase.Click();
            mainWindow.ExecuteButton.Click();

            WindowsElement resultTextBlock = Driver.FindElementByAccessibilityId("ResultTextBlock", TimeSpan.FromMilliseconds(200));
            Assert.Equal("Clicks: 0", resultTextBlock.Text);

            WindowsElement testOverlayButton = Driver.FindElementByAccessibilityId("TestOverlayButton", TimeSpan.FromSeconds(0.5));
            testOverlayButton.Click();

            Assert.Equal("Clicks: 1", resultTextBlock.Text);

            WindowsElement showDialogButton = Driver.FindElementByAccessibilityId("ShowDialogButton", TimeSpan.FromSeconds(0.5));
            showDialogButton.Click();
            //The presence of the close button indicates the dialog is shown
            WindowsElement closeDialogButton = Driver.FindElementByAccessibilityId("CloseDialogButton", TimeSpan.FromSeconds(0.5));
            testOverlayButton.Click();
            Assert.Equal("Clicks: 1", resultTextBlock.Text);

            closeDialogButton.Click();

            Driver.WaitUntil(() =>
            {
                Output.WriteLine("Input");
                testOverlayButton.Click();
                Assert.Equal("Clicks: 2", resultTextBlock.Text);
                Output.WriteLine("Output");
            }, TimeSpan.FromSeconds(2));
        }
    }
}
