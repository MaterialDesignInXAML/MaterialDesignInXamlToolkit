using System;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal static class TreeHelper
    {
        public static double GetVisibleWidth(FrameworkElement element, FrameworkElement parent, FlowDirection flowDirection)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            var location = element.TransformToAncestor(parent).Transform(new Point(0, 0));
            if (flowDirection != parent.FlowDirection)
                location.X -= element.ActualWidth;

            int width = (int)Math.Floor(element.ActualWidth);
            var hitTest = parent.InputHitTest(new Point(location.X + width, location.Y));

            if (IsAncestorTill(hitTest as FrameworkElement, element, parent))
            {
                return width;
            }

            //BinarySearch here
            int end = (int)Math.Floor(element.ActualWidth);
            int start = 0;

            while (start < end)
            {
                width = (end + start) / 2;
                hitTest = parent.InputHitTest(new Point(location.X + width, location.Y));

                if (IsAncestorTill(hitTest as FrameworkElement, element, parent))
                {
                    //Speed tweak
                    hitTest = parent.InputHitTest(new Point(location.X + width + 1, location.Y));

                    if (IsAncestorTill(hitTest as FrameworkElement, element, parent))
                    {
                        start = width;
                    }
                    else
                    {
                        return width;
                    }
                }
                else
                {
                    end = width;
                }
            }


            //for (int width = (int) Math.Floor(element.ActualWidth); width >= 0; width--)
            //{
            //    var hitTest = parent.InputHitTest(new Point(location.X + width, location.Y));
            //
            //    if (hitTest == null) continue;
            //    
            //    if (IsAncestorTill(hitTest as FrameworkElement, element, parent))
            //    {
            //        return width;
            //    }
            //}

            return element.ActualWidth;
        }

        private static bool IsAncestorTill(FrameworkElement element, object ancestor, object container)
        {
            if (element == null) return false;

            FrameworkElement parent = element;

            do
            {
                if (ReferenceEquals(parent, ancestor)) return true;
                if (ReferenceEquals(parent, container)) return false;
            } while ((parent = (parent.Parent ?? VisualTreeHelper.GetParent(parent)) as FrameworkElement) != null);

            return false;
        }

        public static Visual FindMainTreeVisual(Visual visual)
        {
            DependencyObject root = null;
            DependencyObject dependencyObject = visual;

            while (dependencyObject != null)
            {
                root = dependencyObject;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return root as Visual;
        }
    }
}
