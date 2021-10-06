using System;
using System.Windows;

namespace MaterialDesignThemes.Wpf
{
    public static class ComboBoxAssist
    {
        /// <summary>
        /// By default ComboBox uses the wrapper popup. Popup can be switched to classic Windows desktop view by means of this attached property.
        /// </summary>
        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static readonly DependencyProperty ClassicModeProperty = DependencyProperty.RegisterAttached(
            "ClassicMode",
            typeof(bool),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static bool GetClassicMode(DependencyObject element)
            => (bool)element.GetValue(ClassicModeProperty);

        [Obsolete("ClassicMode is now obsolete and has no affect.")]
        public static void SetClassicMode(DependencyObject element, bool value)
            => element.SetValue(ClassicModeProperty, value);

        /// <summary>
        /// By default the selected item is displayed in the drop down list, as per Material Design specifications.
        /// To change this to a behavior of hiding the selected item from the drop down list, set this attached property to false.
        /// </summary>
        public static readonly DependencyProperty ShowSelectedItemProperty = DependencyProperty.RegisterAttached(
            "ShowSelectedItem",
            typeof(bool),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(true,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static bool GetShowSelectedItem(DependencyObject element)
            => (bool)element.GetValue(ShowSelectedItemProperty);

        public static void SetShowSelectedItem(DependencyObject element, bool value)
            => element.SetValue(ShowSelectedItemProperty, value);
    }
}
