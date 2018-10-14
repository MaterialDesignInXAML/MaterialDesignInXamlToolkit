using System.Windows;
using System.Windows.Controls;

namespace HexGridControl
{
    public class HexList: ListBox
    {
        static HexList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HexList), new FrameworkPropertyMetadata(typeof(HexList)));
        }

        public static readonly DependencyProperty OrientationProperty = HexGrid.OrientationProperty.AddOwner(typeof (HexList));

        public static readonly DependencyProperty RowCountProperty = HexGrid.RowCountProperty.AddOwner(typeof (HexList));

        public static readonly DependencyProperty ColumnCountProperty = HexGrid.ColumnCountProperty.AddOwner(typeof (HexList));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is HexItem);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new HexItem();
        }
    }
}
