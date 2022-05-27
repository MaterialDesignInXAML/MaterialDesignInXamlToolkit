using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Xunit;

namespace MaterialDesignThemes.Wpf.Tests;

public class ClockTests
{
    [StaFact(Timeout = 500)]
    public void CanGenerateHoursButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(1, 12).Select(x => $"{x:0}");
        Assert.Equal(expected.OrderBy(x => x), buttonContents.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void CanGenerateHoursButtonsWith24Hours()
    {
        var clock = new Clock { Is24Hours = true };
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(13, 11).Select(x => $"{x:00}")
            .Concat(new[] { "00" })
            .Concat(Enumerable.Range(1, 12).Select(x => $"{x:#}"));
        Assert.Equal(expected.OrderBy(x => x), buttonContents.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void CanGenerateMinuteButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas minutesCanvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        var buttonContents = minutesCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        Assert.Equal(expected.OrderBy(x => x), buttonContents.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void CanGenerateSecondsButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas secondsCanvas = clock.FindVisualChild<Canvas>(Clock.SecondsCanvasPartName);
        var buttonContents = secondsCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        Assert.Equal(expected.OrderBy(x => x), buttonContents.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void TimeChangedEvent_WhenTimeChanges_EventIsRaised()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();
        List<TimeChangedEventArgs> invocations = new();

        clock.TimeChanged += OnTimeChanged;

        void OnTimeChanged(object sender, TimeChangedEventArgs e)
            => invocations.Add(e);
        DateTime now = DateTime.Now;

        clock.Time = now;
        clock.Time += TimeSpan.FromMinutes(-2);

        Assert.Equal(2, invocations.Count);

        Assert.Equal(default, invocations[0].OldTime);
        Assert.Equal(now, invocations[0].NewTime);

        Assert.Equal(now, invocations[1].OldTime);
        Assert.Equal(now + TimeSpan.FromMinutes(-2), invocations[1].NewTime);
    }

    
}