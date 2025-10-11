namespace MaterialDesignThemes.Motion;

public static class AnimationVectorFactory
{
    public static AnimationVector1D Create(float v1) => new(v1);

    public static AnimationVector2D Create(float v1, float v2) => new(v1, v2);

    public static AnimationVector3D Create(float v1, float v2, float v3) => new(v1, v2, v3);

    public static AnimationVector4D Create(float v1, float v2, float v3, float v4) => new(v1, v2, v3, v4);
}