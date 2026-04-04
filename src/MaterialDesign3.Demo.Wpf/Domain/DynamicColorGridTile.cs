using System.Windows.Media;

namespace MaterialDesign3Demo.Domain;

public sealed class DynamicColorGridTile
{
    public DynamicColorGridTile(string label, string colorHex, Brush backgroundBrush, Brush foregroundBrush, int column, int columnSpan)
    {
        Label = label;
        ColorHex = colorHex;
        BackgroundBrush = backgroundBrush;
        ForegroundBrush = foregroundBrush;
        Column = column;
        ColumnSpan = columnSpan;
    }

    public string Label { get; }

    public string ColorHex { get; }

    public Brush BackgroundBrush { get; }

    public Brush ForegroundBrush { get; }

    public int Column { get; }

    public int ColumnSpan { get; }
}
