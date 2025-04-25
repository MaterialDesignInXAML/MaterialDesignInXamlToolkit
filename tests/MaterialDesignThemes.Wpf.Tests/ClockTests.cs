
using TUnit.Core;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using System.Threading.Tasks;

namespace MaterialDesignThemes.Wpf.Tests;

public class ClockTests
{
    [StaFact(Timeout = 500)]
    public async Task CanGenerateHoursButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(1, 12).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x)).IsEqualTo(expected.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public async Task CanGenerateHoursButtonsWith24Hours()
    {
        var clock = new Clock { Is24Hours = true };
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(13, 11).Select(x => $"{x:00}")
            .Concat(new[] { "00" })
            .Concat(Enumerable.Range(1, 12).Select(x => $"{x:#}"));
        await Assert.That(buttonContents.OrderBy(x => x)).IsEqualTo(expected.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void CanGenerateMinuteButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas minutesCanvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        var buttonContents = minutesCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x)).IsEqualTo(expected.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void CanGenerateSecondsButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas secondsCanvas = clock.FindVisualChild<Canvas>(Clock.SecondsCanvasPartName);
        var buttonContents = secondsCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x)).IsEqualTo(expected.OrderBy(x => x));
    }

    [StaFact(Timeout = 500)]
    public void TimeChangedEvent_WhenTimeChanges_EventIsRaised()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();
        List<TimeChangedEventArgs> invocations = new();

        clock.TimeChanged += OnTimeChanged;

        void OnTimeChanged(object? sender, TimeChangedEventArgs e)
            => invocations.Add(e);
        DateTime now = DateTime.Now;

        clock.Time = now;
        clock.Time += TimeSpan.FromMinutes(-2);

        await Assert.That(invocations.Count).IsEqualTo(2);

        await Assert.That(invocations[0].OldTime).IsEqualTo(default);
        await Assert.That(invocations[0].NewTime).IsEqualTo(now);

        await Assert.That(invocations[1].OldTime).IsEqualTo(now);
        await Assert.That(invocations[1].NewTime).IsEqualTo(now + TimeSpan.FromMinutes(-2));
    }


}
