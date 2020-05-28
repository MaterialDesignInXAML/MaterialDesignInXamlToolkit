using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MaterialDesignThemes.UITests.DemoApp;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class MainWindowTests : TestBase
    {
        [Fact]
        public async Task CanOpenAllPagesOnTheDemoApp()
        {
            LaunchApplication();

            var mainWindow = new MainWindow(Driver);

            foreach (AppiumWebElement? listItem in mainWindow.PageListItems)
            {
                mainWindow.HamburgerToggleButton.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));

                listItem.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
