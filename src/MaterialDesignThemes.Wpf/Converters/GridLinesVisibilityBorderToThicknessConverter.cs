using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class GridLinesVisibilityBorderToThicknessConverter : IValueConverter
{
    public static readonly GridLinesVisibilityBorderToThicknessConverter Instance = new();

    private const double GridLinesThickness = 1;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not DataGridGridLinesVisibility visibility)
            return Binding.DoNothing;

        double thickness = parameter as double? ?? GridLinesThickness;

        return visibility switch
        {
            DataGridGridLinesVisibility.All => new Thickness(0, 0, thickness, thickness),
            DataGridGridLinesVisibility.Horizontal => new Thickness(0, 0, 0, thickness),
            DataGridGridLinesVisibility.Vertical => new Thickness(0, 0, thickness, 0),
            DataGridGridLinesVisibility.None => new Thickness(0),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
