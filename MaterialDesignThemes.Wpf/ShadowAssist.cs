using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf
{
    [Obsolete("Use Elevation instead")]
    public enum ShadowDepth
    {
        [Obsolete("Use Elevation.Dp0 instead")]
        Depth0,
        [Obsolete("Use Elevation.Dp2 instead")]
        Depth1,
        [Obsolete("Use Elevation instead, consider Dp4 or Dp3")]
        Depth2,
        [Obsolete("Use Elevation instead, consider Dp8 or Dp7")]
        Depth3,
        [Obsolete("Use Elevation instead, consider Dp12 or Dp16")]
        Depth4,
        [Obsolete("Use Elevation instead, consider Dp16 or Dp24")]
        Depth5
    }

    [Flags]
    public enum ShadowEdges
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,
        All = Left | Top | Right | Bottom
    }

    public static class ShadowAssist
    {
        #region AttachedProperty : ShadowDepthProperty
        [Obsolete("Use ElevationAssist.LevelProperty instead")]
        public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached(
            "ShadowDepth",
            typeof(ShadowDepth),
            typeof(ShadowAssist),
            new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.AffectsRender, OnShadowDepthPropertyChanged));

        [Obsolete]
        private static void OnShadowDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ShadowDepth depth)
            {
                d.SetValue(ElevationAssist.ElevationProperty, GetElevation(depth));
            }
        }

        [Obsolete("Use ElevationAssist.SetLevel instead")]
        public static void SetShadowDepth(DependencyObject element, ShadowDepth value) => element.SetValue(ShadowDepthProperty, value);

        [Obsolete("Use ElevationAssist.GetLevel instead")]
        public static ShadowDepth GetShadowDepth(DependencyObject element) => (ShadowDepth)element.GetValue(ShadowDepthProperty);

        [Obsolete("Only used for backwards compatibility")]
        internal static Elevation GetElevation(ShadowDepth depth) => depth switch
        {
            ShadowDepth.Depth0 => Elevation.Dp0,
            ShadowDepth.Depth1 => Elevation.Dp2,
            ShadowDepth.Depth2 => Elevation.Dp4,
            ShadowDepth.Depth3 => Elevation.Dp8,
            ShadowDepth.Depth4 => Elevation.Dp12,
            ShadowDepth.Depth5 => Elevation.Dp24,
            _ => throw new ArgumentOutOfRangeException(nameof(depth), depth, null)
        };
        #endregion

        #region AttachedProperty : LocalInfoPropertyKey
        private static readonly DependencyPropertyKey LocalInfoPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            "LocalInfo", typeof(ShadowLocalInfo), typeof(ShadowAssist), new PropertyMetadata(default(ShadowLocalInfo)));

        private static void SetLocalInfo(DependencyObject element, ShadowLocalInfo? value)
            => element.SetValue(LocalInfoPropertyKey, value);

        private static ShadowLocalInfo? GetLocalInfo(DependencyObject element)
            => (ShadowLocalInfo?)element.GetValue(LocalInfoPropertyKey.DependencyProperty);

        private class ShadowLocalInfo
        {
            public ShadowLocalInfo(double standardOpacity) => StandardOpacity = standardOpacity;

            public double StandardOpacity { get; }
        }
        #endregion

        #region AttachedProperty : DarkenProperty
        public static readonly DependencyProperty DarkenProperty = DependencyProperty.RegisterAttached(
            "Darken", typeof(bool), typeof(ShadowAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.AffectsRender, DarkenPropertyChangedCallback));

        private static void DarkenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {

            var uiElement = dependencyObject as UIElement;
            var dropShadowEffect = uiElement?.Effect as DropShadowEffect;

            if (dropShadowEffect is null) return;

            if ((bool)dependencyPropertyChangedEventArgs.NewValue)
            {
                dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, null);

                SetLocalInfo(dependencyObject, new ShadowLocalInfo(dropShadowEffect.Opacity));

                TimeSpan time = GetShadowAnimationDuration(dependencyObject);

                var doubleAnimation = new DoubleAnimation()
                {
                    To = 1,
                    Duration = new Duration(time),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase(),
                    AccelerationRatio = 0.4,
                    DecelerationRatio = 0.2
                };
                dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, doubleAnimation);
            }
            else
            {
                var shadowLocalInfo = GetLocalInfo(dependencyObject);
                if (shadowLocalInfo is null) return;

                TimeSpan time = GetShadowAnimationDuration(dependencyObject);

                var doubleAnimation = new DoubleAnimation()
                {
                    To = shadowLocalInfo.StandardOpacity,
                    Duration = new Duration(time),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase(),
                    AccelerationRatio = 0.4,
                    DecelerationRatio = 0.2
                };
                dropShadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, doubleAnimation);
            }
        }

        public static void SetDarken(DependencyObject element, bool value)
        {
            element.SetValue(DarkenProperty, value);
        }

        public static bool GetDarken(DependencyObject element)
        {
            return (bool)element.GetValue(DarkenProperty);
        }
        #endregion

        #region AttachedProperty : CacheModeProperty
        public static readonly DependencyProperty CacheModeProperty = DependencyProperty.RegisterAttached(
            "CacheMode", typeof(CacheMode), typeof(ShadowAssist), new FrameworkPropertyMetadata(new BitmapCache { EnableClearType = true, SnapsToDevicePixels = true }, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCacheMode(DependencyObject element, CacheMode value)
        {
            element.SetValue(CacheModeProperty, value);
        }

        public static CacheMode GetCacheMode(DependencyObject element)
        {
            return (CacheMode)element.GetValue(CacheModeProperty);
        }
        #endregion

        #region AttachedProperty : ShadowEdgesProperty
        public static readonly DependencyProperty ShadowEdgesProperty = DependencyProperty.RegisterAttached(
            "ShadowEdges", typeof(ShadowEdges), typeof(ShadowAssist), new PropertyMetadata(ShadowEdges.All));

        public static void SetShadowEdges(DependencyObject element, ShadowEdges value)
        {
            element.SetValue(ShadowEdgesProperty, value);
        }

        public static ShadowEdges GetShadowEdges(DependencyObject element)
        {
            return (ShadowEdges)element.GetValue(ShadowEdgesProperty);
        }
        #endregion

        #region AttachedProperty : ShadowAnimationDurationProperty
        public static readonly DependencyProperty ShadowAnimationDurationProperty =
           DependencyProperty.RegisterAttached(
               name: "ShadowAnimationDuration",
               propertyType: typeof(TimeSpan),
               ownerType: typeof(ShadowAssist),
               defaultMetadata: new FrameworkPropertyMetadata(
                   defaultValue: new TimeSpan(0, 0, 0, 0, 180),
                   flags: FrameworkPropertyMetadataOptions.Inherits)
               );
        
        public static TimeSpan GetShadowAnimationDuration(DependencyObject element) => (TimeSpan)element.GetValue(ShadowAnimationDurationProperty);
        public static void SetShadowAnimationDuration(DependencyObject element, TimeSpan value) => element.SetValue(ShadowAnimationDurationProperty, value);
        #endregion
    }
}