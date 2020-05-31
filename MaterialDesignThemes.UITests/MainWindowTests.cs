using System;
using System.Threading.Tasks;
using MaterialDesignThemes.UITests.DemoApp;
using OpenQA.Selenium.Appium;
using Xunit;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests
{
    public class MainWindowTests : TestBase
    {
        public MainWindowTests(ITestOutputHelper output) 
            : base(output) 
        { }

        [Fact]
        public async Task CanOpenAllPagesOnTheDemoApp()
        {
            LaunchApplication();

            var mainWindow = new MainWindow(Driver);

            foreach (AppiumWebElement? listItem in mainWindow.PageListItems)
            {
                _output.WriteLine($"Opening page {listItem}");
                mainWindow.HamburgerToggleButton.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));

                listItem.Click();
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
