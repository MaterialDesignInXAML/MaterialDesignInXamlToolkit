using System;

namespace VTTests
{
    public class Retry
    {
        public int? MinAttempts { get; }
        public TimeSpan Timeout { get; }

        public Retry(int minAttempts, TimeSpan timeout)
        {
            MinAttempts = minAttempts;
            Timeout = timeout;
        }

        public override string ToString() 
            => $"{(MinAttempts != null ? $"Attempts: {MinAttempts}, " : "")}Timeout: {Timeout}";

        public static Retry Default { get; } = new Retry(3, TimeSpan.FromSeconds(2));
    }
}
