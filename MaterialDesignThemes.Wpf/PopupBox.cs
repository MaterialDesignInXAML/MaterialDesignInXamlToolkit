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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Controlz;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Defines how the <see cref="PopupBox"/> popup is aligned to the toggle part of the control.
    /// </summary>
    public enum PopupBoxPlacementMode
    {
        /// <summary>
        /// Display the popup below the toggle, and align the left edges.
        /// </summary>
        BottomAndAlignLeftEdges,
        /// <summary>
        /// Display the popup below the toggle, and align the right edges.
        /// </summary>
        BottomAndAlignRightEdges,
        /// <summary>
        /// Display the popup below the toggle, and align the center of the popup with the center of the toggle.
        /// </summary>
        BottomAndAlignCentres,
        /// <summary>
        /// Display the popup above the toggle, and align the left edges.
        /// </summary>
        TopAndAlignLeftEdges,
        /// <summary>
        /// Display the popup above the toggle, and align the right edges.
        /// </summary>
        TopAndAlignRightEdges,
        /// <summary>
        /// Display the popup above the toggle, and align the center of the popup with the center of the toggle.
        /// </summary>
        TopAndAlignCentres
    }

    /// <summary>
    /// Defines what causes the <see cref="PopupBox"/> to open it's popup.
    /// </summary>
    public enum PopupBoxPopupMode
    {
        /// <summary>
        /// Open when the toggle button is clicked.
        /// </summary>
        Click,
        /// <summary>
        /// Open when the mouse goes over the toggle button.
        /// </summary>
        MouseOver,
        /// <summary>
        /// Open when the mouse goes over the toggle button, or the space in which the popup box would occupy should it be open.
        /// </summary>
        MouseOverEager
    }

    /// <summary>
    /// Popup box, similar to a <see cref="ComboBox"/>, but allows more customizable content.    
    /// </summary>
    [TemplatePart(Name = PopupPartName, Type = typeof(Popup))]
    [TemplatePart(Name = PopupContentControlPartName, Type = typeof(ContentControl))]
    [TemplateVisualState(GroupName = "PopupStates", Name = PopupIsOpenStateName)]
    [TemplateVisualState(GroupName = "PopupStates", Name = PopupIsClosedStateName)]
    [ContentProperty("PopupContent")]
    public class PopupBox : ContentControl
    {
        public const string PopupPartName = "PART_Popup";
        public const string TogglePartName = "PART_Toggle";
        public const string PopupContentControlPartName = "PART_PopupContentControl";
        public const string PopupIsOpenStateName = "IsOpen";
        public const string PopupIsClosedStateName = "IsClosed";
        private PopupEx _popup;
        private ContentControl _popupContentControl;

        static PopupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(typeof(PopupBox)));
            ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(null, CoerceToolTipIsEnabled));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseButtonDown), true);            
        }

        public static readonly DependencyProperty ToggleContentProperty = DependencyProperty.Register(
            "ToggleContent", typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Content to display in the toggle button.
        /// </summary>
        public object ToggleContent
        {
            get { return (object) GetValue(ToggleContentProperty); }
            set { SetValue(ToggleContentProperty, value); }
        }

        public static readonly DependencyProperty ToggleContentTemplateProperty = DependencyProperty.Register(
            "ToggleContentTemplate", typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Template for <see cref="ToggleContent"/>.
        /// </summary>
        public DataTemplate ToggleContentTemplate
        {
            get { return (DataTemplate) GetValue(ToggleContentTemplateProperty); }
            set { SetValue(ToggleContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            "PopupContent", typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Content to display in the content.
        /// </summary>
        public object PopupContent
        {
            get { return (object) GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
            "PopupContentTemplate", typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Popup content template.
        /// </summary>
        public DataTemplate PopupContentTemplate
        {
            get { return (DataTemplate) GetValue(PopupContentTemplateProperty); }
            set { SetValue(PopupContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenOnEditProperty = DependencyProperty.Register(
            "StaysOpenOnEdit", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates if the opup should stay open after a click is made inside the popup.
        /// </summary>
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
            if (popupBox.PopupMode == PopupBoxPopupMode.Click)
            {
                if (newValue)
                    Mouse.Capture(popupBox, CaptureMode.SubTree);
                else
                    Mouse.Capture(null);
            }

            if (newValue)
            {                
                popupBox.AnimateChildren();
            }

            VisualStateManager.GoToState(popupBox, newValue ? PopupIsOpenStateName : PopupIsClosedStateName, true);
        }        

        /// <summary>
        /// Gets or sets whether the popup is currently open.
        /// </summary>
        public bool IsPopupOpen
        {
            get { return (bool) GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenProperty = DependencyProperty.Register(
            "StaysOpen", typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates of the popup should stay open if a click occurs inside the popup.
        /// </summary>
        public bool StaysOpen
        {
            get { return (bool) GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(
            "PlacementMode", typeof (PopupBoxPlacementMode), typeof (PopupBox), new PropertyMetadata(default(PopupBoxPlacementMode)));

        /// <summary>
        /// Gets or sets how the popup is aligned in relation to the toggle.
        /// </summary>
        public PopupBoxPlacementMode PlacementMode
        {
            get { return (PopupBoxPlacementMode) GetValue(PropertyTypeProperty); }
            set { SetValue(PropertyTypeProperty, value); }
        }

        public static readonly DependencyProperty PopupModeProperty = DependencyProperty.Register(
            "PopupMode", typeof (PopupBoxPopupMode), typeof (PopupBox), new PropertyMetadata(default(PopupBoxPopupMode)));        

        /// <summary>
        /// Gets or sets what trigger causes the popup to open.
        /// </summary>
        public PopupBoxPopupMode PopupMode
        {
            get { return (PopupBoxPopupMode) GetValue(PopupModeProperty); }
            set { SetValue(PopupModeProperty, value); }
        }

        /// <summary>
        /// Framework use. Provides the method used to position the popup.
        /// </summary>
        public CustomPopupPlacementCallback PopupPlacementMethod => GetPopupPlacement;

        public override void OnApplyTemplate()
        {
            if (_popup != null)
                _popup.Loaded -= PopupOnLoaded;

            base.OnApplyTemplate();

            _popup = GetTemplateChild(PopupPartName) as PopupEx;
            _popupContentControl = GetTemplateChild(PopupContentControlPartName) as ContentControl;

            if (_popup != null)
                _popup.Loaded += PopupOnLoaded;

            VisualStateManager.GoToState(this, IsPopupOpen ? PopupIsOpenStateName : PopupIsClosedStateName, false);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);

            if (IsPopupOpen && !IsKeyboardFocusWithin)
            {                
                Close();
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (PopupMode == PopupBoxPopupMode.MouseOverEager
                || PopupMode == PopupBoxPopupMode.MouseOver)

                SetCurrentValue(IsPopupOpenProperty, true);

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (PopupMode == PopupBoxPopupMode.MouseOverEager
                || PopupMode == PopupBoxPopupMode.MouseOver)

                Close();

            base.OnMouseEnter(e);
        }

        protected void Close()
        {
            if (IsPopupOpen)
                SetCurrentValue(IsPopupOpenProperty, false);
        }

        private CustomPopupPlacement[] GetPopupPlacement(Size popupSize, Size targetSize, Point offset)
        {
            double x, y;
            switch (PlacementMode)
            {
                case PopupBoxPlacementMode.BottomAndAlignLeftEdges:
                    x = 0 - Math.Abs(offset.X*3);
                    y = targetSize.Height - Math.Abs(offset.Y);                    
                    break;
                case PopupBoxPlacementMode.BottomAndAlignRightEdges:
                    x = 0 - popupSize.Width + targetSize.Width - offset.X;
                    y = targetSize.Height - Math.Abs(offset.Y);
                    break;
                case PopupBoxPlacementMode.BottomAndAlignCentres:
                    x = targetSize.Width/2 - popupSize.Width/2 - Math.Abs(offset.X*2);
                    y = targetSize.Height - Math.Abs(offset.Y);
                    break;
                case PopupBoxPlacementMode.TopAndAlignLeftEdges:
                    x = 0 - Math.Abs(offset.X * 3);
                    y = 0 - popupSize.Height - Math.Abs(offset.Y*2);
                    break;
                case PopupBoxPlacementMode.TopAndAlignRightEdges:
                    x = 0 - popupSize.Width + targetSize.Width - offset.X;
                    y = 0 - popupSize.Height - Math.Abs(offset.Y * 2);
                    break;
                case PopupBoxPlacementMode.TopAndAlignCentres:
                    x = targetSize.Width/2 - popupSize.Width/2 - Math.Abs(offset.X*2);
                    y = 0 - popupSize.Height - Math.Abs(offset.Y * 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var point = new Point(x, y);            
            return new[] {new CustomPopupPlacement(point, PopupPrimaryAxis.Horizontal)};
        }

        private void AnimateChildren()
        {
            if (_popupContentControl == null) return;
            if (VisualTreeHelper.GetChildrenCount(_popupContentControl) != 1) return;
            var contentPresenter = VisualTreeHelper.GetChild(_popupContentControl, 0) as ContentPresenter;

            var controls = contentPresenter.VisualDepthFirstTraversal().OfType<ButtonBase>();
            double translateYFrom;
            if (PlacementMode == PopupBoxPlacementMode.TopAndAlignCentres
                || PlacementMode == PopupBoxPlacementMode.TopAndAlignLeftEdges
                || PlacementMode == PopupBoxPlacementMode.TopAndAlignRightEdges)
            {
                controls = controls.Reverse();
                translateYFrom = 40;
            }
            else
                translateYFrom = -40;

            var i = 0;
            foreach (var uiElement in controls)
            {
                var transformGroup = new TransformGroup
                {
                    Children = new TransformCollection(new Transform[]
                    {
                        new ScaleTransform(.5, .5),
                        new TranslateTransform(0, translateYFrom)
                    })
                };
                uiElement.SetCurrentValue(RenderTransformOriginProperty, new Point(.5, .5));
                uiElement.RenderTransform = transformGroup;

                var scaleXAnimation = new DoubleAnimation(.5, 1, new Duration(TimeSpan.FromMilliseconds(100)));
                Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                Storyboard.SetTarget(scaleXAnimation, uiElement);
                var scaleYAnimation = new DoubleAnimation(.5, 1, new Duration(TimeSpan.FromMilliseconds(100)));
                Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                Storyboard.SetTarget(scaleYAnimation, uiElement);
                var translateYAnimation = new DoubleAnimation(translateYFrom, 0, new Duration(TimeSpan.FromMilliseconds(100)));
                Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)"));
                Storyboard.SetTarget(translateYAnimation, uiElement);
                var storyboard = new Storyboard();
                storyboard.Children.Add(scaleXAnimation);
                storyboard.Children.Add(scaleYAnimation);
                storyboard.Children.Add(translateYAnimation);
                storyboard.BeginTime = TimeSpan.FromMilliseconds(i++ * 20);

                storyboard.Begin();
            }            
        }

        #region Capture

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetCapture();

        private static void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            var popupBox = (PopupBox) sender;

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
            var popupBox = (PopupBox) sender;

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
            if (IsPopupOpen && !StaysOpen)
            {
                Close();
                e.Handled = true;
            }
            else
                base.OnMouseLeftButtonUp(e);
        }

        #endregion

        private void PopupOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (PopupMode == PopupBoxPopupMode.MouseOverEager)
                _popup.IsOpen = true;
        }

        private static object CoerceToolTipIsEnabled(DependencyObject dependencyObject, object value)
        {
            var popupBox = (PopupBox) dependencyObject;
            return popupBox.IsPopupOpen ? false : value;
        }
    }
}
