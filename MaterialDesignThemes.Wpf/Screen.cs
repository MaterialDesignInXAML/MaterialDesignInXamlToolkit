using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    ///<summary>
    /// Represents a display device or multiple display devices on a single system.
    /// Based on http://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Screen.cs
    /// </summary>
    internal class Screen
    {
        private static class NativeMethods
        {
            private const string User32 = "user32.dll";

            [DllImport(User32, ExactSpelling = true, CharSet = CharSet.Auto)]
            [ResourceExposure(ResourceScope.None)]
            public static extern int GetSystemMetrics(int nIndex);

            [DllImport(User32, CharSet = CharSet.Auto)]
            [ResourceExposure(ResourceScope.None)]
            public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]MONITORINFOEX info);

            [DllImport(User32, ExactSpelling = true)]
            [ResourceExposure(ResourceScope.None)]
            public static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

            [DllImport(User32, ExactSpelling = true)]
            [ResourceExposure(ResourceScope.None)]
            public static extern IntPtr MonitorFromPoint(POINTSTRUCT pt, int flags);

            [DllImport(User32, ExactSpelling = true)]
            [ResourceExposure(ResourceScope.None)]
            public static extern IntPtr MonitorFromRect(ref RECT rect, int flags);

            public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
            public class MONITORINFOEX
            {
                internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
                internal RECT rcMonitor = new RECT();
                internal RECT rcWork = new RECT();
                internal int dwFlags = 0;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
                internal char[] szDevice = new char[32];
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;

                public RECT(Rect r)
                {
                    left = (int)r.Left;
                    top = (int)r.Top;
                    right = (int)r.Right;
                    bottom = (int)r.Bottom;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public class COMRECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINTSTRUCT
            {
                public int x;
                public int y;
                public POINTSTRUCT(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }
            }

            public static readonly HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

            public const int SM_CMONITORS = 80;
        }

        private readonly IntPtr _hmonitor;

        /// <summary>
        /// Available working area on the screen. This excludes taskbars and other
        /// docked windows.
        /// </summary>
        private Rect _workingArea = Rect.Empty;

        private static readonly object _syncLock = new object();//used to lock this class before sync'ing to SystemEvents

        private static int _desktopChangedCount = -1;//static counter of desktop size changes

        private int _currentDesktopChangedCount = -1;//instance-based counter used to invalidate WorkingArea

        // This identifier is just for us, so that we don't try to call the multimon
        // functions if we just need the primary monitor... this is safer for
        // non-multimon OSes.
        private const int PRIMARY_MONITOR = unchecked((int)0xBAADF00D);

        private const int MONITOR_DEFAULTTONEAREST = 0x00000002;
        private const int MONITORINFOF_PRIMARY = 0x00000001;

        private static readonly bool _multiMonitorSupport = NativeMethods.GetSystemMetrics(NativeMethods.SM_CMONITORS) != 0;
        private static Screen[] _screens;

        private Screen(IntPtr monitor)
        {
            if (!_multiMonitorSupport || monitor == (IntPtr)PRIMARY_MONITOR)
            {
                // Single monitor system
                Bounds = new Rect(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop,
                    SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight);
                Primary = true;
                DeviceName = "DISPLAY";
            }
            else
            {
                // MultiMonitor System
                // We call the 'A' version of GetMonitorInfoA() because
                // the 'W' version just never fills out the struct properly on Win2K.
                NativeMethods.MONITORINFOEX info = new NativeMethods.MONITORINFOEX();
                NativeMethods.GetMonitorInfo(new HandleRef(null, monitor), info);
                Bounds = new Rect(info.rcMonitor.left, info.rcMonitor.top, info.rcMonitor.right - info.rcMonitor.left, info.rcMonitor.bottom - info.rcMonitor.top);
                Primary = (info.dwFlags & MONITORINFOF_PRIMARY) != 0;

                DeviceName = new string(info.szDevice);
                DeviceName = DeviceName.TrimEnd((char)0);

            }
            _hmonitor = monitor;
        }

        /// <summary>
        /// Gets an array of all of the displays on the system.
        /// </summary>
        public static Screen[] AllScreens
        {
            get
            {
                if (_screens == null)
                {
                    if (_multiMonitorSupport)
                    {
                        MonitorEnumCallback closure = new MonitorEnumCallback();
                        NativeMethods.MonitorEnumProc proc = closure.Callback;
                        NativeMethods.EnumDisplayMonitors(NativeMethods.NullHandleRef, null, proc, IntPtr.Zero);

                        if (closure.Screens.Count > 0)
                        {
                            Screen[] temp = new Screen[closure.Screens.Count];
                            closure.Screens.CopyTo(temp, 0);
                            _screens = temp;
                        }
                        else
                        {
                            _screens = new[] { new Screen((IntPtr)PRIMARY_MONITOR) };
                        }
                    }
                    else
                    {
                        _screens = new[] { PrimaryScreen };
                    }

                    // Now that we have our screens, attach a display setting changed
                    // event so that we know when to invalidate them.
                    SystemEvents.DisplaySettingsChanging += OnDisplaySettingsChanging;
                }

                return _screens;
            }
        }

        /// <summary>
        /// Gets the bounds of the display.
        /// </summary>
        public Rect Bounds { get; }

        /// <summary>
        /// Gets the device name associated with a display.
        /// </summary>
        public string DeviceName { get; }

        /// <summary>
        /// Gets a value indicating whether a particular display is the primary device.
        /// </summary>
        public bool Primary { get; }

        /// <summary>
        /// Gets the primary display.
        /// </summary>
        public static Screen PrimaryScreen
        {
            get
            {
                if (_multiMonitorSupport)
                {
                    foreach (Screen screen in AllScreens)
                    {
                        if (screen.Primary)
                        {
                            return screen;
                        }
                    }
                    return null;
                }
                return new Screen((IntPtr)PRIMARY_MONITOR);
            }
        }

        /// <summary>
        /// Gets the working area of the screen.
        /// </summary>
        public Rect WorkingArea
        {
            get
            {

                //if the static Screen class has a different desktop change count 
                //than this instance then update the count and recalculate our working area
                if (_currentDesktopChangedCount != DesktopChangedCount)
                {
                    Interlocked.Exchange(ref _currentDesktopChangedCount, DesktopChangedCount);

                    if (!_multiMonitorSupport || _hmonitor == (IntPtr)PRIMARY_MONITOR)
                    {
                        // Single monitor system
                        _workingArea = SystemParameters.WorkArea;
                    }
                    else
                    {
                        // MultiMonitor System
                        // We call the 'A' version of GetMonitorInfoA() because
                        // the 'W' version just never fills out the struct properly on Win2K.
                        NativeMethods.MONITORINFOEX info = new NativeMethods.MONITORINFOEX();
                        NativeMethods.GetMonitorInfo(new HandleRef(null, _hmonitor), info);
                        _workingArea = new Rect(info.rcWork.left, info.rcWork.top, info.rcWork.right - info.rcWork.left, info.rcWork.bottom - info.rcWork.top);
                    }
                }
                return _workingArea;
            }
        }

        /// <summary>
        /// Screen instances call this property to determine
        /// if their WorkingArea cache needs to be invalidated.
        /// </summary>
        private static int DesktopChangedCount
        {
            get
            {
                if (_desktopChangedCount == -1)
                {

                    lock (_syncLock)
                    {

                        //now that we have a lock, verify (again) our changecount...
                        if (_desktopChangedCount == -1)
                        {
                            //sync the UserPreference.Desktop change event.  We'll keep count 
                            //of desktop changes so that the WorkingArea property on Screen 
                            //instances know when to invalidate their cache.
                            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;

                            _desktopChangedCount = 0;
                        }
                    }
                }
                return _desktopChangedCount;
            }
        }

        /// <summary>
        /// Specifies a value that indicates whether the specified object is equal to this one.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Screen comp && _hmonitor == comp._hmonitor;
        }

        /// <summary>
        /// Retrieves a <see cref='Screen'/> for the monitor that contains the specified point.
        /// </summary>
        public static Screen FromPoint(Point point)
        {
            if (_multiMonitorSupport)
            {
                NativeMethods.POINTSTRUCT pt = new NativeMethods.POINTSTRUCT((int)point.X, (int)point.Y);
                return new Screen(NativeMethods.MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST));
            }
            return new Screen((IntPtr)PRIMARY_MONITOR);
        }

        /// <summary>
        /// Retrieves a <see cref='Screen'/> for the monitor that contains the largest region of the Rect.
        /// </summary>
        public static Screen FromRect(Rect rect)
        {
            if (_multiMonitorSupport)
            {
                NativeMethods.RECT rc = new NativeMethods.RECT(rect);
                return new Screen(NativeMethods.MonitorFromRect(ref rc, MONITOR_DEFAULTTONEAREST));
            }
            return new Screen((IntPtr)PRIMARY_MONITOR);
        }

        ///<summary>
        /// Retrieves the working area for the monitor that is closest to the specified point.
        /// </summary>
        public static Rect GetWorkingArea(Point pt)
        {
            return FromPoint(pt).WorkingArea;
        }

        ///<summary>
        /// Retrieves the working area for the monitor that contains the largest region of the specified Rect.
        /// </summary>
        public static Rect GetWorkingArea(Rect rect)
        {
            return FromRect(rect).WorkingArea;
        }

        ///<summary>
        /// Retrieves the bounds of the monitor that is closest to the specified point.
        /// </summary>
        public static Rect GetBounds(Point pt)
        {
            return FromPoint(pt).Bounds;
        }

        /// <summary>
        /// Retrieves the bounds of the monitor that contains the largest region of the specified Rect.
        /// </summary>
        public static Rect GetBounds(Rect rect)
        {
            return FromRect(rect).Bounds;
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        public override int GetHashCode()
        {
            return (int)_hmonitor;
        }

        /// <summary>
        /// Called by the SystemEvents class when our display settings are
        /// changing.  We cache screen information and at this point we must
        /// invalidate our cache.
        /// </summary>
        private static void OnDisplaySettingsChanging(object sender, EventArgs e)
        {
            // Now that we've responded to this event, we don't need it again until
            // someone re-queries. We will re-add the event at that time.
            SystemEvents.DisplaySettingsChanging -= OnDisplaySettingsChanging;

            // Display settings changed, so the set of screens we have is invalid.
            _screens = null;
        }

        /// <summary>
        /// Called by the SystemEvents class when our display settings have
        /// changed.  Here, we increment a static counter that Screen instances
        /// can check against to invalidate their cache.
        /// </summary>
        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Desktop)
            {
                Interlocked.Increment(ref _desktopChangedCount);
            }
        }

        /// <summary>
        /// Retrieves a string representing this object.
        /// </summary>
        public override string ToString()
        {
            return GetType().Name + "[Bounds=" + Bounds + " WorkingArea=" + WorkingArea + " Primary=" + Primary + " DeviceName=" + DeviceName;
        }

        private class MonitorEnumCallback
        {
            public List<Screen> Screens { get; } = new List<Screen>();

            public virtual bool Callback(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lparam)
            {
                Screens.Add(new Screen(monitor));
                return true;
            }
        }
    }
}