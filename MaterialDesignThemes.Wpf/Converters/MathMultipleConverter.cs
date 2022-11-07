﻿using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters
{
    public sealed class MathMultipleConverter : IMultiValueConverter
    {
        public MathOperation Operation { get; set; }

        public object? Convert(object?[]? value, Type? targetType, object? parameter, CultureInfo? culture)
        {
            if (value is null || value.Length < 2 || value[0] is null || value[1] is null) return Binding.DoNothing;

            if (!double.TryParse(value[0]!.ToString(), out double value1) || !double.TryParse(value[1]!.ToString(), out double value2))
                return 0;

            switch (Operation)
            {
                default:
                    // (case MathOperation.Add:)
                    return value1 + value2;
                case MathOperation.Divide:
                    return value1 / value2;
                case MathOperation.Multiply:
                    return value1 * value2;
                case MathOperation.Subtract:
                    return value1 - value2;
                case MathOperation.Pow:
                    return Math.Pow(value1, value2);
            }

        }

        public object?[]? ConvertBack(object? value, Type[]? targetTypes, object? parameter, CultureInfo? culture)
            => throw new NotImplementedException();
    }
}
