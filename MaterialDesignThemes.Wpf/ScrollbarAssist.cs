using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ScrollBarAssist
    {

        public static readonly DependencyProperty ArrowsVisibilityProperty =
            DependencyProperty.RegisterAttached("ArrowsVisibility", typeof(Visibility), typeof(ScrollBarAssist), new PropertyMetadata(Visibility.Visible));

        public static void SetArrowsVisibility(DependencyObject element, Visibility value)
        {
            element.SetValue(ArrowsVisibilityProperty, value);
        }

        public static Visibility GetArrowsVisibility(DependencyObject element)
        {
            return (Visibility)element.GetValue(ArrowsVisibilityProperty);
        }
    }
}
