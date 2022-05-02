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

        private static bool IsAncestorTill(FrameworkElement? element, object ancestor, object container)
        {
            if (element is null) return false;

            FrameworkElement? parent = element;

            do
            {
                if (ReferenceEquals(parent, ancestor)) return true;
                if (ReferenceEquals(parent, container)) return false;
            } while ((parent = (parent.Parent ?? VisualTreeHelper.GetParent(parent)) as FrameworkElement) != null);

            return false;
        }

        public static Visual? FindMainTreeVisual(Visual? visual)
        {
            DependencyObject? root = null;
            DependencyObject? dependencyObject = visual;

            while (dependencyObject != null)
            {
                root = dependencyObject;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            return root as Visual;
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T? FindChild<T>(this DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T? foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (child is not T)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
    }
}
