using System;
using System.ComponentModel;
using System.Globalization;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class SnackbarMessageTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var s = value as string;
            if (s != null)
            {
                return new SnackbarMessage
                {
                    Content = s
                };
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
