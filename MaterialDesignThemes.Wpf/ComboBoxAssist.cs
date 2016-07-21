using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ComboBoxAssist
    {
        #region ShowSelectedItem

        public static readonly DependencyProperty ShowSelectedItemProperty = DependencyProperty.RegisterAttached(
            "ShowSelectedItem",
            typeof (bool),
            typeof (ComboBoxAssist),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetShowSelectedItem(DependencyObject element, object value)
        {
            return (bool)element.GetValue(ShowSelectedItemProperty);
        }

        public static void SetShowSelectedItem(DependencyObject element, object value)
        {
            element.SetValue(ShowSelectedItemProperty, value);
        }

        #endregion
    }
}
