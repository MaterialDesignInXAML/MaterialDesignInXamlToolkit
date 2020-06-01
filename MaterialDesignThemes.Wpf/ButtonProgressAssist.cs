using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class ButtonProgressAssist
    {
        private const double DefaultMaximum = 100.0;

        #region AttachedProperty : MinimumProperty
        public static readonly DependencyProperty MinimumProperty
            = DependencyProperty.RegisterAttached("Minimum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

        public static double GetMinimum(ButtonBase element) => (double)element.GetValue(MinimumProperty);
        public static void SetMinimum(ButtonBase element, double value) => element.SetValue(MinimumProperty, value);
        #endregion

        #region AttachedProperty : MaximumProperty
        public static readonly DependencyProperty MaximumProperty
            = DependencyProperty.RegisterAttached("Maximum", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(DefaultMaximum));

        public static double GetMaximum(ButtonBase element) => (double)element.GetValue(MaximumProperty);
        public static void SetMaximum(ButtonBase element, double value) => element.SetValue(MaximumProperty, value);
        #endregion

        #region AttachedProperty : ValueProperty
        public static readonly DependencyProperty ValueProperty
            = DependencyProperty.RegisterAttached("Value", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));
        
        public static double GetValue(ButtonBase element) => (double)element.GetValue(ValueProperty);
        public static void SetValue(ButtonBase element, double value) => element.SetValue(ValueProperty, value);
        #endregion

        #region AttachedProperty : IsIndeterminate
        public static readonly DependencyProperty IsIndeterminateProperty
            = DependencyProperty.RegisterAttached("IsIndeterminate", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));

        public static bool GetIsIndeterminate(ButtonBase element) => (bool)element.GetValue(IsIndeterminateProperty);
        public static void SetIsIndeterminate(ButtonBase element, bool isIndeterminate) => element.SetValue(IsIndeterminateProperty, isIndeterminate);
        #endregion

        #region AttachedProperty : IndicatorForegroundProperty
        public static readonly DependencyProperty IndicatorForegroundProperty
            = DependencyProperty.RegisterAttached("IndicatorForeground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetIndicatorForeground(ButtonBase element) => (Brush)element.GetValue(IndicatorForegroundProperty);
        public static void SetIndicatorForeground(ButtonBase element, Brush indicatorForeground) => element.SetValue(IndicatorForegroundProperty, indicatorForeground);
        #endregion

        #region AttachedProperty : IndicatorBackgroundProperty
        public static readonly DependencyProperty IndicatorBackgroundProperty
            = DependencyProperty.RegisterAttached("IndicatorBackground", typeof(Brush), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(Brush)));

        public static Brush GetIndicatorBackground(ButtonBase element) => (Brush)element.GetValue(IndicatorBackgroundProperty);
        public static void SetIndicatorBackground(ButtonBase element, Brush indicatorBackground) => element.SetValue(IndicatorBackgroundProperty, indicatorBackground);
        #endregion

        #region AttachedProperty : IsIndicatorVisibleProperty
        public static readonly DependencyProperty IsIndicatorVisibleProperty
            = DependencyProperty.RegisterAttached("IsIndicatorVisible", typeof(bool), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(bool)));
        
        public static bool GetIsIndicatorVisible(ButtonBase element) => (bool)element.GetValue(IsIndicatorVisibleProperty);
        public static void SetIsIndicatorVisible(ButtonBase element, bool isIndicatorVisible) => element.SetValue(IsIndicatorVisibleProperty, isIndicatorVisible);
        #endregion

        #region AttachedProperty : OpacityProperty

        public static readonly DependencyProperty OpacityProperty
            = DependencyProperty.RegisterAttached("Opacity", typeof(double), typeof(ButtonProgressAssist), new FrameworkPropertyMetadata(default(double)));

        public static double GetOpacity(ButtonBase element) => (double)element.GetValue(OpacityProperty);
        public static void SetOpacity(ButtonBase element, double opacity) => element.SetValue(OpacityProperty, opacity);
        #endregion
    }
}