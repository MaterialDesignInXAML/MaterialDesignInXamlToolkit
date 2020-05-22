using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class UnitTest1 : TestBase
    {
        [Fact]
        public async Task CanOpenAllPagesOnTheDemoApp()
        {
            await LaunchApplication();

            WindowsElement? hamburgerToggleButton = Driver.FindElementByName("HamburgerToggleButton");
            Assert.NotNull(hamburgerToggleButton);
            Click(hamburgerToggleButton);

            await Task.Delay(TimeSpan.FromSeconds(1));

            WindowsElement? listBox = Driver.FindElementByName("DemoPagesListBox");
            Assert.NotNull(listBox);

            IList<AppiumWebElement>? listItems = listBox.FindElements(By.XPath("//ListItem"));
            listItems = listItems.Skip(listItems.Count - 3).ToList();

            foreach (AppiumWebElement? listItem in listItems)
            {
                Click(listItem);
                await Task.Delay(TimeSpan.FromSeconds(1));
                hamburgerToggleButton = Driver.FindElementByName("HamburgerToggleButton");
                Click(hamburgerToggleButton);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
