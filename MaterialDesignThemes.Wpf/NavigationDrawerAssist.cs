using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class NavigationDrawerAssist
    {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

        #region CornerRadius
        /// <summary>
        /// Controls the corner radius of the selection box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NavigationDrawerAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetCornerRadius(DependencyObject element)
            => (CornerRadius)element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion

        #region UnselectedIcon
        public static PackIconKind GetUnselectedIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(UnselectedIconProperty);
        public static void SetUnselectedIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(UnselectedIconProperty, value);

        public static readonly DependencyProperty UnselectedIconProperty =
            DependencyProperty.RegisterAttached("UnselectedIcon", typeof(PackIconKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconKind.None));
        #endregion

        #region SelectedIcon
        public static PackIconKind GetSelectedIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(SelectedIconProperty);
        public static void SetSelectedIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(SelectedIconProperty, value);

        public static readonly DependencyProperty SelectedIconProperty =
            DependencyProperty.RegisterAttached("SelectedIcon", typeof(PackIconKind), typeof(NavigationDrawerAssist), new PropertyMetadata(PackIconKind.None));
        #endregion

        #region IconSize
        public static int GetIconSize(DependencyObject element)
            => (int)element.GetValue(IconSizeProperty);
        public static void SetIconSize(DependencyObject element, int value)
            => element.SetValue(IconSizeProperty, value);

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.RegisterAttached("IconSize", typeof(int), typeof(NavigationDrawerAssist), new PropertyMetadata(24));
        #endregion
    }
}
