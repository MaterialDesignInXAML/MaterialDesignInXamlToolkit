using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class TestBase
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected WindowsDriver<WindowsElement> Driver { get; private set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        private bool AppHasLaunched { get; set; }

        public void Click(AppiumWebElement element)
        {
            WaitForElement(() => new Actions(Driver).MoveToElement(element).Click().Perform());
        }

        public void SendKeys(AppiumWebElement element, string input)
        {
            WaitForElement(() => new Actions(Driver).SendKeys(element, input).Perform());
        }

        public void DragAndDrop(AppiumWebElement element, int startOffsetX, int startOffsetY, int endOffsetX, int endOffsetY)
        {
            var crop = new Actions(Driver);
            crop.MoveToElement(element, startOffsetX, startOffsetY)
                .ClickAndHold()
                .MoveByOffset(endOffsetX, endOffsetY)
                .Release()
                .Perform();
        }

        public void WaitForElement(Action action)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            wait.Until(a =>
            {
                action();
                return true;
            });
        }

        protected bool WaitForElement(Func<bool> func, int timeoutSeconds = 20)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            try
            {
                return wait.Until(a =>
                {
                    return func();
                });
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        protected async Task LaunchApplication()
        {
            if (AppHasLaunched) return;

            string workingDirectory = Path.GetFullPath(@"..\..\..\..\MainDemo.Wpf\bin\Debug\netcoreapp3.1\");
            var processInfo = new ProcessStartInfo
            {
                WorkingDirectory = workingDirectory,
                FileName = Path.Combine(workingDirectory, "MaterialDesignDemo.exe")
            };

            Process appProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processInfo.FileName))
                .FirstOrDefault(x => !x.HasExited && x.MainWindowHandle != IntPtr.Zero);
            if (appProcess is null)
            {
                if (!File.Exists(Path.Combine(processInfo.WorkingDirectory, processInfo.FileName)))
                {
                    throw new FileNotFoundException($"{processInfo.FileName} was not in expected directory: {processInfo.WorkingDirectory}");
                }
                appProcess = Process.Start(processInfo);

                //Forcing the the driver to wait for the application
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            Process[] winappProcess = Process.GetProcessesByName("WinAppDriver");
            if (winappProcess.Length == 0)
            {
                var path = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("WinAppDriver was not in default directory");
                }
                Process.Start(path);
            }

            var appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("appTopLevelWindow", appProcess.MainWindowHandle.ToInt32().ToString("x"));

            Driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appOptions);

            WindowsElement? element = Driver.FindElementByName("Material Design In XAML Toolkit");

            Assert.NotNull(element);

            AppHasLaunched = true;
        }
    }
}
