using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class UnitTest1 : TestBase
    {
        [Fact]
        public async Task CanOpenAllPagesOnTheDemoApp()
        {
            await LaunchApplication();

            WindowsElement? listBox = Driver.FindElementByName("DemoPagesListBox");
            Assert.NotNull(listBox);

            IList<AppiumWebElement>? listItems = listBox.FindElements(By.XPath("//ListItem"));

            foreach (AppiumWebElement? listItem in listItems)
            {
                var hamburgerToggleButton = Driver.FindElementByName("HamburgerToggleButton");
                hamburgerToggleButton.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));

                listItem.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
