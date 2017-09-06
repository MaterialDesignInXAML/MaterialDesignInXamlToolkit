using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ValidationAssist
    {
        #region ShowOnFocusProperty

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty OnlyShowOnFocusProperty = DependencyProperty.RegisterAttached(
            "OnlyShowOnFocus",
            typeof(bool),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetOnlyShowOnFocus(DependencyObject element)
        {
            return (bool)element.GetValue(OnlyShowOnFocusProperty);
        }

        public static void SetOnlyShowOnFocus(DependencyObject element, bool value)
        {
            element.SetValue(OnlyShowOnFocusProperty, value);
        }

        #endregion

        #region UsePopupProperty

        /// <summary>
        /// The hint property
        /// </summary>
        public static readonly DependencyProperty UsePopupProperty = DependencyProperty.RegisterAttached(
            "UsePopup",
            typeof(bool),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetUsePopup(DependencyObject element)
        {
            return (bool)element.GetValue(UsePopupProperty);
        }

        public static void SetUsePopup(DependencyObject element, bool value)
        {
            element.SetValue(UsePopupProperty, value);
        }

        #endregion

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static readonly DependencyProperty SuppressProperty = DependencyProperty.RegisterAttached(
            "Suppress", typeof (bool), typeof (ValidationAssist), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Framework use only.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetSuppress(DependencyObject element, bool value)
        {
            element.SetValue(SuppressProperty, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        public static bool GetSuppress(DependencyObject element)
        {
            return (bool) element.GetValue(SuppressProperty);
        }
    }
}
