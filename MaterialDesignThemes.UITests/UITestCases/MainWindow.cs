using System;
using OpenQA.Selenium.Appium.Windows;

namespace MaterialDesignThemes.UITests.UITestCases
{
    public class MainWindow
    {
        private WindowsDriver<WindowsElement> Driver { get; }

        public MainWindow(WindowsDriver<WindowsElement> driver)
            => Driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public WindowsElement TestCaseListBox 
            => Driver.FindElementByName("TestCaseListBox");

        public WindowsElement ExecuteButton
            => Driver.FindElementByName("Execute Test")!;

        public WindowsElement GetTestCase(string testCaseName) 
            => (WindowsElement)TestCaseListBox.FindElementByName(testCaseName);
    }
}
