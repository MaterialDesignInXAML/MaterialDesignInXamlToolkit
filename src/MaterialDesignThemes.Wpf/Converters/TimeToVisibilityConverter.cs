using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

[Obsolete("This class is obsolete and will be removed in a future version.")]
public class TimeToVisibilityConverter : MarkupExtension, IValueConverter
{
    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var time = (DateTime)value;

        bool isPm = ((time.Hour >= 13) || (time.Hour == 0));

        return isPm ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}
