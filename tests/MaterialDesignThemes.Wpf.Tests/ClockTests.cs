namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class ClockTests
{
    [Test]
    public async Task CanGenerateHoursButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(1, 12).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x))
            .IsEquivalentTo(expected.OrderBy(x => x));
    }

    [Test]
    public async Task CanGenerateHoursButtonsWith24Hours()
    {
        var clock = new Clock { Is24Hours = true };
        clock.ApplyDefaultStyle();

        Canvas hoursCanvas = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName);
        var buttonContents = hoursCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(13, 11).Select(x => $"{x:00}")
            .Concat(["00"])
            .Concat(Enumerable.Range(1, 12).Select(x => $"{x:#}"));
        await Assert.That(buttonContents.OrderBy(x => x)).IsEquivalentTo(expected.OrderBy(x => x));
    }

    [Test]
    public async Task CanGenerateMinuteButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas minutesCanvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        var buttonContents = minutesCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x)).IsEquivalentTo(expected.OrderBy(x => x));
    }

    [Test]
    public async Task CanGenerateSecondsButtons()
    {
        var clock = new Clock();
        clock.ApplyDefaultStyle();

        Canvas secondsCanvas = clock.FindVisualChild<Canvas>(Clock.SecondsCanvasPartName);
        var buttonContents = secondsCanvas.GetVisualChildren<ClockItemButton>().Select(x => x.Content.ToString());

        var expected = Enumerable.Range(0, 60).Select(x => $"{x:0}");
        await Assert.That(buttonContents.OrderBy(x => x)).IsEquivalentTo(expected.OrderBy(x => x));
    }

    [Test]
    public async Task TimeChangedEvent_WhenTimeChanges_EventIsRaised()
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


    [Test]
    [Arguments(1)]
    [Arguments(2)]
    [Arguments(3)]
    [Arguments(4)]
    [Arguments(5)]
    [Arguments(6)]
    [Arguments(10)]
    [Arguments(12)]
    [Arguments(15)]
    [Arguments(20)]
    [Arguments(30)]
    [Arguments(60)]
    public async Task MinuteSelectionStep_WhenSet_OnlyMatchingMinuteButtonsAreEnabled(int minuteSelectionStep)
    {
        var clock = new Clock
        {
            MinuteSelectionStep = minuteSelectionStep
        };

        clock.ApplyDefaultStyle();

        Canvas minutesCanvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        var minuteButtons = minutesCanvas.GetVisualChildren<ClockItemButton>();

        foreach (var button in minuteButtons)
        {
            int value = int.Parse(button.Content!.ToString()!);

            bool shouldBeEnabled = value % minuteSelectionStep == 0;

            await Assert.That(button.IsEnabled).IsEqualTo(shouldBeEnabled);
            await Assert.That(button.Opacity).IsEqualTo(shouldBeEnabled ? 1d : 0.38d);
        }
    }

    [Test]
    public async Task MinuteSelectionStep_Default_AllButtonsAreEnabled()
    {
        var clock = new Clock();

        clock.ApplyDefaultStyle();

        Canvas minutesCanvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        var minuteButtons = minutesCanvas.GetVisualChildren<ClockItemButton>();

        await Assert.That(minuteButtons.All(x => x.IsEnabled)).IsTrue();
    }

    [Test]
    public async Task MinuteSelectionStep_DefaultValue_IsOne()
    {
        var clock = new Clock();

        await Assert.That(clock.MinuteSelectionStep).IsEqualTo(1);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-1)]
    [Arguments(61)]
    [Arguments(7)]
    [Arguments(13)]
    [Arguments(59)]
    public async Task MinuteSelectionStep_InvalidValues_Throws(int value)
    {
        var clock = new Clock();

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => clock.MinuteSelectionStep = value);

        await Assert.That(ex.Message).IsEqualTo($"{nameof(Clock.MinuteSelectionStep)} must be a divisor of 60 and between 1 and 60. (Parameter '{nameof(Clock.MinuteSelectionStep)}')");
    }
}
