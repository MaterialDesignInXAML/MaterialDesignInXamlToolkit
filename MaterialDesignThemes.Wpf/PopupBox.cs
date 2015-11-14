using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Controlz;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    public class PopupBox : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        private PopupEx _popup;
        
        static PopupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
            ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(null, new CoerceValueCallback(CoerceToolTipIsEnabled)));
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            "PopupContent", typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        public object PopupContent
        {
            get { return (object) GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
            "PopupContentTemplate", typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate PopupContentTemplate
        {
            get { return (DataTemplate) GetValue(PopupContentTemplateProperty); }
            set { SetValue(PopupContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenOnEditProperty = DependencyProperty.Register(
            "StaysOpenOnEdit", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        public bool StaysOpenOnEdit
        {
            get { return (bool) GetValue(StaysOpenOnEditProperty); }
            set { SetValue(StaysOpenOnEditProperty, value); }
        }

        public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
            "IsPopupOpen", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        public bool IsPopupOpen
        {
            get { return (bool) GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = GetTemplateChild(PopupPartName) as PopupEx;
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);

            if (IsPopupOpen && !IsKeyboardFocusWithin)
            {
                //TODO check if keyboard in popup
                Close();
            }
        }

        private void Close()
        {
            if (IsPopupOpen)
                SetCurrentValue(IsPopupOpenProperty, false);
        }

        #region Capture
        /*

       private static void OnLostMouseCapture(object sender, MouseEventArgs e) 
        {
            ComboBox comboBox = (ComboBox)sender; 

            // ISSUE (jevansa) -- task 22022:
            //        We need a general mechanism to do this, or at the very least we should
            //        share it amongst the controls which need it (Popup, MenuBase, ComboBox). 
            if (Mouse.Captured != comboBox)
            { 
                if (e.OriginalSource == comboBox) 
                {
                    // If capture is null or it's not below the combobox, close. 
                    // More workaround for task 22022 -- check if it's a descendant (following Logical links too)
                    if (Mouse.Captured == null || !MenuBase.IsDescendant(comboBox, Mouse.Captured as DependencyObject))
                    {
                        comboBox.Close(); 
                    }
                } 
                else 
                {
                    if (MenuBase.IsDescendant(comboBox, e.OriginalSource as DependencyObject)) 
                    {
                        // Take capture if one of our children gave up capture (by closing their drop down)
                        if (comboBox.IsDropDownOpen && Mouse.Captured == null && MS.Win32.SafeNativeMethods.GetCapture() == IntPtr.Zero)
                        { 
                            Mouse.Capture(comboBox, CaptureMode.SubTree);
                            e.Handled = true; 
                        } 
                    }
                    else 
                    {
                        comboBox.Close();
                    }
                } 
            }
        } 
 
        private static void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        { 
            ComboBox comboBox = (ComboBox)sender;

            // If we (or one of our children) are clicked, claim the focus (don't steal focus if our context menu is clicked)
            if (!comboBox.IsContextMenuOpen && !comboBox.IsKeyboardFocusWithin) 
            {
                comboBox.Focus(); 
            } 

            e.Handled = true;   // Always handle so that parents won't take focus away 

            // Note: This half should be moved into OnMouseDownOutsideCapturedElement
            // When we have capture, all clicks off the popup will have the combobox as
            // the OriginalSource.  So when the original source is the combobox, that 
            // means the click was off the popup and we should dismiss.
            if (Mouse.Captured == comboBox && e.OriginalSource == comboBox) 
            { 
                comboBox.Close();
                Debug.Assert(!comboBox.CheckAccess() || Mouse.Captured != comboBox, "On the dispatcher thread, ComboBox should not have capture after closing the dropdown"); 
            }
        }

        private static void OnPreviewMouseButtonDown(object sender, MouseButtonEventArgs e) 
        {
            ComboBox comboBox = (ComboBox)sender; 
 
            if (comboBox.IsEditable)
            { 
                Visual originalSource = e.OriginalSource as Visual;
                Visual textBox = comboBox.EditableTextBoxSite;

                if (originalSource != null && textBox != null 
                    && textBox.IsAncestorOf(originalSource))
                { 
                    if (comboBox.IsDropDownOpen && !comboBox.StaysOpenOnEdit) 
                    {
                        // When combobox is not editable, clicks anywhere outside 
                        // the combobox will close it.  When the combobox is editable
                        // then clicking the text box should close the combobox as well.
                        comboBox.Close();
                    } 
                    else if (!comboBox.IsContextMenuOpen && !comboBox.IsKeyboardFocusWithin)
                    { 
                        // If textBox is clicked, claim focus 
                        comboBox.Focus();
                        e.Handled = true;   // Handle so that textbox won't try to update cursor position 
                    }
                }
            }
        } 

 
        /// <summary> 
        ///     An event reporting the left mouse button was released.
        /// </summary> 
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            // Ignore the first mouse button up if we haven't gone over the popup yet 
            // And ignore all mouse ups over the items host.
            if (HasMouseEnteredItemsHost && !IsMouseOverItemsHost) 
            { 
                if (IsDropDownOpen)
                { 
                    Close();
                    e.Handled = true;
                    Debug.Assert(!CheckAccess() || Mouse.Captured != this, "On the dispatcher thread, ComboBox should not have capture after closing the dropdown");
                } 
            }
 
            base.OnMouseLeftButtonUp(e); 
        }
 
        private static void OnMouseMove(object sender, MouseEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
 
            // The mouse moved, see if we're over the items host yet
            if (comboBox.IsDropDownOpen) 
            { 
                bool isMouseOverItemsHost = comboBox.ItemsHost != null ? comboBox.ItemsHost.IsMouseOver : false;
 
                // When mouse enters items host, start tracking mouse movements
                if (isMouseOverItemsHost && !comboBox.HasMouseEnteredItemsHost)
                {
                    comboBox.SetInitialMousePosition(); 
                }
 
                comboBox.IsMouseOverItemsHost = isMouseOverItemsHost; 
                comboBox.HasMouseEnteredItemsHost |= isMouseOverItemsHost;
            } 

            // If we get a mouse move and we have capture, then the mouse was
            // outside the ComboBox.  We should autoscroll.
            if (Mouse.LeftButton == MouseButtonState.Pressed && comboBox.HasMouseEnteredItemsHost) 
            {
                if (Mouse.Captured == comboBox) 
                { 
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                    { 
                        comboBox.DoAutoScroll(comboBox.HighlightedItem);
                    }
                    else
                    { 
                        // We missed the mouse up, release capture
                        comboBox.ReleaseMouseCapture(); 
                        comboBox.ResetLastMousePosition(); 
                    }
 

                    e.Handled = true;
                }
            } 
        }


    */
        #endregion

        private static object CoerceToolTipIsEnabled(DependencyObject dependencyObject, object value)
        {
            var popupBox = (PopupBox)dependencyObject;
            return popupBox.IsPopupOpen ? false : value;
        }
    }
}
