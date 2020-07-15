using System;
using System.Runtime.InteropServices;

namespace VTTests
{
    internal class NativeMethods
    {
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
