using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public static class CustomPopupPlacementCallbackHelper
    {
        public static readonly CustomPopupPlacementCallback LargePopupCallback;

        static CustomPopupPlacementCallbackHelper()
        {
            LargePopupCallback =
                (size, targetSize, offset) => new[] { new CustomPopupPlacement(new Point(), PopupPrimaryAxis.Horizontal) };
        }
    }
}
