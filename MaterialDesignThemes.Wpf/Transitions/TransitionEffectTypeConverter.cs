using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class TransitionEffectTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(string) || typeof(TransitionEffectKind).IsAssignableFrom(sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            TransitionEffectBase? transitionEffect;

            if (value is string stringValue &&
                Enum.TryParse(stringValue, out TransitionEffectKind effectKind))
            {
                transitionEffect = new TransitionEffect(effectKind);
            }
            else
            {
                transitionEffect = value as TransitionEffectBase;
            }

            if (transitionEffect is null)
                throw new XamlParseException($"Could not parse to type {typeof(TransitionEffectKind).FullName} or {typeof(TransitionEffectBase).FullName}.");

            return transitionEffect;
        }
    }
}