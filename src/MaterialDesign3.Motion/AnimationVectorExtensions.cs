namespace MaterialDesignThemes.Motion;

internal static class AnimationVectorExtensions
{
    public static T NewInstance<T>(this T vector)
        where T : AnimationVector
    {
        if (vector is null)
        {
            throw new ArgumentNullException(nameof(vector));
        }

        return (T)vector.NewVector();
    }

    public static T Copy<T>(this T vector)
        where T : AnimationVector
    {
        if (vector is null)
        {
            throw new ArgumentNullException(nameof(vector));
        }

        var newVector = vector.NewInstance();
        for (var i = 0; i < newVector.Size; i++)
        {
            newVector[i] = vector.GetValue(i);
        }

        return newVector;
    }

    public static void CopyFrom<T>(this T vector, T source)
        where T : AnimationVector
    {
        if (vector is null)
        {
            throw new ArgumentNullException(nameof(vector));
        }

        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var size = vector.Size;
        for (var i = 0; i < size; i++)
        {
            vector[i] = source.GetValue(i);
        }
    }
}