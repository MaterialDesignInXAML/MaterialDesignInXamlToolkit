using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class NavigationBarAssist
    {
        private static readonly CornerRadius DefaultCornerRadius = new CornerRadius(2.0);

        #region CornerRadius
        /// <summary>
        /// Controls the corner radius of the selection box.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(NavigationBarAssist), new PropertyMetadata(DefaultCornerRadius));

        public static CornerRadius GetCornerRadius(DependencyObject element)
            => (CornerRadius)element.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(CornerRadiusProperty, value);
        #endregion

        #region Property ShowSelectionBackground

        public static readonly DependencyProperty ShowSelectionBackgroundProperty = DependencyProperty.RegisterAttached(
            "ShowSelectionBackground", typeof(bool), typeof(NavigationBarAssist), new PropertyMetadata(false));

        public static object GetShowSelectionBackground(DependencyObject element) => (bool)element.GetValue(ShowSelectionBackgroundProperty);
        public static void SetShowSelectionBackground(DependencyObject element, bool value) => element.SetValue(ShowSelectionBackgroundProperty, value);

        #endregion

        #region Property SelectionCornerRadius

        public static readonly DependencyProperty SelectionCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "SelectionCornerRadius", typeof(CornerRadius), typeof(NavigationBarAssist), new PropertyMetadata(default(CornerRadius)));

        public static object GetSelectionCornerRadius(DependencyObject element) => (CornerRadius)element.GetValue(SelectionCornerRadiusProperty);
        public static void SetSelectionCornerRadius(DependencyObject element, CornerRadius value) => element.SetValue(SelectionCornerRadiusProperty, value);

        #endregion

        #region SelectionHeight
        public static int GetSelectionHeight(DependencyObject element)
            => (int)element.GetValue(SelectionHeightProperty);
        public static void SetSelectionHeight(DependencyObject element, int value)
            => element.SetValue(SelectionHeightProperty, value);

        public static readonly DependencyProperty SelectionHeightProperty =
            DependencyProperty.RegisterAttached("SelectionHeight", typeof(int), typeof(NavigationBarAssist), new PropertyMetadata(default(int)));
        #endregion

        #region SelectionWidth
        public static int GetSelectionWidth(DependencyObject element)
            => (int)element.GetValue(SelectionWidthProperty);
        public static void SetSelectionWidth(DependencyObject element, int value)
            => element.SetValue(SelectionWidthProperty, value);

        public static readonly DependencyProperty SelectionWidthProperty =
            DependencyProperty.RegisterAttached("SelectionWidth", typeof(int), typeof(NavigationBarAssist), new PropertyMetadata(default(int)));
        #endregion

        #region UnselectedIcon
        public static PackIconKind GetUnselectedIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(UnselectedIconProperty);
        public static void SetUnselectedIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(UnselectedIconProperty, value);

        public static readonly DependencyProperty UnselectedIconProperty =
            DependencyProperty.RegisterAttached("UnselectedIcon", typeof(PackIconKind), typeof(NavigationBarAssist), new PropertyMetadata(PackIconKind.None));
        #endregion

        #region SelectedIcon
        public static PackIconKind GetSelectedIcon(DependencyObject element)
            => (PackIconKind)element.GetValue(SelectedIconProperty);
        public static void SetSelectedIcon(DependencyObject element, PackIconKind value)
            => element.SetValue(SelectedIconProperty, value);

        public static readonly DependencyProperty SelectedIconProperty =
            DependencyProperty.RegisterAttached("SelectedIcon", typeof(PackIconKind), typeof(NavigationBarAssist), new PropertyMetadata(PackIconKind.None));
        #endregion

        #region IconSize
        public static int GetIconSize(DependencyObject element)
            => (int)element.GetValue(IconSizeProperty);
        public static void SetIconSize(DependencyObject element, int value)
            => element.SetValue(IconSizeProperty, value);

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.RegisterAttached("IconSize", typeof(int), typeof(NavigationBarAssist), new PropertyMetadata(24));
        #endregion

        #region IsTextVisible
        public static bool GetIsTextVisible(DependencyObject element)
            => (bool)element.GetValue(IsTextVisibleProperty);
        public static void SetIsTextVisible(DependencyObject element, bool value)
            => element.SetValue(IsTextVisibleProperty, value);

        public static readonly DependencyProperty IsTextVisibleProperty =
            DependencyProperty.RegisterAttached("IsTextVisible", typeof(bool), typeof(NavigationBarAssist), new PropertyMetadata(true));
        #endregion
    }
}