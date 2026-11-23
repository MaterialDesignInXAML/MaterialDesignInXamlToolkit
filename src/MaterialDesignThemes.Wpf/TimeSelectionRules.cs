namespace MaterialDesignThemes.Wpf;

internal static class TimeSelectionRules
{
    // Rules describe minute slots; seconds remain available through WithSeconds.
    internal static bool IsAllowed(DateTime time, Predicate<DateTime>? predicate) => predicate == null || predicate(time.Date.AddHours(time.Hour).AddMinutes(time.Minute));

    internal static DateTime? Nearest(DateTime time, Predicate<DateTime>? predicate, int? hour = null)
    {
        if ((!hour.HasValue || time.Hour == hour) && IsAllowed(time, predicate))
            return time;

        DateTime? nearest = null;
        long distance = long.MaxValue;
        long fraction = time.TimeOfDay.Ticks % TimeSpan.TicksPerMinute;

        for (int h = hour ?? 0; h <= (hour ?? 23); h++)
        {
            for (int m = 0; m < 60; m++)
            {
                var candidate = time.Date.AddHours(h).AddMinutes(m).AddTicks(fraction);

                if (!IsAllowed(candidate, predicate))
                    continue;

                long delta = Math.Abs(candidate.Ticks - time.Ticks);

                if (delta < distance)
                {
                    nearest = candidate;
                    distance = delta;
                }
            }
        }

        return nearest;
    }
}
