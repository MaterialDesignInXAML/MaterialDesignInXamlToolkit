using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public class TabControlNavButtonVisibilityConverter : IValueConverter
{
    [TypeConverter(typeof(NavigationPanelPlacementTypeConverter))]
    public NavigationPanelPlacement[] VisiblePlacements { get; set; } = [];

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is NavigationPanelPlacement placement)
        {
            return VisiblePlacements.Contains(placement) ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Visible;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    class NavigationPanelPlacementTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is string s)
            {
                return s.Split([',', ' '], StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Enum.Parse(typeof(NavigationPanelPlacement), x.Trim()))
                        .Cast<NavigationPanelPlacement>()
                        .ToArray();
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
