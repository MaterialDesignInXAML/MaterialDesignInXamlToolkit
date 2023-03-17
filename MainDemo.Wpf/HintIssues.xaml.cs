using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for HintIssues.xaml
/// </summary>
public partial class HintIssues : UserControl
{
    public HintIssues()
    {
        InitializeComponent();
    }
}

internal class FixedWidthConverter : IValueConverter
{
    public double FixedWidth { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? FixedWidth : double.NaN;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

internal class BoolToParameterConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is true ? parameter : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
