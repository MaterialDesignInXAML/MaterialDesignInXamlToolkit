using System;
using System.Collections.Generic;
using System.Windows;

namespace VTTests
{
    internal static class VTTest
    {
        public static string GetId(DependencyObject obj) => (string)obj.GetValue(IdProperty);

        public static void SetId(DependencyObject obj, string value) => obj.SetValue(IdProperty, value);

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.RegisterAttached("Id", typeof(string), typeof(VTTest), new PropertyMetadata(""));

        internal static string GetOrSetId(DependencyObject obj, IDictionary<string, WeakReference<DependencyObject>> cache)
        {
            string id = GetId(obj);
            if (string.IsNullOrWhiteSpace(id))
            {
                SetId(obj, id = Guid.NewGuid().ToString());
            }
            lock(cache)
            {
                cache[id] = new WeakReference<DependencyObject>(obj);
            }
            return id;
        }
    }
}
