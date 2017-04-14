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

        public static IEnumerable<DependencyObject> VisualBreadthFirstTraversal(this DependencyObject node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));            

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                yield return child;                
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(node); i++)
            {
                var child = VisualTreeHelper.GetChild(node, i);
                
                foreach (var descendant in child.VisualDepthFirstTraversal())
                {
                    yield return descendant;
                }
            }
        }

        public static bool IsAncestorOf(this DependencyObject parent, DependencyObject node)
        {
            return node != null && parent.VisualDepthFirstTraversal().Contains(node);
        }

        /// <summary>
        /// Returns full visual ancestry, starting at the leaf.
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = VisualTreeHelper.GetParent(leaf);
            }
        }

        public static IEnumerable<DependencyObject> GetLogicalAncestry(this DependencyObject leaf)
        {
            while (leaf != null)
            {
                yield return leaf;
                leaf = LogicalTreeHelper.GetParent(leaf);
            }
        }

        public static bool IsDescendantOf(this DependencyObject leaf, DependencyObject ancestor)
        {
            DependencyObject parent = null;
            foreach (var node in leaf.GetVisualAncestry())
            {
                if (Equals(node, ancestor))
                    return true;

                parent = node;
            }

            return parent.GetLogicalAncestry().Contains(ancestor);
        }
    }
}