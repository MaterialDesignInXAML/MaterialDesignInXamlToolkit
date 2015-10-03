using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Customised popup for validation purposes.    
    /// </summary>
    /// <remarks>
    /// Originally taken from MahApps.
    /// </remarks>
    public class CustomValidationPopup : Popup
    {
        private Window _hostWindow;

        public CustomValidationPopup()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            IsOpen = false;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var target = PlacementTarget as FrameworkElement;
            if (target == null) return;

            target.SizeChanged += HostWindowSizeOrLocationChanged;

            _hostWindow = Window.GetWindow(target);
            if (_hostWindow == null) return;

            _hostWindow.LocationChanged += HostWindowSizeOrLocationChanged;
            _hostWindow.SizeChanged += HostWindowSizeOrLocationChanged;

            _hostWindow.StateChanged += HostWindowOnStateChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var target = PlacementTarget as FrameworkElement;
            if (target != null)
                target.SizeChanged -= HostWindowSizeOrLocationChanged;

            if (_hostWindow == null) return;

            _hostWindow.LocationChanged -= HostWindowSizeOrLocationChanged;
            _hostWindow.SizeChanged -= HostWindowSizeOrLocationChanged;
            _hostWindow.StateChanged -= HostWindowOnStateChanged;
        }

        private void HostWindowOnStateChanged(object sender, EventArgs e)
        {
            if (_hostWindow == null || _hostWindow.WindowState == WindowState.Minimized) return;

            var target = PlacementTarget as FrameworkElement;
            var holder = target?.DataContext as AdornedElementPlaceholder;

            if (holder?.AdornedElement == null) return;

            var errorTemplate = holder.AdornedElement.GetValue(Validation.ErrorTemplateProperty);
            holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, null);
            holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, errorTemplate);
        }

        private void HostWindowSizeOrLocationChanged(object sender, EventArgs e)
        {
            var offset = HorizontalOffset;
            // "bump" the offset to cause the popup to reposition itself on its own
            HorizontalOffset = offset + 1;
            HorizontalOffset = offset;
        }
    }
}
