using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
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

        public AppiumWebElement GetTestCase(string testCaseName) 
            => TestCaseListBox.FindElementByName(testCaseName);
    }
}
