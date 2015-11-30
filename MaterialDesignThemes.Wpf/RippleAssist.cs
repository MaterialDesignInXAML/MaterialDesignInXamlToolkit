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

        /// <summary>
        /// Set to <c>true</c> to cause the ripple to originate from the centre of the 
        /// content.  Otherwise the effect will originate from the mouse down position.        
        /// </summary>
        public static readonly DependencyProperty IsCenteredProperty = DependencyProperty.RegisterAttached(
            "IsCentered", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Set to <c>true</c> to cause the ripple to originate from the centre of the 
        /// content.  Otherwise the effect will originate from the mouse down position.        
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetIsCentered(DependencyObject element, bool value)
        {
            element.SetValue(IsCenteredProperty, value);
        }

        /// <summary>
        /// Set to <c>true</c> to cause the ripple to originate from the centre of the 
        /// content.  Otherwise the effect will originate from the mouse down position.        
        /// </summary>
        /// <param name="element"></param>        
        public static bool GetIsCentered(DependencyObject element)
        {
            return (bool)element.GetValue(IsCenteredProperty);
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