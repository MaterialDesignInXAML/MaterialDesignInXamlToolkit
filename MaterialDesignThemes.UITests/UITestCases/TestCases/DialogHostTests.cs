using System;
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
            using var recorder = new TestRecorder(Driver, Output);

            var mainWindow = new MainWindow(Driver);
            mainWindow.ExecuteTestCase("DialogHost/ShowsDialog");

            WindowsElement resultTextBlock = Driver.GetElementByAccessibilityId("ResultTextBlock");
            Driver.WaitFor(() => resultTextBlock.Text == "Clicks: 0");

            WindowsElement testOverlayButton = Driver.GetElementByAccessibilityId("TestOverlayButton");
            testOverlayButton.Click();

            Driver.WaitFor(() => resultTextBlock.Text == "Clicks: 1");

            WindowsElement showDialogButton = Driver.GetElementByAccessibilityId("ShowDialogButton");
            showDialogButton.Click();
            //The presence of the close button indicates the dialog is shown
            WindowsElement closeDialogButton = Driver.GetElementByAccessibilityId("CloseDialogButton");
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

            recorder.Success();
        }
    }
}
