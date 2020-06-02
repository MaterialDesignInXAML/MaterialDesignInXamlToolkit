using System;
using System.Threading.Tasks;
using MaterialDesignThemes.UITests.DemoApp;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests
{
    public class DemoAppTests : TestBase
    {
        public DemoAppTests(ITestOutputHelper output) 
            : base(output, App.DemoAppPath) 
        {
            WindowsElement? element = Driver.FindElementByName("Material Design In XAML Toolkit");
            Assert.NotNull(element);
        }

        [Fact]
        public async Task CanOpenAllPagesOnTheDemoApp()
        {
            var mainWindow = new MainWindow(Driver);

            foreach (AppiumWebElement? listItem in mainWindow.PageListItems)
            {
                Output.WriteLine($"Opening page {listItem}");
                mainWindow.HamburgerToggleButton.Click();
                await Task.Delay(TimeSpan.FromSeconds(1.2));

                listItem.Click();
                await Task.Delay(TimeSpan.FromSeconds(1.2));
            }
        }
    }
}
