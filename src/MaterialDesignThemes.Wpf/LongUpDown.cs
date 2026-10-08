#if !NET8_0_OR_GREATER
using System.Globalization;
#endif

namespace MaterialDesignThemes.Wpf;

public class LongUpDown
#if NET8_0_OR_GREATER
    : UpDownBase<long>
#else
    : UpDownBase<long, LongArithmetic>
#endif
{
    static LongUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(LongUpDown), new FrameworkPropertyMetadata(typeof(LongUpDown)));
    }
}

#if !NET8_0_OR_GREATER
public class LongArithmetic : IArithmetic<long>
{
    public long Add(long value1, long value2) => value1 + value2;

    public long Subtract(long value1, long value2) => value1 - value2;

    public int Compare(long value1, long value2) => value1.CompareTo(value2);

    public long MinValue() => long.MinValue;

    public long MaxValue() => long.MaxValue;

    public long One() => 1L;

    public long Max(long value1, long value2) => Math.Max(value1, value2);

    public long Min(long value1, long value2) => Math.Min(value1, value2);

    public bool TryParse(string text, IFormatProvider? formatProvider, out long value)
        => long.TryParse(text, NumberStyles.Integer, formatProvider, out value);
}
#endif
