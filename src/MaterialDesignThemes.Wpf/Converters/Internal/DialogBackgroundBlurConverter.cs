using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters.Internal;

public sealed class DialogBackgroundBlurConverter : IMultiValueConverter
{
    public static readonly DialogBackgroundBlurConverter Instance = new();
    public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values is [bool isOpen, bool applyBlurBackground, double blurRadius]
            && isOpen
            && applyBlurBackground)
        {
            return new BlurEffect() { Radius = blurRadius };
        }

        return null;
    }
    public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}
