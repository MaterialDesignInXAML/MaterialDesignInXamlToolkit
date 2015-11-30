using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    internal static class DependencyObjectExtensions
    {
        public static IEnumerable<DependencyObject> GetVisualAncestory(this DependencyObject value)
        {
            while (value != null)
            {
                yield return value;
                value = VisualTreeHelper.GetParent(value);
            }
        } 
    }
}