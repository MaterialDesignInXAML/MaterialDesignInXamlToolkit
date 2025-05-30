namespace MaterialDesignThemes.Wpf.Tests;

public class EnumDataAttribute<TEnum> : MatrixAttribute<TEnum>
    where TEnum : struct, Enum
{
    public EnumDataAttribute()
#if NET6_0_OR_GREATER
        : base(Enum.GetValues<TEnum>())
#else
        : base([.. Enum.GetValues(typeof(TEnum)).OfType<TEnum>()])
#endif
    {

    }
}
