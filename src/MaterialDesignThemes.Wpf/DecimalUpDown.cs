#if !NET8_0_OR_GREATER
using System.Globalization;
#endif

namespace MaterialDesignThemes.Wpf;

public class DecimalUpDown
#if NET8_0_OR_GREATER
    : UpDownBase<decimal>
#else
    : UpDownBase<decimal, DecimalArithmetic>
#endif
{
    static DecimalUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DecimalUpDown), new FrameworkPropertyMetadata(typeof(DecimalUpDown)));
    }
}

#if !NET8_0_OR_GREATER
public class DecimalArithmetic : IArithmetic<decimal>
{
    public decimal Add(decimal value1, decimal value2) => value1 + value2;

    public decimal Subtract(decimal value1, decimal value2) => value1 - value2;

    public int Compare(decimal value1, decimal value2) => value1.CompareTo(value2);

    public decimal MinValue() => decimal.MinValue;

    public decimal MaxValue() => decimal.MaxValue;

    public decimal One() => 1m;

    public decimal Max(decimal value1, decimal value2) => Math.Max(value1, value2);

    public decimal Min(decimal value1, decimal value2) => Math.Min(value1, value2);

    public bool TryParse(string text, IFormatProvider? formatProvider, out decimal value)
        => decimal.TryParse(text, NumberStyles.Number, formatProvider, out value);
}
#endif
