using System;

namespace MaterialDesign3Demo.Domain;

public sealed class EnumOption<TEnum>
    where TEnum : struct, Enum
{
    public EnumOption(TEnum value, string displayName)
    {
        Value = value;
        DisplayName = displayName;
    }

    public TEnum Value { get; }

    public string DisplayName { get; }
}
