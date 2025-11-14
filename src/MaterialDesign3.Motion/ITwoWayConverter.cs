namespace MaterialDesignThemes.Motion;

/// <summary>
/// <see cref="ITwoWayConverter{T,TAnimationVector}"/> class contains the definition on how to convert from an arbitrary type T to a
/// <see cref="AnimationVector"/>, and convert the <see cref="AnimationVector"/> back to the type T. This allows animations
/// to run on any type of objects, e.g. position, rectangle, color, etc.
/// </summary>
public interface ITwoWayConverter<T, TAnimationVector>
    where TAnimationVector : AnimationVector
{
    Func<T, TAnimationVector> ConvertToVector { get; }

    Func<TAnimationVector, T> ConvertFromVector { get; }
}
