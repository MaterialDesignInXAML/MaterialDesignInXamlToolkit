using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    
    public sealed class MathMultipleConverter : IMultiValueConverter
    {
        public MathOperation Operation { get; set; }

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            double value1, value2;
            if (Double.TryParse(value[0].ToString(), out value1) && Double.TryParse(value[1].ToString(), out value2))
            {
                switch (Operation)
                {
                    default:
                    case MathOperation.Add:
                        return value1 + value2;
                    case MathOperation.Divide:
                        return value1 / value2;
                    case MathOperation.Multiply:
                        return value1 * value2;
                    case MathOperation.Sub:
                        return value1 - value2;
                }
            }

            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}