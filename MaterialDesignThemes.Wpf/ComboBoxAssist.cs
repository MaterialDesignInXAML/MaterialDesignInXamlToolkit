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
        /// <summary>
        /// By default ComboBox uses the wrapper popup. Popup can be switched to classic Windows desktop view by means of this attached property.
        /// </summary>
        public static readonly DependencyProperty ClassicModeProperty = DependencyProperty.RegisterAttached(
            "ClassicMode",
            typeof (bool),
            typeof (ComboBoxAssist),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetClassicMode(DependencyObject element, object value)
        {
            return (bool)element.GetValue(ClassicModeProperty);
        }

        public static void SetClassicMode(DependencyObject element, object value)
        {
            element.SetValue(ClassicModeProperty, value);
        }

        /// <summary>
        /// By default the selected item is hidden from the drop down list, as per Material Design specifications. 
        /// To revert to a more classic Windows desktop behaviour, and show the currently selected item again in the drop
        /// down, set this attached propety to true.
        /// </summary>
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
    }
}
