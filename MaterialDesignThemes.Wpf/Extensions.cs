using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace MaterialDesignThemes.Wpf
{
    internal static class Extensions
    {
        public static IEnumerable<DependencyObject> VisualDepthFirstTraversal(this DependencyObject node)
        {
            if (node is null) throw new ArgumentNullException(nameof(node));

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
            if (node is null) throw new ArgumentNullException(nameof(node));

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

        public static bool IsAncestorOf(this DependencyObject parent, DependencyObject? node)
            => node != null && parent.VisualDepthFirstTraversal().Contains(node);

        /// <summary>
        /// Returns full visual ancestry, starting at the leaf.
        /// <para>If element is not of <see cref="Visual"/> or <see cref="Visual3D"/> the
        /// logical ancestry is used.</para>
        /// </summary>
        /// <param name="leaf"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> GetVisualAncestry(this DependencyObject? leaf)
        {
            while (leaf is not null)
            {
                yield return leaf;
                leaf = leaf is Visual || leaf is Visual3D
                    ? VisualTreeHelper.GetParent(leaf)
                    : LogicalTreeHelper.GetParent(leaf);
            }
        }

        public static IEnumerable<DependencyObject?> GetLogicalAncestry(this DependencyObject leaf)
        {
            while (leaf is not null)
            {
                yield return leaf;
                leaf = LogicalTreeHelper.GetParent(leaf);
            }
        }

        public static bool IsDescendantOf(this DependencyObject? leaf, DependencyObject? ancestor)
        {
            DependencyObject? parent = null;
            foreach (var node in leaf.GetVisualAncestry())
            {
                if (Equals(node, ancestor))
                    return true;

                parent = node;
            }

            return parent?.GetLogicalAncestry().Contains(ancestor) == true;
        }
    }
}