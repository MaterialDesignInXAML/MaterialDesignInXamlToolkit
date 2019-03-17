using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

using WPFVisualTreeHelper= System.Windows.Media.VisualTreeHelper;

namespace MaterialDesignThemes.Wpf.Tests
{
    public static class VisualTreeHelper
    {
        public static T FindVisualChild<T>(this DependencyObject source, string name) where T : FrameworkElement
        {
            return GetAllVisualChildren(source).OfType<T>().Single(x => x.Name == name);
        }

        public static IEnumerable<T> GetVisualChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return GetVisualChildrenImplementation();

            IEnumerable<T> GetVisualChildrenImplementation()
            {
                int count = WPFVisualTreeHelper.GetChildrenCount(source);
                for (int i = 0; i < count; i++)
                {
                    if (WPFVisualTreeHelper.GetChild(source, i) is T child)
                    {
                        yield return child;
                    }
                }
            }
        }

        private static IEnumerable<DependencyObject> GetAllVisualChildren(DependencyObject source)
        {
            var stack = new Queue<DependencyObject>();
            stack.Enqueue(source);

            while (stack.Any())
            {
                DependencyObject current = stack.Dequeue();
                int childCount = WPFVisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < childCount; i++)
                {
                    var child = WPFVisualTreeHelper.GetChild(current, i);
                    yield return child;
                    stack.Enqueue(child);
                }
            }
        }
    }
}