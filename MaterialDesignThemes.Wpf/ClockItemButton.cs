using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{

    [TemplatePart(Name = ThumbPartName, Type = typeof(Thumb))]
    public class ClockItemButton : ToggleButton
    {
        public const string ThumbPartName = "PART_Thumb";

        public static readonly DependencyProperty CentreXProperty = DependencyProperty.Register(
            nameof(CentreX), typeof(double), typeof(ClockItemButton), new PropertyMetadata(default(double)));

        public double CentreX
        {
            get { return (double)GetValue(CentreXProperty); }
            set { SetValue(CentreXProperty, value); }
        }

        public static readonly DependencyProperty CentreYProperty = DependencyProperty.Register(
            nameof(CentreY), typeof(double), typeof(ClockItemButton), new PropertyMetadata(default(double)));

        public double CentreY
        {
            get { return (double)GetValue(CentreYProperty); }
            set { SetValue(CentreYProperty, value); }
        }

        private static readonly DependencyPropertyKey XPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "X", typeof(double), typeof(ClockItemButton),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty XProperty =
            XPropertyKey.DependencyProperty;

        public double X
        {
            get { return (double)GetValue(XProperty); }
            private set { SetValue(XPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey YPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "Y", typeof(double), typeof(ClockItemButton),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty YProperty =
            YPropertyKey.DependencyProperty;

        private Thumb _thumb;

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            private set { SetValue(YPropertyKey, value); }
        }

        public override void OnApplyTemplate()
        {
            if (_thumb != null)
            {
                _thumb.DragStarted -= ThumbOnDragStarted;
                _thumb.DragDelta -= ThumbOnDragDelta;
                _thumb.DragCompleted -= ThumbOnDragCompleted;
            }

            _thumb = GetTemplateChild(ThumbPartName) as Thumb;

            if (_thumb != null)
            {
                _thumb.DragStarted += ThumbOnDragStarted;
                _thumb.DragDelta += ThumbOnDragDelta;
                _thumb.DragCompleted += ThumbOnDragCompleted;
            }

            base.OnApplyTemplate();
        }

        public static readonly RoutedEvent DragDeltaEvent =
            EventManager.RegisterRoutedEvent(
                "DragDelta",
                RoutingStrategy.Bubble,
                typeof(DragDeltaEventHandler),
                typeof(ClockItemButton));

        private static void OnDragDelta(
            DependencyObject d, double horizontalChange, double verticalChange)
        {
            var instance = (ClockItemButton)d;
            var dragDeltaEventArgs = new DragDeltaEventArgs(horizontalChange, verticalChange) {
                RoutedEvent = DragDeltaEvent,
                Source = d
            };

            instance.RaiseEvent(dragDeltaEventArgs);
        }

        public static readonly RoutedEvent DragStartedEvent =
            EventManager.RegisterRoutedEvent(
                "DragStarted",
                RoutingStrategy.Bubble,
                typeof(DragStartedEventHandler),
                typeof(ClockItemButton));

        public static readonly RoutedEvent DragCompletedEvent =
            EventManager.RegisterRoutedEvent(
                "DragCompleted",
                RoutingStrategy.Bubble,
                typeof(DragCompletedEventHandler),
                typeof(ClockItemButton));

        private static void OnDragStarted(DependencyObject d, double horizontalOffset, double verticalOffset)
        {
            var instance = (ClockItemButton)d;
            var dragStartedEventArgs = new DragStartedEventArgs(horizontalOffset, verticalOffset) {
                RoutedEvent = DragStartedEvent,
                Source = d
            };

            instance.RaiseEvent(dragStartedEventArgs);
        }

        private static void OnDragCompleted(DependencyObject d, double horizontalChange, double verticalChange, bool canceled)
        {
            var instance = (ClockItemButton)d;
            var dragCompletedEventArgs = new DragCompletedEventArgs(horizontalChange, verticalChange, canceled) {
                RoutedEvent = DragCompletedEvent,
                Source = d
            };

            instance.RaiseEvent(dragCompletedEventArgs);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (_thumb == null) return;

            base.OnPreviewMouseLeftButtonDown(e);
            if (!IsChecked.HasValue || !IsChecked.Value)
            {
                OnToggle();
            }
        }

        /// <summary> 
        /// This override method is called when the control is clicked by mouse or keyboard
        /// </summary> 
        protected override void OnClick()
        {
            if (_thumb == null)
                base.OnClick();
        }

        private void ThumbOnDragStarted(object sender, DragStartedEventArgs dragStartedEventArgs)
        {
            //Get the absolute position of where the drag operation started
            OnDragStarted(this, CentreX + dragStartedEventArgs.HorizontalOffset - Width / 2.0, CentreY + dragStartedEventArgs.VerticalOffset - Height / 2.0);
        }

        private void ThumbOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            OnDragDelta(this, dragDeltaEventArgs.HorizontalChange, dragDeltaEventArgs.VerticalChange);
        }

        private void ThumbOnDragCompleted(object sender, DragCompletedEventArgs dragCompletedEventArgs)
        {
            OnDragCompleted(this, dragCompletedEventArgs.HorizontalChange, dragCompletedEventArgs.VerticalChange, dragCompletedEventArgs.Canceled);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                X = CentreX - finalSize.Width / 2;
                Y = CentreY - finalSize.Height / 2;
            }));

            return base.ArrangeOverride(finalSize);
        }
    }
}