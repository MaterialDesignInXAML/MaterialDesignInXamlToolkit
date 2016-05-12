using System;
using System.ComponentModel;
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
using ControlzEx;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// Defines how the <see cref="PopupBox"/> popup is aligned to the toggle part of the control.
    /// </summary>
    public enum PopupBoxPlacementMode
    {
        /// <summary>
        /// Display the popup below the toggle, and align the left edges.3
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
        TopAndAlignCentres,
        /// <summary>
        /// Display the popup to the left of the toggle, and align the top edges.
        /// </summary>
        LeftAndAlignTopEdges,
        /// <summary>
        /// Display the popup to the left of the toggle, and align the bottom edges.
        /// </summary>
        LeftAndAlignBottomEdges,
        /// <summary>
        /// Display the popup to the left of the toggle, and align the middles.
        /// </summary>
        LeftAndAlignMiddles,
        /// <summary>
        /// Display the popup to the right of the toggle, and align the top edges.
        /// </summary>
        RightAndAlignTopEdges,
        /// <summary>
        /// Display the popup to the right of the toggle, and align the bottom edges.
        /// </summary>
        RightAndAlignBottomEdges,
        /// <summary>
        /// Display the popup to the right of the toggle, and align the middles.
        /// </summary>
        RightAndAlignMiddles,
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
    [TemplatePart(Name = TogglePartName, Type = typeof(ToggleButton))]
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
        private ToggleButton _toggleButton;

        static PopupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(typeof(PopupBox)));
            ToolTipService.IsEnabledProperty.OverrideMetadata(typeof(PopupBox), new FrameworkPropertyMetadata(null, CoerceToolTipIsEnabled));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.LostMouseCaptureEvent, new MouseEventHandler(OnLostMouseCapture));
            EventManager.RegisterClassHandler(typeof(PopupBox), Mouse.MouseDownEvent, new MouseButtonEventHandler(OnMouseButtonDown), true);            
        }

        public static readonly DependencyProperty ToggleContentProperty = DependencyProperty.Register(
            nameof(ToggleContent), typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Content to display in the toggle button.
        /// </summary>
        public object ToggleContent
        {
            get { return (object) GetValue(ToggleContentProperty); }
            set { SetValue(ToggleContentProperty, value); }
        }

        public static readonly DependencyProperty ToggleContentTemplateProperty = DependencyProperty.Register(
            nameof(ToggleContentTemplate), typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Template for <see cref="ToggleContent"/>.
        /// </summary>
        public DataTemplate ToggleContentTemplate
        {
            get { return (DataTemplate) GetValue(ToggleContentTemplateProperty); }
            set { SetValue(ToggleContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ToggleCheckedContentProperty = DependencyProperty.Register(
            nameof(ToggleCheckedContent), typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Content to display in the toggle when it's checked (when the popup is open). Optional; if not provided the <see cref="ToggleContent"/> is used.
        /// </summary>
        public object ToggleCheckedContent
        {
            get { return (object) GetValue(ToggleCheckedContentProperty); }
            set { SetValue(ToggleCheckedContentProperty, value); }
        }

        public static readonly DependencyProperty ToggleCheckedContentTemplateProperty = DependencyProperty.Register(
            nameof(ToggleCheckedContentTemplate), typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Template for <see cref="ToggleCheckedContent"/>.
        /// </summary>
        public DataTemplate ToggleCheckedContentTemplate
        {
            get { return (DataTemplate) GetValue(ToggleCheckedContentTemplateProperty); }
            set { SetValue(ToggleCheckedContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ToggleCheckedContentCommandProperty = DependencyProperty.Register(
            nameof(ToggleCheckedContentCommand), typeof (ICommand), typeof (PopupBox), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Command to execute if toggle is checked (popup is open) and <see cref="ToggleCheckedContent"/> is set.
        /// </summary>
        public ICommand ToggleCheckedContentCommand
        {
            get { return (ICommand) GetValue(ToggleCheckedContentCommandProperty); }
            set { SetValue(ToggleCheckedContentCommandProperty, value); }
        }

        public static readonly DependencyProperty ToggleCheckedContentCommandParameterProperty = DependencyProperty.Register(
            nameof(ToggleCheckedContentCommandParameter), typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Command parameter to use in conjunction with <see cref="ToggleCheckedContentCommand"/>.
        /// </summary>
        public object ToggleCheckedContentCommandParameter
        {
            get { return (object) GetValue(ToggleCheckedContentCommandParameterProperty); }
            set { SetValue(ToggleCheckedContentCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
            nameof(PopupContent), typeof (object), typeof (PopupBox), new PropertyMetadata(default(object)));

        /// <summary>
        /// Content to display in the content.
        /// </summary>
        public object PopupContent
        {
            get { return (object) GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
            nameof(PopupContentTemplate), typeof (DataTemplate), typeof (PopupBox), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Popup content template.
        /// </summary>
        public DataTemplate PopupContentTemplate
        {
            get { return (DataTemplate) GetValue(PopupContentTemplateProperty); }
            set { SetValue(PopupContentTemplateProperty, value); }
        }

        [Obsolete]
        public static readonly DependencyProperty StaysOpenOnEditProperty = DependencyProperty.Register(
            nameof(StaysOpenOnEdit), typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Prefer <see cref="StaysOpen"/>.
        /// </summary>
        [Obsolete]
        public bool StaysOpenOnEdit
        {
            get { return (bool) GetValue(StaysOpenOnEditProperty); }
            set { SetValue(StaysOpenOnEditProperty, value); }
        }

        public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
            nameof(IsPopupOpen), typeof (bool), typeof (PopupBox), new FrameworkPropertyMetadata(default(bool), IsPopupOpenPropertyChangedCallback));

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
            
            popupBox.AnimateChildrenIn(!newValue);                 
            popupBox._popup?.RefreshPosition();

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
            nameof(StaysOpen), typeof (bool), typeof (PopupBox), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates of the popup should stay open if a click occurs inside the popup.
        /// </summary>
        public bool StaysOpen
        {
            get { return (bool) GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty PlacementModeProperty = DependencyProperty.Register(
            nameof(PlacementMode), typeof (PopupBoxPlacementMode), typeof (PopupBox), new PropertyMetadata(default(PopupBoxPlacementMode), PlacementModePropertyChangedCallback));

        private static void PlacementModePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PopupBox)dependencyObject)._popup?.RefreshPosition();
        }

        /// <summary>
        /// Gets or sets how the popup is aligned in relation to the toggle.
        /// </summary>
        public PopupBoxPlacementMode PlacementMode
        {
            get { return (PopupBoxPlacementMode) GetValue(PlacementModeProperty); }
            set { SetValue(PlacementModeProperty, value); }
        }

        public static readonly DependencyProperty PopupModeProperty = DependencyProperty.Register(
            nameof(PopupMode), typeof (PopupBoxPopupMode), typeof (PopupBox), new PropertyMetadata(default(PopupBoxPopupMode)));

        /// <summary>
        /// Gets or sets what trigger causes the popup to open.
        /// </summary>
        public PopupBoxPopupMode PopupMode
        {
            get { return (PopupBoxPopupMode) GetValue(PopupModeProperty); }
            set { SetValue(PopupModeProperty, value); }
        }

        /// <summary>
        /// Get or sets how to unfurl controls when opening the popups. Only child elements of type <see cref="ButtonBase"/> are animated.
        /// </summary>
        public static readonly DependencyProperty UnfurlOrientationProperty = DependencyProperty.Register(
            nameof(UnfurlOrientation), typeof (Orientation), typeof (PopupBox), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// Gets or sets how to unfurl controls when opening the popups. Only child elements of type <see cref="ButtonBase"/> are animated.
        /// </summary>
        public Orientation UnfurlOrientation
        {
            get { return (Orientation) GetValue(UnfurlOrientationProperty); }
            set { SetValue(UnfurlOrientationProperty, value); }
        }

        /// <summary>
        /// Framework use. Provides the method used to position the popup.
        /// </summary>
        public CustomPopupPlacementCallback PopupPlacementMethod => GetPopupPlacement;

        /// <summary>
        /// Event raised when the checked toggled content (if set) is clicked.
        /// </summary>
        public static readonly RoutedEvent ToggleCheckedContentClickEvent = EventManager.RegisterRoutedEvent("ToggleCheckedContentClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PopupBox));

        /// <summary>
        /// Event raised when the checked toggled content (if set) is clicked.
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler ToggleCheckedContentClick { add { AddHandler(ToggleCheckedContentClickEvent, value); } remove { RemoveHandler(ToggleCheckedContentClickEvent, value); } }


        /// <summary>
        /// Raises <see cref="ToggleCheckedContentClickEvent"/>.
        /// </summary>
        protected virtual void OnToggleCheckedContentClick()
        {
            var newEvent = new RoutedEventArgs(ToggleCheckedContentClickEvent, this);
            RaiseEvent(newEvent);
        }

        public override void OnApplyTemplate()
        {
            if (_popup != null)
                _popup.Loaded -= PopupOnLoaded;
            if (_toggleButton != null)
                _toggleButton.PreviewMouseLeftButtonUp -= ToggleButtonOnPreviewMouseLeftButtonUp;

            base.OnApplyTemplate();

            _popup = GetTemplateChild(PopupPartName) as PopupEx;
            _popupContentControl = GetTemplateChild(PopupContentControlPartName) as ContentControl;
            _toggleButton = GetTemplateChild(TogglePartName) as ToggleButton;

            if (_popup != null)
                _popup.Loaded += PopupOnLoaded;
            if (_toggleButton != null)
                _toggleButton.PreviewMouseLeftButtonUp += ToggleButtonOnPreviewMouseLeftButtonUp;                                

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
            if (IsEnabled &&
                (PopupMode == PopupBoxPopupMode.MouseOverEager
                 || PopupMode == PopupBoxPopupMode.MouseOver))

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
			
			
            if (FlowDirection == FlowDirection.RightToLeft)
                offset.X += targetSize.Width / 2;

			
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
                case PopupBoxPlacementMode.LeftAndAlignTopEdges:
                    x = 0 - popupSize.Width - Math.Abs(offset.X * 2);
                    y = 0 - Math.Abs(offset.Y * 3);
                    break;
                case PopupBoxPlacementMode.LeftAndAlignBottomEdges:
                    x = 0 - popupSize.Width - Math.Abs(offset.X * 2);
                    y = 0 - (popupSize.Height - targetSize.Height);
                    break;
                case PopupBoxPlacementMode.LeftAndAlignMiddles:
                    x = 0 - popupSize.Width - Math.Abs(offset.X * 2);
                    y = targetSize.Height / 2 - popupSize.Height / 2 - Math.Abs(offset.Y * 2);
                    break;
                case PopupBoxPlacementMode.RightAndAlignTopEdges:
                    x = targetSize.Width;
                    y = 0 - Math.Abs(offset.X * 3);
                    break;
                case PopupBoxPlacementMode.RightAndAlignBottomEdges:
                    x = targetSize.Width;
                    y = 0 - (popupSize.Height - targetSize.Height);
                    break;
                case PopupBoxPlacementMode.RightAndAlignMiddles:
                    x = targetSize.Width;
                    y = targetSize.Height / 2 - popupSize.Height / 2 - Math.Abs(offset.Y * 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var point = new Point(x, y);            
            return new[] {new CustomPopupPlacement(point, PopupPrimaryAxis.Horizontal)};
        }

        private void AnimateChildrenIn(bool reverse)
        {
            if (_popupContentControl == null) return;
            if (VisualTreeHelper.GetChildrenCount(_popupContentControl) != 1) return;
            var contentPresenter = VisualTreeHelper.GetChild(_popupContentControl, 0) as ContentPresenter;

            var controls = contentPresenter.VisualDepthFirstTraversal().OfType<ButtonBase>();
            double translateCoordinateFrom;
            if ((PlacementMode == PopupBoxPlacementMode.TopAndAlignCentres
                 || PlacementMode == PopupBoxPlacementMode.TopAndAlignLeftEdges
                 || PlacementMode == PopupBoxPlacementMode.TopAndAlignRightEdges
                 || PlacementMode == PopupBoxPlacementMode.LeftAndAlignBottomEdges
                 || PlacementMode == PopupBoxPlacementMode.RightAndAlignBottomEdges
                 || (UnfurlOrientation == Orientation.Horizontal &&
                     (
                         PlacementMode == PopupBoxPlacementMode.LeftAndAlignBottomEdges
                         || PlacementMode == PopupBoxPlacementMode.LeftAndAlignMiddles
                         || PlacementMode == PopupBoxPlacementMode.LeftAndAlignTopEdges
                         ))
                ))
            {
                controls = controls.Reverse();
                translateCoordinateFrom = 80;
            }
            else
                translateCoordinateFrom = -80;

            var translateCoordinatePath =
                "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform."
                + (UnfurlOrientation == Orientation.Horizontal ? "X)" : "Y)");

            var sineEase = new SineEase();

            var i = 0;
            foreach (var uiElement in controls)
            {                
                var deferredStart = i++*20;
                var deferredEnd = deferredStart+200.0;                

                var absoluteZeroKeyTime = KeyTime.FromPercent(0.0);
                var deferredStartKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredStart));
                var deferredEndKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredEnd));

                var elementTranslateCoordinateFrom = translateCoordinateFrom * i;
                var translateTransform = new TranslateTransform(
                    UnfurlOrientation == Orientation.Vertical ? 0 : elementTranslateCoordinateFrom,
                    UnfurlOrientation == Orientation.Vertical ? elementTranslateCoordinateFrom : 0);

                var transformGroup = new TransformGroup
                {                    
                    Children = new TransformCollection(new Transform[]
                    {
                        new ScaleTransform(0, 0),
                        translateTransform
                    })
                };
                uiElement.SetCurrentValue(RenderTransformOriginProperty, new Point(.5, .5));
                uiElement.RenderTransform = transformGroup;

                var opacityAnimation = new DoubleAnimationUsingKeyFrames();                
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));                
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                Storyboard.SetTarget(opacityAnimation, uiElement);

                var scaleXAnimation = new DoubleAnimationUsingKeyFrames();
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                Storyboard.SetTarget(scaleXAnimation, uiElement);

                var scaleYAnimation = new DoubleAnimationUsingKeyFrames();
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                Storyboard.SetTarget(scaleYAnimation, uiElement);

                var translateCoordinateAnimation = new DoubleAnimationUsingKeyFrames();
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateCoordinateFrom, absoluteZeroKeyTime, sineEase));
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateCoordinateFrom, deferredStartKeyTime, sineEase));
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredEndKeyTime, sineEase));

                Storyboard.SetTargetProperty(translateCoordinateAnimation, new PropertyPath(translateCoordinatePath));
                Storyboard.SetTarget(translateCoordinateAnimation, uiElement);

                var storyboard = new Storyboard();
                storyboard.Children.Add(opacityAnimation);
                storyboard.Children.Add(scaleXAnimation);
                storyboard.Children.Add(scaleYAnimation);
                storyboard.Children.Add(translateCoordinateAnimation);

                if (reverse)
                {
                    storyboard.AutoReverse = true;
                    storyboard.Begin();
                    storyboard.Seek(TimeSpan.FromMilliseconds(deferredEnd));
                    storyboard.Resume();

                }
                else
                    storyboard.Begin();                
            }            
        }

        #region Capture

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetCapture();

        private static void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            var popupBox = (PopupBox) sender;

            if (Equals(Mouse.Captured, popupBox)) return;

            if (Equals(e.OriginalSource, popupBox))
            {
                if (Mouse.Captured == null || popupBox._popup == null || !(Mouse.Captured as DependencyObject).IsDescendantOf(popupBox._popup))
                {
                    popupBox.Close();
                }
            }
            else
            {                
                if ((Mouse.Captured as DependencyObject).GetVisualAncestry().Contains(popupBox._popup.Child))
                {
                    // Take capture if one of our children gave up capture (by closing their drop down)
                    if (!popupBox.IsPopupOpen || Mouse.Captured != null || GetCapture() != IntPtr.Zero) return;

                    Mouse.Capture(popupBox, CaptureMode.SubTree);
                    e.Handled = true;
                }
                else
                {
                    if (popupBox.StaysOpen)
                    {
                        // Take capture back because click happend outside of control
                        Mouse.Capture(popupBox, CaptureMode.SubTree);
                        e.Handled = true;
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

        private void ToggleButtonOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (PopupMode == PopupBoxPopupMode.Click || !IsPopupOpen) return;
            
            if (ToggleCheckedContent != null)
            {
                OnToggleCheckedContentClick();

                if (ToggleCheckedContentCommand != null
                    && ToggleCheckedContentCommand.CanExecute(ToggleCheckedContentCommandParameter)
                    )
                {
                    ToggleCheckedContentCommand.Execute(ToggleCheckedContentCommandParameter);
                }
            }
            
            Close();
            Mouse.Capture(null);
            mouseButtonEventArgs.Handled = true;            
        }

        private static object CoerceToolTipIsEnabled(DependencyObject dependencyObject, object value)
        {
            var popupBox = (PopupBox) dependencyObject;
            return popupBox.IsPopupOpen ? false : value;
        }
    }
}
