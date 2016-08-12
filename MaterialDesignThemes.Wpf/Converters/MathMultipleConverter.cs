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
    public sealed class MathMultipleConverter : IMultiValueConverter
    {
        public MathOperation Operation { get; set; }

        /// <summary>
        /// if true, null arguments will be treated as 0;
        /// default: false
        /// </summary>
        public bool TreatNullAsZero { get; set; }

        public MathMultipleConverter()
        {
            TreatNullAsZero = false;
        }

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length < 2)
            {
                if (TreatNullAsZero)
                {
                    return 0;
                }
                else
                {
                    return Binding.DoNothing;
                }
            }

            // null values caused a NullReferenceException because the ToString() call in Double.TryParse(value[0].ToString(), out value1)
            //     if you do not assign null values in your code, this situation might still occur at design time (see issue #462)
            // 
            //     -> do nothing as default in case of a null value
            if (!TreatNullAsZero && (value[0] == null || value[1] == null))
            {
                return Binding.DoNothing;
            }

            // a value might be null here, but it will be treated as 0
            double value1 = 0.0;
            double value2 = 0.0;

            // treat null as 0
            //     -> set true to assume, that the value is OK (invalid number values will set it to false at parse time)
            bool value1Ok = true;
            bool value2Ok = true;

            if (value[0] != null)
            {
                value1Ok = double.TryParse(value[0].ToString(), out value1);
            }

            if (value[1] != null)
            {
                value2Ok = double.TryParse(value[1].ToString(), out value2);
            }

            if (value1Ok && value2Ok)
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
                    case MathOperation.Subtract:
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