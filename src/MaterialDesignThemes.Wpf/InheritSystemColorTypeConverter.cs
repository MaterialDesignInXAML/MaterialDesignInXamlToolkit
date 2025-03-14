using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf;

internal sealed class InheritSystemColorTypeConverter : TypeConverter
{
    private const string Inherit = "Inherit";

    private ColorConverter ColorConverter { get; } = new();

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string) ||
           ColorConverter.CanConvertFrom(context, sourceType) ||
           base.CanConvertFrom(context, sourceType);

    public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
        => ColorConverter.CanConvertTo(context, destinationType) ||
           base.CanConvertTo(context, destinationType);

    public override object ConvertFrom(ITypeDescriptorContext? td, System.Globalization.CultureInfo? ci, object? value)
    {
        if (value is null)
        {
            throw GetConvertFromException(value);
        }

        string? s = value as string ?? throw new ArgumentNullException(nameof(value));
        
        if (string.Equals(s, Inherit, StringComparison.OrdinalIgnoreCase))
        {
            return Theme.GetSystemAccentColor() ?? default;
        }

        return ColorConverter.ConvertFrom(td, ci, s);
    }

    public override object ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is Color color &&
            color != default &&
            color == Theme.GetSystemAccentColor())
        {
            return Inherit;
        }
        return ColorConverter.ConvertTo(context, culture, value, destinationType);
    }
}
