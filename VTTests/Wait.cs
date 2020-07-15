using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VTTests
{
    public static class Wait
    {
        public static async Task For(Func<Task<bool>> action, Retry? retry = null)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            retry ??= Retry.Default;

            int delay = (int)(retry.Timeout.TotalMilliseconds / 10);
            if (delay < 15)
            {
                delay = 0;
            }

            int numAttempts = 0;
            var sw = Stopwatch.StartNew();
            Exception? thrownException = null;
            do
            {
                numAttempts++;
                try
                {
                    if (await action())
                    {
                        //Success
                        return;
                    }
                }
                catch (Exception ex)
                {
                    thrownException = ex;
                }
                if (delay > 0)
                {
                    await Task.Delay(delay);
                }
            }
            while (ShouldRetry());
            throw new TimeoutException($"Timeout of '{retry}' exceeded", thrownException);

            bool ShouldRetry() =>
                sw.Elapsed <= retry.Timeout ||
                numAttempts < retry.MinAttempts;
        }

        public static async Task For(Func<Task> action, Retry? retry = null)
        {
            await For(async () =>
            {
                await action();
                return true;
            }, retry);
        }

        public static async Task<T> For<T>(Func<Task<T>> action, Retry? retry = null)
            where T : class
        {
            T? rv = default;
            await For(async () =>
            {
                rv = await action();
                return true;
            }, retry);

            return rv ?? throw new Exception("Return value is null");
        }
    }
}
