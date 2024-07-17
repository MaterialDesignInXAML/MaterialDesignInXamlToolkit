using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignDemo.Shared.Converters;

public sealed class BooleanToDoubleConverter : MarkupExtension, IValueConverter
{
    public double TrueValue { get; set; }
    public double FalseValue { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is true ? TrueValue : FalseValue;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();    
}
