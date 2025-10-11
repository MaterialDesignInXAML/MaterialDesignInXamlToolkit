namespace MaterialDesignThemes.Motion;

/// <summary>
/// <see cref="AnimationVector"/> class that is the base class of <see cref="AnimationVector1D"/>,
/// <see cref="AnimationVector2D"/>, <see cref="AnimationVector3D"/> and <see cref="AnimationVector4D"/>.
/// In order to animate any arbitrary type, it is required to provide a <see cref="ITwoWayConverter{T,TAnimationVector}"/>
/// that defines how to convert that arbitrary type T to an <see cref="AnimationVector"/>, and vice versa.
/// </summary>
public abstract class AnimationVector
{
    internal abstract void Reset();

    internal abstract AnimationVector NewVector();

    internal abstract float GetValue(int index);

    internal abstract void SetValue(int index, float value);

    internal abstract int Size { get; }

    public float this[int index]
    {
        get => GetValue(index);
        set => SetValue(index, value);
    }
}
