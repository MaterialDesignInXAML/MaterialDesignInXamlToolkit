namespace MaterialDesignThemes.Motion;

/// <summary>
/// This class defines a 4D vector that contains four Float fields for its four dimensions.
/// </summary>
public sealed class AnimationVector4D : AnimationVector
{
    public AnimationVector4D()
        : this(0f, 0f, 0f, 0f)
    {
    }

    public AnimationVector4D(float v1, float v2, float v3, float v4)
    {
        V1 = v1;
        V2 = v2;
        V3 = v3;
        V4 = v4;
    }

    public float V1 { get; internal set; }

    public float V2 { get; internal set; }

    public float V3 { get; internal set; }

    public float V4 { get; internal set; }

    internal override void Reset()
    {
        V1 = 0f;
        V2 = 0f;
        V3 = 0f;
        V4 = 0f;
    }

    internal override AnimationVector NewVector() => new AnimationVector4D(0f, 0f, 0f, 0f);

    internal override float GetValue(int index) => index switch
    {
        0 => V1,
        1 => V2,
        2 => V3,
        3 => V4,
        _ => 0f,
    };

    internal override void SetValue(int index, float value)
    {
        switch (index)
        {
            case 0:
                V1 = value;
                break;
            case 1:
                V2 = value;
                break;
            case 2:
                V3 = value;
                break;
            case 3:
                V4 = value;
                break;
        }
    }

    internal override int Size => 4;

    public override string ToString() => $"AnimationVector4D(V1 = {V1}, V2 = {V2}, V3 = {V3}, V4 = {V4})";

    public override bool Equals(object? obj) =>
        obj is AnimationVector4D other &&
        other.V1.Equals(V1) &&
        other.V2.Equals(V2) &&
        other.V3.Equals(V3) &&
        other.V4.Equals(V4);

    public override int GetHashCode() => HashCode.Combine(V1, V2, V3, V4);
}
