using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace MaterialDesignThemes.Wpf
{
    public static class ToggleButtonAssist
    {
        private static readonly DependencyPropertyKey HasOnContentPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "HasOnContent", typeof(bool), typeof(ToggleButtonAssist),
                new PropertyMetadata(false));

        public static readonly DependencyProperty HasOnContentProperty = HasOnContentPropertyKey.DependencyProperty;

        private static void SetHasOnContent(DependencyObject element, object value)
        {
            element.SetValue(HasOnContentPropertyKey, value);
        }

        /// <summary>
        /// Framework use only.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetHasOnContent(DependencyObject element)
        {
            return (bool)element.GetValue(HasOnContentProperty);
        }

        /// <summary>
        /// Allows on (IsChecked) content to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        public static readonly DependencyProperty OnContentProperty = DependencyProperty.RegisterAttached(
            "OnContent", typeof (object), typeof (ToggleButtonAssist), new PropertyMetadata(default(object), OnContentPropertyChangedCallback));

        private static void OnContentPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            SetHasOnContent(dependencyObject, dependencyPropertyChangedEventArgs.NewValue != null);
        }

        /// <summary>
        /// Allows on (IsChecked) content to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetOnContent(DependencyObject element, object value)
        {
            element.SetValue(OnContentProperty, value);
        }

        /// <summary>
        /// Allows on (IsChecked) content to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        public static object GetOnContent(DependencyObject element)
        {
            return (object) element.GetValue(OnContentProperty);
        }

        /// <summary>
        /// Allows an on (IsChecked) template to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        public static readonly DependencyProperty OnContentTemplateProperty = DependencyProperty.RegisterAttached(
            "OnContentTemplate", typeof (DataTemplate), typeof (ToggleButtonAssist), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Allows an on (IsChecked) template to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        public static void SetOnContentTemplate(DependencyObject element, DataTemplate value)
        {
            element.SetValue(OnContentTemplateProperty, value);
        }

        /// <summary>
        /// Allows an on (IsChecked) template to be provided on supporting <see cref="ToggleButton"/> styles.
        /// </summary>
        public static DataTemplate GetOnContentTemplate(DependencyObject element)
        {
            return (DataTemplate) element.GetValue(OnContentTemplateProperty);
        }
    }
}