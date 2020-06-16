using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace MaterialDesignThemes.UITests.DemoApp
{
    public class MainWindow
    {
        private WindowsDriver<WindowsElement> Driver { get; }

        public MainWindow(WindowsDriver<WindowsElement> driver) 
            => Driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public WindowsElement PagesListBox 
            => Driver.FindElementByName("DemoPagesListBox")!;

        public IList<AppiumWebElement> PageListItems
            => PagesListBox.FindElements(By.XPath("//ListItem"))!;

        public WindowsElement HamburgerToggleButton 
            => Driver.FindElementByName("HamburgerToggleButton")!;
    }
}
