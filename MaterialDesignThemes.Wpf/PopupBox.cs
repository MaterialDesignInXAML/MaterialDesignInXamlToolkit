using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Controlz;

namespace MaterialDesignThemes.Wpf
{
    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = TogglePartName, Type = typeof(ToggleButton))]
    public class PopupBox : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string TogglePartName = "PART_Toggle";
        private PopupEx _popup;
        private ToggleButton _toggleButton;
        
        static PopupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(typeof(PopupBox)));
            ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(null, CoerceToolTipIsEnabled));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseButtonDown), true);
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
            "IsPopupOpen", typeof (bool), typeof (PopupBox), new FrameworkPropertyMetadata(default(bool), IsPopupOpenPropertyChangedCallback));

        private static void IsPopupOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var popupBox = (PopupBox) dependencyObject;
            var newValue = (bool)dependencyPropertyChangedEventArgs.NewValue;
            if (newValue)
                Mouse.Capture(popupBox, CaptureMode.SubTree);
        }

        public bool IsPopupOpen
        {
            get { return (bool) GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _popup = GetTemplateChild(PopupPartName) as PopupEx;
            _toggleButton = GetTemplateChild(TogglePartName) as ToggleButton;
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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetCapture();

        private static void OnLostMouseCapture(object sender, MouseEventArgs e) 
        {
            var popupBox = (PopupBox)sender; 

            if (Mouse.Captured != popupBox)
            { 
                if (e.OriginalSource == popupBox) 
                {
                    if (Mouse.Captured == null || popupBox._popup == null || !(Mouse.Captured as DependencyObject).GetVisualAncestory().Contains(popupBox._popup))
                    {
                        popupBox.Close(); 
                    }
                } 
                else 
                {
                    if ((Mouse.Captured as DependencyObject).GetVisualAncestory().Contains(popupBox._popup)) 
                    {
                        // Take capture if one of our children gave up capture (by closing their drop down)
                        if (popupBox.IsPopupOpen && Mouse.Captured == null && GetCapture() == IntPtr.Zero)
                        { 
                            Mouse.Capture(popupBox, CaptureMode.SubTree);
                            e.Handled = true; 
                        } 
                    }
                    else 
                    {
                        popupBox.Close();
                    }
                } 
            }
        }

        private static void OnMouseButtonDown(object sender, MouseButtonEventArgs e)
        { 
            var popupBox = (PopupBox)sender;

            if (!popupBox.IsKeyboardFocusWithin) 
            {
                popupBox.Focus(); 
            } 

            e.Handled = true;   

            if (Mouse.Captured == popupBox && e.OriginalSource == popupBox) 
            { 
                popupBox.Close();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (IsPopupOpen)
            { 
                Close();
                e.Handled = true;
            } 
            else
                base.OnMouseLeftButtonUp(e); 
        }
 
        #endregion

        private static object CoerceToolTipIsEnabled(DependencyObject dependencyObject, object value)
        {
            var popupBox = (PopupBox)dependencyObject;
            return popupBox.IsPopupOpen ? false : value;
        }
    }
}
