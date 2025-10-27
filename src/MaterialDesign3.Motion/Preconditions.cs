namespace MaterialDesignThemes.Motion;

internal static class Preconditions
{
    internal static void ThrowIllegalArgumentException(string message) =>
        throw new ArgumentException(message);

    internal static void RequirePrecondition(bool value, Func<string> lazyMessage)
    {
        if (value)
        {
            return;
        }

        ThrowIllegalArgumentException(lazyMessage());
    }

    internal static void ThrowIllegalStateException(string message) =>
        throw new InvalidOperationException(message);

    internal static void ThrowIllegalStateExceptionForNullCheck(string message) =>
        throw new InvalidOperationException(message);

    internal static T CheckPreconditionNotNull<T>(T? value, Func<string> lazyMessage)
        where T : class
    {
        if (value is null)
        {
            ThrowIllegalStateExceptionForNullCheck(lazyMessage());
        }

        return value!;
    }

    internal static void CheckPrecondition(bool value, Func<string> lazyMessage)
    {
        if (value)
        {
            return;
        }

        ThrowIllegalStateException(lazyMessage());
    }
}
