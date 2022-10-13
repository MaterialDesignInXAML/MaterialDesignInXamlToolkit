using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal class ComboBoxClearButtonMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var padding = (Thickness)values[0];
            var borderThickness = (Thickness)values[1];
            var floatingHintTopOffset = ((Thickness)values[2]).Top;
            return new Thickness(
                borderThickness.Left,
                borderThickness.Top + padding.Top + floatingHintTopOffset,
                borderThickness.Right + padding.Right + Constants.ComboBoxArrowSize + Constants.TextBoxInnerButtonSpacing,
                borderThickness.Bottom + padding.Bottom);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
