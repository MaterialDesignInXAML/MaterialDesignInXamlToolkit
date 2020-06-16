using System;
using System.IO;
using System.Runtime.CompilerServices;
using OpenQA.Selenium.Appium.Windows;
using Xunit.Abstractions;

namespace MaterialDesignThemes.UITests
{
    public sealed class TestRecorder : IDisposable
    {
        private static readonly string _ProjectName = typeof(TestRecorder).Assembly.GetName().Name!;

        private WindowsDriver<WindowsElement> Driver { get; }
        private ITestOutputHelper Output { get; }

        private string BaseFileName { get; }
        private string Directory { get; }

        private bool IsDisposed { get; set; }
        private bool IsSuccess { get; set; }

        public TestRecorder(
            WindowsDriver<WindowsElement> driver,
            ITestOutputHelper output,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string unitTestMethod = "")
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Output = output ?? throw new ArgumentNullException(nameof(output));

            Directory = callerFilePath.Substring(callerFilePath.IndexOf(_ProjectName) + _ProjectName.Length + 1);
            Directory = Path.ChangeExtension(Directory, "").TrimEnd('.');
            Directory = Path.Combine(Path.GetFullPath("."), "Screenshots", Directory);
            System.IO.Directory.CreateDirectory(Directory);


            BaseFileName = unitTestMethod;
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                BaseFileName = BaseFileName.Replace($"{invalidChar}", "");
            }
        }

        /// <summary>
        /// Calling this method indicates that the test completed successfully and no additional recording is needed.
        /// </summary>
        public void Success() => IsSuccess = true;

        public void SaveScreenshot([CallerLineNumber]int? lineNumber = null)
            => SaveScreenshot(lineNumber?.ToString() ?? "");

        private void SaveScreenshot(string suffix)
        {
            string fileName = $"{BaseFileName}{suffix}.jpg";
            string fullPath = Path.Combine(Directory, fileName);
            File.Delete(fullPath);

            var screenshot = Driver.GetScreenshot();
            screenshot.SaveAsFile(fullPath);
            Output.WriteLine("Saved screenshot: " + fullPath);
        }

        #region IDisposable Support
        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    if (!IsSuccess)
                    {
                        SaveScreenshot("");
                    }
                }
                IsDisposed = true;
            }
        }

        public void Dispose() => Dispose(true);
        #endregion

    }
}
