using System.Windows;

namespace MaterialDesignThemes.Wpf
{
	public delegate void ClockChoiceMadeEventHandler(object sender, ClockChoiceMadeEventArgs e);

	public class ClockChoiceMadeEventArgs : RoutedEventArgs
	{
		private readonly ClockDisplayMode _displayMode;

		public ClockChoiceMadeEventArgs(ClockDisplayMode displayMode)
		{
			_displayMode = displayMode;
		}

		public ClockChoiceMadeEventArgs(ClockDisplayMode displayMode, RoutedEvent routedEvent) : base(routedEvent)
		{
			_displayMode = displayMode;
		}

		public ClockChoiceMadeEventArgs(ClockDisplayMode displayMode, RoutedEvent routedEvent, object source) : base(routedEvent, source)
		{
			_displayMode = displayMode;
		}

		public ClockDisplayMode Mode
		{
			get { return _displayMode; }
		}
	}
}