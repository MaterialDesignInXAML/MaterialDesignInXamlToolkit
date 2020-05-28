using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;

namespace MaterialDesignThemes.UITests
{
    public class TestBase
    {
#if DEBUG
        private const string Configuration = "Debug";
#else
        private const string Configuration = "Release";
#endif

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected WindowsDriver<WindowsElement> Driver { get; private set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        private bool AppHasLaunched { get; set; }

        protected void LaunchApplication()
        {
            if (AppHasLaunched) return;

            IntPtr mainWindowHandle = StartApp();

            StartWinAppDriver();

            var appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("deviceName", "WindowsPC");
            appOptions.AddAdditionalCapability("appTopLevelWindow", mainWindowHandle.ToInt64().ToString("x"));

            Driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appOptions);

            WindowsElement? element = Driver.FindElementByName("Material Design In XAML Toolkit");

            Assert.NotNull(element);

            AppHasLaunched = true;
        }

        private static IntPtr StartApp()
        {
            string workingDirectory = Path.GetFullPath(@$"..\..\..\..\MainDemo.Wpf\bin\{Configuration}\netcoreapp3.1\");
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
                appProcess.WaitForInputIdle();
                while(appProcess?.MainWindowHandle == IntPtr.Zero)
                {
                    //NET Core runtime currently has an issue where the main window handle wont get refreshed.
                    //appProcess.Refresh();
                    //https://github.com/dotnet/runtime/issues/32690
                    appProcess = Process.GetProcessById(appProcess.Id);
                }
            }

            if ((appProcess?.MainWindowHandle ?? IntPtr.Zero) == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to find window handle for process");
            }

            ShowWindow(appProcess!.MainWindowHandle);

            return appProcess.MainWindowHandle;
        }

        private static void StartWinAppDriver()
        {
            Process[] winappProcess = Process.GetProcessesByName("WinAppDriver");
            if (winappProcess.Length == 0)
            {
                //Search default install location on all drives
                foreach(var drive in Environment.GetLogicalDrives())
                {
                    var path = @$"{drive}Program Files (x86)\Windows Application Driver\WinAppDriver.exe";
                    if (File.Exists(path))
                    {
                        Process.Start(path);
                        // Wait to find socket
                        using var client = new TcpClient();
                        client.Connect("127.0.0.1", 4723);
                        return;
                    }
                }
                throw new InvalidOperationException("WinAppDriver was not found.");
            }
        }

        private static void ShowWindow(IntPtr windowHandle)
        {
            NativeMethods.ShowWindow(windowHandle, (int)NativeMethods.ShowWindowCommands.Restore);
            //NB: This will not work in all cases: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setforegroundwindow#remarks
            NativeMethods.SetForegroundWindow(windowHandle);
        }

        internal class NativeMethods
        {
            public enum ShowWindowCommands
            {
                /// <summary>
                /// Hides the window and activates another window.
                /// </summary>
                Hide = 0,
                /// <summary>
                /// Activates and displays a window. If the window is minimized or
                /// maximized, the system restores it to its original size and position.
                /// An application should specify this flag when displaying the window
                /// for the first time.
                /// </summary>
                Normal = 1,
                /// <summary>
                /// Activates the window and displays it as a minimized window.
                /// </summary>
                ShowMinimized = 2,
                /// <summary>
                /// Maximizes the specified window.
                /// </summary>
                Maximize = 3, // is this the right value?
                /// <summary>
                /// Activates the window and displays it as a maximized window.
                /// </summary>      
                ShowMaximized = 3,
                /// <summary>
                /// Displays a window in its most recent size and position. This value
                /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
                /// the window is not activated.
                /// </summary>
                ShowNoActivate = 4,
                /// <summary>
                /// Activates the window and displays it in its current size and position.
                /// </summary>
                Show = 5,
                /// <summary>
                /// Minimizes the specified window and activates the next top-level
                /// window in the Z order.
                /// </summary>
                Minimize = 6,
                /// <summary>
                /// Displays the window as a minimized window. This value is similar to
                /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
                /// window is not activated.
                /// </summary>
                ShowMinNoActive = 7,
                /// <summary>
                /// Displays the window in its current size and position. This value is
                /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
                /// window is not activated.
                /// </summary>
                ShowNA = 8,
                /// <summary>
                /// Activates and displays the window. If the window is minimized or
                /// maximized, the system restores it to its original size and position.
                /// An application should specify this flag when restoring a minimized window.
                /// </summary>
                Restore = 9,
                /// <summary>
                /// Sets the show state based on the SW_* value specified in the
                /// STARTUPINFO structure passed to the CreateProcess function by the
                /// program that started the application.
                /// </summary>
                ShowDefault = 10,
                /// <summary>
                ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
                /// that owns the window is not responding. This flag should only be
                /// used when minimizing windows from a different thread.
                /// </summary>
                ForceMinimize = 11
            }

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("User32.dll")]
            public static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        }
    }
}
