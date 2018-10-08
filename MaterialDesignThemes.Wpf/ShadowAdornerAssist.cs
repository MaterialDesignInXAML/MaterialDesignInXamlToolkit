using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace MaterialDesignThemes.Wpf
{
    public static class ShadowAdornerAssist
    {
        public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.RegisterAttached(
    "ShadowDepth", typeof(ShadowDepth), typeof(ShadowAdornerAssist), new FrameworkPropertyMetadata(default(ShadowDepth), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(ShadowDepthChanged)));

        public static void SetShadowDepth(DependencyObject element, ShadowDepth value)
        {
            element.SetValue(ShadowDepthProperty, value);
        }

        public static ShadowDepth GetShadowDepth(DependencyObject element)
        {
            return (ShadowDepth)element.GetValue(ShadowDepthProperty);
        }

        private static void ShadowDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdateAdroner((UIElement)d, (ShadowDepth)e.NewValue);
        }

        public static readonly DependencyProperty InternalAdornerProperty =
            DependencyProperty.RegisterAttached("InternalAdorner", typeof(ShadowAdorner), typeof(ShadowAdornerAssist));

        public static ShadowAdorner GetInteranlAdorner(DependencyObject target)
        {
            return (ShadowAdorner)target.GetValue(InternalAdornerProperty);
        }
        public static void SetInternalAdorner(DependencyObject target, ShadowAdorner value)
        {
            target.SetValue(InternalAdornerProperty, value);
        }

        private static void UpdateAdroner(UIElement adorned, ShadowDepth shadowDepth)
        {
            var layer = AdornerLayer.GetAdornerLayer(adorned);

            if (layer == null)
            {
                // if we don't have an adorner layer it's probably
                // because it's too early in the window's construction
                // Let's re-run at a slightly later time
                Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() => UpdateAdroner(adorned, shadowDepth)));
                return;
            }

            var existingAdorner = GetInteranlAdorner(adorned);

            if (existingAdorner == null)
            {
                var newAdorner = new ShadowAdorner(adorned);
                layer.Add(newAdorner);
                SetInternalAdorner(adorned, newAdorner);
            }
            else
            {
                //existingAdorner.ShadowDepth = shadowDepth;
            }
        }

    }
}
