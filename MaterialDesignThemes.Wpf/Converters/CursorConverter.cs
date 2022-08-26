using System.Globalization;
using System.Windows.Data;
using Cursor = System.Windows.Input.Cursor;

namespace MaterialDesignThemes.Wpf.Converters;

/// <summary>
/// Value converter that uses the Cursor from the bound property if set, otherwise it returns the <see cref="FallbackCursor"/>.
/// </summary>
public sealed class CursorConverter : IValueConverter
{
    public Cursor FallbackCursor { get; set; } = Cursors.Arrow;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value as Cursor ?? FallbackCursor;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}