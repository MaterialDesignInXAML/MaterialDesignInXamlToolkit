using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace MaterialDesignThemes.Wpf
{
    /// <summary>
    /// this is a simple utility to "bind" a single adorner to an element since there is no built-in way to do that in xaml.
    /// <a href="https://docs.microsoft.com/en-us/dotnet/framework/wpf/controls/adorners-overview">see here</a>
    /// </summary>
    public static class AdornerAssist
    {
        public static readonly DependencyProperty AdornerTypeProperty = DependencyProperty.RegisterAttached(
            "AdornerType", typeof(Type), typeof(AdornerAssist), new PropertyMetadata(default(Type)));

        public static void SetAdornerType(DependencyObject element, Type value) => element.SetValue(AdornerTypeProperty, value);
        public static Type GetAdornerType(DependencyObject element) => (Type) element.GetValue(AdornerTypeProperty);

        public static readonly DependencyProperty ShowAdornerProperty = DependencyProperty.RegisterAttached(
            "ShowAdorner", typeof(bool), typeof(AdornerAssist), new PropertyMetadata(ShowAdornerChanged));

        public static void SetShowAdorner(DependencyObject element, bool value) => element.SetValue(ShowAdornerProperty, value);
        public static bool GetShowAdorner(DependencyObject element) => (bool) element.GetValue(ShowAdornerProperty);

        private static void ShowAdornerChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            var adornerType = GetAdornerType(element)
                              ?? throw new InvalidOperationException("AdornerAssist.AdornerType is required");

            var uiElement = (UIElement) element;

            var layer = AdornerLayer.GetAdornerLayer(uiElement)
                        ?? throw new InvalidOperationException("Element has no adorner layer");

            if (args.OldValue is bool oldValue && oldValue)
            {
                var adorners = layer.GetAdorners(uiElement);
                if (adorners != null)
                    foreach (var adorner in adorners.Where(a => adornerType.IsInstanceOfType(a)))
                        layer.Remove(adorner);
            }

            if (args.NewValue is bool newValue && newValue)
                layer.Add((Adorner) Activator.CreateInstance(adornerType, element));
        }
    }
}