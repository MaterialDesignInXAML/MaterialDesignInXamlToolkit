using System;
using System.IO;
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
            try
            {
                var mainWindow = new MainWindow(Driver);
                AppiumWebElement testCase = mainWindow.GetTestCase("DialogHost/ShowsDialog");
                testCase.Click();
                mainWindow.ExecuteButton.Click();

                WindowsElement resultTextBlock = Driver.FindElementByAccessibilityId("ResultTextBlock", TimeSpan.FromSeconds(0.5));
                Driver.WaitFor(() => resultTextBlock.Text == "Clicks: 0");

                WindowsElement testOverlayButton = Driver.FindElementByAccessibilityId("TestOverlayButton", TimeSpan.FromSeconds(0.5));
                testOverlayButton.Click();

                Driver.WaitFor(() => resultTextBlock.Text == "Clicks: 1");

                WindowsElement showDialogButton = Driver.FindElementByAccessibilityId("ShowDialogButton", TimeSpan.FromSeconds(0.5));
                showDialogButton.Click();
                //The presence of the close button indicates the dialog is shown
                WindowsElement closeDialogButton = Driver.FindElementByAccessibilityId("CloseDialogButton", TimeSpan.FromSeconds(0.5));
                testOverlayButton.Click();
                Driver.WaitFor(() => resultTextBlock.Text == "Clicks: 1");

                closeDialogButton.Click();

                Driver.WaitUntil(() =>
                {
                    Output.WriteLine("Input");
                    testOverlayButton.Click();
                    Assert.Equal("Clicks: 2", resultTextBlock.Text);
                    Output.WriteLine("Output");
                }, TimeSpan.FromSeconds(2));
            }
            finally
            {
                var screenshot = Driver.GetScreenshot();
                string fullPath = Path.GetFullPath($"{nameof(WhenDialogHostIsShown_OverlayPreventsClicking)}.jpg");
                File.Delete(fullPath);
                Output.WriteLine("Saving image " + fullPath);
                screenshot.SaveAsFile(fullPath);
                Output.WriteLine("Wrote image");
            }
        }
    }
}
