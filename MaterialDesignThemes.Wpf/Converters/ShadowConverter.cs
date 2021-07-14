using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ShadowConverter : IValueConverter
    {
        public static readonly ShadowConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ShadowDepth depth)
            {
                return Clone(Convert(depth));
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public static DropShadowEffect? Convert(ShadowDepth shadowDepth)
            => ShadowInfo.GetDropShadow(shadowDepth);

        private static DropShadowEffect? Clone(DropShadowEffect? dropShadowEffect)
        {
            if (dropShadowEffect is null) return null;
            return new DropShadowEffect()
            {
                BlurRadius = dropShadowEffect.BlurRadius,
                Color = dropShadowEffect.Color,
                Direction = dropShadowEffect.Direction,
                Opacity = dropShadowEffect.Opacity,
                RenderingBias = dropShadowEffect.RenderingBias,
                ShadowDepth = dropShadowEffect.ShadowDepth
            };
        }
    }
}
