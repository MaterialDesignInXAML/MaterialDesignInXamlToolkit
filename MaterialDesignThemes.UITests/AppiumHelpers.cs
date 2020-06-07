using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Sdk;
using DefaultWait = OpenQA.Selenium.Support.UI.DefaultWait<OpenQA.Selenium.Appium.Windows.WindowsDriver<OpenQA.Selenium.Appium.Windows.WindowsElement>>;

namespace MaterialDesignThemes.UITests
{
    public static class AppiumHelpers
    {
        private static IClock Clock { get; } = new SystemClock();

        public static WindowsElement FindElementByAccessibilityId(
            this WindowsDriver<WindowsElement> driver, 
            string selector, 
            TimeSpan timeout,
            TimeSpan? pollingInterval = null)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException($"'{nameof(selector)}' cannot be null or empty", nameof(selector));
            }

            DefaultWait wait = GetWait(driver, timeout, pollingInterval);
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            return wait.Until(driver => driver.FindElementByAccessibilityId(selector)!);
        }

        public static void WaitFor(this WindowsDriver<WindowsElement> driver,
            Func<bool> condition,
            TimeSpan? timeout = null,
            TimeSpan? pollingInterval = null)
            => driver.WaitUntil<bool>(_ =>
            {
                Assert.True(condition());
                return true;
            }, timeout ?? TimeSpan.FromSeconds(2), pollingInterval);

        public static void WaitUntil(
            this WindowsDriver<WindowsElement> driver,
            Action condition,
            TimeSpan timeout,
            TimeSpan? pollingInterval = null)
            => driver.WaitUntil<bool>(_ =>
            {
                condition();
                return true;
            }, timeout, pollingInterval);

        public static void WaitUntil(
            this WindowsDriver<WindowsElement> driver,
            Action<WindowsDriver<WindowsElement>> condition,
            TimeSpan timeout,
            TimeSpan? pollingInterval = null)
            => driver.WaitUntil<bool>(driver =>
            {
                condition(driver);
                return true;
            }, timeout, pollingInterval);

        public static TResult WaitUntil<TResult>(
            this WindowsDriver<WindowsElement> driver,
            Func<TResult> condition,
            TimeSpan timeout,
            TimeSpan? pollingInterval = null) 
            => driver.WaitUntil(_ => condition(), timeout, pollingInterval);

        public static TResult WaitUntil<TResult>(
            this WindowsDriver<WindowsElement> driver,
            Func<WindowsDriver<WindowsElement>, TResult> condition,
            TimeSpan timeout,
            TimeSpan? pollingInterval = null)
        {
            if (driver is null)
            {
                throw new ArgumentNullException(nameof(driver));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            DefaultWait wait = GetWait(driver, timeout, pollingInterval);
            wait.IgnoreExceptionTypes(
                typeof(WebDriverException),
                typeof(XunitException)
            );
            return wait.Until(condition);
        }

        private static DefaultWait GetWait(
            WindowsDriver<WindowsElement> driver,
            TimeSpan timeout,
            TimeSpan? pollingInterval = null)
        {
            TimeSpan interval = pollingInterval ?? TimeSpan.FromMilliseconds(timeout.TotalMilliseconds / 10);
            var wait = new DefaultWait(driver, Clock)
            {
                Timeout = timeout,
                PollingInterval = interval
            };
            return wait;
        }
    }
}
