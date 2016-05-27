using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ListBoxAssist
    {
        static ListBoxAssist()
        {
            EventManager.RegisterClassHandler(typeof (ListBox), UIElement.PreviewMouseLeftButtonDownEvent,
                new MouseButtonEventHandler(Target));
        }

        private static void Target(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            Point pt = mouseButtonEventArgs.GetPosition((UIElement)sender);
            //VisualTreeHelper.HitTest()
            //mouseButtonEventArgs.
        }

        public static readonly DependencyProperty IsQuickUnselectEnabledProperty = DependencyProperty.RegisterAttached(
            "IsQuickUnselectEnabled", typeof(bool), typeof(ListBoxAssist), new FrameworkPropertyMetadata(default(bool)));

        public static void SetIsQuickUnselectEnabled(DependencyObject element, bool value)
        {
            element.SetValue(IsQuickUnselectEnabledProperty, value);
        }

        public static bool GetIsQuickUnselectEnabled(DependencyObject element)
        {
            return (bool)element.GetValue(IsQuickUnselectEnabledProperty);
        }
    }
}
