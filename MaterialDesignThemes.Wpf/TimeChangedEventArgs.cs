using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf;

public sealed class TimeChangedEventArgs : RoutedEventArgs
{
    public DateTime OldTime { get; }
    public DateTime NewTime { get; }

    public TimeChangedEventArgs(RoutedEvent routedEvent, DateTime oldTime, DateTime newTime)
        : base(routedEvent)
    {
        OldTime = oldTime;
        NewTime = newTime;
    }
}
