using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// This class was initially based on work done in ControlzEx
    /// https://github.com/ControlzEx/ControlzEx
    /// 
    /// This custom popup can be used by validation error templates or something else.
    /// It provides some additional nice features:
    ///     - repositioning if host-window size or location changed
    ///     - repositioning if host-window gets maximized and vice versa
    ///     - it's only topmost if the host-window is activated
    /// </summary>
    public class PopupEx : Popup
    {
        public static readonly DependencyProperty CloseOnMouseLeftButtonDownProperty
            = DependencyProperty.Register(nameof(CloseOnMouseLeftButtonDown),
                                          typeof(bool),
                                          typeof(PopupEx),
                                          new PropertyMetadata(false));

        /// <summary>
        /// Gets/sets if the popup can be closed by left mouse button down.
        /// </summary>
        public bool CloseOnMouseLeftButtonDown
        {
            get => (bool)GetValue(CloseOnMouseLeftButtonDownProperty);
            set => SetValue(CloseOnMouseLeftButtonDownProperty, value);
        }

        public static readonly DependencyProperty AllowTopMostProperty
            = DependencyProperty.Register(nameof(AllowTopMost),
                                          typeof(bool),
                                          typeof(PopupEx),
                                          new PropertyMetadata(true));

        public bool AllowTopMost
        {
            get => (bool)GetValue(AllowTopMostProperty);
            set => SetValue(AllowTopMostProperty, value);
        }

        public PopupEx()
        {
            Loaded += PopupEx_Loaded;
            Opened += PopupEx_Opened;
        }

        /// <summary>
        /// Causes the popup to update it's position according to it's current settings.
        /// </summary>
        public void RefreshPosition()
        {
            var offset = HorizontalOffset;
            // "bump" the offset to cause the popup to reposition itself on its own
            SetCurrentValue(HorizontalOffsetProperty, offset + 1);
            SetCurrentValue(HorizontalOffsetProperty, offset);
        }

        private void PopupEx_Loaded(object? sender, RoutedEventArgs e)
        {
            var target = PlacementTarget as FrameworkElement;
            if (target is null)
            {
                return;
            }

            _hostWindow = Window.GetWindow(target);
            if (_hostWindow is null)
            {
                return;
            }

            _hostWindow.LocationChanged -= HostWindow_SizeOrLocationChanged;
            _hostWindow.LocationChanged += HostWindow_SizeOrLocationChanged;
            _hostWindow.SizeChanged -= HostWindow_SizeOrLocationChanged;
            _hostWindow.SizeChanged += HostWindow_SizeOrLocationChanged;
            target.SizeChanged -= HostWindow_SizeOrLocationChanged;
            target.SizeChanged += HostWindow_SizeOrLocationChanged;
            _hostWindow.StateChanged -= HostWindow_StateChanged;
            _hostWindow.StateChanged += HostWindow_StateChanged;
            _hostWindow.Activated -= HostWindow_Activated;
            _hostWindow.Activated += HostWindow_Activated;
            _hostWindow.Deactivated -= HostWindow_Deactivated;
            _hostWindow.Deactivated += HostWindow_Deactivated;

            Unloaded -= PopupEx_Unloaded;
            Unloaded += PopupEx_Unloaded;
        }

        private void PopupEx_Opened(object? sender, EventArgs e)
            => SetTopmostState(_hostWindow?.IsActive ?? true);

        private void HostWindow_Activated(object? sender, EventArgs e)
            => SetTopmostState(true);

        private void HostWindow_Deactivated(object? sender, EventArgs e)
            => SetTopmostState(_hostWindow?.Topmost ?? false);

        private void PopupEx_Unloaded(object? sender, RoutedEventArgs e)
        {
            if (PlacementTarget is FrameworkElement target)
            {
                target.SizeChanged -= HostWindow_SizeOrLocationChanged;
            }
            if (_hostWindow != null)
            {
                _hostWindow.LocationChanged -= HostWindow_SizeOrLocationChanged;
                _hostWindow.SizeChanged -= HostWindow_SizeOrLocationChanged;
                _hostWindow.StateChanged -= HostWindow_StateChanged;
                _hostWindow.Activated -= HostWindow_Activated;
                _hostWindow.Deactivated -= HostWindow_Deactivated;
            }
            Unloaded -= PopupEx_Unloaded;
            Opened -= PopupEx_Opened;
            _hostWindow = null;
        }

        private void HostWindow_StateChanged(object? sender, EventArgs e)
        {
            if (_hostWindow != null && _hostWindow.WindowState != WindowState.Minimized)
            {
                // special handling for validation popup
                var target = PlacementTarget as FrameworkElement;
                var holder = target != null ? target.DataContext as AdornedElementPlaceholder : null;
                if (holder != null && holder.AdornedElement != null)
                {
                    PopupAnimation = PopupAnimation.None;
                    IsOpen = false;
                    var errorTemplate = holder.AdornedElement.GetValue(Validation.ErrorTemplateProperty);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, null);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, errorTemplate);
                }
            }
        }

        private void HostWindow_SizeOrLocationChanged(object? sender, EventArgs e)
            => RefreshPosition();

        private void SetTopmostState(bool isTop)
        {
            isTop &= AllowTopMost;
            // Don’t apply state if it’s the same as incoming state
            if (appliedTopMost.HasValue && appliedTopMost == isTop)
            {
                return;
            }

            if (Child is null)
            {
                return;
            }

            var hwndSource = (PresentationSource.FromVisual(Child)) as HwndSource;
            if (hwndSource is null)
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

            appliedTopMost = isTop;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (CloseOnMouseLeftButtonDown)
            {
                IsOpen = false;
            }
        }

        private Window? _hostWindow;
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

            public void Offset(int dx, int dy)
            {
                _left += dx;
                _top += dy;
                _right += dx;
                _bottom += dy;
            }

            public int Left
            {
                get => _left;
                set => _left = value;
            }

            public int Right
            {
                get => _right;
                set => _right = value;
            }

            public int Top
            {
                get => _top;
                set => _top = value;
            }

            public int Bottom
            {
                get => _bottom;
                set => _bottom = value;
            }

            public int Width => _right - _left;

            public int Height => _bottom - _top;

            public POINT Position => new POINT { x = _left, y = _top };

            public SIZE Size => new SIZE { cx = Width, cy = Height };

            public static RECT Union(RECT rect1, RECT rect2)
            {
                return new RECT
                {
                    Left = Math.Min(rect1.Left, rect2.Left),
                    Top = Math.Min(rect1.Top, rect2.Top),
                    Right = Math.Max(rect1.Right, rect2.Right),
                    Bottom = Math.Max(rect1.Bottom, rect2.Bottom),
                };
            }

            public override bool Equals(object? obj)
            {
                try
                {
                    var rc = (RECT)obj!;
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
                => (_left << 16 | LOWORD(_right)) ^ (_top << 16 | LOWORD(_bottom));
        }

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SWP uFlags);

        [SecurityCritical]
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