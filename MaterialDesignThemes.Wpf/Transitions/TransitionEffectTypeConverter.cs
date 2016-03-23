using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Markup;

namespace MaterialDesignThemes.Wpf.Transitions
{
    public class TransitionEffectTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || typeof(TransitionEffectKind).IsAssignableFrom(sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            TransitionEffectBase transitionEffect = null;

            var stringValue = value as string;
            if (stringValue != null)
            {
                TransitionEffectKind effectKind;
                if (Enum.TryParse(stringValue, out effectKind))
                    transitionEffect = new TransitionEffect(effectKind);
            }
            else
                transitionEffect = value as TransitionEffectBase;

            if (transitionEffect == null)
                throw new XamlParseException($"Could not parse to type {typeof (TransitionEffectKind).FullName} or {typeof (TransitionEffectBase).FullName}.");

            return transitionEffect;
        }
    }
}