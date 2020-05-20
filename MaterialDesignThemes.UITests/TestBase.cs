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

namespace MaterialDesignThemes.UITests
{
    public class TestBase
    {
        protected WindowsDriver<WindowsElement>? Driver { get; private set; }

        private bool AppHasLaunched { get; set; }


        public void Click(AppiumWebElement element)
        {
            WaitForElement(() => new Actions(Driver).MoveToElement(element).Click().Perform());
        }

        public void SendKeys(AppiumWebElement element, string input)
        {
            WaitForElement(() => new Actions(Driver).SendKeys(element, input).Perform());
        }

        // Would this be more readable/easier to use taking in two Points instead of four ints?
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

            Process[] inputProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(processInfo.FileName));
            if (inputProcess.Length == 0)
            {
                if (!File.Exists(Path.Combine(processInfo.WorkingDirectory, processInfo.FileName)))
                {
                    throw new FileNotFoundException($"{processInfo.FileName} was not in expected directory: {processInfo.WorkingDirectory}");
                }
                var process = Process.Start(processInfo);

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
            appOptions.AddAdditionalCapability("app", "Root");
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");

            var desktopSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appOptions);
            //var elements = Driver.FindElementsByName("Material Design In XAML Toolkit");
            var wait = new WebDriverWait(desktopSession, TimeSpan.FromSeconds(20));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));

            WindowsElement app = wait.Until(d => desktopSession.FindElementByAccessibilityId("Material Design in XAML Toolkit"));
            var window = app.GetAttribute("NativeWindowHandle");
            window = int.Parse(window).ToString("x");

            var newOptions = new AppiumOptions();
            newOptions.AddAdditionalCapability("appTopLevelWindow", window);

            Driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), newOptions);
            desktopSession.Dispose();
            AppHasLaunched = true;
        }
    }
}
