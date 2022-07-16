using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    public class ShadowConverter : IValueConverter
    {
        public static readonly ShadowConverter Instance = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => value switch
            {
                Elevation elevation => Clone(Convert(elevation)),
#pragma warning disable CS0618
                ShadowDepth depth => Clone(Convert(ShadowAssist.GetElevation(depth))),
#pragma warning restore CS0618
                _ => null
            };

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();

        public static DropShadowEffect? Convert(Elevation elevation) => ElevationAssist.GetDropShadow(elevation);

        private static DropShadowEffect? Clone(DropShadowEffect? dropShadowEffect)
        {
            if (dropShadowEffect is null) return null;
            return new DropShadowEffect
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
