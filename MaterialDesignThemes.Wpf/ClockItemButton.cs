using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
	[TemplatePart(Name = ThumbPartName, Type = typeof(Thumb))]
	public class ClockItemButton : ToggleButton
	{
		public const string ThumbPartName = "PART_Thumb";
		
		public static readonly DependencyProperty CentreXProperty = DependencyProperty.Register(
			"CentreX", typeof (double), typeof (ClockItemButton), new PropertyMetadata(default(double)));

		public double CentreX
		{
			get { return (double) GetValue(CentreXProperty); }
			set { SetValue(CentreXProperty, value); }
		}

		public static readonly DependencyProperty CentreYProperty = DependencyProperty.Register(
			"CentreY", typeof (double), typeof (ClockItemButton), new PropertyMetadata(default(double)));

		public double CentreY
		{
			get { return (double) GetValue(CentreYProperty); }
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
				_thumb.DragDelta -= ThumbOnDragDelta;
			}

			_thumb = GetTemplateChild(ThumbPartName) as Thumb;

			if (_thumb != null)
			{
				_thumb.DragDelta += ThumbOnDragDelta;
            }

			base.OnApplyTemplate();
		}

		private void ThumbOnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
		{
			System.Diagnostics.Debug.WriteLine("drag that shit");
		}

		protected override Size ArrangeOverride(Size finalSize)
		{		
			Dispatcher.BeginInvoke(new Action(() =>
			{
				X = CentreX - finalSize.Width/2;
				Y = CentreY - finalSize.Height/2;
			}));

			return base.ArrangeOverride(finalSize);
		}
	}
}