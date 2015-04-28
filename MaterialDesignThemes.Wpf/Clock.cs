using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf.Converters;

namespace MaterialDesignThemes.Wpf
{
	public enum ClockDisplayMode
	{
		Hours,
		Minutes
	}

	public enum ClockDisplayAutomation
	{
		None,
		Cycle,
		ToMinutesOnly
	}

	[TemplatePart(Name = HoursCanvasPartName, Type = typeof (Canvas))]
	[TemplatePart(Name = MinutesCanvasPartName, Type = typeof(Canvas))]
	[TemplatePart(Name = MinuteReadOutPartName, Type = typeof(TextBlock))]
	[TemplatePart(Name = HourReadOutPartName, Type = typeof(TextBlock))]
	public class Clock : Control
	{
		public const string HoursCanvasPartName = "PART_HoursCanvas";
		public const string MinutesCanvasPartName = "PART_MinutesCanvas";
		public const string MinuteReadOutPartName = "PART_MinuteReadOut";
		public const string HourReadOutPartName = "PART_HourReadOut";

        private Point _centreCanvas = new Point(0, 0);
        private Point _currentStartPosition = new Point(0, 0);
		private TextBlock _hourReadOutPartName;
		private TextBlock _minuteReadOutPartName;

		static Clock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Clock), new FrameworkPropertyMetadata(typeof (Clock)));
		}

		public Clock()
		{
            AddHandler(ClockItemButton.DragStartedEvent, new DragStartedEventHandler(ClockItemDragStartedHandler));
			AddHandler(ClockItemButton.DragDeltaEvent, new DragDeltaEventHandler(ClockItemDragDeltaHandler));
			AddHandler(ClockItemButton.DragCompletedEvent, new DragCompletedEventHandler(ClockItemDragCompletedHandler));
		}

		public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
			"Time", typeof (DateTime), typeof (Clock), new FrameworkPropertyMetadata(default(DateTime), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TimePropertyChangedCallback));

		private static void TimePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
		{
			var clock = (Clock) dependencyObject;
			clock.IsPostMeridiem = clock.Time.Hour >= 12;
		}

		public DateTime Time
		{
			get { return (DateTime) GetValue(TimeProperty); }
			set { SetValue(TimeProperty, value); }
		}

		public static readonly DependencyProperty IsPostMeridiemProperty = DependencyProperty.Register(
			"IsPostMeridiem", typeof (bool), typeof (Clock), new PropertyMetadata(default(bool), IsPostMeridiemPropertyChangedCallback));

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
			get { return (bool) GetValue(IsPostMeridiemProperty); }
			set { SetValue(IsPostMeridiemProperty, value); }
		}

		public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(
			"DisplayMode", typeof (ClockDisplayMode), typeof (Clock), new PropertyMetadata(ClockDisplayMode.Hours));

		public ClockDisplayMode DisplayMode
		{
			get { return (ClockDisplayMode) GetValue(DisplayModeProperty); }
			set { SetValue(DisplayModeProperty, value); }
		}

		public static readonly DependencyProperty DisplayAutomationProperty = DependencyProperty.Register(
			"DisplayAutomation", typeof (ClockDisplayAutomation), typeof (Clock), new PropertyMetadata(default(ClockDisplayAutomation)));

		public ClockDisplayAutomation DisplayAutomation
		{
			get { return (ClockDisplayAutomation) GetValue(DisplayAutomationProperty); }
			set { SetValue(DisplayAutomationProperty, value); }
		}

		public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
			"ButtonStyle", typeof (Style), typeof (Clock), new PropertyMetadata(default(Style)));		

		public Style ButtonStyle
		{
			get { return (Style) GetValue(ButtonStyleProperty); }
			set { SetValue(ButtonStyleProperty, value); }
		}

		public static readonly DependencyProperty LesserButtonStyleProperty = DependencyProperty.Register(
			"LesserButtonStyle", typeof (Style), typeof (Clock), new PropertyMetadata(default(Style)));

		public Style LesserButtonStyle
		{
			get { return (Style) GetValue(LesserButtonStyleProperty); }
			set { SetValue(LesserButtonStyleProperty, value); }
		}

		public static readonly DependencyProperty ButtonRadiusRatioProperty = DependencyProperty.Register(
			"ButtonRadiusRatio", typeof (double), typeof (Clock), new PropertyMetadata(default(double)));

		public double ButtonRadiusRatio
		{
			get { return (double) GetValue(ButtonRadiusRatioProperty); }
			set { SetValue(ButtonRadiusRatioProperty, value); }
		}

		private static readonly DependencyPropertyKey HourLineAnglePropertyKey =
			DependencyProperty.RegisterReadOnly(
				"HourLineAngle", typeof (double), typeof (Clock),
				new PropertyMetadata(default(double)));

		public static readonly DependencyProperty HourLineAngleProperty =
			HourLineAnglePropertyKey.DependencyProperty;

		public double HourLineAngle
		{
			get { return (double) GetValue(HourLineAngleProperty); }
			private set { SetValue(HourLineAnglePropertyKey, value); }
		}

		public static readonly RoutedEvent ClockChoiceMadeEvent =
			EventManager.RegisterRoutedEvent(
				"ClockChoiceMade",
				RoutingStrategy.Bubble,
				typeof (ClockChoiceMadeEventHandler),
				typeof (Clock));

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
			var hoursCanvas = GetTemplateChild(HoursCanvasPartName) as Canvas;
			if (hoursCanvas != null)
				GenerateButtons(hoursCanvas, Enumerable.Range(1, 12).ToList(), new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Hours), i => "ButtonStyle");

			var minutesCanvas = GetTemplateChild(MinutesCanvasPartName) as Canvas;
			if (minutesCanvas != null)
				GenerateButtons(minutesCanvas, Enumerable.Range(1, 60).ToList(), new ClockItemIsCheckedConverter(() => Time, ClockDisplayMode.Minutes),
					i => ((i / 5.0)%1) == 0.0 ? "ButtonStyle" : "LesserButtonStyle");

			if (_hourReadOutPartName != null)
				_hourReadOutPartName.PreviewMouseLeftButtonDown -= HourReadOutPartNameOnPreviewMouseLeftButtonDown;
			if (_minuteReadOutPartName != null)
				_minuteReadOutPartName.PreviewMouseLeftButtonDown -= MinuteReadOutPartNameOnPreviewMouseLeftButtonDown;
			_hourReadOutPartName = GetTemplateChild(HourReadOutPartName) as TextBlock;
			_minuteReadOutPartName = GetTemplateChild(MinuteReadOutPartName) as TextBlock;
			if (_hourReadOutPartName != null)
				_hourReadOutPartName.PreviewMouseLeftButtonDown += HourReadOutPartNameOnPreviewMouseLeftButtonDown;
			if (_minuteReadOutPartName != null)
				_minuteReadOutPartName.PreviewMouseLeftButtonDown += MinuteReadOutPartNameOnPreviewMouseLeftButtonDown;

			base.OnApplyTemplate();
		}

		private void MinuteReadOutPartNameOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			SetCurrentValue(Clock.DisplayModeProperty, ClockDisplayMode.Minutes);
		}

		private void HourReadOutPartNameOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			SetCurrentValue(Clock.DisplayModeProperty, ClockDisplayMode.Hours);
		}

		private void GenerateButtons(Panel canvas, ICollection<int> range, IValueConverter isCheckedConverter, Func<int, string> stylePropertySelector)
		{
			var anglePerItem = 360.0 / range.Count;
			var radiansPerItem = anglePerItem * (Math.PI / 180);

			//nothing fancy with sizing/measuring...we are demanding a height
			if (canvas.Width < 10.0 || Math.Abs(canvas.Height - canvas.Width) > 0.0) return;

            _centreCanvas = new Point(canvas.Width / 2, canvas.Height / 2);
            var hypotenuseRadius = _centreCanvas.X * ButtonRadiusRatio;

			foreach (var i in range)
			{
				var button = new ClockItemButton();
				button.SetBinding(StyleProperty, GetBinding(stylePropertySelector(i)));

				var adjacent = Math.Cos(i*radiansPerItem)*hypotenuseRadius;
				var opposite = Math.Sin(i*radiansPerItem)*hypotenuseRadius;

                button.CentreX = _centreCanvas.X + opposite;
                button.CentreY = _centreCanvas.Y - adjacent;

				button.SetBinding(ToggleButton.IsCheckedProperty, GetBinding("Time", converter: isCheckedConverter, converterParameter: i));
				button.SetBinding(Canvas.LeftProperty, GetBinding("X", button));
				button.SetBinding(Canvas.TopProperty, GetBinding("Y", button));

				button.Content = i;
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
				case ClockDisplayAutomation.ToMinutesOnly:
					if (DisplayMode == ClockDisplayMode.Hours)
						DisplayMode = ClockDisplayMode.Minutes;					
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

            var time = DisplayMode == ClockDisplayMode.Hours
                ? new DateTime(Time.Year, Time.Month, Time.Day, (int)Math.Round(6 * angle / Math.PI, MidpointRounding.AwayFromZero) % 12, Time.Minute, Time.Second)
                : new DateTime(Time.Year, Time.Month, Time.Day, Time.Hour, (int)Math.Round(30 * angle / Math.PI, MidpointRounding.AwayFromZero) % 60, Time.Second);

            SetCurrentValue(TimeProperty, time);	
        }

		private BindingBase GetBinding(string propertyName, object owner = null, IValueConverter converter = null, object converterParameter = null)
		{
			var result = new Binding(propertyName) {Source = owner ?? this, Converter = converter, ConverterParameter = converterParameter};
			return result;
		}
	}
}
