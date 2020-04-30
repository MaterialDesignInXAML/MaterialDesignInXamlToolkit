using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// this is a simple utility to add and remove a single adorner to an element since there is no built-in way to do that in xaml.
    /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/adorners-overview">see here</a>
    /// </summary>
    public static class AdornerEx
    {
        public static void AddAdorner<T>(this UIElement element, T adorner) where T : Adorner
        {
            AdornerLayer.GetAdornerLayer(element)?.Add(adorner);
        }

        public static void RemoveAdorner<T>(this UIElement element) where T : Adorner
        {
            var layer = AdornerLayer.GetAdornerLayer(element);
            var adorners = layer?.GetAdorners(element);
            if (adorners == null) return;

            foreach (var adorner in adorners.Where(a => a is T))
                layer.Remove(adorner);
        }
    }
}