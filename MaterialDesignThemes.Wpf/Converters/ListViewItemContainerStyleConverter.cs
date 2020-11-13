using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    /// <summary>
    /// Helps coerce the correct item container style for a <see cref="ListView"/>, according to whether the list is displaying in standard mode, or using a <see cref="ListView.View"/>, such as a <see cref="GridView"/>.
    /// </summary>
    public class ListViewGridViewConverter : IValueConverter
    {
        /// <summary>
        /// Item container style to use when <see cref="ListView.View"/> is <c>null</c>.
        /// </summary>
        public object? DefaultValue { get; set; }

        /// <summary>
        /// Item container style to use when <see cref="ListView.View"/> is not <c>null</c>, typically when a <see cref="GridView"/> is applied.
        /// </summary>
        public object? ViewValue { get; set; }

        /// <summary>
        /// Returns the item container <see cref="Style"/> to use for a <see cref="ListView"/>.
        /// </summary>
        /// <param name="value">Should be a <see cref="ListView"/> or <see cref="ViewBase"/> instance.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ListView listView)
            {
                return listView.View != null ? ViewValue : DefaultValue;
            }

            return value is ViewBase ? ViewValue : DefaultValue;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => Binding.DoNothing;
    }
}
