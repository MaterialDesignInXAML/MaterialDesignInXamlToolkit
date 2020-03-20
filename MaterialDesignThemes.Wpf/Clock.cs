using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace MaterialDesignThemes.Wpf
{
    public enum ClockDisplayMode
    {
        Hours,
        Minutes,
        Seconds,
    }

    public enum ClockDisplayAutomation
    {
        None,
        Cycle,
        ToMinutesOnly,
        ToSeconds,
        CycleWithSeconds,
    }

    [TemplatePart(Name = HoursCanvasPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = MinutesCanvasPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = SecondsCanvasPartName, Type = typeof(Canvas))]
    [TemplatePart(Name = MinuteReadOutPartName, Type = typeof(TextBlock))]
    [TemplatePart(Name = HourReadOutPartName, Type = typeof(TextBlock))]
    [TemplateVisualState(GroupName = "DisplayModeStates", Name = HoursVisualStateName)]
    [TemplateVisualState(GroupName = "DisplayModeStates", Name = MinutesVisualStateName)]
    public class Clock : Control
    {
        public const string HoursCanvasPartName = "PART_HoursCanvas";
        public const string MinutesCanvasPartName = "PART_MinutesCanvas";
        public const string SecondsCanvasPartName = "PART_SecondsCanvas";
        public const string MinuteReadOutPartName = "PART_MinuteReadOut";
        public const string SecondReadOutPartName = "PART_SecondReadOut";
        public const string HourReadOutPartName = "PART_HourReadOut";

        public const string HoursVisualStateName = "Hours";
        public const string MinutesVisualStateName = "Minutes";
        public const string SecondsVisualStateName = "Seconds";

        private Point _centreCanvas = new Point(0, 0);
        private Point _currentStartPosition = new Point(0, 0);
        private TextBlock _hourReadOutPartName;
        private TextBlock _minuteReadOutPartName;
        private TextBlock _secondReadOutPartName;

        static Clock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Clock), new FrameworkPropertyMetadata(typeof(Clock)));
        }

        public Clock()
        {
            AddHandler(ClockItemButton.DragStartedEvent, new DragStartedEventHandler(ClockItemDragStartedHandler));
            AddHandler(ClockItemButton.DragDeltaEvent, new DragDeltaEventHandler(ClockItemDragDeltaHandler));
            AddHandler(ClockItemButton.DragCompletedEvent, new DragCompletedEventHandler(ClockItemDragCompletedHandler));
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            nameof(Time), typeof(DateTime), typeof(Clock), new FrameworkPropertyMetadata(default(DateTime), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TimePropertyChangedCallback));

        private static void TimePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var clock = (Clock)dependencyObject;
            SetFlags(clock);
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        private static readonly DependencyPropertyKey IsMidnightHourPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMidnightHour", typeof(bool), typeof(Clock),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsMidnightHourProperty =
            IsMidnightHourPropertyKey.DependencyProperty;

        public bool IsMidnightHour
        {
            get { return (bool)GetValue(IsMidnightHourProperty); }
            private set { SetValue(IsMidnightHourPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey IsMiddayHourPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMiddayHour", typeof(bool), typeof(Clock),
                new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsMiddayHourProperty =
            IsMiddayHourPropertyKey.DependencyProperty;

        public bool IsMiddayHour
        {
            get { return (bool)GetValue(IsMiddayHourProperty); }
            private set { SetValue(IsMiddayHourPropertyKey, value); }
        }

        public static readonly DependencyProperty IsPostMeridiemProperty = DependencyProperty.Register(
            nameof(IsPostMeridiem), typeof(bool), typeof(Clock), new PropertyMetadata(default(bool), IsPostMeridiemPropertyChangedCallback));

        private static void IsPostMeridiemPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var clock = (Clock)dependencyObject;
            if (clock.IsPostMeridiem && clock.Time.Hour < 12)
                clock.Time = new DateTime(clock.Time.Year, clock.Time.Month, clock.Time.Day, clock.Time.Hour + 12, clock.Time.Minute, clock.Time.Second);
            else if (!clock.IsPostMeridiem && clock.Time.Hour >= 12)
                clock.Time = new DateTime(clock.Time.Year, clock.Time.Month, clock.Time.Day, clock.Time.Hour - 12, clock.Time.Minute, clock.Time.Second);
        }

        public bool IsPostMeridiem
        {
            get { return (bool)GetValue(IsPostMeridiemProperty); }
            set { SetValue(IsPostMeridiemProperty, value); }
        }

        public static readonly DependencyProperty Is24HoursProperty = DependencyProperty.Register(
            nameof(Is24Hours), typeof(bool), typeof(Clock), new PropertyMetadata(default(bool), Is24HoursChanged));

        private static void Is24HoursChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Clock)d).GenerateButtons();
        }

        public bool Is24Hours
        {
            get { return (bool)GetValue(Is24HoursProperty); }
            set { SetValue(Is24HoursProperty, value); }
        }


        public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(
            nameof(DisplayMode), typeof(ClockDisplayMode), typeof(Clock), new FrameworkPropertyMetadata(ClockDisplayMode.Hours, DisplayModePropertyChangedCallback));

        private static void DisplayModePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((Clock)dependencyObject).GotoVisualState(!TransitionAssist.GetDisableTransitions(dependencyObject));
        }

        public ClockDisplayMode DisplayMode
        {
            get { return (ClockDisplayMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }

        public static readonly DependencyProperty DisplayAutomationProperty = DependencyProperty.Register(
            nameof(DisplayAutomation), typeof(ClockDisplayAutomation), typeof(Clock), new PropertyMetadata(default(ClockDisplayAutomation)));

        public ClockDisplayAutomation DisplayAutomation
        {
            get { return (ClockDisplayAutomation)GetValue(DisplayAutomationProperty); }
            set { SetValue(DisplayAutomationProperty, value); }
        }

        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
            nameof(ButtonStyle), typeof(Style), typeof(Clock), new PropertyMetadata(default(Style)));

        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty LesserButtonStyleProperty = DependencyProperty.Register(
            nameof(LesserButtonStyle), typeof(Style), typeof(Clock), new PropertyMetadata(default(Style)));

        public Style LesserButtonStyle
        {
            get { return (Style)GetValue(LesserButtonStyleProperty); }
            set { SetValue(LesserButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ButtonRadiusRatioProperty = DependencyProperty.Register(
            nameof(ButtonRadiusRatio), typeof(double), typeof(Clock), new PropertyMetadata(default(double)));

        public double ButtonRadiusRatio
        {
            get { return (double)GetValue(ButtonRadiusRatioProperty); }
            set { SetValue(ButtonRadiusRatioProperty, value); }
        }

        public static readonly DependencyProperty ButtonRadiusInnerRatioProperty = DependencyProperty.Register(
            nameof(ButtonRadiusInnerRatio), typeof(double), typeof(Clock), new PropertyMetadata(default(double)));

        public double ButtonRadiusInnerRatio
        {
            get { return (double)GetValue(ButtonRadiusInnerRatioProperty); }
            set { SetValue(ButtonRadiusInnerRatioProperty, value); }
        }

        private static readonly DependencyPropertyKey HourLineAnglePropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HourLineAngle", typeof(double), typeof(Clock),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty HourLineAngleProperty =
            HourLineAnglePropertyKey.DependencyProperty;

        public double HourLineAngle
        {
            get { return (double)GetValue(HourLineAngleProperty); }
            private set { SetValue(HourLineAnglePropertyKey, value); }
        }

        public static readonly RoutedEvent ClockChoiceMadeEvent =
            EventManager.RegisterRoutedEvent(
                "ClockChoiceMade",
                RoutingStrategy.Bubble,
                typeof(ClockChoiceMadeEventHandler),
                typeof(Clock));

        private static void OnClockChoiceMade(DependencyObject d, ClockDisplayMode displayMode)
        {
            var instance = (Clock)d;
            var dragCompletedEventArgs = new ClockChoiceMadeEventArgs(displayMode)
            {
                RoutedEvent = ClockChoiceMadeEvent,
            };

            instance.RaiseEvent(dragCompletedEventArgs);
        }

        public override void OnApplyTemplate()
        {
            SetFlags(this);

            GenerateButtons();

            if (_hourReadOutPartName != null)
                _hourReadOutPartName.PreviewMouseLeftButtonDown -= HourReadOutPartNameOnPreviewMouseLeftButtonDown;
            if (_minuteReadOutPartName != null)
                _minuteReadOutPartName.PreviewMouseLeftButtonDown -= MinuteReadOutPartNameOnPreviewMouseLeftButtonDown;
            if (_secondReadOutPartName != null)
                _secondReadOutPartName.PreviewMouseLeftButtonDown -= SecondReadOutPartNameOnPreviewMouseLeftButtonDown;
            _hourReadOutPartName = GetTemplateChild(HourReadOutPartName) as TextBlock;
            _minuteReadOutPartName = GetTemplateChild(MinuteReadOutPartName) as TextBlock;
            _secondReadOutPartName = GetTemplateChild(SecondReadOutPartName) as TextBlock;
            if (_hourReadOutPartName != null)
                _hourReadOutPartName.PreviewMouseLeftButtonDown += HourReadOutPartNameOnPreviewMouseLeftButtonDown;
            if (_minuteReadOutPartName != null)
                _minuteReadOutPartName.PreviewMouseLeftButtonDown += MinuteReadOutPartNameOnPreviewMouseLeftButtonDown;
            if (_secondReadOutPartName != null)
                _secondReadOutPartName.PreviewMouseLeftButtonDown += SecondReadOutPartNameOnPreviewMouseLeftButtonDown;

            base.OnApplyTemplate();

            GotoVisualState(false);
        }

        private void GotoVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this,
                DisplayMode == ClockDisplayMode.Hours
                    ? HoursVisualStateName
                    : DisplayMode == ClockDisplayMode.Minutes
                        ? MinutesVisualStateName
                        : SecondsVisualStateName, useTransitions);
        }

        private void GenerateButtons()
        {
            if (GetTemplateChild(HoursCanvasPartName) is Canvas hoursCanvas)
            {
                RemoveExistingButtons(hoursCanvas);

                if (Is24Hours)
                {
                    GenerateButtons(hoursCanvas, Enumerable.Range(13, 12).ToList(), ButtonRadiusRatio,
                        new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Hours, Is24Hours), i => "ButtonStyle", "00",
                        ClockDisplayMode.Hours);
                    GenerateButtons(hoursCanvas, Enumerable.Range(1, 12).ToList(), ButtonRadiusInnerRatio,
                        new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Hours, Is24Hours), i => "ButtonStyle", "#",
                        ClockDisplayMode.Hours);
                }
                else
                    GenerateButtons(hoursCanvas, Enumerable.Range(1, 12).ToList(), ButtonRadiusRatio,
                        new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Hours, Is24Hours), i => "ButtonStyle", "0",
                        ClockDisplayMode.Hours);
            }

            if (GetTemplateChild(MinutesCanvasPartName) is Canvas minutesCanvas)
            {
                RemoveExistingButtons(minutesCanvas);

                GenerateButtons(minutesCanvas, Enumerable.Range(1, 60).ToList(), ButtonRadiusRatio,
                    new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Minutes, Is24Hours),
                    i => ((i / 5.0) % 1) == 0.0 ? "ButtonStyle" : "LesserButtonStyle", "0",
                        ClockDisplayMode.Minutes);
            }

            if (GetTemplateChild(SecondsCanvasPartName) is Canvas secondsCanvas)
            {
                RemoveExistingButtons(secondsCanvas);

                GenerateButtons(secondsCanvas, Enumerable.Range(1, 60).ToList(), ButtonRadiusRatio,
                    new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Seconds, Is24Hours),
                    i => ((i / 5.0) % 1) == 0.0 ? "ButtonStyle" : "LesserButtonStyle", "0",
                        ClockDisplayMode.Seconds);
            }

            void RemoveExistingButtons(Canvas canvas)
            {
                for (int i = canvas.Children.Count - 1; i >= 0; i--)
                {
                    if (canvas.Children[i] is ClockItemButton)
                    {
                        canvas.Children.RemoveAt(i);
                    }
                }
            }
        }

        private void SecondReadOutPartNameOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetCurrentValue(Clock.DisplayModeProperty, ClockDisplayMode.Seconds);
        }

        private void MinuteReadOutPartNameOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetCurrentValue(Clock.DisplayModeProperty, ClockDisplayMode.Minutes);
        }

        private void HourReadOutPartNameOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SetCurrentValue(Clock.DisplayModeProperty, ClockDisplayMode.Hours);
        }

        private void GenerateButtons(Panel canvas, ICollection<int> range, double radiusRatio, IValueConverter isCheckedConverter, Func<int, string> stylePropertySelector,
            string format, ClockDisplayMode clockDisplayMode)
        {
            var anglePerItem = 360.0 / range.Count;
            var radiansPerItem = anglePerItem * (Math.PI / 180);

            //nothing fancy with sizing/measuring...we are demanding a height
            if (canvas.Width < 10.0 || Math.Abs(canvas.Height - canvas.Width) > 0.0) return;

            _centreCanvas = new Point(canvas.Width / 2, canvas.Height / 2);
            var hypotenuseRadius = _centreCanvas.X * radiusRatio;

            foreach (var i in range)
            {
                var button = new ClockItemButton();
                button.SetBinding(StyleProperty, GetBinding(stylePropertySelector(i)));

                var adjacent = Math.Cos(i * radiansPerItem) * hypotenuseRadius;
                var opposite = Math.Sin(i * radiansPerItem) * hypotenuseRadius;

                button.CentreX = _centreCanvas.X + opposite;
                button.CentreY = _centreCanvas.Y - adjacent;

                button.SetBinding(ToggleButton.IsCheckedProperty, GetBinding("Time", converter: isCheckedConverter, converterParameter: i));
                button.SetBinding(Canvas.LeftProperty, GetBinding("X", button));
                button.SetBinding(Canvas.TopProperty, GetBinding("Y", button));

                button.Content = (i == 60 ? 0 : (i == 24 && clockDisplayMode == ClockDisplayMode.Hours ? 0 : i)).ToString(format);
                canvas.Children.Add(button);
            }
        }


        private void ClockItemDragCompletedHandler(object sender, DragCompletedEventArgs e)
        {
            OnClockChoiceMade(this, DisplayMode);

            switch (DisplayAutomation)
            {
                case ClockDisplayAutomation.None:
                    break;
                case ClockDisplayAutomation.Cycle:
                    DisplayMode = DisplayMode == ClockDisplayMode.Hours ? ClockDisplayMode.Minutes : ClockDisplayMode.Hours;
                    break;
                case ClockDisplayAutomation.CycleWithSeconds:
                    if (DisplayMode == ClockDisplayMode.Hours)
                        DisplayMode = ClockDisplayMode.Minutes;
                    else if (DisplayMode == ClockDisplayMode.Minutes)
                        DisplayMode = ClockDisplayMode.Seconds;
                    else
                        DisplayMode = ClockDisplayMode.Hours;
                    break;
                case ClockDisplayAutomation.ToMinutesOnly:
                    if (DisplayMode == ClockDisplayMode.Hours)
                        DisplayMode = ClockDisplayMode.Minutes;
                    break;
                case ClockDisplayAutomation.ToSeconds:
                    if (DisplayMode == ClockDisplayMode.Hours)
                        DisplayMode = ClockDisplayMode.Minutes;
                    else if (DisplayMode == ClockDisplayMode.Minutes)
                        DisplayMode = ClockDisplayMode.Seconds;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ClockItemDragStartedHandler(object sender, DragStartedEventArgs dragStartedEventArgs)
        {
            _currentStartPosition = new Point(dragStartedEventArgs.HorizontalOffset, dragStartedEventArgs.VerticalOffset);
        }

        private void ClockItemDragDeltaHandler(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            var currentDragPosition = new Point(_currentStartPosition.X + dragDeltaEventArgs.HorizontalChange, _currentStartPosition.Y + dragDeltaEventArgs.VerticalChange);
            var delta = new Point(currentDragPosition.X - _centreCanvas.X, currentDragPosition.Y - _centreCanvas.Y);

            var angle = Math.Atan2(delta.X, -delta.Y);
            if (angle < 0) angle += 2 * Math.PI;

            DateTime time;
            if (DisplayMode == ClockDisplayMode.Hours)
            {
                if (Is24Hours)
                {
                    var outerBoundary = (_centreCanvas.X * ButtonRadiusInnerRatio +
                                         (_centreCanvas.X * ButtonRadiusRatio - _centreCanvas.X * ButtonRadiusInnerRatio) / 2);
                    var sqrt = Math.Sqrt((_centreCanvas.X - currentDragPosition.X) * (_centreCanvas.X - currentDragPosition.X) + (_centreCanvas.Y - currentDragPosition.Y) * (_centreCanvas.Y - currentDragPosition.Y));
                    var localIsPostMerdiem = sqrt > outerBoundary;

                    var hour = (int)Math.Round(6 * angle / Math.PI, MidpointRounding.AwayFromZero) % 12 + (localIsPostMerdiem ? 12 : 0);
                    if (hour == 12)
                        hour = 0;
                    else if (hour == 0)
                        hour = 12;
                    time = new DateTime(Time.Year, Time.Month, Time.Day, hour, Time.Minute, Time.Second);
                }
                else
                    time = new DateTime(Time.Year, Time.Month, Time.Day,
                        (int)Math.Round(6 * angle / Math.PI, MidpointRounding.AwayFromZero) % 12 + (IsPostMeridiem ? 12 : 0),
                        Time.Minute, Time.Second);
            }
            else
            {
                var value = (int)Math.Round(30 * angle / Math.PI, MidpointRounding.AwayFromZero) % 60;
                if (DisplayMode == ClockDisplayMode.Minutes)
                    time = new DateTime(Time.Year, Time.Month, Time.Day, Time.Hour, value, Time.Second);
                else
                    time = new DateTime(Time.Year, Time.Month, Time.Day, Time.Hour, Time.Minute, value);
            }
            SetCurrentValue(TimeProperty, time);
        }

        private static void SetFlags(Clock clock)
        {
            clock.IsPostMeridiem = clock.Time.Hour >= 12;
            clock.IsMidnightHour = clock.Time.Hour == 0;
            clock.IsMiddayHour = clock.Time.Hour == 12;
        }

        private BindingBase GetBinding(string propertyName, object owner = null, IValueConverter converter = null, object converterParameter = null)
        {
            var result = new Binding(propertyName) { Source = owner ?? this, Converter = converter, ConverterParameter = converterParameter };
            return result;
        }
    }
}
