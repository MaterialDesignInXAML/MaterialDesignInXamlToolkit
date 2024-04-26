using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

[Obsolete("This class is obsolete and will be removed in a future version. Please use the more generic-named OutlinedStyleActiveBorderMarginCompensationConverter instead.")]
public class OutlinedDateTimePickerActiveBorderThicknessConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length == 2
            && values[0] is Thickness baseThickness
            && values[1] is Thickness thicknessToSubtract)
        {
            var thickness = new Thickness(baseThickness.Left - thicknessToSubtract.Left,
                baseThickness.Top - thicknessToSubtract.Top,
                baseThickness.Right - thicknessToSubtract.Right,
                baseThickness.Bottom - thicknessToSubtract.Bottom);
            return thickness;
        }
        return default(Thickness);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
