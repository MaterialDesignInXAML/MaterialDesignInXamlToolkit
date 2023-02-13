using System.Globalization;
using System.Windows.Data;
using MaterialDesignDemo.Domain;

namespace MaterialDesignDemo;

/// <summary>
/// Interaction logic for SmartHint.xaml
/// </summary>
public partial class SmartHint : UserControl
{
    public SmartHint()
    {
        DataContext = new SmartHintViewModel();
        InitializeComponent();
    }
}

internal class CustomPaddingConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        Thickness? defaultPadding = values[0] as Thickness?;
        bool applyCustomPadding = (bool) values[1];
        Thickness customPadding = (Thickness) values[2];
        return applyCustomPadding ? customPadding : defaultPadding ?? DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
