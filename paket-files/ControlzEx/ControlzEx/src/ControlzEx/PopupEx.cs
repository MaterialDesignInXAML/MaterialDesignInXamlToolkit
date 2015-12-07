using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace ControlzEx
{
    /// <summary>
    /// This custom popup can be used by validation error templates or something else.
    /// It provides some additional nice features:
    ///     - repositioning if host-window size or location changed
    ///     - repositioning if host-window gets maximized and vice versa
    ///     - it's only topmost if the host-window is activated
    /// </summary>
    public class PopupEx : Popup
    {
        public static readonly DependencyProperty CloseOnMouseLeftButtonDownProperty
            = DependencyProperty.Register("CloseOnMouseLeftButtonDown",
                                          typeof(bool),
                                          typeof(PopupEx),
                                          new PropertyMetadata(false));

        /// <summary>
        /// Gets/sets if the popup can be closed by left mouse button down.
        /// </summary>
        public bool CloseOnMouseLeftButtonDown
        {
            get { return (bool)GetValue(CloseOnMouseLeftButtonDownProperty); }
            set { SetValue(CloseOnMouseLeftButtonDownProperty, value); }
        }

        public PopupEx()
        {
            this.Loaded += this.PopupEx_Loaded;
            this.Opened += this.PopupEx_Opened;
        }

        /// <summary>
        /// Causes the popup to update it's position according to it's current settings.
        /// </summary>
        public void RefreshPosition()
        {
            var offset = this.HorizontalOffset;
            // "bump" the offset to cause the popup to reposition itself on its own
            SetCurrentValue(HorizontalOffsetProperty, offset + 1);
            SetCurrentValue(HorizontalOffsetProperty, offset);
        }

        private void PopupEx_Loaded(object sender, RoutedEventArgs e)
        {
            var target = this.PlacementTarget as FrameworkElement;
            if (target == null)
            {
                return;
            }

            this.hostWindow = Window.GetWindow(target);
            if (this.hostWindow == null)
            {
                return;
            }

            this.hostWindow.LocationChanged -= this.hostWindow_SizeOrLocationChanged;
            this.hostWindow.LocationChanged += this.hostWindow_SizeOrLocationChanged;
            this.hostWindow.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            this.hostWindow.SizeChanged += this.hostWindow_SizeOrLocationChanged;
            target.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            target.SizeChanged += this.hostWindow_SizeOrLocationChanged;
            this.hostWindow.StateChanged -= this.hostWindow_StateChanged;
            this.hostWindow.StateChanged += this.hostWindow_StateChanged;
            this.hostWindow.Activated -= this.hostWindow_Activated;
            this.hostWindow.Activated += this.hostWindow_Activated;
            this.hostWindow.Deactivated -= this.hostWindow_Deactivated;
            this.hostWindow.Deactivated += this.hostWindow_Deactivated;

            this.Unloaded -= this.PopupEx_Unloaded;
            this.Unloaded += this.PopupEx_Unloaded;
        }

        private void PopupEx_Opened(object sender, EventArgs e)
        {
            this.SetTopmostState(true);
        }

        private void hostWindow_Activated(object sender, EventArgs e)
        {
            this.SetTopmostState(true);
        }

        private void hostWindow_Deactivated(object sender, EventArgs e)
        {
            this.SetTopmostState(false);
        }

        private void PopupEx_Unloaded(object sender, RoutedEventArgs e)
        {
            var target = this.PlacementTarget as FrameworkElement;
            if (target != null)
            {
                target.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            }
            if (this.hostWindow != null)
            {
                this.hostWindow.LocationChanged -= this.hostWindow_SizeOrLocationChanged;
                this.hostWindow.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
                this.hostWindow.StateChanged -= this.hostWindow_StateChanged;
                this.hostWindow.Activated -= this.hostWindow_Activated;
                this.hostWindow.Deactivated -= this.hostWindow_Deactivated;
            }
            this.Unloaded -= this.PopupEx_Unloaded;
            this.Opened -= this.PopupEx_Opened;
            this.hostWindow = null;
        }

        private void hostWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.hostWindow != null && this.hostWindow.WindowState != WindowState.Minimized)
            {
                // special handling for validation popup
                var target = this.PlacementTarget as FrameworkElement;
                var holder = target != null ? target.DataContext as AdornedElementPlaceholder : null;
                if (holder != null && holder.AdornedElement != null)
                {
                    this.PopupAnimation = PopupAnimation.None;
                    this.IsOpen = false;
                    var errorTemplate = holder.AdornedElement.GetValue(Validation.ErrorTemplateProperty);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, null);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, errorTemplate);
                }
            }
        }

        private void hostWindow_SizeOrLocationChanged(object sender, EventArgs e)
        {
            RefreshPosition();
        }

        private void SetTopmostState(bool isTop)
        {
            // Don’t apply state if it’s the same as incoming state
            if (this.appliedTopMost.HasValue && this.appliedTopMost == isTop)
            {
                return;
            }

            if (this.Child == null)
            {
                return;
            }

            var hwndSource = (PresentationSource.FromVisual(this.Child)) as HwndSource;
            if (hwndSource == null)
            {
                return;
            }
            var hwnd = hwndSource.Handle;

            RECT rect;
            if (!GetWindowRect(hwnd, out rect))
            {
                return;
            }
            //Debug.WriteLine("setting z-order " + isTop);

            var left = rect.Left;
            var top = rect.Top;
            var width = rect.Width;
            var height = rect.Height;
            if (isTop)
            {
                SetWindowPos(hwnd, HWND_TOPMOST, left, top, width, height, SWP.TOPMOST);
            }
            else
            {
                // Z-Order would only get refreshed/reflected if clicking the
                // the titlebar (as opposed to other parts of the external
                // window) unless I first set the popup to HWND_BOTTOM
                // then HWND_TOP before HWND_NOTOPMOST
                SetWindowPos(hwnd, HWND_BOTTOM, left, top, width, height, SWP.TOPMOST);
                SetWindowPos(hwnd, HWND_TOP, left, top, width, height, SWP.TOPMOST);
                SetWindowPos(hwnd, HWND_NOTOPMOST, left, top, width, height, SWP.TOPMOST);
            }

            this.appliedTopMost = isTop;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (CloseOnMouseLeftButtonDown)
            {
                this.IsOpen = false;
            }
        }

        private Window hostWindow;
        private bool? appliedTopMost;
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// SetWindowPos options
        /// </summary>
        [Flags]
        internal enum SWP
        {
            ASYNCWINDOWPOS = 0x4000,
            DEFERERASE = 0x2000,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            HIDEWINDOW = 0x0080,
            NOACTIVATE = 0x0010,
            NOCOPYBITS = 0x0100,
            NOMOVE = 0x0002,
            NOOWNERZORDER = 0x0200,
            NOREDRAW = 0x0008,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            NOSIZE = 0x0001,
            NOZORDER = 0x0004,
            SHOWWINDOW = 0x0040,
            TOPMOST = SWP.NOACTIVATE | SWP.NOOWNERZORDER | SWP.NOSIZE | SWP.NOMOVE | SWP.NOREDRAW | SWP.NOSENDCHANGING,
        }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal static int LOWORD(int i)
        {
            return (short)(i & 0xFFFF);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            public int cx;
            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            private int _left;
            private int _top;
            private int _right;
            private int _bottom;

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public void Offset(int dx, int dy)
            {
                _left += dx;
                _top += dy;
                _right += dx;
                _bottom += dy;
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Left
            {
                get { return _left; }
                set { _left = value; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Right
            {
                get { return _right; }
                set { _right = value; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Top
            {
                get { return _top; }
                set { _top = value; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Bottom
            {
                get { return _bottom; }
                set { _bottom = value; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Width
            {
                get { return _right - _left; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public int Height
            {
                get { return _bottom - _top; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public POINT Position
            {
                get { return new POINT { x = _left, y = _top }; }
            }

            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public SIZE Size
            {
                get { return new SIZE { cx = Width, cy = Height }; }
            }

            public static RECT Union(RECT rect1, RECT rect2)
            {
                return new RECT {
                    Left = Math.Min(rect1.Left, rect2.Left),
                    Top = Math.Min(rect1.Top, rect2.Top),
                    Right = Math.Max(rect1.Right, rect2.Right),
                    Bottom = Math.Max(rect1.Bottom, rect2.Bottom),
                };
            }

            public override bool Equals(object obj)
            {
                try
                {
                    var rc = (RECT)obj;
                    return rc._bottom == _bottom
                        && rc._left == _left
                        && rc._right == _right
                        && rc._top == _top;
                }
                catch (InvalidCastException)
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return (_left << 16 | LOWORD(_right)) ^ (_top << 16 | LOWORD(_bottom));
            }
        }

        [SecurityCritical]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [SecurityCritical]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SWP uFlags);

        [SecurityCritical]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SWP uFlags)
        {
            if (!_SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags))
            {
                // If this fails it's never worth taking down the process.  Let the caller deal with the error if they want.
                return false;
            }

            return true;
        }
    }
}