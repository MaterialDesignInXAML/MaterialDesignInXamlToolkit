using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class ClockLineConverter : MarkupExtension, IValueConverter
{
    public static readonly ClockLineConverter HoursInstance = new() { DisplayMode = ClockDisplayMode.Hours };
    public static readonly ClockLineConverter MinutesInstance = new() { DisplayMode = ClockDisplayMode.Minutes };
    public static readonly ClockLineConverter SecondsInstance = new() { DisplayMode = ClockDisplayMode.Seconds };

    public ClockDisplayMode DisplayMode { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var time = (DateTime)value;

        return DisplayMode == ClockDisplayMode.Hours
            ? ((time.Hour > 13) ? time.Hour - 12 : time.Hour) * (360 / 12)
            : DisplayMode == ClockDisplayMode.Minutes
                ? (time.Minute == 0 ? 60 : time.Minute) * (360 / 60)
                : (time.Second == 0 ? 60 : time.Second) * (360 / 60);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;

    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;
}
