namespace MaterialDesignThemes.Wpf;

public class NumericUpDown : UpDownBase<int, IntArithmetic>
{
    public NumericUpDown()
    {
        ValueStep = 1;
    }
}

public class IntArithmetic : IArithmetic<int>
{
    public int Add(int value1, int value2) => value1 + value2;

    public int Subtract(int value1, int value2) => value1 - value2;

    public int Compare(int value1, int value2) => value1.CompareTo(value2);

    public string ConvertToString(int value) => value.ToString();

    public int MinValue() => int.MinValue;

    public int MaxValue() => int.MaxValue;

    public int Max(int value1, int value2) => Math.Max(value1, value2);

    public int Min(int value1, int value2) => Math.Min(value1, value2);

    public bool TryParse(string text, out int value) => int.TryParse(text, out value);
}


