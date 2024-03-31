using System.Globalization;
using System.Windows.Data;

namespace MaterialDesignThemes.Wpf.Converters;

public class ThicknessCloneConverter : IValueConverter
{
    public ThicknessEdges CloneEdges { get; set; } = ThicknessEdges.All;

    public double NonClonedEdgeValue { get; set; }

    public double? FixedLeft { get; set; }
    public double? FixedTop { get; set; }
    public double? FixedRight { get; set; }
    public double? FixedBottom { get; set; }

    public double AdditionalOffsetLeft { get; set; }
    public double AdditionalOffsetTop { get; set; }
    public double AdditionalOffsetRight { get; set; }
    public double AdditionalOffsetBottom { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Thickness thickness)
        {
            double left = (FixedLeft ?? (CloneEdges.HasFlag(ThicknessEdges.Left) ? thickness.Left : NonClonedEdgeValue)) + AdditionalOffsetLeft;
            double top = (FixedTop ?? (CloneEdges.HasFlag(ThicknessEdges.Top) ? thickness.Top : NonClonedEdgeValue)) + AdditionalOffsetTop;
            double right = (FixedRight ?? (CloneEdges.HasFlag(ThicknessEdges.Right) ? thickness.Right : NonClonedEdgeValue)) + AdditionalOffsetRight;
            double bottom = (FixedBottom ?? (CloneEdges.HasFlag(ThicknessEdges.Bottom) ? thickness.Bottom : NonClonedEdgeValue)) + AdditionalOffsetBottom;
            return new Thickness(left, top, right, bottom);
        }
        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

[Flags]
public enum ThicknessEdges
{
    None = 0,
    Left = 1,
    Top = 2,
    Right = 4,
    Bottom = 8,
    All = Left | Top | Right | Bottom
}
