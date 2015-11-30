using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal static class Extensions
    {
        public static IEnumerable<DependencyObject> VisualDepthFirstTraversal(this DependencyObject node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));

            yield return node;

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                foreach (var descendant in child.VisualDepthFirstTraversal())
                {
                    yield return descendant;
                }
            }
        }

        public static bool IsDescendant(this DependencyObject parent, DependencyObject node)
        {
            return node != null && parent.VisualDepthFirstTraversal().Contains(node);
        }
    }
}