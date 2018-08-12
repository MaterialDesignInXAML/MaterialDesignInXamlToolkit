using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonProgressAssist
    {
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.RegisterAttached(
            "Minimum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetMinimum(DependencyObject element, double value)
        {
            element.SetValue(MinimumProperty, value);
        }

        public static double GetMinimum(DependencyObject element)
        {
            return (double)element.GetValue(MinimumProperty);
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached(
            "Maximum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(100.0));

        public static void SetMaximum(DependencyObject element, double value)
        {
            element.SetValue(MaximumProperty, value);
        }

        public static double GetMaximum(DependencyObject element)
        {
            return (double)element.GetValue(MaximumProperty);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetValue(DependencyObject element, double value)
        {
            element.SetValue(ValueProperty, value);
        }

        public static double GetValue(DependencyObject element)
        {
            return (double)element.GetValue(ValueProperty);
        }

        public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.RegisterAttached(
            "IsIndeterminate", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));

        public static void SetIsIndeterminate(DependencyObject element, bool isIndeterminate)
        {
            element.SetValue(IsIndeterminateProperty, isIndeterminate);
        }

        public static bool GetIsIndeterminate(DependencyObject element)
        {
            return (bool)element.GetValue(IndicatorForegroundProperty);
        }

        public static readonly DependencyProperty IndicatorForegroundProperty = DependencyProperty.RegisterAttached(
            "IndicatorForeground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static void SetIndicatorForeground(DependencyObject element, Brush indicatorForeground)
        {
            element.SetValue(IndicatorForegroundProperty, indicatorForeground);
        }

        public static Brush GetIndicatorForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(IndicatorForegroundProperty);
        }

        public static readonly DependencyProperty IndicatorBackgroundProperty = DependencyProperty.RegisterAttached(
            "IndicatorBackground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static void SetIndicatorBackground(DependencyObject element, Brush indicatorBackground)
        {
            element.SetValue(IndicatorBackgroundProperty, indicatorBackground);
        }

        public static Brush GetIndicatorBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(IndicatorForegroundProperty);
        }

        public static readonly DependencyProperty IsIndicatorVisibleProperty = DependencyProperty.RegisterAttached(
            "IsIndicatorVisible", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));

        public static void SetIsIndicatorVisible(DependencyObject element, bool isIndicatorVisible)
        {
            element.SetValue(IsIndicatorVisibleProperty, isIndicatorVisible);
        }

        public static bool GetIsIndicatorVisible(DependencyObject element)
        {
            return (bool)element.GetValue(IsIndicatorVisibleProperty);
        }

        public static readonly DependencyProperty OpacityProperty = DependencyProperty.RegisterAttached(
            "Opacity", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

        public static void SetOpacity(DependencyObject element, double opacity)
        {
            element.SetValue(OpacityProperty, opacity);
        }

        public static double GetOpacity(DependencyObject element)
        {
            return (double)element.GetValue(OpacityProperty);
        }
    }
}
