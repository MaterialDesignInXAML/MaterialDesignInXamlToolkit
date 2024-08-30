namespace MaterialDesignThemes.Wpf;

public class DecimalUpDown : UpDownBase<decimal, DecimalArithmetic>
{
    public DecimalUpDown()
    {
        ValueStep = 1m;
    }
}

public class DecimalArithmetic : IArithmetic<decimal>
{
    public decimal Add(decimal value1, decimal value2) => value1 + value2;

    public decimal Subtract(decimal value1, decimal value2) => value1 - value2;

    public int Compare(decimal value1, decimal value2) => value1.CompareTo(value2);

    public string ConvertToString(decimal value) => value.ToString();

    public decimal MinValue() => decimal.MinValue;

    public decimal MaxValue() => decimal.MaxValue;

    public decimal Max(decimal value1, decimal value2) => Math.Max(value1, value2);

    public decimal Min(decimal value1, decimal value2) => Math.Min(value1, value2);

    public bool TryParse(string text, out decimal value) => decimal.TryParse(text, out value);
}
