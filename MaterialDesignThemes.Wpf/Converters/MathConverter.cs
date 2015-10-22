using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public enum MathOperation
    {
        Add,
        Sub,
        Multiply,
        Divide
    }

    public sealed class MathConverter : IValueConverter
    {
        public MathOperation Operation { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                switch (Operation)
                {
                    default:
                    case MathOperation.Add:
                        return (int)value + System.Convert.ToInt32(parameter);
                    case MathOperation.Divide:
                        return (int)value / System.Convert.ToInt32(parameter);
                    case MathOperation.Multiply:
                        return (int)value * System.Convert.ToInt32(parameter);
                    case MathOperation.Sub:
                        return (int)value + System.Convert.ToInt32(parameter);
                }
            }

            if (value is double)
            {
                switch (Operation)
                {
                    default:
                    case MathOperation.Add:
                        return (double)value + System.Convert.ToDouble(parameter);
                    case MathOperation.Divide:
                        return (double)value / System.Convert.ToDouble(parameter);
                    case MathOperation.Multiply:
                        return (double)value * System.Convert.ToDouble(parameter);
                    case MathOperation.Sub:
                        return (double)value + System.Convert.ToDouble(parameter);
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
