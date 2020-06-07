using System;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests.DemoApp
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
        public void CanOpenAllPagesOnTheDemoApp()
        {
            var mainWindow = new MainWindow(Driver);

            foreach (AppiumWebElement? listItem in mainWindow.PageListItems)
            {
                var rect = mainWindow.PagesListBox.Rect;
                Driver.WaitFor(() => mainWindow.PagesListBox.Rect.Right <= 1);
                Driver.WaitFor(() => mainWindow.HamburgerToggleButton.Displayed);
                mainWindow.HamburgerToggleButton.Click();

                Driver.WaitFor(() => listItem.Location.X >= 0);
                listItem.Click();
            }
        }
    }
}
