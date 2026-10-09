#if !NET8_0_OR_GREATER
using System.Globalization;
#endif

namespace MaterialDesignThemes.Wpf;

public class DoubleUpDown
#if NET8_0_OR_GREATER
    : UpDownBase<double>
#else
    : UpDownBase<double, DoubleArithmetic>
#endif
{
    static DoubleUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleUpDown), new FrameworkPropertyMetadata(typeof(DoubleUpDown)));
    }
}

#if !NET8_0_OR_GREATER
public class DoubleArithmetic : IArithmetic<double>
{
    public double Add(double value1, double value2) => value1 + value2;

    public double Subtract(double value1, double value2) => value1 - value2;

    public int Compare(double value1, double value2) => value1.CompareTo(value2);

    public double MinValue() => double.MinValue;

    public double MaxValue() => double.MaxValue;

    public double One() => 1d;

    public double Max(double value1, double value2) => Math.Max(value1, value2);

    public double Min(double value1, double value2) => Math.Min(value1, value2);

    public bool TryParse(string text, IFormatProvider? formatProvider, out double value)
        => double.TryParse(text, NumberStyles.Number, formatProvider, out value);
}
#endif
