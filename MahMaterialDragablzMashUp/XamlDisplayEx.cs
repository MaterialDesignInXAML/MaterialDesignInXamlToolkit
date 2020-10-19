using System.Windows;
using System.Windows.Controls;

namespace MahAppsDragablzDemo
{
    public static class XamlDisplayEx
    {
        public static readonly DependencyProperty ButtonDockProperty = DependencyProperty.RegisterAttached(
            "ButtonDock", typeof(Dock), typeof(XamlDisplayEx), new PropertyMetadata(default(Dock)));

        public static void SetButtonDock(DependencyObject element, Dock value)
        {
            element.SetValue(ButtonDockProperty, value);
        }

        public static Dock GetButtonDock(DependencyObject element)
        {
            return (Dock)element.GetValue(ButtonDockProperty);
        }
    }
}