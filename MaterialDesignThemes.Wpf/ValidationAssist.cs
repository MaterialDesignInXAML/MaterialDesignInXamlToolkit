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
        public static readonly DependencyProperty ShowOnFocusProperty = DependencyProperty.RegisterAttached(
            "ShowOnFocus",
            typeof(bool),
            typeof(ValidationAssist),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetShowOnFocus(DependencyObject element)
        {
            return (bool) element.GetValue(ShowOnFocusProperty);
        }

        public static void SetShowOnFocus(DependencyObject element, bool value)
        {
            element.SetValue(ShowOnFocusProperty, value);
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
            return (bool)element.GetValue(ShowOnFocusProperty);
        }

        public static void SetUsePopup(DependencyObject element, bool value)
        {
            element.SetValue(ShowOnFocusProperty, value);
        }

        #endregion
    }
}
