using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonAssist
    {
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.RegisterAttached(
            "Minimum", typeof(double), typeof(ButtonAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetMinimum(DependencyObject element, double value)
        {
            element.SetValue(MinimumProperty, value);
        }

        public static double GetMinimum(DependencyObject element)
        {
            return (double)element.GetValue(MinimumProperty);
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached(
            "Maximum", typeof(double), typeof(ButtonAssist), new FrameworkPropertyMetadata(100.0));

        public static void SetMaximum(DependencyObject element, double value)
        {
            element.SetValue(MaximumProperty, value);
        }

        public static double GetMaximum(DependencyObject element)
        {
            return (double)element.GetValue(MaximumProperty);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", typeof(double), typeof(ButtonAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetValue(DependencyObject element, double value)
        {
            element.SetValue(ValueProperty, value);
        }

        public static double GetValue(DependencyObject element)
        {
            return (double)element.GetValue(ValueProperty);
        }
    }
}
