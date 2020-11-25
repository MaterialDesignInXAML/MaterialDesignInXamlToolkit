using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal static class ScaleHelper
    {
        internal static double GetTotalTransformScaleX(Visual visual)
        {
            double totalTransform = 1.0d;
            DependencyObject currentVisualTreeElement = visual;
            do
            {
                visual = (Visual)currentVisualTreeElement as Visual;
                if (visual != null)
                {
                    Transform transform = VisualTreeHelper.GetTransform(visual);
                    if ((transform != null) &&
                        (transform.Value.M12 == 0) &&
                        (transform.Value.OffsetX == 0))
                    {
                        totalTransform *= transform.Value.M11;
                    }
                }
                currentVisualTreeElement = VisualTreeHelper.GetParent(currentVisualTreeElement);
            }
            while (currentVisualTreeElement != null);

            return totalTransform;
        }

        internal static double GetTotalTransformScaleY(Visual visual)
        {
            double totalTransform = 1.0d;
            DependencyObject currentVisualTreeElement = visual;
            do
            {
                visual = currentVisualTreeElement as Visual;
                if (visual != null)
                {
                    Transform transform = VisualTreeHelper.GetTransform(visual);
                    if ((transform != null) &&
                        (transform.Value.M21 == 0) &&
                        (transform.Value.OffsetY == 0))
                    {
                        totalTransform *= transform.Value.M22;
                    }
                }
                currentVisualTreeElement = VisualTreeHelper.GetParent(currentVisualTreeElement);
            }
            while (currentVisualTreeElement != null);

            return totalTransform;
        }
    }
}
