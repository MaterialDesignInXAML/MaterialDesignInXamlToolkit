namespace MaterialDesignThemes.Motion;

/// <summary>
/// This class defines a 1D vector. It contains only one Float value that is initialized in the constructor.
/// </summary>
public sealed class AnimationVector1D : AnimationVector
{
    public AnimationVector1D()
        : this(0f)
    {
    }

    public AnimationVector1D(float value)
    {
        Value = value;
    }

    public float Value { get; internal set; }

    internal override void Reset() => Value = 0f;

    internal override AnimationVector NewVector() => new AnimationVector1D(0f);

    internal override float GetValue(int index) => index == 0 ? Value : 0f;

    internal override void SetValue(int index, float value)
    {
        if (index == 0)
        {
            Value = value;
        }
    }

    internal override int Size => 1;

    public override string ToString() => $"AnimationVector1D(Value = {Value})";

    public override bool Equals(object? obj) =>
        obj is AnimationVector1D other && other.Value.Equals(Value);

    public override int GetHashCode() => Value.GetHashCode();
}