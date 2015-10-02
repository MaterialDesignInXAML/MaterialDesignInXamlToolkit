using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public enum ShadowDepth
    {
        Depth1,
        Depth2,
        Depth3,
        Depth4,
        Depth5
    }

    public static class ShadowAssist
    {
        public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached(
            "ShadowDepth", typeof (ShadowDepth), typeof (ShadowAssist), new PropertyMetadata(default(ShadowDepth)));

        public static void SetShadowDepth(DependencyObject element, ShadowDepth value)
        {
            element.SetValue(ShadowDepthProperty, value);
        }

        public static ShadowDepth GetShadowDepth(DependencyObject element)
        {
            return (ShadowDepth) element.GetValue(ShadowDepthProperty);
        }
        
    }
}