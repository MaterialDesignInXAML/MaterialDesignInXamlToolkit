using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// MahApp's validation popup copy
    /// </summary>
    public sealed class CustomValidationPopup : Popup
    {
        private Window _hostWindow;

        public CustomValidationPopup()
        {
            this.Loaded += this.CustomValidationPopup_Loaded;
            this.Opened += this.CustomValidationPopup_Opened;
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.IsOpen = false;
        }

        private void CustomValidationPopup_Loaded(object sender, RoutedEventArgs e)
        {
            var target = this.PlacementTarget as FrameworkElement;
            if (target == null)
            {
                return;
            }

            this._hostWindow = Window.GetWindow(target);
            if (this._hostWindow == null)
            {
                return;
            }

            this._hostWindow.LocationChanged -= this.hostWindow_SizeOrLocationChanged;
            this._hostWindow.LocationChanged += this.hostWindow_SizeOrLocationChanged;
            this._hostWindow.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            this._hostWindow.SizeChanged += this.hostWindow_SizeOrLocationChanged;
            target.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            target.SizeChanged += this.hostWindow_SizeOrLocationChanged;
            this._hostWindow.StateChanged -= this.hostWindow_StateChanged;
            this._hostWindow.StateChanged += this.hostWindow_StateChanged;
            this._hostWindow.Activated -= this.hostWindow_Activated;
            this._hostWindow.Activated += this.hostWindow_Activated;
            this._hostWindow.Deactivated -= this.hostWindow_Deactivated;
            this._hostWindow.Deactivated += this.hostWindow_Deactivated;

            this.Unloaded -= this.CustomValidationPopup_Unloaded;
            this.Unloaded += this.CustomValidationPopup_Unloaded;
        }

        private void CustomValidationPopup_Opened(object sender, EventArgs e)
        {
            //this.SetTopmostState(true);
        }

        private void hostWindow_Activated(object sender, EventArgs e)
        {
            //this.SetTopmostState(true);
        }

        private void hostWindow_Deactivated(object sender, EventArgs e)
        {
            //this.SetTopmostState(false);
        }

        private void CustomValidationPopup_Unloaded(object sender, RoutedEventArgs e)
        {
            var target = this.PlacementTarget as FrameworkElement;
            if (target != null)
            {
                target.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
            }
            if (this._hostWindow != null)
            {
                this._hostWindow.LocationChanged -= this.hostWindow_SizeOrLocationChanged;
                this._hostWindow.SizeChanged -= this.hostWindow_SizeOrLocationChanged;
                this._hostWindow.StateChanged -= this.hostWindow_StateChanged;
                this._hostWindow.Activated -= this.hostWindow_Activated;
                this._hostWindow.Deactivated -= this.hostWindow_Deactivated;
            }
            this.Unloaded -= this.CustomValidationPopup_Unloaded;
            this.Opened -= this.CustomValidationPopup_Opened;
            this._hostWindow = null;
        }

        private void hostWindow_StateChanged(object sender, EventArgs e)
        {
            if (this._hostWindow != null && this._hostWindow.WindowState != WindowState.Minimized)
            {
                var target = this.PlacementTarget as FrameworkElement;
                var holder = target != null ? target.DataContext as AdornedElementPlaceholder : null;
                if (holder != null && holder.AdornedElement != null)
                {
                    var errorTemplate = holder.AdornedElement.GetValue(Validation.ErrorTemplateProperty);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, null);
                    holder.AdornedElement.SetValue(Validation.ErrorTemplateProperty, errorTemplate);
                }
            }
        }

        private void hostWindow_SizeOrLocationChanged(object sender, EventArgs e)
        {
            var offset = this.HorizontalOffset;
            // "bump" the offset to cause the popup to reposition itself on its own
            this.HorizontalOffset = offset + 1;
            this.HorizontalOffset = offset;
        }
    }
}
