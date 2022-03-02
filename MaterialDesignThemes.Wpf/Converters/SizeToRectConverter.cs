using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    [Obsolete("This will be removed in the 5.0 release as it is no longer used")]
    public class CardClipConverter : IMultiValueConverter
    {
        /// <summary>
        /// 1 - Content presenter render size,
        /// 2 - Clipping border padding (main control padding)
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? Convert(object?[]? values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values?.Length != 2 || values[0] is not Size || values[1] is not Thickness)
                return Binding.DoNothing;

            var size = (Size)values[0]!;
            var farPoint = new Point(
                Math.Max(0, size.Width),
                Math.Max(0, size.Height));
            var padding = (Thickness)values[1]!;
            farPoint.Offset(padding.Left + padding.Right, padding.Top + padding.Bottom);

            return new Rect(
                new Point(),
                new Point(farPoint.X, farPoint.Y));
        }

        public object?[]? ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => null;
    }
}
