namespace MaterialDesignThemes.Wpf.Tests;

[TestExecutor<STAThreadExecutor>]
public class TimeSelectionRulesTests
{
    private static readonly DateTime Date = new(2026, 10, 25);

    private static bool BusinessHours(DateTime time) => time.TimeOfDay >= TimeSpan.FromHours(6)
        && time.TimeOfDay <= TimeSpan.FromHours(19) && time.Hour != 13 && time.Minute % 10 == 0;

    [Test]
    public async Task NullRulePreservesOriginalTimeIncludingSeconds()
    {
        var time = Date.AddHours(3).AddMinutes(17).AddSeconds(23);
        await Assert.That(new Clock { Time = time }.Time).IsEqualTo(time);
        await Assert.That(new TimePicker { SelectedTime = time }.SelectedTime).IsEqualTo(time);
    }

    [Test]
    [Arguments(3, 17, 6, 0)]
    [Arguments(9, 17, 9, 20)]
    [Arguments(13, 20, 12, 50)]
    [Arguments(19, 10, 19, 0)]
    [Arguments(6, 0, 6, 0)]
    [Arguments(19, 0, 19, 0)]
    public async Task SelectionIsCoercedToNearestAllowedMinute(int hour, int minute, int expectedHour, int expectedMinute)
    {
        var requested = Date.AddHours(hour).AddMinutes(minute);
        var expected = Date.AddHours(expectedHour).AddMinutes(expectedMinute);
        var clock = new Clock { IsTimeAllowed = BusinessHours, Time = requested };
        var picker = new TimePicker { IsTimeAllowed = BusinessHours, SelectedTime = requested };
        await Assert.That(clock.Time).IsEqualTo(expected);
        await Assert.That(picker.SelectedTime).IsEqualTo(expected);
    }

    [Test]
    public async Task NativeButtonsDisableUnavailableHoursAndMinutes()
    {
        var clock = new Clock { Is24Hours = true, IsTimeAllowed = BusinessHours, Time = Date.AddHours(9) };
        clock.ApplyDefaultStyle();
        var hours = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName).GetVisualChildren<ClockItemButton>().ToList();
        await Assert.That(hours.Count).IsEqualTo(24);
        await Assert.That(hours.Count(b => b.IsEnabled)).IsEqualTo(13);
        await Assert.That(hours.Single(b => b.Content.ToString() == "13").IsEnabled).IsFalse();
        var minutes = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName).GetVisualChildren<ClockItemButton>().ToList();
        await Assert.That(minutes.Count(b => b.IsEnabled)).IsEqualTo(6);
        clock.Time = Date.AddHours(19);
        await Assert.That(minutes.Count(b => b.IsEnabled)).IsEqualTo(1);
    }

    [Test]
    [Arguments("3:17", false)]
    [Arguments("9:17", false)]
    [Arguments("13:20", false)]
    [Arguments("19:10", false)]
    [Arguments("9:20", true)]
    [Arguments("19:00", true)]
    public async Task ManualInputUsesTheSameRule(string text, bool accepted)
    {
        var picker = new TimePicker { Is24Hours = true, IsTimeAllowed = BusinessHours, SelectedTime = Date.AddHours(9) };
        picker.ApplyDefaultStyle();
        var input = picker.FindVisualChild<TextBox>(TimePicker.TextBoxPartName);
        input.Text = text;
        input.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
        await Assert.That(picker.SelectedTime).IsEqualTo(accepted ? Date.Add(TimeSpan.Parse(text)) : Date.AddHours(9));
    }

    [Test]
    public async Task ManualInputRuleReceivesTheSelectedDate()
    {
        var picker = new TimePicker { Is24Hours = true, SelectedTime = Date.AddHours(9), IsTimeAllowed = t => t.Date == Date };
        picker.ApplyDefaultStyle();
        var input = picker.FindVisualChild<TextBox>(TimePicker.TextBoxPartName);
        input.Text = "10:20";
        input.RaiseEvent(new RoutedEventArgs(UIElement.LostFocusEvent));
        await Assert.That(picker.SelectedTime).IsEqualTo(Date.AddHours(10).AddMinutes(20));
    }

    [Test]
    public async Task RefreshReevaluatesCapturedBookings()
    {
        var booked = new HashSet<TimeSpan>();
        var clock = new Clock { Time = Date.AddHours(9), IsTimeAllowed = t => BusinessHours(t) && !booked.Contains(t.TimeOfDay) };
        clock.ApplyDefaultStyle();
        booked.Add(TimeSpan.FromHours(9));
        clock.RefreshTimeAvailability();
        await Assert.That(clock.Time).IsEqualTo(Date.AddHours(8).AddMinutes(50));
    }

    [Test]
    public async Task EmptyAvailabilityClearsPickerAndDisablesClockButtons()
    {
        var picker = new TimePicker { SelectedTime = Date.AddHours(9), IsTimeAllowed = _ => false };
        await Assert.That(picker.SelectedTime).IsNull();
        var clock = new Clock { IsTimeAllowed = _ => false };
        clock.ApplyDefaultStyle();
        var hours = clock.FindVisualChild<Canvas>(Clock.HoursCanvasPartName).GetVisualChildren<ClockItemButton>();
        await Assert.That(hours.Any(b => b.IsEnabled)).IsFalse();
    }

    [Test]
    public async Task RemovingRuleRestoresAvailabilityAndPreservesExistingMinuteStep()
    {
        var clock = new Clock { Time = Date.AddHours(9), MinuteSelectionStep = 15, IsTimeAllowed = _ => false };
        clock.ApplyDefaultStyle();
        clock.IsTimeAllowed = null;
        var minutes = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName).GetVisualChildren<ClockItemButton>();
        await Assert.That(minutes.Count(b => b.IsEnabled)).IsEqualTo(4);
    }

    [Test]
    public async Task DraggingCannotSelectAnUnavailableMinute()
    {
        var clock = new Clock { IsTimeAllowed = BusinessHours, Time = Date.AddHours(9), DisplayMode = ClockDisplayMode.Minutes };
        clock.ApplyDefaultStyle();
        var canvas = clock.FindVisualChild<Canvas>(Clock.MinutesCanvasPartName);
        double center = canvas.Width / 2;
        double angle = 17 * Math.PI / 30;
        clock.RaiseEvent(new DragStartedEventArgs(center + Math.Sin(angle) * center * .8, center - Math.Cos(angle) * center * .8)
        {
            RoutedEvent = ClockItemButton.DragStartedEvent
        });
        clock.RaiseEvent(new DragDeltaEventArgs(0, 0) { RoutedEvent = ClockItemButton.DragDeltaEvent });
        await Assert.That(clock.Time).IsEqualTo(Date.AddHours(9).AddMinutes(20));
    }

    [Test]
    public async Task RejectedMeridiemChangeKeepsHeaderConsistentWithTime()
    {
        var clock = new Clock { IsTimeAllowed = t => t.Hour >= 14 && t.Hour <= 18, Time = Date.AddHours(14) };
        clock.IsPostMeridiem = false;
        await Assert.That(clock.Time).IsEqualTo(Date.AddHours(14));
        await Assert.That(clock.IsPostMeridiem).IsTrue();
    }
}
