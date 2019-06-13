using System;
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

        public static bool GetClassicMode(DependencyObject element)
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

        public static bool GetShowSelectedItem(DependencyObject element)
        {
            return (bool)element.GetValue(ShowSelectedItemProperty);
        }

        public static void SetShowSelectedItem(DependencyObject element, object value)
        {
            element.SetValue(ShowSelectedItemProperty, value);
        }

        /// <summary>
        /// The transition duration of a ComboBoxItem from the selected/mouse over state to the normal state
        /// </summary>
        public static readonly DependencyProperty ComboBoxItemToNormalVisualStateTransitionDurationProperty = DependencyProperty.RegisterAttached(
            "ComboBoxItemToNormalVisualStateTransitionDuration",
            typeof(Duration),
            typeof(ComboBoxAssist),
            new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(0)),
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits, VisualTransitionDurationChanged));

        public static Duration GetComboBoxItemToNormalVisualStateTransitionDuration(DependencyObject element)
        {
            return (Duration)element.GetValue(ComboBoxItemToNormalVisualStateTransitionDurationProperty);
        }

        public static void SetComboBoxItemToNormalVisualStateTransitionDuration(DependencyObject element, object value)
        {
            // Set the value of the GeneratedDuration property if a target element is VisualTransition
            VisualTransition visualTransition = element as VisualTransition;
            if (visualTransition != null)
            {
                visualTransition.GeneratedDuration = (Duration)value;
            }

            element.SetValue(ComboBoxItemToNormalVisualStateTransitionDurationProperty, value);
        }

        private static void VisualTransitionDurationChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            // Update the value of the GeneratedDuration property with new value if a target element is VisualTransition
            if (element is VisualTransition)
            {
                (element as VisualTransition).GeneratedDuration = (Duration)e.NewValue;
            }
        }
    }
}
