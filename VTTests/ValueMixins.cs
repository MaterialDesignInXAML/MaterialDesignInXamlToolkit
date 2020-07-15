using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace VTTests
{
    public static class ValueMixins
    {
        [return:MaybeNull]
        public static T GetValueAs<T>(this IValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.ValueType is null)
            {
                return default;
            }

            //Special case for color
            if (typeof(T) == typeof(Color) || typeof(T) == typeof(Color?))
            {
                if (string.Equals(value.ValueType, typeof(SolidColorBrush).AssemblyQualifiedName) &&
                    value.GetValueAs<SolidColorBrush>()?.Color is T color)
                {
                    return color;
                }
            }

            var type = Type.GetType(value.ValueType);
            var converter = TypeDescriptor.GetConverter(type);
            return (T)converter.ConvertFromString(value.Value);
        }
    }
}
