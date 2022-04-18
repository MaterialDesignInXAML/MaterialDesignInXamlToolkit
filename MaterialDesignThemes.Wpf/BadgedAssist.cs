using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MaterialDesignThemes.Wpf
{
    public static class BadgedAssist
    {
        #region Badge
        public static object? GetBadge(DependencyObject element)
            => (object)element.GetValue(BadgeProperty);
        public static void SetBadge(DependencyObject element, object? value)
            => element.SetValue(BadgeProperty, value);

        public static readonly DependencyProperty BadgeProperty =
            DependencyProperty.RegisterAttached("Badge", typeof(object), typeof(BadgedAssist), new PropertyMetadata(default(object)));
        #endregion

        #region BadgeBackground
        public static Brush? GetBadgeBackground(DependencyObject element)
            => (Brush)element.GetValue(BadgeBackgroundProperty);
        public static void SetBadgeBackground(DependencyObject element, Brush? value)
            => element.SetValue(BadgeBackgroundProperty, value);

        public static readonly DependencyProperty BadgeBackgroundProperty =
            DependencyProperty.RegisterAttached("BadgeBackground", typeof(Brush), typeof(BadgedAssist), new PropertyMetadata(default(Brush)));
        #endregion

        #region BadgeForeground
        public static Brush? GetBadgeForeground(DependencyObject element)
            => (Brush)element.GetValue(BadgeForegroundProperty);
        public static void SetBadgeForeground(DependencyObject element, Brush? value)
            => element.SetValue(BadgeForegroundProperty, value);

        public static readonly DependencyProperty BadgeForegroundProperty =
            DependencyProperty.RegisterAttached("BadgeForeground", typeof(Brush), typeof(BadgedAssist), new PropertyMetadata(default(Brush)));
        #endregion

        #region BadgePlacementMode
        public static BadgePlacementMode GetBadgePlacementMode(DependencyObject element)
            => (BadgePlacementMode)element.GetValue(BadgePlacementModeProperty);
        public static void SetBadgePlacementMode(DependencyObject element, BadgePlacementMode value)
            => element.SetValue(BadgePlacementModeProperty, value);

        public static readonly DependencyProperty BadgePlacementModeProperty =
            DependencyProperty.RegisterAttached("BadgePlacementMode", typeof(BadgePlacementMode), typeof(BadgedAssist), new PropertyMetadata(default(BadgePlacementMode)));
        #endregion

        #region IsMiniBadge
        public static bool GetIsMiniBadge(DependencyObject element)
            => (bool)element.GetValue(IsMiniBadgeProperty);
        public static void SetIsMiniBadge(DependencyObject element, bool value)
            => element.SetValue(IsMiniBadgeProperty, value);

        public static readonly DependencyProperty IsMiniBadgeProperty =
            DependencyProperty.RegisterAttached("IsMiniBadge", typeof(bool), typeof(BadgedAssist), new PropertyMetadata(default(bool)));
        #endregion
    }
}
