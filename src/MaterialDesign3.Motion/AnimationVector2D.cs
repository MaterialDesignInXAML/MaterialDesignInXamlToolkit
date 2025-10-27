namespace MaterialDesignThemes.Motion;

/// <summary>
/// This class defines a 2D vector that contains two Float value fields.
/// </summary>
public sealed class AnimationVector2D : AnimationVector
{
    public AnimationVector2D()
        : this(0f, 0f)
    {
    }

    public AnimationVector2D(float v1, float v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public float V1 { get; internal set; }

    public float V2 { get; internal set; }

    internal override void Reset()
    {
        V1 = 0f;
        V2 = 0f;
    }

    internal override AnimationVector NewVector() => new AnimationVector2D(0f, 0f);

    internal override float GetValue(int index) => index switch
    {
        0 => V1,
        1 => V2,
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
        }
    }

    internal override int Size => 2;

    public override string ToString() => $"AnimationVector2D(V1 = {V1}, V2 = {V2})";

    public override bool Equals(object? obj) =>
        obj is AnimationVector2D other && other.V1.Equals(V1) && other.V2.Equals(V2);

    public override int GetHashCode() => HashCode.Combine(V1, V2);
}