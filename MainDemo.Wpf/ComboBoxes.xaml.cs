using MaterialDesignDemo.Domain;
using Screen = System.Windows.Forms.Screen;
using DrawingPoint = System.Drawing.Point;

namespace MaterialDesignDemo;

public partial class ComboBoxes
{
    private const double DropShadowHeight = 6;    // This does not account for DPI scaling!

    public static CustomPopupPlacementCallback Rotate90DegreesClockWiseCallback { get; } = (popupSize, targetSize, offset) =>
    {
        // ComboBox is rotated 90 degrees clockwise (ie. Left=Up, Right=Down)
        var comboBox = VisualTreeUtil.GetElementUnderMouse<ComboBox>();
        var comboBoxLocation = comboBox.PointToScreen(new Point(0, 0));
        Screen screen = Screen.FromPoint(new DrawingPoint((int)comboBoxLocation.X, (int)comboBoxLocation.Y));
        int comboBoxOffsetX = (int)(comboBoxLocation.X - screen.Bounds.X) % screen.Bounds.Width;
        double y = offset.Y - DropShadowHeight;
        double x = offset.X;
        double rotatedComboBoxHeight = targetSize.Width;
        if (comboBoxOffsetX + x > popupSize.Width + rotatedComboBoxHeight)
        {
            x -= popupSize.Width + rotatedComboBoxHeight;
        }
        return new[] { new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.Horizontal) };
    };

    public static CustomPopupPlacementCallback Rotate90DegreesCounterClockWiseCallback { get; } = (popupSize, targetSize, offset) =>
    {
        // ComboBox is rotated 90 degrees counter-clockwise (ie. Left=Down, Right=Up)
        var comboBox = VisualTreeUtil.GetElementUnderMouse<ComboBox>();
        var comboBoxLocation = comboBox.PointToScreen(new Point(0, 0));
        Screen screen = Screen.FromPoint(new DrawingPoint((int)comboBoxLocation.X, (int)comboBoxLocation.Y));
        int comboBoxOffsetX = (int)(comboBoxLocation.X - screen.Bounds.X) % screen.Bounds.Width;
        double y = offset.Y - popupSize.Height + DropShadowHeight;
        double x = offset.X;
        double rotatedComboBoxHeight = targetSize.Width;
        if (comboBoxOffsetX + x + rotatedComboBoxHeight + popupSize.Width > screen.Bounds.Width)
        {
            x -= popupSize.Width;
        }
        else
        {
            x += rotatedComboBoxHeight;
        }
        return new[] { new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.Horizontal) };
    };

    public ComboBoxes()
    {
        InitializeComponent();
        DataContext = new ComboBoxesViewModel();
    }

    private void ClearFilledComboBox_Click(object sender, System.Windows.RoutedEventArgs e)
        => FilledComboBox.SelectedItem = null;

    private void ClearOutlinedComboBox_Click(object sender, System.Windows.RoutedEventArgs e)
        => OutlinedComboBox.SelectedItem = null;
}
