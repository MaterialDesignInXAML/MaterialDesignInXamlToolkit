using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class RippleAssist
    {
        #region ClipToBound

        public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached(
            "ClipToBounds", typeof (bool), typeof (RippleAssist), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetClipToBounds(DependencyObject element, bool value)
        {
            element.SetValue(ClipToBoundsProperty, value);
        }

        public static bool GetClipToBounds(DependencyObject element)
        {
            return (bool) element.GetValue(ClipToBoundsProperty);
        }

        #endregion

        #region StayOnCenter

        public static readonly DependencyProperty StayOnCenterProperty = DependencyProperty.RegisterAttached(
            "StayOnCenter", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetStayOnCenter(DependencyObject element, bool value)
        {
            element.SetValue(StayOnCenterProperty, value);
        }

        public static bool GetStayOnCenter(DependencyObject element)
        {
            return (bool)element.GetValue(StayOnCenterProperty);
        }

        #endregion

        #region RippleSizeMultiplier

        public static readonly DependencyProperty RippleSizeMultiplierProperty = DependencyProperty.RegisterAttached(
            "RippleSizeMultiplier", typeof(double), typeof(RippleAssist), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetRippleSizeMultiplier(DependencyObject element, double value)
        {
            element.SetValue(RippleSizeMultiplierProperty, value);
        }

        public static double GetRippleSizeMultiplier(DependencyObject element)
        {
            return (double)element.GetValue(RippleSizeMultiplierProperty);
        }

        #endregion

    }
}