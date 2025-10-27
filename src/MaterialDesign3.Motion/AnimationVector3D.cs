namespace MaterialDesignThemes.Motion;

/// <summary>
/// This class defines a 3D vector that contains three Float value fields for the three dimensions.
/// </summary>
public sealed class AnimationVector3D : AnimationVector
{
    public AnimationVector3D()
        : this(0f, 0f, 0f)
    {
    }

    public AnimationVector3D(float v1, float v2, float v3)
    {
        V1 = v1;
        V2 = v2;
        V3 = v3;
    }

    public float V1 { get; internal set; }

    public float V2 { get; internal set; }

    public float V3 { get; internal set; }

    internal override void Reset()
    {
        V1 = 0f;
        V2 = 0f;
        V3 = 0f;
    }

    internal override AnimationVector NewVector() => new AnimationVector3D(0f, 0f, 0f);

    internal override float GetValue(int index) => index switch
    {
        0 => V1,
        1 => V2,
        2 => V3,
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
        }
    }

    internal override int Size => 3;

    public override string ToString() => $"AnimationVector3D(V1 = {V1}, V2 = {V2}, V3 = {V3})";

    public override bool Equals(object? obj) =>
        obj is AnimationVector3D other &&
        other.V1.Equals(V1) &&
        other.V2.Equals(V2) &&
        other.V3.Equals(V3);

    public override int GetHashCode() => HashCode.Combine(V1, V2, V3);
}