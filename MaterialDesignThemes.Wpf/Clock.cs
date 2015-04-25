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
	public enum ClockDisplay
	{
		Hours,
		Minutes
	}

	[TemplatePart(Name = HoursCanvasPartName, Type = typeof (Canvas))]
	[TemplatePart(Name = MinutesCanvasPartName, Type = typeof(Canvas))]
	public class Clock : Control
	{
		public const string HoursCanvasPartName = "PART_HoursCanvas";
		public const string MinutesCanvasPartName = "PART_MinutesCanvas";

		static Clock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Clock), new FrameworkPropertyMetadata(typeof (Clock)));
		}

		public Clock()
		{
		}

		public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
			"Time", typeof (DateTime), typeof (Clock), new PropertyMetadata(default(DateTime)));

		public DateTime Time
		{
			get { return (DateTime) GetValue(TimeProperty); }
			set { SetValue(TimeProperty, value); }
		}

		public static readonly DependencyProperty ClockDisplayProperty = DependencyProperty.Register(
			"ClockDisplay", typeof (ClockDisplay), typeof (Clock), new PropertyMetadata(default(ClockDisplay)));

		public ClockDisplay ClockDisplay
		{
			get { return (ClockDisplay) GetValue(ClockDisplayProperty); }
			set { SetValue(ClockDisplayProperty, value); }
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

		public override void OnApplyTemplate()
		{			
			var hoursCanvas = GetTemplateChild(HoursCanvasPartName) as Canvas;
			if (hoursCanvas != null)
				GenerateButtons(hoursCanvas, Enumerable.Range(1, 12).ToList(), new ClockItemIsCheckedConverter(() => Time, ClockDisplay.Hours), i => "ButtonStyle");

			var minutesCanvas = GetTemplateChild(MinutesCanvasPartName) as Canvas;
			if (minutesCanvas != null)
				GenerateButtons(minutesCanvas, Enumerable.Range(1, 60).ToList(), new ClockItemIsCheckedConverter(() => Time, ClockDisplay.Minutes),
					i => ((i / 5.0)%1) == 0.0 ? "ButtonStyle" : "LesserButtonStyle");

			base.OnApplyTemplate();
		}

		private void GenerateButtons(Panel canvas, ICollection<int> range, IValueConverter isCheckedConverter, Func<int, string> stylePropertySelector)
		{
			var anglePerItem = 360.0 / range.Count;
			var radiansPerItem = anglePerItem * (Math.PI / 180);

			//nothing fancy with sizing/measuring...we are demanding a height
			if (canvas.Width < 10.0 || Math.Abs(canvas.Height - canvas.Width) > 0.0) return;

			var centreCanvasX = canvas.Width/2;
			var centreCanvasY = canvas.Height/2;
			var hypotenuseRadius = centreCanvasX * ButtonRadiusRatio;

			foreach (var i in range)
			{
				var button = new ClockItemButton();
				button.SetBinding(StyleProperty, GetBinding(stylePropertySelector(i)));

				var adjacent = Math.Cos(i*radiansPerItem)*hypotenuseRadius;
				var opposite = Math.Sin(i*radiansPerItem)*hypotenuseRadius;

				button.CentreX = centreCanvasX + opposite;
				button.CentreY = centreCanvasY - adjacent;

				button.SetBinding(ToggleButton.IsCheckedProperty, GetBinding("Time", converter: isCheckedConverter, converterParameter: i));
				button.SetBinding(Canvas.LeftProperty, GetBinding("X", button));
				button.SetBinding(Canvas.TopProperty, GetBinding("Y", button));

				button.Content = i;
				canvas.Children.Add(button);
			}
        }
		
		private BindingBase GetBinding(string propertyName, object owner = null, IValueConverter converter = null, object converterParameter = null)
		{
			var result = new Binding(propertyName) {Source = owner ?? this, Converter = converter, ConverterParameter = converterParameter};
			return result;
		}
	}
}
