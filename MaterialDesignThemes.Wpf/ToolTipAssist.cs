using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public static class ToolTipAssist
    {
        public static CustomPopupPlacementCallback CustomPopupPlacementCallback => CustomPopupPlacementCallbackImpl;

        public static CustomPopupPlacement[] CustomPopupPlacementCallbackImpl(Size popupSize, Size targetSize, Point offset)
        {
            return new []
            {
                new CustomPopupPlacement(new Point(targetSize.Width/2 - popupSize.Width/2, targetSize.Height + 14), PopupPrimaryAxis.Horizontal) 
            };
        }
    }
}
