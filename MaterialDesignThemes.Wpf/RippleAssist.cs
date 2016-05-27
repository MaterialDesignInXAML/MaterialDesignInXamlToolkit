using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public enum RippleEffect
    {
        Standard,
        Centered,
        None
    }
    public static class RippleAssist
    {
        #region ClipToBound

        public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached(
            "ClipToBounds", typeof(bool), typeof(RippleAssist), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetClipToBounds(DependencyObject element, bool value)
        {
            element.SetValue(ClipToBoundsProperty, value);
        }

        public static bool GetClipToBounds(DependencyObject element)
        {
            return (bool)element.GetValue(ClipToBoundsProperty);
        }

        #endregion

        #region StayOnCenter
        /// <summary>
        /// Set to <c>RippleEffect.Standard</c> to cause the ripple to originate from the centre of the 
        /// content.  
        /// Set to <c>RippleEffect.Centered</c> to cause the ripple to originate from the mouse down position.        
        /// Set to <c>RippleEffect.None</c> to disalbe the ripple effect.        
        /// </summary>
        public static readonly DependencyProperty EffectProperty = DependencyProperty.RegisterAttached(
    "Effect", typeof(RippleEffect), typeof(RippleAssist), new FrameworkPropertyMetadata(RippleEffect.Standard, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetEffect(DependencyObject element, RippleEffect value)
        {
            element.SetValue(EffectProperty, value);
        }
        public static RippleEffect GetEffect(DependencyObject element)
        {
            return (RippleEffect)element.GetValue(EffectProperty);
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

        #region Feedback

        public static readonly DependencyProperty FeedbackProperty = DependencyProperty.RegisterAttached(
            "Feedback", typeof(Brush), typeof(RippleAssist), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetFeedback(DependencyObject element, Brush value)
        {
            element.SetValue(FeedbackProperty, value);
        }

        public static Brush GetFeedback(DependencyObject element)
        {
            return (Brush)element.GetValue(FeedbackProperty);
        }

        #endregion


    }
}