using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MaterialDesignColors.ColorManipulation;

namespace MaterialDesignThemes.Wpf
{
    public class HexColorItem :  ButtonBase
    {
        static HexColorItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexColorItem), new FrameworkPropertyMetadata(typeof(HexColorItem)));
        }

        public Hsb Hsv { get; set; }

        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register(
            nameof(Column), typeof(int), typeof(HexColorItem));

        public int Column
        {
            get { return (int)GetValue(ColumnProperty); }
            set { SetValue(ColumnProperty, value); }
        }

        public static readonly DependencyProperty RowProperty = DependencyProperty.Register(
            nameof(Row), typeof(int), typeof(HexColorItem));

        public int Row
        {
            get { return (int)GetValue(RowProperty); }
            set { SetValue(RowProperty, value); }
        }

        public static readonly DependencyProperty ColorBrushProperty = DependencyProperty.Register(
            nameof(ColorBrush), typeof(Brush), typeof(HexColorItem));

        public Brush ColorBrush
        {
            get { return (Brush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }
    }
}
