#if !NET8_0_OR_GREATER
using System.Globalization;
#endif

namespace MaterialDesignThemes.Wpf;

public class NumericUpDown
#if NET8_0_OR_GREATER
    : UpDownBase<int>
#else
    : UpDownBase<int, IntArithmetic>
#endif
{
    static NumericUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
    }
}

#if !NET8_0_OR_GREATER
public class IntArithmetic : IArithmetic<int>
{
    public int Add(int value1, int value2) => value1 + value2;

    public int Subtract(int value1, int value2) => value1 - value2;

    public int Compare(int value1, int value2) => value1.CompareTo(value2);

    public int MinValue() => int.MinValue;

    public int MaxValue() => int.MaxValue;
    public int One() => 1;

    public int Max(int value1, int value2) => Math.Max(value1, value2);

    public int Min(int value1, int value2) => Math.Min(value1, value2);

    public bool TryParse(string text, IFormatProvider? formatProvider, out int value)
        => int.TryParse(text, NumberStyles.Integer, formatProvider, out value);
}
#endif
