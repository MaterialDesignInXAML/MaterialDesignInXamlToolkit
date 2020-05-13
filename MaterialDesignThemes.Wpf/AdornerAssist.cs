using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// This is a simple utility to add and remove a single adorner to an element
    /// since there is no built-in way to do that in xaml.
    /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/adorners-overview">See here</a>
    /// </summary>
    public static class AdornerAssist
    {
        public static void AddAdornerOfType<T>(this UIElement element, T adorner) where T : Adorner
        {
            if (adorner is null)
            {
                throw new ArgumentNullException(nameof(adorner));
            }
            
            var adornerLayer = AdornerLayer.GetAdornerLayer(element)
                ?? throw new InvalidOperationException("This element has no adorner layer.");
            
            adornerLayer.Add(adorner);
        }

        public static void RemoveAdornersOfType<T>(this UIElement element) where T : Adorner
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(element);
            var adorners = adornerLayer?.GetAdorners(element);

            if (adorners is null)
            {
                return;
            }

            foreach (var adorner in adorners.OfType<T>())
            {
                adornerLayer.Remove(adorner);
            }
        }
    }
}